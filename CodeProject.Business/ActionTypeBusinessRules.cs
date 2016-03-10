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
    public class ActionTypeBusinessRules : AbstractValidator<ActionType>
    {

        public ActionTypeBusinessRules()
        {
            RuleFor(a => a.Id).NotEmpty().WithMessage("Id is required.");   
        }

    }
}
