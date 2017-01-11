using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.Models
{
    /*
     * Roles is a enumeration for create user role
     */
    public enum Roles
    {
        MusterUser = 0,
        User = 1
    }
    public class UserModels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Roles Roles { get; set; }

		[NotMapped]
		public string Token { get; set; }
	}
}