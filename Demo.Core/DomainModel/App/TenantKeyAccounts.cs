// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Demo.Core.Data;
using System;
using System.Collections.Generic;

namespace Demo.Core.DomainModel.App
{
    public partial class TenantKeyAccounts : BaseEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Region { get; set; }
        public string TimeZone { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public virtual TenantKeyAccountAssignments TenantKeyAccountAssignments { get; set; }
    }
}