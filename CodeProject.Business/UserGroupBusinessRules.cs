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
    public class UserGroupBusinessRules : AbstractValidator<UserGroup>
    {

        public UserGroupBusinessRules()
        {          
            RuleFor(c => c.GroupId).NotEmpty().WithMessage("Group Id is required.");
            RuleFor(c => c.UserId).NotEmpty().WithMessage("User Id is required.");  
        }

    }
}
