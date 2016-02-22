﻿using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class GroupTypeViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<Group> Groups { get; set; }
    }
}