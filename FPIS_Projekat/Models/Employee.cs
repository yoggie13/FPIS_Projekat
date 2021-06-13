using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Ime zaposlenog")]
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Offer> OffersMade { get; set; }

        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}
