using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.Models
{
    public class Offer
    {
        [Key]
        public int ID { get; set; }
        public Employee _Employee { get; set; }
        public Client _Client { get; set; }
        public DateTime Date { get; set; }
        public List<OfferItem> OfferItems { get; set; }

    }
}
