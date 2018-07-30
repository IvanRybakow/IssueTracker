using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueTracker.Common.Models
{
    [Table("Users")]
    public class UserEntity
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<IssueEntity> Issues { get; set; }
        public ICollection<IssueTransitionEntity> IssueTransitions { get; set; }
    }
}
