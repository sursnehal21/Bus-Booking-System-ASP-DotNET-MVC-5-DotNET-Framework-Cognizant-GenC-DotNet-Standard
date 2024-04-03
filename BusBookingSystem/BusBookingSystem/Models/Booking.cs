using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusBookingSystem.Models
{
    [Table("Bookings")] //using this annotation we can override the name of the table in BbsDbcontext *not recommended*
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Route")]
        public int RouteId { get; set; }

        [ForeignKey("Schedule")]
        public int ScheduleId { get; set; }


        [Required(ErrorMessage = "Source Required")]
        [Display(Name = "Source")]
        public string Source { get; set; }

        [Required(ErrorMessage = "Destination Required")]
        [Display(Name = "Destination")]
        public string Destination { get; set; }

        [Display(Name = "Number Of Passengers:")]
        [Range(1,6,ErrorMessage ="Min 1 and Max 6 Passengers are allowed")]
        public int NumberOfPassengers { get; set; }

        [Display(Name ="Booking Date:")]
        public DateTime BookingDate { get; set; }




      

      

        public User User { get; set; }

        public Route Route { get; set; }


        public  Schedule Schedule { get; set; }



    }
}
