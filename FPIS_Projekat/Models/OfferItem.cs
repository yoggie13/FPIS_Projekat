using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.Models
{
    public class OfferItem
    {
        [Required]
        public Offer _Offer { get; set; }
        [Key]
        public int ID { get; set; }
    
        public Device _Device { get; set; }
        [Required]
        public TariffPackage _TariffPackage { get; set; }
       
    }
}
