﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colu.Models.IssueAsset
{
    public class Verification
    {
        [JsonProperty("domain")]
        public Domain Domain { get; set; }
    }
}