﻿using System;
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
    public class ContentFormMapBusinessRules : AbstractValidator<ContentFormMap>
    {

        public ContentFormMapBusinessRules()
        {
            
        }

    }
}
