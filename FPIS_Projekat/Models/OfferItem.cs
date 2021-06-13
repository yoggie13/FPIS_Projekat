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
        [Key, Column(Order = 2)]
        public int ID { get; set; }
        [Key, Column(Order = 1)]
        public Offer _Offer { get; set; }
        public Device _Device { get; set; }
        public TariffPackage _TariffPackage { get; set; }
       
    }
}
