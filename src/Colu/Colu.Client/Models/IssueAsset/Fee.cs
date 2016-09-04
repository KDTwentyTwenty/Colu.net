﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Colu.Models.IssueAsset
{
    public class Fee
    {
        [JsonProperty("locked")]
        public Boolean Locked { get; set; }

        public IList<Item> Item { get; set; }
    }
}
