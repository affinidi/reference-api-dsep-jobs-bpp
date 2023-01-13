using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace BAP.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class XInputResponseInner : IEquatable<XInputResponseInner>
    {
        /// <summary>
        /// The _name_ attribute of the input tag in the XInput form
        /// </summary>
        /// <value>The _name_ attribute of the input tag in the XInput form</value>

        [DataMember(Name = "input")]
        public string Input { get; set; }

        /// <summary>
        /// The value of the input field. Files must be sent as data URLs. For more information on Data URLs visit https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/Data_URLs
        /// </summary>
        /// <value>The value of the input field. Files must be sent as data URLs. For more information on Data URLs visit https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/Data_URLs</value>

        [DataMember(Name = "value")]
        public string Value { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class XInputResponseInner {\n");
            sb.Append("  Input: ").Append(Input).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
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
            return obj.GetType() == GetType() && Equals((XInputResponseInner)obj);
        }

        /// <summary>
        /// Returns true if XInputResponseInner instances are equal
        /// </summary>
        /// <param name="other">Instance of XInputResponseInner to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(XInputResponseInner other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Input == other.Input ||
                    Input != null &&
                    Input.Equals(other.Input)
                ) &&
                (
                    Value == other.Value ||
                    Value != null &&
                    Value.Equals(other.Value)
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
                if (Input != null)
                    hashCode = hashCode * 59 + Input.GetHashCode();
                if (Value != null)
                    hashCode = hashCode * 59 + Value.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(XInputResponseInner left, XInputResponseInner right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(XInputResponseInner left, XInputResponseInner right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}

