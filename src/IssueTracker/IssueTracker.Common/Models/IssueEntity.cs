using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using IssueTracker.Common.Enumerations;

namespace IssueTracker.Common.Models
{
    [Table("Issues")]
    public partial class IssueEntity
    {
        public int Id { get; set; }
        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }
        [DisplayName("Name")]
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        [DisplayName("Created By")]
        public UserEntity CreatedBy { get; set; }
        public ICollection<IssueTransitionEntity> Transitions { get; set; }
        public IssueStatuses Status { get; set; }
        public IssuePriorities Priority { get; set; }
        public IssueUrgencies Urgency { get; set; }

    }
}
