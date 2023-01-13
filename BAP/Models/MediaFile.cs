using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace BAP.Models
{
    /// <summary>
    /// This object contains a url to a media file.
    /// </summary>
    [DataContract]
    public partial class MediaFile : IEquatable<MediaFile>
    {
        /// <summary>
        /// indicates the nature and format of the document, file, or assortment of bytes. MIME types are defined and standardized in IETF&#x27;s RFC 6838
        /// </summary>
        /// <value>indicates the nature and format of the document, file, or assortment of bytes. MIME types are defined and standardized in IETF&#x27;s RFC 6838</value>

        [DataMember(Name = "mimetype")]
        public string Mimetype { get; set; }

        /// <summary>
        /// The URL of the file
        /// </summary>
        /// <value>The URL of the file</value>

        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// The digital signature of the file signed by the sender
        /// </summary>
        /// <value>The digital signature of the file signed by the sender</value>

        [DataMember(Name = "signature")]
        public string Signature { get; set; }

        /// <summary>
        /// The signing algorithm used by the sender
        /// </summary>
        /// <value>The signing algorithm used by the sender</value>

        [DataMember(Name = "dsa")]
        public string Dsa { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class MediaFile {\n");
            sb.Append("  Mimetype: ").Append(Mimetype).Append("\n");
            sb.Append("  Url: ").Append(Url).Append("\n");
            sb.Append("  Signature: ").Append(Signature).Append("\n");
            sb.Append("  Dsa: ").Append(Dsa).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((MediaFile)obj);
        }

        /// <summary>
        /// Returns true if MediaFile instances are equal
        /// </summary>
        /// <param name="other">Instance of MediaFile to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MediaFile other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Mimetype == other.Mimetype ||
                    Mimetype != null &&
                    Mimetype.Equals(other.Mimetype)
                ) &&
                (
                    Url == other.Url ||
                    Url != null &&
                    Url.Equals(other.Url)
                ) &&
                (
                    Signature == other.Signature ||
                    Signature != null &&
                    Signature.Equals(other.Signature)
                ) &&
                (
                    Dsa == other.Dsa ||
                    Dsa != null &&
                    Dsa.Equals(other.Dsa)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                if (Mimetype != null)
                    hashCode = hashCode * 59 + Mimetype.GetHashCode();
                if (Url != null)
                    hashCode = hashCode * 59 + Url.GetHashCode();
                if (Signature != null)
                    hashCode = hashCode * 59 + Signature.GetHashCode();
                if (Dsa != null)
                    hashCode = hashCode * 59 + Dsa.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(MediaFile left, MediaFile right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MediaFile left, MediaFile right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}

