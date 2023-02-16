using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace bpp.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class DescriptorAdditionalDesc : IEquatable<DescriptorAdditionalDesc>
    {
        /// <summary>
        /// Gets or Sets Url
        /// </summary>

        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or Sets ContentType
        /// </summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum ContentTypeEnum
        {
            /// <summary>
            /// Enum TextplainEnum for text/plain
            /// </summary>
            [EnumMember(Value = "text/plain")]
            TextplainEnum = 0,
            /// <summary>
            /// Enum TexthtmlEnum for text/html
            /// </summary>
            [EnumMember(Value = "text/html")]
            TexthtmlEnum = 1,
            /// <summary>
            /// Enum ApplicationjsonEnum for application/json
            /// </summary>
            [EnumMember(Value = BPPConstants.RESPONSE_MEDIA_TYPE)]
            ApplicationjsonEnum = 2
        }

        /// <summary>
        /// Gets or Sets ContentType
        /// </summary>

        [DataMember(Name = "content_type")]
        public ContentTypeEnum? ContentType { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DescriptorAdditionalDesc {\n");
            sb.Append("  Url: ").Append(Url).Append("\n");
            sb.Append("  ContentType: ").Append(ContentType).Append("\n");
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
            return obj.GetType() == GetType() && Equals((DescriptorAdditionalDesc)obj);
        }

        /// <summary>
        /// Returns true if DescriptorAdditionalDesc instances are equal
        /// </summary>
        /// <param name="other">Instance of DescriptorAdditionalDesc to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DescriptorAdditionalDesc other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Url == other.Url ||
                    Url != null &&
                    Url.Equals(other.Url)
                ) &&
                (
                    ContentType == other.ContentType ||
                    ContentType != null &&
                    ContentType.Equals(other.ContentType)
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
                if (Url != null)
                    hashCode = hashCode * 59 + Url.GetHashCode();
                if (ContentType != null)
                    hashCode = hashCode * 59 + ContentType.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(DescriptorAdditionalDesc left, DescriptorAdditionalDesc right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DescriptorAdditionalDesc left, DescriptorAdditionalDesc right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}

