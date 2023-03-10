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
    /// Describes the billing details of an order. This must be provided by BAP user before confirmation of the order.
    /// </summary>
    [DataContract]
    public partial class Billing : IEquatable<Billing>
    { 
        /// <summary>
        /// Name of the person under who&#x27;s name the bill will be generated.
        /// </summary>
        /// <value>Name of the person under who&#x27;s name the bill will be generated.</value>

        [DataMember(Name="name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Organization
        /// </summary>

        [DataMember(Name="organization")]
        public Organization Organization { get; set; }

        /// <summary>
        /// Gets or Sets Address
        /// </summary>

        [DataMember(Name="address")]
        public Address Address { get; set; }

        /// <summary>
        /// Email address of the person / organization being billed. The BPP must send the bill to this email address. The format of the bill may be defined in the network policy.
        /// </summary>
        /// <value>Email address of the person / organization being billed. The BPP must send the bill to this email address. The format of the bill may be defined in the network policy.</value>

        [DataMember(Name="email")]
        public string Email { get; set; }

        /// <summary>
        /// Phone number of the person / organization being billed. The BPP must send the bill to this phone number as per the format specified in the network policy. In case the bill is a downloadable file, it is recommended the bill should be sent to the phone number as a downloadable link.
        /// </summary>
        /// <value>Phone number of the person / organization being billed. The BPP must send the bill to this phone number as per the format specified in the network policy. In case the bill is a downloadable file, it is recommended the bill should be sent to the phone number as a downloadable link.</value>

        [DataMember(Name="phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or Sets Time
        /// </summary>

        [DataMember(Name="time")]
        public Time Time { get; set; }

        /// <summary>
        /// This is the identity of a Tax-paying person or an organization. This number can be provided to the BPP to avail tax benefits, if applicable. The format of this string should be specified in the network policy
        /// </summary>
        /// <value>This is the identity of a Tax-paying person or an organization. This number can be provided to the BPP to avail tax benefits, if applicable. The format of this string should be specified in the network policy</value>

        [DataMember(Name="tax_number")]
        public string TaxNumber { get; set; }

        /// <summary>
        /// Date and time at which this bill was generated by the BPP.
        /// </summary>
        /// <value>Date and time at which this bill was generated by the BPP.</value>

        [DataMember(Name="created_at")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Billing {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Organization: ").Append(Organization).Append("\n");
            sb.Append("  Address: ").Append(Address).Append("\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("  Phone: ").Append(Phone).Append("\n");
            sb.Append("  Time: ").Append(Time).Append("\n");
            sb.Append("  TaxNumber: ").Append(TaxNumber).Append("\n");
            sb.Append("  CreatedAt: ").Append(CreatedAt).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Billing)obj);
        }

        /// <summary>
        /// Returns true if Billing instances are equal
        /// </summary>
        /// <param name="other">Instance of Billing to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Billing other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) && 
                (
                    Organization == other.Organization ||
                    Organization != null &&
                    Organization.Equals(other.Organization)
                ) && 
                (
                    Address == other.Address ||
                    Address != null &&
                    Address.Equals(other.Address)
                ) && 
                (
                    Email == other.Email ||
                    Email != null &&
                    Email.Equals(other.Email)
                ) && 
                (
                    Phone == other.Phone ||
                    Phone != null &&
                    Phone.Equals(other.Phone)
                ) && 
                (
                    Time == other.Time ||
                    Time != null &&
                    Time.Equals(other.Time)
                ) && 
                (
                    TaxNumber == other.TaxNumber ||
                    TaxNumber != null &&
                    TaxNumber.Equals(other.TaxNumber)
                ) && 
                (
                    CreatedAt == other.CreatedAt ||
                    CreatedAt != null &&
                    CreatedAt.Equals(other.CreatedAt)
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
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                    if (Organization != null)
                    hashCode = hashCode * 59 + Organization.GetHashCode();
                    if (Address != null)
                    hashCode = hashCode * 59 + Address.GetHashCode();
                    if (Email != null)
                    hashCode = hashCode * 59 + Email.GetHashCode();
                    if (Phone != null)
                    hashCode = hashCode * 59 + Phone.GetHashCode();
                    if (Time != null)
                    hashCode = hashCode * 59 + Time.GetHashCode();
                    if (TaxNumber != null)
                    hashCode = hashCode * 59 + TaxNumber.GetHashCode();
                    if (CreatedAt != null)
                    hashCode = hashCode * 59 + CreatedAt.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Billing left, Billing right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Billing left, Billing right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
