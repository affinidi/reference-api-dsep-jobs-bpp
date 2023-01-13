using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace bpp.Models
{
    /// <summary>
    /// Describes a form
    /// </summary>
    [DataContract]
    public partial class Form : IEquatable<Form>
    {
        /// <summary>
        /// The URL from where the form can be fetched. The content fetched from the url must be processed as per the mime_type specified in this object. Once fetched, the rendering platform can choosed to render the form as-is as an embeddable element; or process it further to blend with the theme of the application. In case the interface is non-visual, the the render can process the form data and reproduce it as per the standard specified in the form.
        /// </summary>
        /// <value>The URL from where the form can be fetched. The content fetched from the url must be processed as per the mime_type specified in this object. Once fetched, the rendering platform can choosed to render the form as-is as an embeddable element; or process it further to blend with the theme of the application. In case the interface is non-visual, the the render can process the form data and reproduce it as per the standard specified in the form.</value>

        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// The form content string. This content will again follow the mime_type field for processing. Typically forms should be sent as an html string starting with &lt;form&gt;&lt;/form&gt; tags. The application must render this form after removing any css or javascript code if necessary. The &#x60;action&#x60; attribute in the form should have a url where the form needs to be submitted.
        /// </summary>
        /// <value>The form content string. This content will again follow the mime_type field for processing. Typically forms should be sent as an html string starting with &lt;form&gt;&lt;/form&gt; tags. The application must render this form after removing any css or javascript code if necessary. The &#x60;action&#x60; attribute in the form should have a url where the form needs to be submitted.</value>

        [DataMember(Name = "data")]
        public string Data { get; set; }

        /// <summary>
        /// This field indicates the nature and format of the form received by querying the url. MIME types are defined and standardized in IETF&#x27;s RFC 6838.
        /// </summary>
        /// <value>This field indicates the nature and format of the form received by querying the url. MIME types are defined and standardized in IETF&#x27;s RFC 6838.</value>

        [DataMember(Name = "mime_type")]
        public string MimeType { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Form {\n");
            sb.Append("  Url: ").Append(Url).Append("\n");
            sb.Append("  Data: ").Append(Data).Append("\n");
            sb.Append("  MimeType: ").Append(MimeType).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Form)obj);
        }

        /// <summary>
        /// Returns true if Form instances are equal
        /// </summary>
        /// <param name="other">Instance of Form to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Form other)
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
                    Data == other.Data ||
                    Data != null &&
                    Data.Equals(other.Data)
                ) &&
                (
                    MimeType == other.MimeType ||
                    MimeType != null &&
                    MimeType.Equals(other.MimeType)
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
                if (Data != null)
                    hashCode = hashCode * 59 + Data.GetHashCode();
                if (MimeType != null)
                    hashCode = hashCode * 59 + MimeType.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(Form left, Form right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Form left, Form right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}

