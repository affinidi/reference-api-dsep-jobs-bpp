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

namespace BAP.Models
{ 
    /// <summary>
    /// An object representing a scalar quantity.
    /// </summary>
    [DataContract]
    public partial class Scalar : IEquatable<Scalar>
    { 
        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum TypeEnum
        {
            /// <summary>
            /// Enum CONSTANTEnum for CONSTANT
            /// </summary>
            [EnumMember(Value = "CONSTANT")]
            CONSTANTEnum = 0,
            /// <summary>
            /// Enum VARIABLEEnum for VARIABLE
            /// </summary>
            [EnumMember(Value = "VARIABLE")]
            VARIABLEEnum = 1        }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>

        [DataMember(Name="type")]
        public TypeEnum? Type { get; set; }

        /// <summary>
        /// Gets or Sets Value
        /// </summary>
        [Required]

        [DataMember(Name="value")]
        public decimal? Value { get; set; }

        /// <summary>
        /// Gets or Sets EstimatedValue
        /// </summary>

        [DataMember(Name="estimated_value")]
        public decimal? EstimatedValue { get; set; }

        /// <summary>
        /// Gets or Sets ComputedValue
        /// </summary>

        [DataMember(Name="computed_value")]
        public decimal? ComputedValue { get; set; }

        /// <summary>
        /// Gets or Sets Range
        /// </summary>

        [DataMember(Name="range")]
        public ScalarRange Range { get; set; }

        /// <summary>
        /// Gets or Sets Unit
        /// </summary>
        [Required]

        [DataMember(Name="unit")]
        public string Unit { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Scalar {\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            sb.Append("  EstimatedValue: ").Append(EstimatedValue).Append("\n");
            sb.Append("  ComputedValue: ").Append(ComputedValue).Append("\n");
            sb.Append("  Range: ").Append(Range).Append("\n");
            sb.Append("  Unit: ").Append(Unit).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Scalar)obj);
        }

        /// <summary>
        /// Returns true if Scalar instances are equal
        /// </summary>
        /// <param name="other">Instance of Scalar to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Scalar other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Type == other.Type ||
                    Type != null &&
                    Type.Equals(other.Type)
                ) && 
                (
                    Value == other.Value ||
                    Value != null &&
                    Value.Equals(other.Value)
                ) && 
                (
                    EstimatedValue == other.EstimatedValue ||
                    EstimatedValue != null &&
                    EstimatedValue.Equals(other.EstimatedValue)
                ) && 
                (
                    ComputedValue == other.ComputedValue ||
                    ComputedValue != null &&
                    ComputedValue.Equals(other.ComputedValue)
                ) && 
                (
                    Range == other.Range ||
                    Range != null &&
                    Range.Equals(other.Range)
                ) && 
                (
                    Unit == other.Unit ||
                    Unit != null &&
                    Unit.Equals(other.Unit)
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
                    if (Type != null)
                    hashCode = hashCode * 59 + Type.GetHashCode();
                    if (Value != null)
                    hashCode = hashCode * 59 + Value.GetHashCode();
                    if (EstimatedValue != null)
                    hashCode = hashCode * 59 + EstimatedValue.GetHashCode();
                    if (ComputedValue != null)
                    hashCode = hashCode * 59 + ComputedValue.GetHashCode();
                    if (Range != null)
                    hashCode = hashCode * 59 + Range.GetHashCode();
                    if (Unit != null)
                    hashCode = hashCode * 59 + Unit.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Scalar left, Scalar right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Scalar left, Scalar right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}