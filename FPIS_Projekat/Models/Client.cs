using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FPIS_Projekat.Models
{
    public class Client
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Ime klijenta")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        [JsonIgnore]

        public List<Offer> OffersReceived { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
