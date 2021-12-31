using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace webapp.Models
{
    public class Config
    {
        public string csvPath { get; set; }

        public static Config FromJSON(string jsonString)
        {
            Config parsedConfig = JsonSerializer.Deserialize<Config>(jsonString);
            return parsedConfig;
        }
        


    }
}
