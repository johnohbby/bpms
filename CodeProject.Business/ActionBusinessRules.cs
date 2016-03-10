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
    public class ActionBusinessRules : AbstractValidator<CodeProject.Business.Entities.Action>
    {

        public ActionBusinessRules()
        {
            RuleFor(a => a.ActionTypeId).NotEmpty().WithMessage("Action Type Id is required.");   
        }

    }
}
