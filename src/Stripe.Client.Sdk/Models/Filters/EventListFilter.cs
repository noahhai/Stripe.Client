﻿using System;
using Newtonsoft.Json;
using Stripe.Client.Sdk.Attributes;
using Stripe.Client.Sdk.Extensions;

namespace Stripe.Client.Sdk.Models.Filters
{
    public class EventListFilter : ListFilter
    {
        public string Type { get; set; }

        [JsonIgnore]
        public DateTime? CreatedDateTime { get; set; }

        [JsonIgnore]
        public DateFilter CreatedFilter { get; set; }

        [ChildModel]
        public object Created
        {
            get { return CreatedDateTime.HasValue ? (object)CreatedDateTime.Value.ToEpoch() : CreatedFilter; }
        }
    }
}