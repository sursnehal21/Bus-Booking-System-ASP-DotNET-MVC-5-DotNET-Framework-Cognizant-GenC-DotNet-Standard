using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusBookingSystem.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [ForeignKey("Route")]
        public int RouteId { get; set; }

       [ForeignKey("BusInfo")]
        public int BusId { get; set; }

        [Display(Name = "Source")]
        public string SourceBusStop { get; set; }

        [Display(Name = "Destination")]
        public string DestinationBusStop { get; set; }

        public string SourceBusStopId { get; set; }

        public string DestinationBusStopId { get; set; }

        [Display(Name ="Trip Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BusDate { get; set; }

        [Display(Name = "Departure Time")]
        public string DepartureTime { get; set; }

        [Display(Name = "Arrival Time")]
        public string ArrivalTime { get; set; }

        [Required(ErrorMessage ="Price Required")]
        public float Price { get; set; }
        

       public  BusInfo BusInfo { get; set; }

        public Route Route { get; set; }

    }
}