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
    public class StatusTranslationBusinessRules : AbstractValidator<StatusTranslation>
    {

        public StatusTranslationBusinessRules()
        {
            RuleFor(a => a.ActionTypeId).NotEmpty().WithMessage("Action Type is required.");
            RuleFor(a => a.StatusIdFrom).NotEmpty().WithMessage("Status From is required.");
            RuleFor(a => a.StatusIdTo).NotEmpty().WithMessage("Status To is required.");  
        }

    }
}
