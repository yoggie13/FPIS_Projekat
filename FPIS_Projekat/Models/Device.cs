using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.Models
{
    public class Device
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Naziv uređaja")]
        public string Name { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        [ForeignKey("ManufacturerID")]
        [Required]
        public Manufacturer _Manufacturer { get; set; }

        public override string ToString()
        {
            return Name + " - " + _Manufacturer.Name;
        }
    }
}
