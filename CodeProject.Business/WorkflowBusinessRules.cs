using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using CodeProject.Business.Entities;
using System.Configuration;
using CodeProject.Interfaces;

namespace CodeProject.Business
{
    public class WorkflowBusinessRules : AbstractValidator<Workflow>
    {

        public WorkflowBusinessRules()
        {          
             RuleFor(w => w.Name).NotEmpty().WithMessage("Name is required.");
        }

    }
}
