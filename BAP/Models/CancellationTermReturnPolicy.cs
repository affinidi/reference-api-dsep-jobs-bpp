using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace BAP.Models
{
    /// <summary>
    /// Describes the return policy of an item or an order
    /// </summary>
    [DataContract]
    public partial class CancellationTermReturnPolicy : IEquatable<CancellationTermReturnPolicy>
    {
        /// <summary>
        /// Indicates whether the item is eligible for return
        /// </summary>
        /// <value>Indicates whether the item is eligible for return</value>

        [DataMember(Name = "return_eligible")]
        public bool? ReturnEligible { get; set; }

        /// <summary>
        /// Applicable only for buyer managed returns where the buyer has to return the item to the origin before a certain date-time, failing which they will not be eligible for refund.
        /// </summary>
        /// <value>Applicable only for buyer managed returns where the buyer has to return the item to the origin before a certain date-time, failing which they will not be eligible for refund.</value>

        [DataMember(Name = "return_within")]
        public Time ReturnWithin { get; set; }

        /// <summary>
        /// Gets or Sets ReturnLocation
        /// </summary>

        [DataMember(Name = "return_location")]
        public Location ReturnLocation { get; set; }

        /// <summary>
        /// Gets or Sets FulfillmentManagedBy
        /// </summary>

        [DataMember(Name = "fulfillment_managed_by")]
        public string FulfillmentManagedBy { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CancellationTermReturnPolicy {\n");
            sb.Append("  ReturnEligible: ").Append(ReturnEligible).Append("\n");
            sb.Append("  ReturnWithin: ").Append(ReturnWithin).Append("\n");
            sb.Append("  ReturnLocation: ").Append(ReturnLocation).Append("\n");
            sb.Append("  FulfillmentManagedBy: ").Append(FulfillmentManagedBy).Append("\n");
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
            return obj.GetType() == GetType() && Equals((CancellationTermReturnPolicy)obj);
        }

        /// <summary>
        /// Returns true if CancellationTermReturnPolicy instances are equal
        /// </summary>
        /// <param name="other">Instance of CancellationTermReturnPolicy to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CancellationTermReturnPolicy other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    ReturnEligible == other.ReturnEligible ||
                    ReturnEligible != null &&
                    ReturnEligible.Equals(other.ReturnEligible)
                ) &&
                (
                    ReturnWithin == other.ReturnWithin ||
                    ReturnWithin != null &&
                    ReturnWithin.Equals(other.ReturnWithin)
                ) &&
                (
                    ReturnLocation == other.ReturnLocation ||
                    ReturnLocation != null &&
                    ReturnLocation.Equals(other.ReturnLocation)
                ) &&
                (
                    FulfillmentManagedBy == other.FulfillmentManagedBy ||
                    FulfillmentManagedBy != null &&
                    FulfillmentManagedBy.Equals(other.FulfillmentManagedBy)
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
                if (ReturnEligible != null)
                    hashCode = hashCode * 59 + ReturnEligible.GetHashCode();
                if (ReturnWithin != null)
                    hashCode = hashCode * 59 + ReturnWithin.GetHashCode();
                if (ReturnLocation != null)
                    hashCode = hashCode * 59 + ReturnLocation.GetHashCode();
                if (FulfillmentManagedBy != null)
                    hashCode = hashCode * 59 + FulfillmentManagedBy.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(CancellationTermReturnPolicy left, CancellationTermReturnPolicy right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CancellationTermReturnPolicy left, CancellationTermReturnPolicy right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}

