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
    public class ContentRightBusinessRules : AbstractValidator<ContentRight>
    {

        public ContentRightBusinessRules()
        {
            RuleFor(a => a.ContentId).NotEmpty().WithMessage("Content is required.");
            RuleFor(a => a.RightTypeId).NotEmpty().WithMessage("Right Type is required."); 
        }

    }
}
