// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Demo.Core.Data;
using System;
using System.Collections.Generic;

namespace Demo.Core.DomainModel.App
{
    public partial class Users: BaseEntity
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime? LastLoginAt { get; set; }

        public virtual UserAzureB2cObjectIdAssignments UserAzureB2cObjectIdAssignments { get; set; }
    }
}