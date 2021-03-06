﻿using System;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Common.Enumerations;
using IssueTracker.Common.Models;
using IssueTracker.Persistance;
using IssueTracker.WebUI.Models;
using IssueTracker.WebUI.TagHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.WebUI.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IssueTrackerContext _context;

        public IssuesController(IssueTrackerContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index(SortState sortOrder = SortState.Column1, bool isDesc = true,  bool filter = false)
        {
            IQueryable<IssueEntity> issues = _context.Issues.Include(i => i.CreatedBy);
            if (filter)
            {
                issues = issues.Where(i => i.CreatedBy.Login == User.Identity.Name);
            }
            switch (sortOrder)
            {
                case SortState.Column1:
                    issues = isDesc ? issues.OrderByDescending(i => i.CreationDate) : issues.OrderBy(i => i.CreationDate);
                    break;
                case SortState.Column2:
                    issues = isDesc ? issues.OrderByDescending(i => i.ShortDescription) : issues.OrderBy(i => i.ShortDescription);
                    break;
                case SortState.Column3:
                    issues = isDesc ? issues.OrderByDescending(i => i.Status) : issues.OrderBy(i => i.Status);
                    break;
                case SortState.Column4:
                    issues = isDesc ? issues.OrderByDescending(i => i.Priority) : issues.OrderBy(i => i.Priority);
                    break;
                case SortState.Column5:
                    issues = isDesc ? issues.OrderByDescending(i => i.Urgency) : issues.OrderBy(i => i.Urgency);
                    break;
                case SortState.Column6:
                    issues = isDesc ? issues.OrderByDescending(i => i.CreatedBy.FullName) : issues.OrderBy(i => i.CreatedBy.FullName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, null);
            }

            var viewModel = new IssuesViewModel
            {
                Issues = await issues.ToListAsync(),
                SortModel = new IssueSortViewModel(sortOrder, isDesc, filter.ToString()),
                Caption = filter ? "Issues Created By You" : "All Issues"
            };
                
            return View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShortDescription,Description,Priority,Urgency")] IssueEntity issueEntity)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == User.Identity.Name);
                issueEntity.CreatedBy = user;
                issueEntity.CreationDate = DateTime.Now;
                issueEntity.Status = IssueStatuses.New;
                var newRecord = new IssueTransitionEntity
                {
                    Command = TransitionCommands.Enter,
                    Issue = issueEntity,
                    MadeBy = user,
                    TransitionDate = DateTime.Now,
                    Comment = "Issue Created"
                };
                _context.Add(issueEntity);
                _context.Add(newRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "FirstName", issueEntity.CreatedById);
            return View(issueEntity);
        }

        // GET: Issues/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueEntity = await _context.Issues
                .Include(i => i.CreatedBy)
                .Include(i => i.Transitions)
                .ThenInclude(t => t.MadeBy)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (issueEntity == null)
            {
                return NotFound();
            }
            var possibleCommands = issueEntity.GetPossibleActions();
            possibleCommands.Add(TransitionCommands.Save);
            ViewBag.PossibleActions = possibleCommands;
            return View(issueEntity);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShortDescription,Description,Priority,Urgency")] IssueEntity issueEntity,
            string comment, TransitionCommands command = TransitionCommands.Save)
        {
            var currerntEntity = await _context.Issues.FirstOrDefaultAsync(i => i.Id == id);
            if (id != issueEntity.Id || currerntEntity == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == User.Identity.Name);
            var record  = new IssueTransitionEntity
            {
                MadeBy = user,
                Command = command,
                Issue = currerntEntity,
                TransitionDate = DateTime.Now,
                Comment = comment
            };
            _context.Add(record);

                currerntEntity.ShortDescription = issueEntity.ShortDescription;
                currerntEntity.Description = issueEntity.Description;
                currerntEntity.Priority = issueEntity.Priority;
                currerntEntity.Urgency = issueEntity.Urgency;
                currerntEntity.ChangeState(command);
                _context.Update(currerntEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.PossibleActions = issueEntity.GetPossibleActions();
            return View(issueEntity);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueEntity = await _context.Issues
                .Include(i => i.CreatedBy)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (issueEntity == null)
            {
                return NotFound();
            }

            return View(issueEntity);
        }

        // POST: Issues/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issueEntity = await _context.Issues.SingleOrDefaultAsync(m => m.Id == id);
            _context.Issues.Remove(issueEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}