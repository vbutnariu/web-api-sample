﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Demo.Core.DomainModel.App
{
    public partial class UserAzureB2cObjectIdAssignments
    {
        public string UserId { get; set; }
        public string ObjectId { get; set; }

        public virtual Users User { get; set; }
    }
}