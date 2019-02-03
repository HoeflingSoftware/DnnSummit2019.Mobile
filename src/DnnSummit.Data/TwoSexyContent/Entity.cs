﻿using Newtonsoft.Json;
using System;

namespace DnnSummit.Data.TwoSexyContent
{
    [JsonObject]
    internal class Entity
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }
    }
}
