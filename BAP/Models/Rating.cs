///*
// *  
// *
// *  
// *
// * OpenAPI spec version: 1.0.0-dsep-draft
// * 
// * Generated by: https://github.com/swagger-api/swagger-codegen.git
// */
//using System;
//using System.Linq;
//using System.IO;
//using System.Text;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel.DataAnnotations;
//using System.Runtime.Serialization;
//using Newtonsoft.Json;

//namespace BAP.Models
//{ 
//    /// <summary>
//    /// Describes the rating of a person or an object.
//    /// </summary>
//    [DataContract]
//    public partial class Rating : IEquatable<Rating>
//    { 
//        /// <summary>
//        /// Category of the object being rated
//        /// </summary>
//        /// <value>Category of the object being rated</value>

//        [DataMember(Name="rating_category")]
//        public string RatingCategory { get; set; }

//        /// <summary>
//        /// Id of the object being rated
//        /// </summary>
//        /// <value>Id of the object being rated</value>

//        [DataMember(Name="id")]
//        public string Id { get; set; }

//        /// <summary>
//        /// Rating value given to the object
//        /// </summary>
//        /// <value>Rating value given to the object</value>

//        [DataMember(Name="value")]
//        public decimal? Value { get; set; }

//        /// <summary>
//        /// Gets or Sets FeedbackForm
//        /// </summary>

//        //[DataMember(Name="feedback_form")]
//        //public FeedbackForm FeedbackForm { get; set; }

//        /// <summary>
//        /// Gets or Sets FeedbackId
//        /// </summary>

//        [DataMember(Name="feedback_id")]
//        public string FeedbackId { get; set; }

//        /// <summary>
//        /// Returns the string presentation of the object
//        /// </summary>
//        /// <returns>String presentation of the object</returns>
//        public override string ToString()
//        {
//            var sb = new StringBuilder();
//            sb.Append("class Rating {\n");
//            sb.Append("  RatingCategory: ").Append(RatingCategory).Append("\n");
//            sb.Append("  Id: ").Append(Id).Append("\n");
//            sb.Append("  Value: ").Append(Value).Append("\n");
//            sb.Append("  FeedbackForm: ").Append(FeedbackForm).Append("\n");
//            sb.Append("  FeedbackId: ").Append(FeedbackId).Append("\n");
//            sb.Append("}\n");
//            return sb.ToString();
//        }

//        /// <summary>
//        /// Returns the JSON string presentation of the object
//        /// </summary>
//        /// <returns>JSON string presentation of the object</returns>
//        public string ToJson()
//        {
//            return JsonConvert.SerializeObject(this, Formatting.Indented);
//        }

//        /// <summary>
//        /// Returns true if objects are equal
//        /// </summary>
//        /// <param name="obj">Object to be compared</param>
//        /// <returns>Boolean</returns>
//        public override bool Equals(object obj)
//        {
//            if (ReferenceEquals(null, obj)) return false;
//            if (ReferenceEquals(this, obj)) return true;
//            return obj.GetType() == GetType() && Equals((Rating)obj);
//        }

//        /// <summary>
//        /// Returns true if Rating instances are equal
//        /// </summary>
//        /// <param name="other">Instance of Rating to be compared</param>
//        /// <returns>Boolean</returns>
//        public bool Equals(Rating other)
//        {
//            if (ReferenceEquals(null, other)) return false;
//            if (ReferenceEquals(this, other)) return true;

//            return 
//                (
//                    RatingCategory == other.RatingCategory ||
//                    RatingCategory != null &&
//                    RatingCategory.Equals(other.RatingCategory)
//                ) && 
//                (
//                    Id == other.Id ||
//                    Id != null &&
//                    Id.Equals(other.Id)
//                ) && 
//                (
//                    Value == other.Value ||
//                    Value != null &&
//                    Value.Equals(other.Value)
//                ) && 
//                (
//                    FeedbackForm == other.FeedbackForm ||
//                    FeedbackForm != null &&
//                    FeedbackForm.Equals(other.FeedbackForm)
//                ) && 
//                (
//                    FeedbackId == other.FeedbackId ||
//                    FeedbackId != null &&
//                    FeedbackId.Equals(other.FeedbackId)
//                );
//        }

//        /// <summary>
//        /// Gets the hash code
//        /// </summary>
//        /// <returns>Hash code</returns>
//        public override int GetHashCode()
//        {
//            unchecked // Overflow is fine, just wrap
//            {
//                var hashCode = 41;
//                // Suitable nullity checks etc, of course :)
//                    if (RatingCategory != null)
//                    hashCode = hashCode * 59 + RatingCategory.GetHashCode();
//                    if (Id != null)
//                    hashCode = hashCode * 59 + Id.GetHashCode();
//                    if (Value != null)
//                    hashCode = hashCode * 59 + Value.GetHashCode();
//                    if (FeedbackForm != null)
//                    hashCode = hashCode * 59 + FeedbackForm.GetHashCode();
//                    if (FeedbackId != null)
//                    hashCode = hashCode * 59 + FeedbackId.GetHashCode();
//                return hashCode;
//            }
//        }

//        #region Operators
//        #pragma warning disable 1591

//        public static bool operator ==(Rating left, Rating right)
//        {
//            return Equals(left, right);
//        }

//        public static bool operator !=(Rating left, Rating right)
//        {
//            return !Equals(left, right);
//        }

//        #pragma warning restore 1591
//        #endregion Operators
//    }
//}
