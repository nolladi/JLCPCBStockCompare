using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class ModelToPrint:BOM
    {
        public int Stock { get; set; }

        [Name("Max PCB")]
        public int? MaxPCB { get; set; }
    }
}
