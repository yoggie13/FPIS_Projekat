using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FPIS_Projekat.Models
{
    public class TariffPackage
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Naziv paketa")]
        public string Name { get; set; }
        public int NumberOfMinutes { get; set; }
        public int NumberOfMessages { get; set; }
        public int NumberOfMB { get; set; }
        public double Price { get; set; }
        public PackageType _PackageType { get; set; }
        [JsonIgnore]
        public List<OfferItem> Offers { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
