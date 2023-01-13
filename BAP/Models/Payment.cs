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
    /// Describes the terms of payment settlement of an order between a BAP and a BPP. The BAP may choose to send its own settlement terms to the BPP before the confirmation of an order. The BPP can choose to accept it, or send its own settlement terms to the BAP. &lt;br/&gt;&lt;br/&gt; The terms of settlement must contain, &lt;i&gt; &lt;ul&gt;&lt;li&gt;The final amount to be paid by the BAP to the BPP&lt;/li&gt;&lt;li&gt;At what stage must the settlement be done i.e On-order, Pre-fulfillment, On-fulfillment, or Post-Fulfillment&lt;/li&gt;&lt;li&gt;A payment endpoint, in case of online settlement&lt;/li&gt;&lt;li&gt;A reference to the order for which the payment is being made&lt;/li&gt;&lt;li&gt;The destination account where the payment must ultimately go to. This may be a bank account, or a virtual payment address - in case the provider does not want to display the account number to the BAP or its consumer&lt;/li&gt;&lt;/ul&gt;&lt;/i&gt;&lt;b&gt;Note:&lt;/b&gt;&lt;b&gt;&lt;i&gt;&lt;ul&gt;&lt;li&gt;These terms ONLY describe the terms of settlement between the BAP and the BPP.&lt;/li&gt;&lt;li&gt;These do not affect \&quot;how\&quot; the payment is collected from the consumer, and how it is ultimately credited to the provider&#x27;s account.&lt;/li&gt;&lt;li&gt;If the terms of settlement sent by the BAP to the BPP MATCH the terms of settlement sent by the BPP to the BAP, then it indicates a mutual agreement between both the actors.&lt;/li&gt;&lt;li&gt;Once an agreement has been reached, the BAP and BPP can initiate the order confirmation process.&lt;/li&gt;&lt;/ul&gt;&lt;/i&gt;&lt;b/&gt;
    /// </summary>
    [DataContract]
    public partial class Payment : IEquatable<Payment>
    { 
        /// <summary>
        /// The payment URL of the payee.  If this field is empty or missing, it indicates that the payment has to be settled manually between the payer and the payee.&lt;br/&gt;&lt;br/&gt;&lt;b&gt;Recommendations:&lt;/b&gt;&lt;ul&gt;&lt;li&gt;The list of supported URI schemes must be published in the network policy&lt;/li&gt;&lt;li&gt;If this field is set, then the BAP must render this link as a CTA or render the payment screen within a webview&lt;/li&gt;&lt;/ul&gt;
        /// </summary>
        /// <value>The payment URL of the payee.  If this field is empty or missing, it indicates that the payment has to be settled manually between the payer and the payee.&lt;br/&gt;&lt;br/&gt;&lt;b&gt;Recommendations:&lt;/b&gt;&lt;ul&gt;&lt;li&gt;The list of supported URI schemes must be published in the network policy&lt;/li&gt;&lt;li&gt;If this field is set, then the BAP must render this link as a CTA or render the payment screen within a webview&lt;/li&gt;&lt;/ul&gt;</value>

        [DataMember(Name="uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Describes the contents of the uri field. If the value is text/html, it is recommended for the BAP to render the contents inside a webview. This generally does not render a good user experience on the BAP, hence the payment page developers are recommended to develop their payment pages in a mobile-friendly manner.
        /// </summary>
        /// <value>Describes the contents of the uri field. If the value is text/html, it is recommended for the BAP to render the contents inside a webview. This generally does not render a good user experience on the BAP, hence the payment page developers are recommended to develop their payment pages in a mobile-friendly manner.</value>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum UriMimeTypeEnum
        {
            /// <summary>
            /// Enum TexthtmlEnum for text/html
            /// </summary>
            [EnumMember(Value = "text/html")]
            TexthtmlEnum = 0,
            /// <summary>
            /// Enum TextplainEnum for text/plain
            /// </summary>
            [EnumMember(Value = "text/plain")]
            TextplainEnum = 1,
            /// <summary>
            /// Enum ApplicationoctetStreamproviderapplicationIdEnum for application/octet-stream;provider=$application_id
            /// </summary>
            [EnumMember(Value = "application/octet-stream;provider=$application_id")]
            ApplicationoctetStreamproviderapplicationIdEnum = 2        }

        /// <summary>
        /// Describes the contents of the uri field. If the value is text/html, it is recommended for the BAP to render the contents inside a webview. This generally does not render a good user experience on the BAP, hence the payment page developers are recommended to develop their payment pages in a mobile-friendly manner.
        /// </summary>
        /// <value>Describes the contents of the uri field. If the value is text/html, it is recommended for the BAP to render the contents inside a webview. This generally does not render a good user experience on the BAP, hence the payment page developers are recommended to develop their payment pages in a mobile-friendly manner.</value>

        [DataMember(Name="uri_mime_type")]
        public UriMimeTypeEnum? UriMimeType { get; set; }

        /// <summary>
        /// Gets or Sets _Params
        /// </summary>

        [DataMember(Name="params")]
        public string _Params { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum TypeEnum
        {
            /// <summary>
            /// Enum ONORDEREnum for ON-ORDER
            /// </summary>
            [EnumMember(Value = "ON-ORDER")]
            ONORDEREnum = 0,
            /// <summary>
            /// Enum PREFULFILLMENTEnum for PRE-FULFILLMENT
            /// </summary>
            [EnumMember(Value = "PRE-FULFILLMENT")]
            PREFULFILLMENTEnum = 1,
            /// <summary>
            /// Enum ONFULFILLMENTEnum for ON-FULFILLMENT
            /// </summary>
            [EnumMember(Value = "ON-FULFILLMENT")]
            ONFULFILLMENTEnum = 2,
            /// <summary>
            /// Enum POSTFULFILLMENTEnum for POST-FULFILLMENT
            /// </summary>
            [EnumMember(Value = "POST-FULFILLMENT")]
            POSTFULFILLMENTEnum = 3        }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>

        [DataMember(Name="type")]
        public TypeEnum? Type { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum StatusEnum
        {
            /// <summary>
            /// Enum PAIDEnum for PAID
            /// </summary>
            [EnumMember(Value = "PAID")]
            PAIDEnum = 0,
            /// <summary>
            /// Enum NOTPAIDEnum for NOT-PAID
            /// </summary>
            [EnumMember(Value = "NOT-PAID")]
            NOTPAIDEnum = 1        }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>

        [DataMember(Name="status")]
        public StatusEnum? Status { get; set; }

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
            sb.Append("class Payment {\n");
            sb.Append("  Uri: ").Append(Uri).Append("\n");
            sb.Append("  UriMimeType: ").Append(UriMimeType).Append("\n");
            sb.Append("  _Params: ").Append(_Params).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Payment)obj);
        }

        /// <summary>
        /// Returns true if Payment instances are equal
        /// </summary>
        /// <param name="other">Instance of Payment to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Payment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Uri == other.Uri ||
                    Uri != null &&
                    Uri.Equals(other.Uri)
                ) && 
                (
                    UriMimeType == other.UriMimeType ||
                    UriMimeType != null &&
                    UriMimeType.Equals(other.UriMimeType)
                ) && 
                (
                    _Params == other._Params ||
                    _Params != null &&
                    _Params.Equals(other._Params)
                ) && 
                (
                    Type == other.Type ||
                    Type != null &&
                    Type.Equals(other.Type)
                ) && 
                (
                    Status == other.Status ||
                    Status != null &&
                    Status.Equals(other.Status)
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
                    if (Uri != null)
                    hashCode = hashCode * 59 + Uri.GetHashCode();
                    if (UriMimeType != null)
                    hashCode = hashCode * 59 + UriMimeType.GetHashCode();
                    if (_Params != null)
                    hashCode = hashCode * 59 + _Params.GetHashCode();
                    if (Type != null)
                    hashCode = hashCode * 59 + Type.GetHashCode();
                    if (Status != null)
                    hashCode = hashCode * 59 + Status.GetHashCode();
                    if (Time != null)
                    hashCode = hashCode * 59 + Time.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Payment left, Payment right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Payment left, Payment right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
