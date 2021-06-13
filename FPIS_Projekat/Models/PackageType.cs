using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.Models
{
    public class PackageType
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
