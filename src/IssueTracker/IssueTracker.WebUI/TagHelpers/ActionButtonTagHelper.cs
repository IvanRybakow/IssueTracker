using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Common.Enumerations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IssueTracker.WebUI.TagHelpers
{
    public class ActionButtonTagHelper : TagHelper
    {
        public TransitionCommands Command { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string glyphicon = string.Empty;
            string className = string.Empty;
            switch (Command)
            {
                case TransitionCommands.Save:
                    className = "secondary";
                    glyphicon = "glyphicon-edit";
                    break;
                case TransitionCommands.Enter:
                    break;
                case TransitionCommands.Open:
                    className = "primary";
                    glyphicon = "glyphicon-play";
                    break;
                case TransitionCommands.Solve:
                    className = "success";
                    glyphicon = "glyphicon-floppy-disk";
                    break;
                case TransitionCommands.Reopen:
                    className = "info";
                    glyphicon = "glyphicon-backward";
                    break;
                case TransitionCommands.Close:
                    className = "danger";
                    glyphicon = "glyphicon-stop";
                    break;
            }


            output.TagName = "button";
            output.Content.SetContent(Command.ToString());
            output.Attributes.Add("Class", $"btn btn-{className} command-button");
            output.Attributes.Add("Id", Command.ToString());
            output.Attributes.Add("type", "button");
            TagBuilder tag = new TagBuilder("i");
            tag.AddCssClass("glyphicon");
            tag.AddCssClass(glyphicon);
            output.PreContent.AppendHtml(tag);
        }
    }
}
