using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextTech.PayHub.Accounting.Domain;

namespace NextTech.PayHub.Accounting.Infrastructure.EF
{   
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            #region Primary Key

            builder.HasKey(q => q.Id);

            #endregion

            #region Properties      
            
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(250);

            #endregion

            #region Table & Column Mappings

            builder.ToTable("Account", "Accounting");

            #endregion
        }
    }
}
