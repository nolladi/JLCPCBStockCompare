using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace webapp.Models
{
    
    public class BOM
    {

        public int? ID { get; set; }
        public string Name { get; set; }
        public string Footprint { get; set; }
        public int? Quantity { get; set; }

        [Name("Manufacturer Part")]
        public string ManufacturerPart { get; set; }
        public string Manufacturer { get; set; }
        public string Supplier { get; set; }

        [Name("Supplier Part")]
        public string SupplierPart { get; set; }
        public float? Price { get; set; }
        public string Designator { get; set; }
    }


}
