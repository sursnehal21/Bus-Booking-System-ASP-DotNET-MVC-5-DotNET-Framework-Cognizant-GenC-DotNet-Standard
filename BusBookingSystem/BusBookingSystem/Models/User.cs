using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusBookingSystem.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [Required]
        public int UserId { get; set; }
        public string Role { get; set; }

        [Required(ErrorMessage ="First Name Required")]
        [Display(Name ="First Name:")]
        [MaxLength(40, ErrorMessage ="Max 20 characters allowed"), MinLength(3,ErrorMessage ="Minimum 3 characters required")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last Name Required")]
        [Display(Name = "Last Name:")]
        [MaxLength(40, ErrorMessage = "Max 20 characters allowed"), MinLength(3, ErrorMessage = "Minimum 3 characters required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "User Name Required")]
        [Display(Name = "User Name:")]
        [MaxLength(40, ErrorMessage = "Max 20 characters allowed"), MinLength(5, ErrorMessage = "Minimum 5 characters required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email ID Required")]
        [Display(Name = "Email:")]
        [MaxLength(40, ErrorMessage = "Max 20 characters allowed"), MinLength(5, ErrorMessage = "Minimum 5 characters required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [Display(Name = "Password:")]
        [DataType(DataType.Password)]
        [MaxLength(16, ErrorMessage = "Max 16 characters allowed"), MinLength(8, ErrorMessage = "Minimum 8 characters required")]
        public string Password { get; set; }


        [Display(Name = "Confirm Password:")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage="Incorrect Password")]
        [MaxLength(16, ErrorMessage = "Max 16 characters allowed"), MinLength(8, ErrorMessage = "Minimum 8 characters required")]
        public string ConfirmPassword { get; set; }

        
       



        public ICollection<Booking> Bookings {  get; set; }
        public ICollection<Payment> Payments { get; set; }

    }
}