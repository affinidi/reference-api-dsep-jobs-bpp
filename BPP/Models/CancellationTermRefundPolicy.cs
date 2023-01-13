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
    public partial class CancellationTermRefundPolicy : IEquatable<CancellationTermRefundPolicy>
    {
        /// <summary>
        /// Indicates if cancellation will result in a refund
        /// </summary>
        /// <value>Indicates if cancellation will result in a refund</value>

        [DataMember(Name = "refund_eligible")]
        public bool? RefundEligible { get; set; }

        /// <summary>
        /// Time within which refund will be processed after successful cancellation.
        /// </summary>
        /// <value>Time within which refund will be processed after successful cancellation.</value>

        [DataMember(Name = "refund_within")]
        public Time RefundWithin { get; set; }

        /// <summary>
        /// Gets or Sets RefundAmount
        /// </summary>

        [DataMember(Name = "refund_amount")]
        public Price RefundAmount { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CancellationTermRefundPolicy {\n");
            sb.Append("  RefundEligible: ").Append(RefundEligible).Append("\n");
            sb.Append("  RefundWithin: ").Append(RefundWithin).Append("\n");
            sb.Append("  RefundAmount: ").Append(RefundAmount).Append("\n");
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
            return obj.GetType() == GetType() && Equals((CancellationTermRefundPolicy)obj);
        }

        /// <summary>
        /// Returns true if CancellationTermRefundPolicy instances are equal
        /// </summary>
        /// <param name="other">Instance of CancellationTermRefundPolicy to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CancellationTermRefundPolicy other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    RefundEligible == other.RefundEligible ||
                    RefundEligible != null &&
                    RefundEligible.Equals(other.RefundEligible)
                ) &&
                (
                    RefundWithin == other.RefundWithin ||
                    RefundWithin != null &&
                    RefundWithin.Equals(other.RefundWithin)
                ) &&
                (
                    RefundAmount == other.RefundAmount ||
                    RefundAmount != null &&
                    RefundAmount.Equals(other.RefundAmount)
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
                if (RefundEligible != null)
                    hashCode = hashCode * 59 + RefundEligible.GetHashCode();
                if (RefundWithin != null)
                    hashCode = hashCode * 59 + RefundWithin.GetHashCode();
                if (RefundAmount != null)
                    hashCode = hashCode * 59 + RefundAmount.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(CancellationTermRefundPolicy left, CancellationTermRefundPolicy right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CancellationTermRefundPolicy left, CancellationTermRefundPolicy right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}

