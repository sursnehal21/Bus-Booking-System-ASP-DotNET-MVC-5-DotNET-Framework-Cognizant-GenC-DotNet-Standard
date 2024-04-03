using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusBookingSystem.Models
{
    [Table("Route")]
    public class Route
    {
        [Key]
        public int RouteId { get; set; }

        [ForeignKey("BusInfo")]
        public int BusId { get; set; }

        [ForeignKey("BusStop")]
        public int BusStopId { get; set; }

        [Display(Name = "Boarding:")]
        [Required(ErrorMessage ="Boarding Required")]
        public string SourceBusStop { get; set; }

        public string SourceBusStopId { get; set; }


        [Display(Name = "Dropping:")]
        [Required(ErrorMessage = "Dropping Required")]
        public string DestinationBusStop { get; set; }

        public string DestinationBusStopId { get; set; }


        
        [Display(Name ="Departure Time:")]
        [Required(ErrorMessage = "Departure time Required")]
        public string DepartureTime { get; set; }

        [Display(Name = "Arrival Time:")]
        [Required(ErrorMessage = "Arrival time Required")]
        public string ArrivalTime { get; set; }

        [Display(Name = "Total Seats Capacity:")]
        [Required(ErrorMessage = "Total Seats Required")]
        public int TotalSeatsCapacity { get; set; }


        
        public ICollection<Schedule> Schedules { get; set; }
        public  BusInfo BusInfo { get; set; }
        public  BusStop BusStop { get; set; }

    }
}