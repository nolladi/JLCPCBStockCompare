using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace webapp.Models
{
    public class JLC
    {
        [Name("LCSC Part")]
        public string LCSCPart { get; set; }

       // [Name("First Category")]
      //  public string FirstCategory { get; set; }

      //  [Name("Second Category")]
      //  public string SecondCategory { get; set; }

      //  [Name("MFR.Part")]
      //  public string MFRPart { get; set; }

       // public string Manufacturer { get; set;}
     //   public string Package { get; set; }

      //  [Name("Solder Joint")]
      //  public int SolderJoint { get; set; }

       // [Name("Library Type")]
       // public string LibraryType { get; set; }

       // public string Description { get; set; }
      //  public string Datasheet { get; set; }
      //  public string Price { get; set; }
        public int Stock { get; set; }



    }
}
