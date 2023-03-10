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
    /// Describes the location of a runtime object.
    /// </summary>
    [DataContract]
    public partial class Location : IEquatable<Location>
    { 
        /// <summary>
        /// Gets or Sets Id
        /// </summary>

        [DataMember(Name="id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets Descriptor
        /// </summary>

        [DataMember(Name="descriptor")]
        public Descriptor Descriptor { get; set; }

        /// <summary>
        /// Gets or Sets Gps
        /// </summary>
        [RegularExpression(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$")]
        [DataMember(Name="gps")]
        public string Gps { get; set; }

        /// <summary>
        /// Gets or Sets Address
        /// </summary>

        [DataMember(Name="address")]
        public Address Address { get; set; }

        /// <summary>
        /// Gets or Sets StationCode
        /// </summary>

        [DataMember(Name="station_code")]
        public string StationCode { get; set; }

        /// <summary>
        /// Gets or Sets City
        /// </summary>

        [DataMember(Name="city")]
        public City City { get; set; }

        /// <summary>
        /// Gets or Sets Country
        /// </summary>

        [DataMember(Name="country")]
        public Country Country { get; set; }

        /// <summary>
        /// Gets or Sets Circle
        /// </summary>

        [DataMember(Name="circle")]
        public Circle Circle { get; set; }

        /// <summary>
        /// Gets or Sets Polygon
        /// </summary>

        [DataMember(Name="polygon")]
        public string Polygon { get; set; }

        /// <summary>
        /// Gets or Sets _3dspace
        /// </summary>

        [DataMember(Name="3dspace")]
        public string _3dspace { get; set; }

        /// <summary>
        /// Gets or Sets Time
        /// </summary>

        [DataMember(Name="time")]
        public Time Time { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Location {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Descriptor: ").Append(Descriptor).Append("\n");
            sb.Append("  Gps: ").Append(Gps).Append("\n");
            sb.Append("  Address: ").Append(Address).Append("\n");
            sb.Append("  StationCode: ").Append(StationCode).Append("\n");
            sb.Append("  City: ").Append(City).Append("\n");
            sb.Append("  Country: ").Append(Country).Append("\n");
            sb.Append("  Circle: ").Append(Circle).Append("\n");
            sb.Append("  Polygon: ").Append(Polygon).Append("\n");
            sb.Append("  _3dspace: ").Append(_3dspace).Append("\n");
            sb.Append("  Time: ").Append(Time).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Location)obj);
        }

        /// <summary>
        /// Returns true if Location instances are equal
        /// </summary>
        /// <param name="other">Instance of Location to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Location other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Id == other.Id ||
                    Id != null &&
                    Id.Equals(other.Id)
                ) && 
                (
                    Descriptor == other.Descriptor ||
                    Descriptor != null &&
                    Descriptor.Equals(other.Descriptor)
                ) && 
                (
                    Gps == other.Gps ||
                    Gps != null &&
                    Gps.Equals(other.Gps)
                ) && 
                (
                    Address == other.Address ||
                    Address != null &&
                    Address.Equals(other.Address)
                ) && 
                (
                    StationCode == other.StationCode ||
                    StationCode != null &&
                    StationCode.Equals(other.StationCode)
                ) && 
                (
                    City == other.City ||
                    City != null &&
                    City.Equals(other.City)
                ) && 
                (
                    Country == other.Country ||
                    Country != null &&
                    Country.Equals(other.Country)
                ) && 
                (
                    Circle == other.Circle ||
                    Circle != null &&
                    Circle.Equals(other.Circle)
                ) && 
                (
                    Polygon == other.Polygon ||
                    Polygon != null &&
                    Polygon.Equals(other.Polygon)
                ) && 
                (
                    _3dspace == other._3dspace ||
                    _3dspace != null &&
                    _3dspace.Equals(other._3dspace)
                ) && 
                (
                    Time == other.Time ||
                    Time != null &&
                    Time.Equals(other.Time)
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
                    if (Id != null)
                    hashCode = hashCode * 59 + Id.GetHashCode();
                    if (Descriptor != null)
                    hashCode = hashCode * 59 + Descriptor.GetHashCode();
                    if (Gps != null)
                    hashCode = hashCode * 59 + Gps.GetHashCode();
                    if (Address != null)
                    hashCode = hashCode * 59 + Address.GetHashCode();
                    if (StationCode != null)
                    hashCode = hashCode * 59 + StationCode.GetHashCode();
                    if (City != null)
                    hashCode = hashCode * 59 + City.GetHashCode();
                    if (Country != null)
                    hashCode = hashCode * 59 + Country.GetHashCode();
                    if (Circle != null)
                    hashCode = hashCode * 59 + Circle.GetHashCode();
                    if (Polygon != null)
                    hashCode = hashCode * 59 + Polygon.GetHashCode();
                    if (_3dspace != null)
                    hashCode = hashCode * 59 + _3dspace.GetHashCode();
                    if (Time != null)
                    hashCode = hashCode * 59 + Time.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Location left, Location right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Location left, Location right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
