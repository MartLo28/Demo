using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime LastLogin { get; set; }
        public bool HasCompletedWalkthrough { get; set; }
    }
}
