using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusBookingSystem.Models
{
    public class BusStop
    {//airport
        [Key]
        public int BusStopId { get; set; }

        [Display(Name = "Bus Stop:")]
        [Required(ErrorMessage = "Bus Stop Required")]
        [MaxLength(30, ErrorMessage = "Maximum 30 characters"), MinLength(3, ErrorMessage = "Minimum 3 characters allowed")]
        public string BusStopName { get; set; }

        [Required, MaxLength(30)]
        public string City { get; set; }




        public ICollection<Route> Routes { get; set; }

    }
}