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
    public class EmailBusinessRules : AbstractValidator<Email>
    {

        public EmailBusinessRules()
        {
           RuleFor(a => a.From).NotEmpty().WithMessage("From is required.");
           RuleFor(a => a.To).NotEmpty().WithMessage("To is required.");   
        }

    }
}
