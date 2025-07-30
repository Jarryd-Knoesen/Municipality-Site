using System.ComponentModel.DataAnnotations;

namespace PROG7312_P1_V1.DataModels
{
    public class Profile
    {
        [Key]
        public string ProfileID { get; set; }

        [Required]
        public string FullName { get; set; }

        //[Required]
        //public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public DateTime JoinDate { get; set; } = DateTime.Now;

    }
}
