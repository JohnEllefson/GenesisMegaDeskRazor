using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Newtonsoft.Json;

namespace GenesisMegaDeskRazor.Models
{
    public class DeskQuote
    {
        public int Id { get; set; }
        [JsonProperty]
        public Desk Desk;
        [JsonProperty]
        public string CustomerName;
        [JsonProperty]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public double BaseDeskPrice { get; set; }
        public double AdditionalSqInchCost { get; set; }
        public double DrawerCost { get; set; }
        public double SurfaceMaterialCost { get; set; }
        public double RushOrderCost { get; set; }
        [JsonProperty]
        public double TotalPrice { get; set; }
        public double[,]? rushOrderPrices;
    }
}
