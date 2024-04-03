using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusBookingSystem.Models
{
    public class BusInfo
    {
        [Key]
        public int BusId { get; set; }

        [Display(Name ="Bus Name:")]
        [Required(ErrorMessage ="Bus Name Required")]
        [MaxLength(30, ErrorMessage = "Maximum 30 characters"), MinLength(3, ErrorMessage = "Minimum 3 characters allowed")]
        public string BusName { get; set; }


        [Required(ErrorMessage = "Capacity Required")]
        [Display(Name ="Seating Capacity:")]
        [Range(1,60,ErrorMessage ="Max 60 are allowed")]
        public int SeatsCapacity { get; set; }

        public ICollection<Route> Routes { get; set; }

    }
}
