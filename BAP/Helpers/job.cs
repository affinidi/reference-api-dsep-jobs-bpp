﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace BAP.Helpers
{
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum StateEnum
    {
        [EnumMember(Value = "baseSalary")]
        baseSalary = 0,

        [EnumMember(Value = "variableSalary")]
        variableSalary = 1,

        [EnumMember(Value = "allowance")]
        allowance = 2,

        [EnumMember(Value = "comission")]
        comission = 3,

        [EnumMember(Value = "overtime")]
        overtime = 3
    }

    public class Address
    {
        public string name { get; set; }
        public string streetAddress { get; set; }
        public string addressLocality { get; set; }
        public string addressRegion { get; set; }
        public string addressCountry { get; set; }
        public string postalCode { get; set; }
    }

    public class HiringOrganization
    {
        public string name { get; set; }
        public string url { get; set; }
        public string about { get; set; }
        public Address address { get; set; }
    }

    public class Identifier
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class InCodeSet
    {
        public string name { get; set; }
        public string dateModified { get; set; }
        public string url { get; set; }
    }

    public class JobLocation
    {
        public string name { get; set; }
        public Address address { get; set; }
    }

    public class OccupationalCategory
    {
        public InCodeSet inCodeSet { get; set; }
        public string codeValue { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Pay
    {
        public int minValue { get; set; }
        public int maxValue { get; set; }
        public string unitText { get; set; }
        public StateEnum type { get; set; }
    }

    public class Job
    {
        public string id { get; set; }
        public string title { get; set; }
        public string datePosted { get; set; }
        public DateTime validThrough { get; set; }
        public string description { get; set; }
        public HiringOrganization hiringOrganization { get; set; }
        public JobLocation jobLocation { get; set; }
        public List<string> employmentType { get; set; }
        public Salary salary { get; set; }
        public Identifier identifier { get; set; }
        public OccupationalCategory occupationalCategory { get; set; }
        public List<string> responsibilities { get; set; }
        public List<string> skills { get; set; }
    }

    public class Salary
    {
        public string currency { get; set; }
        public List<Pay> pay { get; set; }
    }
}

