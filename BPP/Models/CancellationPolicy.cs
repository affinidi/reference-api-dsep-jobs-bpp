/*
 *  
 *
 *  
 *
 * OpenAPI spec version: 1.0.0-dsep-draft
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace bpp.Models
{ 
    /// <summary>
    /// Describes the cancellation policy of an Order.
    /// </summary>
    [DataContract]
    public partial class CancellationPolicy : IEquatable<CancellationPolicy>
    { 
        /// <summary>
        /// Gets or Sets Cancellable
        /// </summary>

        [DataMember(Name="cancellable")]
        public bool? Cancellable { get; set; }

        /// <summary>
        /// Gets or Sets CancelBefore
        /// </summary>

        [DataMember(Name="cancel_before")]
        public DateTime? CancelBefore { get; set; }

        /// <summary>
        /// Gets or Sets CancellationFee
        /// </summary>

        [DataMember(Name="cancellation_fee")]
        public Fee CancellationFee { get; set; }

        /// <summary>
        /// Gets or Sets FeeAppliedOn
        /// </summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum FeeAppliedOnEnum
        {
            /// <summary>
            /// Enum CURRENTORDEREnum for CURRENT-ORDER
            /// </summary>
            [EnumMember(Value = "CURRENT-ORDER")]
            CURRENTORDEREnum = 0,
            /// <summary>
            /// Enum NEXTORDEREnum for NEXT-ORDER
            /// </summary>
            [EnumMember(Value = "NEXT-ORDER")]
            NEXTORDEREnum = 1        }

        /// <summary>
        /// Gets or Sets FeeAppliedOn
        /// </summary>

        [DataMember(Name="fee_applied_on")]
        public FeeAppliedOnEnum? FeeAppliedOn { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum TypeEnum
        {
            /// <summary>
            /// Enum FullEnum for full
            /// </summary>
            [EnumMember(Value = "full")]
            FullEnum = 0,
            /// <summary>
            /// Enum PartialEnum for partial
            /// </summary>
            [EnumMember(Value = "partial")]
            PartialEnum = 1        }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>

        [DataMember(Name="type")]
        public TypeEnum? Type { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CancellationPolicy {\n");
            sb.Append("  Cancellable: ").Append(Cancellable).Append("\n");
            sb.Append("  CancelBefore: ").Append(CancelBefore).Append("\n");
            sb.Append("  CancellationFee: ").Append(CancellationFee).Append("\n");
            sb.Append("  FeeAppliedOn: ").Append(FeeAppliedOn).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
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
            return obj.GetType() == GetType() && Equals((CancellationPolicy)obj);
        }

        /// <summary>
        /// Returns true if CancellationPolicy instances are equal
        /// </summary>
        /// <param name="other">Instance of CancellationPolicy to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CancellationPolicy other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Cancellable == other.Cancellable ||
                    Cancellable != null &&
                    Cancellable.Equals(other.Cancellable)
                ) && 
                (
                    CancelBefore == other.CancelBefore ||
                    CancelBefore != null &&
                    CancelBefore.Equals(other.CancelBefore)
                ) && 
                (
                    CancellationFee == other.CancellationFee ||
                    CancellationFee != null &&
                    CancellationFee.Equals(other.CancellationFee)
                ) && 
                (
                    FeeAppliedOn == other.FeeAppliedOn ||
                    FeeAppliedOn != null &&
                    FeeAppliedOn.Equals(other.FeeAppliedOn)
                ) && 
                (
                    Type == other.Type ||
                    Type != null &&
                    Type.Equals(other.Type)
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
                    if (Cancellable != null)
                    hashCode = hashCode * 59 + Cancellable.GetHashCode();
                    if (CancelBefore != null)
                    hashCode = hashCode * 59 + CancelBefore.GetHashCode();
                    if (CancellationFee != null)
                    hashCode = hashCode * 59 + CancellationFee.GetHashCode();
                    if (FeeAppliedOn != null)
                    hashCode = hashCode * 59 + FeeAppliedOn.GetHashCode();
                    if (Type != null)
                    hashCode = hashCode * 59 + Type.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(CancellationPolicy left, CancellationPolicy right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CancellationPolicy left, CancellationPolicy right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
