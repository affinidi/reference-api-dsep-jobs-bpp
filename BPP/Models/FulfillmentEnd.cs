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
    /// Details on the end of fulfillment
    /// </summary>
    [DataContract]
    public partial class FulfillmentEnd : IEquatable<FulfillmentEnd>
    { 
        /// <summary>
        /// Gets or Sets Location
        /// </summary>

        [DataMember(Name="location")]
        public Location Location { get; set; }

        /// <summary>
        /// Gets or Sets Time
        /// </summary>

        [DataMember(Name="time")]
        public Time Time { get; set; }

        /// <summary>
        /// Gets or Sets Instructions
        /// </summary>

        [DataMember(Name="instructions")]
        public Descriptor Instructions { get; set; }

        /// <summary>
        /// Gets or Sets Contact
        /// </summary>

        [DataMember(Name="contact")]
        public Contact Contact { get; set; }

        /// <summary>
        /// Gets or Sets Person
        /// </summary>

        [DataMember(Name="person")]
        public Person Person { get; set; }

        /// <summary>
        /// Gets or Sets Authorization
        /// </summary>

        [DataMember(Name="authorization")]
        public FulfillmentAuthorization Authorization { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FulfillmentEnd {\n");
            sb.Append("  Location: ").Append(Location).Append("\n");
            sb.Append("  Time: ").Append(Time).Append("\n");
            sb.Append("  Instructions: ").Append(Instructions).Append("\n");
            sb.Append("  Contact: ").Append(Contact).Append("\n");
            sb.Append("  Person: ").Append(Person).Append("\n");
            sb.Append("  Authorization: ").Append(Authorization).Append("\n");
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
            return obj.GetType() == GetType() && Equals((FulfillmentEnd)obj);
        }

        /// <summary>
        /// Returns true if FulfillmentEnd instances are equal
        /// </summary>
        /// <param name="other">Instance of FulfillmentEnd to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FulfillmentEnd other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Location == other.Location ||
                    Location != null &&
                    Location.Equals(other.Location)
                ) && 
                (
                    Time == other.Time ||
                    Time != null &&
                    Time.Equals(other.Time)
                ) && 
                (
                    Instructions == other.Instructions ||
                    Instructions != null &&
                    Instructions.Equals(other.Instructions)
                ) && 
                (
                    Contact == other.Contact ||
                    Contact != null &&
                    Contact.Equals(other.Contact)
                ) && 
                (
                    Person == other.Person ||
                    Person != null &&
                    Person.Equals(other.Person)
                ) && 
                (
                    Authorization == other.Authorization ||
                    Authorization != null &&
                    Authorization.Equals(other.Authorization)
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
                    if (Location != null)
                    hashCode = hashCode * 59 + Location.GetHashCode();
                    if (Time != null)
                    hashCode = hashCode * 59 + Time.GetHashCode();
                    if (Instructions != null)
                    hashCode = hashCode * 59 + Instructions.GetHashCode();
                    if (Contact != null)
                    hashCode = hashCode * 59 + Contact.GetHashCode();
                    if (Person != null)
                    hashCode = hashCode * 59 + Person.GetHashCode();
                    if (Authorization != null)
                    hashCode = hashCode * 59 + Authorization.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(FulfillmentEnd left, FulfillmentEnd right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FulfillmentEnd left, FulfillmentEnd right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
