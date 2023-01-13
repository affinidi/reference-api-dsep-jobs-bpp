using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace BAP.Models
{
    /// <summary>
    /// Describes the cancellation terms of an item or an order. This can be referenced at an item or order level. Item-level cancellation terms can override the terms at the order level.
    /// </summary>
    [DataContract]
    public partial class CancellationTerm : IEquatable<CancellationTerm>
    {
        /// <summary>
        /// Indicates whether a reason is required to cancel the order
        /// </summary>
        /// <value>Indicates whether a reason is required to cancel the order</value>

        [DataMember(Name = "reason_required")]
        public bool? ReasonRequired { get; set; }

        /// <summary>
        /// Indicates if cancellation will result in a refund
        /// </summary>
        /// <value>Indicates if cancellation will result in a refund</value>

        [DataMember(Name = "refund_eligible")]
        public bool? RefundEligible { get; set; }

        /// <summary>
        /// Indicates if cancellation will result in a return to origin
        /// </summary>
        /// <value>Indicates if cancellation will result in a return to origin</value>

        [DataMember(Name = "return_eligible")]
        public bool? ReturnEligible { get; set; }

        /// <summary>
        /// The state of fulfillment during which these terms are applicable.
        /// </summary>
        /// <value>The state of fulfillment during which these terms are applicable.</value>

        [DataMember(Name = "fulfillment_state")]
        public State FulfillmentState { get; set; }

        /// <summary>
        /// Gets or Sets ReturnPolicy
        /// </summary>

        [DataMember(Name = "return_policy")]
        public CancellationTermReturnPolicy ReturnPolicy { get; set; }

        /// <summary>
        /// Gets or Sets RefundPolicy
        /// </summary>

        [DataMember(Name = "refund_policy")]
        public CancellationTermRefundPolicy RefundPolicy { get; set; }

        /// <summary>
        /// Information related to the time of cancellation.
        /// </summary>
        /// <value>Information related to the time of cancellation.</value>

        [DataMember(Name = "cancel_by")]
        public Time CancelBy { get; set; }

        /// <summary>
        /// Gets or Sets CancellationFee
        /// </summary>

        [DataMember(Name = "cancellation_fee")]
        public Fee CancellationFee { get; set; }

        /// <summary>
        /// Gets or Sets XinputRequired
        /// </summary>

        [DataMember(Name = "xinput_required")]
        public XInput XinputRequired { get; set; }

        /// <summary>
        /// Gets or Sets XinputResponse
        /// </summary>

        [DataMember(Name = "xinput_response")]
        public XInputResponse XinputResponse { get; set; }

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
            sb.Append("class CancellationTerm {\n");
            sb.Append("  ReasonRequired: ").Append(ReasonRequired).Append("\n");
            sb.Append("  RefundEligible: ").Append(RefundEligible).Append("\n");
            sb.Append("  ReturnEligible: ").Append(ReturnEligible).Append("\n");
            sb.Append("  FulfillmentState: ").Append(FulfillmentState).Append("\n");
            sb.Append("  ReturnPolicy: ").Append(ReturnPolicy).Append("\n");
            sb.Append("  RefundPolicy: ").Append(RefundPolicy).Append("\n");
            sb.Append("  CancelBy: ").Append(CancelBy).Append("\n");
            sb.Append("  CancellationFee: ").Append(CancellationFee).Append("\n");
            sb.Append("  XinputRequired: ").Append(XinputRequired).Append("\n");
            sb.Append("  XinputResponse: ").Append(XinputResponse).Append("\n");
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
            return obj.GetType() == GetType() && Equals((CancellationTerm)obj);
        }

        /// <summary>
        /// Returns true if CancellationTerm instances are equal
        /// </summary>
        /// <param name="other">Instance of CancellationTerm to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CancellationTerm other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    ReasonRequired == other.ReasonRequired ||
                    ReasonRequired != null &&
                    ReasonRequired.Equals(other.ReasonRequired)
                ) &&
                (
                    RefundEligible == other.RefundEligible ||
                    RefundEligible != null &&
                    RefundEligible.Equals(other.RefundEligible)
                ) &&
                (
                    ReturnEligible == other.ReturnEligible ||
                    ReturnEligible != null &&
                    ReturnEligible.Equals(other.ReturnEligible)
                ) &&
                (
                    FulfillmentState == other.FulfillmentState ||
                    FulfillmentState != null &&
                    FulfillmentState.Equals(other.FulfillmentState)
                ) &&
                (
                    ReturnPolicy == other.ReturnPolicy ||
                    ReturnPolicy != null &&
                    ReturnPolicy.Equals(other.ReturnPolicy)
                ) &&
                (
                    RefundPolicy == other.RefundPolicy ||
                    RefundPolicy != null &&
                    RefundPolicy.Equals(other.RefundPolicy)
                ) &&
                (
                    CancelBy == other.CancelBy ||
                    CancelBy != null &&
                    CancelBy.Equals(other.CancelBy)
                ) &&
                (
                    CancellationFee == other.CancellationFee ||
                    CancellationFee != null &&
                    CancellationFee.Equals(other.CancellationFee)
                ) &&
                (
                    XinputRequired == other.XinputRequired ||
                    XinputRequired != null &&
                    XinputRequired.Equals(other.XinputRequired)
                ) &&
                (
                    XinputResponse == other.XinputResponse ||
                    XinputResponse != null &&
                    XinputResponse.Equals(other.XinputResponse)
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
                if (ReasonRequired != null)
                    hashCode = hashCode * 59 + ReasonRequired.GetHashCode();
                if (RefundEligible != null)
                    hashCode = hashCode * 59 + RefundEligible.GetHashCode();
                if (ReturnEligible != null)
                    hashCode = hashCode * 59 + ReturnEligible.GetHashCode();
                if (FulfillmentState != null)
                    hashCode = hashCode * 59 + FulfillmentState.GetHashCode();
                if (ReturnPolicy != null)
                    hashCode = hashCode * 59 + ReturnPolicy.GetHashCode();
                if (RefundPolicy != null)
                    hashCode = hashCode * 59 + RefundPolicy.GetHashCode();
                if (CancelBy != null)
                    hashCode = hashCode * 59 + CancelBy.GetHashCode();
                if (CancellationFee != null)
                    hashCode = hashCode * 59 + CancellationFee.GetHashCode();
                if (XinputRequired != null)
                    hashCode = hashCode * 59 + XinputRequired.GetHashCode();
                if (XinputResponse != null)
                    hashCode = hashCode * 59 + XinputResponse.GetHashCode();
                if (ExternalRef != null)
                    hashCode = hashCode * 59 + ExternalRef.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(CancellationTerm left, CancellationTerm right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CancellationTerm left, CancellationTerm right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}

