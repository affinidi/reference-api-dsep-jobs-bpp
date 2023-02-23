using System;
using Microsoft.AspNetCore.SignalR.Protocol;
using static Sodium.Utilities;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.PortableExecutable;
using Newtonsoft.Json;
using Sodium;
using System.Text.Unicode;
using System.Text;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Crypto.Signers;

namespace bpp.Helpers
{
    public class SingingString
    {
        public string signing_string { get; set; }
        public string expires { get; set; }
        public string created { get; set; }
    }
    public static class AuthUtil
    {

        //to create header 
        public static string createAuthorizationHeader(string message)
        {
            SingingString sg = createSigningString((message)); //creates  hashed request details 
            Console.WriteLine("Signing string: " + sg.signing_string);
            var signature = signMessage(sg.signing_string, Environment.GetEnvironmentVariable(EnvironmentVariables.BPP_PRIVATEKEY.ToString())); // signs the message 
            var subscriber_id = Environment.GetEnvironmentVariable(EnvironmentVariables.BPP_SUBSCRIBER_ID);
            var unique_key_id = Environment.GetEnvironmentVariable(EnvironmentVariables.BPP_UNIQUE_KEY_ID);
            //creates header data to be sent as Authorization key 
            string header = "keyId=" + subscriber_id + "|" + unique_key_id + "|" + "ed25519, algorithm = ed25519, created = " + sg.created + ", expires = " + sg.expires + ", headers = (created) (expires) digest, signature = " + signature;
            //string header =   $"Signature keyId = " ${subscriber_id}"|"${config.unique_key_id}|ed25519", algorithm = "ed25519", created = "${created}", expires = "${expires}", headers = "(created) (expires) digest", signature = "${signature}"";
            return header;
        }


        //creates  hashed request details 
        public static SingingString createSigningString(string message, string created = "", string expires = "")
        {
            if (string.IsNullOrEmpty(created))
            {
                created = Convert.ToString(DateTimeOffset.Now.ToUnixTimeSeconds() / 1000);
            }

            if (string.IsNullOrEmpty(expires))
            {
                expires = Convert.ToString(int.Parse(created) + (1 * 60 * 60)); //Add required time to create expired
            }

            var utf8 = new UTF8Encoding();

            var obj = GenericHash.Hash(utf8.GetBytes(message), null, 64);
            var ba64 = Convert.ToBase64String(obj);

            var signing_string = $"(created): ${created} (expires): ${expires} digest: BLAKE - 512 =${ba64}";
            return new SingingString { signing_string = signing_string, expires = expires, created = created };
        }


        // sign the message 

        public static string signMessage(string signing_string, string privateKey)
        {

            var pk = Convert.FromBase64String(privateKey);// sodium.from_base64(privateKey, base64_variants.ORIGINAL); // ed25519 private key
            var signedMessage = Sodium.PublicKeyAuth.Sign(signing_string, pk); //sodium.crypto_sign(signing_string, pk);
            return Convert.ToBase64String(signedMessage);
        }


        public static bool Verify(string sourceSignature, string requestBody, string sourcePublicKey)
        {
            bool valid = false;
            var utf8 = new UTF8Encoding();

            Sodium.PublicKeyAuth.VerifyDetached(
                utf8.GetBytes(sourceSignature)
                , utf8.GetBytes(requestBody)
                , Convert.FromBase64String(sourcePublicKey));

            return valid;
        }

        public static Task<bool> VerifySignature(string publicKey, string dataToVerify, string signature)
        {
            var publicKeyParam = new Ed25519PublicKeyParameters(Convert.FromBase64String(publicKey));
            var dataToVerifyBytes = Encoding.UTF8.GetBytes(dataToVerify);
            var signatureBytes = Encoding.UTF8.GetBytes(signature);

            var verifier = new Ed25519Signer();
            verifier.Init(false, publicKeyParam);
            verifier.BlockUpdate(dataToVerifyBytes, 0, dataToVerifyBytes.Length);
            var isVerified = verifier.VerifySignature(signatureBytes);
            return Task.FromResult(isVerified);
        }

    }
}

