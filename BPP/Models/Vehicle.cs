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
    /// Describes the properties of a vehicle used in a mobility service
    /// </summary>
    [DataContract]
    public partial class Vehicle : IEquatable<Vehicle>
    { 
        /// <summary>
        /// Gets or Sets Category
        /// </summary>

        [DataMember(Name="category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or Sets Capacity
        /// </summary>

        [DataMember(Name="capacity")]
        public int? Capacity { get; set; }

        /// <summary>
        /// Gets or Sets Make
        /// </summary>

        [DataMember(Name="make")]
        public string Make { get; set; }

        /// <summary>
        /// Gets or Sets Model
        /// </summary>

        [DataMember(Name="model")]
        public string Model { get; set; }

        /// <summary>
        /// Gets or Sets Size
        /// </summary>

        [DataMember(Name="size")]
        public string Size { get; set; }

        /// <summary>
        /// Gets or Sets Variant
        /// </summary>

        [DataMember(Name="variant")]
        public string Variant { get; set; }

        /// <summary>
        /// Gets or Sets Color
        /// </summary>

        [DataMember(Name="color")]
        public string Color { get; set; }

        /// <summary>
        /// Gets or Sets EnergyType
        /// </summary>

        [DataMember(Name="energy_type")]
        public string EnergyType { get; set; }

        /// <summary>
        /// Gets or Sets Registration
        /// </summary>

        [DataMember(Name="registration")]
        public string Registration { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Vehicle {\n");
            sb.Append("  Category: ").Append(Category).Append("\n");
            sb.Append("  Capacity: ").Append(Capacity).Append("\n");
            sb.Append("  Make: ").Append(Make).Append("\n");
            sb.Append("  Model: ").Append(Model).Append("\n");
            sb.Append("  Size: ").Append(Size).Append("\n");
            sb.Append("  Variant: ").Append(Variant).Append("\n");
            sb.Append("  Color: ").Append(Color).Append("\n");
            sb.Append("  EnergyType: ").Append(EnergyType).Append("\n");
            sb.Append("  Registration: ").Append(Registration).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Vehicle)obj);
        }

        /// <summary>
        /// Returns true if Vehicle instances are equal
        /// </summary>
        /// <param name="other">Instance of Vehicle to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Vehicle other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Category == other.Category ||
                    Category != null &&
                    Category.Equals(other.Category)
                ) && 
                (
                    Capacity == other.Capacity ||
                    Capacity != null &&
                    Capacity.Equals(other.Capacity)
                ) && 
                (
                    Make == other.Make ||
                    Make != null &&
                    Make.Equals(other.Make)
                ) && 
                (
                    Model == other.Model ||
                    Model != null &&
                    Model.Equals(other.Model)
                ) && 
                (
                    Size == other.Size ||
                    Size != null &&
                    Size.Equals(other.Size)
                ) && 
                (
                    Variant == other.Variant ||
                    Variant != null &&
                    Variant.Equals(other.Variant)
                ) && 
                (
                    Color == other.Color ||
                    Color != null &&
                    Color.Equals(other.Color)
                ) && 
                (
                    EnergyType == other.EnergyType ||
                    EnergyType != null &&
                    EnergyType.Equals(other.EnergyType)
                ) && 
                (
                    Registration == other.Registration ||
                    Registration != null &&
                    Registration.Equals(other.Registration)
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
                    if (Category != null)
                    hashCode = hashCode * 59 + Category.GetHashCode();
                    if (Capacity != null)
                    hashCode = hashCode * 59 + Capacity.GetHashCode();
                    if (Make != null)
                    hashCode = hashCode * 59 + Make.GetHashCode();
                    if (Model != null)
                    hashCode = hashCode * 59 + Model.GetHashCode();
                    if (Size != null)
                    hashCode = hashCode * 59 + Size.GetHashCode();
                    if (Variant != null)
                    hashCode = hashCode * 59 + Variant.GetHashCode();
                    if (Color != null)
                    hashCode = hashCode * 59 + Color.GetHashCode();
                    if (EnergyType != null)
                    hashCode = hashCode * 59 + EnergyType.GetHashCode();
                    if (Registration != null)
                    hashCode = hashCode * 59 + Registration.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Vehicle left, Vehicle right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Vehicle left, Vehicle right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}