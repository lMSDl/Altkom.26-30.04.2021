﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class RequestRawConfiguration : IEntityTypeConfiguration<RequestRaw>
    {
        public void Configure(EntityTypeBuilder<RequestRaw> builder)
        {
        }
    }
}
