// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NextTech.PayHub.Accounting.Infrastructure.EF;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NextTech.PayHub.Accounting.Infrastructure.EF.Migrations
{
    [DbContext(typeof(AccountingDBContext))]
    partial class AccountingDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("NextTech.PayHub.Accounting.Domain.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.HasKey("Id");

                    b.ToTable("Account", "Accounting");
                });

            modelBuilder.Entity("NextTech.PayHub.Accounting.Domain.Card", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("AccountId")
                        .HasColumnType("integer");

                    b.Property<string>("CardCVC")
                        .HasColumnType("text");

                    b.Property<string>("CardExpireDate")
                        .HasColumnType("text");

                    b.Property<string>("CardNumber")
                        .HasColumnType("text");

                    b.Property<int>("CardType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CardNumber", "AccountId", "CardExpireDate", "CardCVC")
                        .IsUnique();

                    b.ToTable("Card", "Accounting");

                    b.HasDiscriminator<int>("CardType");
                });

            modelBuilder.Entity("NextTech.PayHub.Accounting.Domain.AmericanExpress", b =>
                {
                    b.HasBaseType("NextTech.PayHub.Accounting.Domain.Card");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("NextTech.PayHub.Accounting.Domain.MasterCard", b =>
                {
                    b.HasBaseType("NextTech.PayHub.Accounting.Domain.Card");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("NextTech.PayHub.Accounting.Domain.Visa", b =>
                {
                    b.HasBaseType("NextTech.PayHub.Accounting.Domain.Card");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("NextTech.PayHub.Accounting.Domain.Card", b =>
                {
                    b.HasOne("NextTech.PayHub.Accounting.Domain.Account", "Account")
                        .WithMany("Cards")
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("NextTech.PayHub.Accounting.Domain.Account", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
