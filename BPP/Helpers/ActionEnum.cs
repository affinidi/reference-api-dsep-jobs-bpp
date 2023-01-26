using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace bpp.Helpers
{

    /// <summary>
    /// Defines the Beckn API call. Any actions other than the enumerated actions are not supported by Beckn Protocol
    /// </summary>
    /// <value>Defines the Beckn API call. Any actions other than the enumerated actions are not supported by Beckn Protocol</value>
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum ActionEnum
    {
        /// <summary>
        /// Enum SearchEnum for search
        /// </summary>
        [EnumMember(Value = "search")]
        SearchEnum = 0,
        /// <summary>
        /// Enum SelectEnum for select
        /// </summary>
        [EnumMember(Value = "select")]
        SelectEnum = 1,
        /// <summary>
        /// Enum InitEnum for init
        /// </summary>
        [EnumMember(Value = "init")]
        InitEnum = 2,
        /// <summary>
        /// Enum ConfirmEnum for confirm
        /// </summary>
        [EnumMember(Value = "confirm")]
        ConfirmEnum = 3,
        /// <summary>
        /// Enum UpdateEnum for update
        /// </summary>
        [EnumMember(Value = "update")]
        UpdateEnum = 4,
        /// <summary>
        /// Enum StatusEnum for status
        /// </summary>
        [EnumMember(Value = "status")]
        StatusEnum = 5,
        /// <summary>
        /// Enum TrackEnum for track
        /// </summary>
        [EnumMember(Value = "track")]
        TrackEnum = 6,
        /// <summary>
        /// Enum CancelEnum for cancel
        /// </summary>
        [EnumMember(Value = "cancel")]
        CancelEnum = 7,
        /// <summary>
        /// Enum RatingEnum for rating
        /// </summary>
        [EnumMember(Value = "rating")]
        RatingEnum = 8,
        /// <summary>
        /// Enum SupportEnum for support
        /// </summary>
        [EnumMember(Value = "support")]
        SupportEnum = 9,
        /// <summary>
        /// Enum OnSearchEnum for on_search
        /// </summary>
        [EnumMember(Value = "on_search")]
        OnSearchEnum = 10,
        /// <summary>
        /// Enum OnSelectEnum for on_select
        /// </summary>
        [EnumMember(Value = "on_select")]
        OnSelectEnum = 11,
        /// <summary>
        /// Enum OnInitEnum for on_init
        /// </summary>
        [EnumMember(Value = "on_init")]
        OnInitEnum = 12,
        /// <summary>
        /// Enum OnConfirmEnum for on_confirm
        /// </summary>
        [EnumMember(Value = "on_confirm")]
        OnConfirmEnum = 13,
        /// <summary>
        /// Enum OnUpdateEnum for on_update
        /// </summary>
        [EnumMember(Value = "on_update")]
        OnUpdateEnum = 14,
        /// <summary>
        /// Enum OnStatusEnum for on_status
        /// </summary>
        [EnumMember(Value = "on_status")]
        OnStatusEnum = 15,
        /// <summary>
        /// Enum OnTrackEnum for on_track
        /// </summary>
        [EnumMember(Value = "on_track")]
        OnTrackEnum = 16,
        /// <summary>
        /// Enum OnCancelEnum for on_cancel
        /// </summary>
        [EnumMember(Value = "on_cancel")]
        OnCancelEnum = 17,
        /// <summary>
        /// Enum OnRatingEnum for on_rating
        /// </summary>
        [EnumMember(Value = "on_rating")]
        OnRatingEnum = 18,
        /// <summary>
        /// Enum OnSupportEnum for on_support
        /// </summary>
        [EnumMember(Value = "on_support")]
        OnSupportEnum = 19
    }

}

