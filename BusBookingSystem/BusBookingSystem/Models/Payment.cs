using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusBookingSystem.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public string Username { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}" , ApplyFormatInEditMode =true)]
        public DateTime PaymentDate { get; set; }
        public string CardType { get; set; }

        [RegularExpression(@"^\d{16}$",ErrorMessage="Invalid Card Number")]
        public long CardNo { get; set; } 

        [RegularExpression(@"^\d{3}$", ErrorMessage = "Invalid CVV")]
        public int CVV { get; set; }

        public string PaymentMode { get; set; }

        public int BookingId { get; set; }

        public decimal Amount { get; set; }

       

        public virtual Booking Booking { get; set; }

        public int UserId { get; set; }
         public User User { get; set; }

        public int ScheduleId { get; set; }

        public Schedule Schedule {  get; set; }



       
    }
}