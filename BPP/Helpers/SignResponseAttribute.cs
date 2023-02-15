using System;
using System.Security.Cryptography;
using System.Text;
using bpp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

public class SignResponseAttribute : ActionFilterAttribute
{

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult result)
        {
            var json = JsonConvert.SerializeObject(result.Value);
            //  byte[] signature = _rsa.SignData(Encoding.UTF8.GetBytes(json), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            var signature64 = AuthUtil.createAuthorizationHeader(json);  //Convert.ToBase64String(signature);
            Console.WriteLine(" adding Authorization headers");
            context.HttpContext.Response.Headers.Add("authorization", signature64);
            context.HttpContext.Response.Headers.Add("Content-Type", "application/json");
        }
    }
}
