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
    public partial class ReplacementTerm : IEquatable<ReplacementTerm>
    {
        /// <summary>
        /// Applicable only for buyer managed returns where the buyer has to replace the item before a certain date-time, failing which they will not be eligible for replacement
        /// </summary>
        /// <value>Applicable only for buyer managed returns where the buyer has to replace the item before a certain date-time, failing which they will not be eligible for replacement</value>

        [DataMember(Name = "replace_within")]
        public Time ReplaceWithin { get; set; }

        /// <summary>
        /// Gets or Sets ExternalRef
        /// </summary>

        [DataMember(Name = "external_ref")]
        public MediaFile ExternalRef { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ReplacementTerm {\n");
            sb.Append("  ReplaceWithin: ").Append(ReplaceWithin).Append("\n");
            sb.Append("  ExternalRef: ").Append(ExternalRef).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ReplacementTerm)obj);
        }

        /// <summary>
        /// Returns true if ReplacementTerm instances are equal
        /// </summary>
        /// <param name="other">Instance of ReplacementTerm to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ReplacementTerm other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    ReplaceWithin == other.ReplaceWithin ||
                    ReplaceWithin != null &&
                    ReplaceWithin.Equals(other.ReplaceWithin)
                ) &&
                (
                    ExternalRef == other.ExternalRef ||
                    ExternalRef != null &&
                    ExternalRef.Equals(other.ExternalRef)
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
                if (ReplaceWithin != null)
                    hashCode = hashCode * 59 + ReplaceWithin.GetHashCode();
                if (ExternalRef != null)
                    hashCode = hashCode * 59 + ExternalRef.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(ReplacementTerm left, ReplacementTerm right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ReplacementTerm left, ReplacementTerm right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}

