﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Demo.Core.DomainModel.App
{
    public partial class TenantKeyAccountAssignments
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid KeyAccountId { get; set; }

        public virtual TenantKeyAccounts KeyAccount { get; set; }
        public virtual Tenants Tenant { get; set; }
    }
}