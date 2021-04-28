using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasOne(x => x.RequestRaw).WithOne().HasForeignKey<Request>(x => x.RequestRawId).IsRequired();
            builder.HasOne(x => x.Error).WithOne().HasForeignKey<Request>(x => x.ErrorId);
        }
    }
}
