using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FPIS_Projekat.Models
{
    public class Manufacturer
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Device> Devices { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
