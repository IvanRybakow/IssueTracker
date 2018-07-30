using System;
using System.ComponentModel.DataAnnotations.Schema;
using IssueTracker.Common.Enumerations;

namespace IssueTracker.Common.Models
{
    [Table("IssueTransitions")]
    public class IssueTransitionEntity
    {
        public int Id { get; set; }
        public DateTime TransitionDate { get; set; }
        public TransitionCommands Command { get; set; }
        public int IssueId { get; set; }
        public IssueEntity Issue { get; set; }
        public int MadeById { get; set; }
        public UserEntity MadeBy { get; set; }
        public string Comment { get; set; }
    }
}
