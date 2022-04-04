using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextTech.PayHub.Accounting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.PayHub.Accounting.Infrastructure.EF.Configurations
{
    internal class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            #region Primary Key

            builder.HasKey(q => q.Id);

            #endregion

            #region Properties                       

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Ignore(p => p.CardNumberRegex);
            builder.Ignore(p => p.CvcNumberRegex);
            builder.Ignore(p => p.ValidCardInformation);
            builder.Ignore(p => p.ValidationsErrors);
            builder.Ignore(p => p.CardOwner);

            builder.HasDiscriminator<int>("CardType")
            .HasValue<AmericanExpress>(1)
            .HasValue<MasterCard>(2)
            .HasValue<Visa>(3);

            builder.HasIndex(p => new { p.CardNumber, p.AccountId, p.CardExpireDate, p.CardCVC }).IsUnique(true);

            #endregion

            #region Relationship

            builder.HasOne<Account>(p => p.Account)
                .WithMany(g => g.Cards)
                .HasForeignKey(p => p.AccountId);

            #endregion

            #region Table & Column Mappings

            builder.ToTable("Card", "Accounting");

            #endregion
        }
    }
}
