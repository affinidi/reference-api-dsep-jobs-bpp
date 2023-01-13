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
    /// Describes a BPP catalog
    /// </summary>
    [DataContract]
    public partial class Catalog : IEquatable<Catalog>
    { 
        /// <summary>
        /// Gets or Sets Descriptor
        /// </summary>

        [DataMember(Name="descriptor")]
        public Descriptor Descriptor { get; set; }

        /// <summary>
        /// Gets or Sets Categories
        /// </summary>

        [DataMember(Name="categories")]
        public List<Category> Categories { get; set; }

        /// <summary>
        /// Gets or Sets Fulfillments
        /// </summary>

        [DataMember(Name="fulfillments")]
        public List<Fulfillment> Fulfillments { get; set; }

        /// <summary>
        /// Gets or Sets Payments
        /// </summary>

        [DataMember(Name="payments")]
        public List<Payment> Payments { get; set; }

        /// <summary>
        /// Gets or Sets Offers
        /// </summary>

        [DataMember(Name="offers")]
        public List<Offer> Offers { get; set; }

        /// <summary>
        /// Gets or Sets Providers
        /// </summary>

        [DataMember(Name="providers")]
        public List<Provider> Providers { get; set; }

        /// <summary>
        /// Time after which catalog has to be refreshed
        /// </summary>
        /// <value>Time after which catalog has to be refreshed</value>

        [DataMember(Name="exp")]
        public DateTime? Exp { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Catalog {\n");
            sb.Append("  Descriptor: ").Append(Descriptor).Append("\n");
            sb.Append("  Categories: ").Append(Categories).Append("\n");
            sb.Append("  Fulfillments: ").Append(Fulfillments).Append("\n");
            sb.Append("  Payments: ").Append(Payments).Append("\n");
            sb.Append("  Offers: ").Append(Offers).Append("\n");
            sb.Append("  Providers: ").Append(Providers).Append("\n");
            sb.Append("  Exp: ").Append(Exp).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Catalog)obj);
        }

        /// <summary>
        /// Returns true if Catalog instances are equal
        /// </summary>
        /// <param name="other">Instance of Catalog to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Catalog other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Descriptor == other.Descriptor ||
                    Descriptor != null &&
                    Descriptor.Equals(other.Descriptor)
                ) && 
                (
                    Categories == other.Categories ||
                    Categories != null &&
                    Categories.SequenceEqual(other.Categories)
                ) && 
                (
                    Fulfillments == other.Fulfillments ||
                    Fulfillments != null &&
                    Fulfillments.SequenceEqual(other.Fulfillments)
                ) && 
                (
                    Payments == other.Payments ||
                    Payments != null &&
                    Payments.SequenceEqual(other.Payments)
                ) && 
                (
                    Offers == other.Offers ||
                    Offers != null &&
                    Offers.SequenceEqual(other.Offers)
                ) && 
                (
                    Providers == other.Providers ||
                    Providers != null &&
                    Providers.SequenceEqual(other.Providers)
                ) && 
                (
                    Exp == other.Exp ||
                    Exp != null &&
                    Exp.Equals(other.Exp)
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
                    if (Descriptor != null)
                    hashCode = hashCode * 59 + Descriptor.GetHashCode();
                    if (Categories != null)
                    hashCode = hashCode * 59 + Categories.GetHashCode();
                    if (Fulfillments != null)
                    hashCode = hashCode * 59 + Fulfillments.GetHashCode();
                    if (Payments != null)
                    hashCode = hashCode * 59 + Payments.GetHashCode();
                    if (Offers != null)
                    hashCode = hashCode * 59 + Offers.GetHashCode();
                    if (Providers != null)
                    hashCode = hashCode * 59 + Providers.GetHashCode();
                    if (Exp != null)
                    hashCode = hashCode * 59 + Exp.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Catalog left, Catalog right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Catalog left, Catalog right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
