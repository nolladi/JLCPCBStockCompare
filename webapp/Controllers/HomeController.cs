using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using webapp.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using System.Dynamic;
using System.Net;

namespace webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<JLC> records = new List<JLC>();
        private static Dictionary<string, int> keyValuePairs=new Dictionary<string, int>();
        private List<BOM> bomRecords = new List<BOM>();
        public List<ModelToPrint> modelToPrint = new List<ModelToPrint>();
        public List<MinFromModel> minFromModel = new List<MinFromModel>();


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {
            records.Clear();
            keyValuePairs.Clear();

            string stream = "config.json";
            string jsonString = System.IO.File.ReadAllText(stream);
            Models.Config parsing = Models.Config.FromJSON(jsonString);
            ViewData["JLCPath"] = parsing.csvPath;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null,
                BadDataFound=null
            };
            var client = new WebClient();
            var csvstream = client.OpenRead(parsing.csvPath);
            using (var reader = new StreamReader(csvstream))
            using (var csv = new CsvReader(reader, config))
            {
                 records = csv.GetRecords<JLC>().ToList();
            }

            foreach(var record in records)
            {
                keyValuePairs.Add(record.LCSCPart, record.Stock);
            }

            return View();
        }

        public IActionResult UploadBOM(Int32 multiplyQuantity,IFormFile file)
        {

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = "\t",
                MissingFieldFound=null
            };

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, config))
            {
                bomRecords = csv.GetRecords<BOM>().ToList();
            }

            for (int i = 0; i<bomRecords.Count;i++)
            {
                  ModelToPrint singleModelToPrint = new ModelToPrint();
                  singleModelToPrint.ID = bomRecords[i].ID;
                  singleModelToPrint.Footprint = bomRecords[i].Footprint;
                  singleModelToPrint.Manufacturer = bomRecords[i].Manufacturer;
                  singleModelToPrint.ManufacturerPart = bomRecords[i].ManufacturerPart;
                  singleModelToPrint.Name = bomRecords[i].Name;
                  singleModelToPrint.Price = bomRecords[i].Price;
                  singleModelToPrint.Quantity = bomRecords[i].Quantity * multiplyQuantity;
                  singleModelToPrint.Designator = bomRecords[i].Designator;
                  singleModelToPrint.Supplier = bomRecords[i].Supplier;
                  singleModelToPrint.SupplierPart = bomRecords[i].SupplierPart;
                  int value;
                    if (keyValuePairs.TryGetValue(bomRecords[i].SupplierPart, out value))
                    {
                      singleModelToPrint.Stock = value;
                    }
                    else
                    {
                      singleModelToPrint.Stock = 0;
                    }
                   singleModelToPrint.MaxPCB = (int?)Math.Floor((decimal)(singleModelToPrint.Stock / (bomRecords[i].Quantity * multiplyQuantity)));
                
                   modelToPrint.Add(singleModelToPrint);
                
            }

            int index = 0;
            int? min = modelToPrint[index].MaxPCB;


            for (int i = 0; i < modelToPrint.Count; i++)
            {
                if (modelToPrint[i].MaxPCB < min)
                {
                    min = modelToPrint[i].MaxPCB;
                    index = i;
                }
            }

            MinFromModel minFromMaxPCB = new MinFromModel();
            minFromMaxPCB.min = min;
            minFromModel.Add(minFromMaxPCB);

            dynamic mymodel = new ExpandoObject();
            mymodel.modelToPrint = modelToPrint;
            mymodel.minFromModel = minFromModel;
            return View(mymodel);

        }

        public IActionResult CreateNewCompare()
        {

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
