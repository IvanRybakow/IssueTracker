using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using IssueTracker.Common.Enumerations;

namespace IssueTracker.Common.Models
{
    public partial class IssueEntity
    {
        [NotMapped]
        private static readonly Dictionary<Tuple<IssueStatuses, TransitionCommands>, IssueStatuses> PossibleTransitions
            =
            new Dictionary<Tuple<IssueStatuses, TransitionCommands>, IssueStatuses>
            {
                {
                    new Tuple<IssueStatuses, TransitionCommands>(IssueStatuses.New, TransitionCommands.Open),
                    IssueStatuses.Open
                },
                {
                    new Tuple<IssueStatuses, TransitionCommands>(IssueStatuses.Open, TransitionCommands.Solve),
                    IssueStatuses.Solved
                },
                {
                    new Tuple<IssueStatuses, TransitionCommands>(IssueStatuses.Solved, TransitionCommands.Reopen),
                    IssueStatuses.Open
                },
                {
                    new Tuple<IssueStatuses, TransitionCommands>(IssueStatuses.Solved, TransitionCommands.Close),
                    IssueStatuses.Closed
                }
            };

        public List<TransitionCommands> GetPossibleActions()
        {
            return PossibleTransitions.Keys.Where(k => k.Item1 == this.Status).Select(k => k.Item2).ToList();
            ;
        }

        public void ChangeState(TransitionCommands command)
        {
            var transitionExists = PossibleTransitions.TryGetValue(
                new Tuple<IssueStatuses, TransitionCommands>(this.Status, command),
                out var result);
            if (transitionExists) this.Status = result;
        }
    }
}
