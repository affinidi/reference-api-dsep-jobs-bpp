using System;
using Newtonsoft.Json;
using BAP.Models;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace BAP.Models
{
    /// <summary>
    /// Describes an image
    /// </summary>
    [DataContract]
    public partial class Image : IEquatable<Image>
    {
        /// <summary>
        /// URL to the image. This can be a data url or an remote url
        /// </summary>
        /// <value>URL to the image. This can be a data url or an remote url</value>

        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// The size of the image. The network policy can define the default dimensions of each type
        /// </summary>
        /// <value>The size of the image. The network policy can define the default dimensions of each type</value>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum SizeTypeEnum
        {
            /// <summary>
            /// Enum XsEnum for xs
            /// </summary>
            [EnumMember(Value = "xs")]
            XsEnum = 0,
            /// <summary>
            /// Enum SmEnum for sm
            /// </summary>
            [EnumMember(Value = "sm")]
            SmEnum = 1,
            /// <summary>
            /// Enum MdEnum for md
            /// </summary>
            [EnumMember(Value = "md")]
            MdEnum = 2,
            /// <summary>
            /// Enum LgEnum for lg
            /// </summary>
            [EnumMember(Value = "lg")]
            LgEnum = 3,
            /// <summary>
            /// Enum XlEnum for xl
            /// </summary>
            [EnumMember(Value = "xl")]
            XlEnum = 4,
            /// <summary>
            /// Enum CustomEnum for custom
            /// </summary>
            [EnumMember(Value = "custom")]
            CustomEnum = 5
        }

        /// <summary>
        /// The size of the image. The network policy can define the default dimensions of each type
        /// </summary>
        /// <value>The size of the image. The network policy can define the default dimensions of each type</value>

        [DataMember(Name = "size_type")]
        public SizeTypeEnum? SizeType { get; set; }

        /// <summary>
        /// Width of the image in pixels
        /// </summary>
        /// <value>Width of the image in pixels</value>

        [DataMember(Name = "width")]
        public string Width { get; set; }

        /// <summary>
        /// Height of the image in pixels
        /// </summary>
        /// <value>Height of the image in pixels</value>

        [DataMember(Name = "height")]
        public string Height { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Image {\n");
            sb.Append("  Url: ").Append(Url).Append("\n");
            sb.Append("  SizeType: ").Append(SizeType).Append("\n");
            sb.Append("  Width: ").Append(Width).Append("\n");
            sb.Append("  Height: ").Append(Height).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Image)obj);
        }

        /// <summary>
        /// Returns true if Image instances are equal
        /// </summary>
        /// <param name="other">Instance of Image to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Image other)
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
                    SizeType == other.SizeType ||
                    SizeType != null &&
                    SizeType.Equals(other.SizeType)
                ) &&
                (
                    Width == other.Width ||
                    Width != null &&
                    Width.Equals(other.Width)
                ) &&
                (
                    Height == other.Height ||
                    Height != null &&
                    Height.Equals(other.Height)
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
                if (SizeType != null)
                    hashCode = hashCode * 59 + SizeType.GetHashCode();
                if (Width != null)
                    hashCode = hashCode * 59 + Width.GetHashCode();
                if (Height != null)
                    hashCode = hashCode * 59 + Height.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(Image left, Image right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Image left, Image right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}

