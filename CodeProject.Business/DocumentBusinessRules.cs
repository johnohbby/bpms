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
    public class DocumentBusinessRules : AbstractValidator<Document>
    {

        public DocumentBusinessRules()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(a => a.Type).NotEmpty().WithMessage("Type is required."); 
            RuleFor(a => a.FolderId).NotEmpty().WithMessage("Folder is required.");  
        }

    }
}
