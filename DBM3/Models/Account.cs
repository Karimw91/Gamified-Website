using System.ComponentModel.DataAnnotations;

namespace DBM3.Models
{
    public class Account
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }  // Full Name (optional, depending on your use case)
        public string FirstName { get; set; }  // Learner/Instructor First Name
        public string LastName { get; set; }   // Learner/Instructor Last Name
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
