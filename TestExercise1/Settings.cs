using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestExercise1
{
    public class Settings
    {
        [JsonProperty("initdirectory")]
        public string InitialDirectory { get; set; }
        [JsonProperty("enddirectory")]
        public string EndDirectory { get; set; }

    }
}
