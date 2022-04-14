using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using eShop_DHA.Entities;

namespace eShop_DHA.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
               
        }

        public virtual DbSet<Category> Category { get; set; } = null!;
        public virtual DbSet<Product> Product { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("hstore");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category__c", "salesforce");


                entity.HasIndex(e => e.ExternalId, "hcu_idx_category__c_external_id__c")
                    .IsUnique();

                entity.HasIndex(e => e.SfId, "hcu_idx_category__c_sfid")
                    .IsUnique()
                    .UseCollation(new[] { "ucs_basic" });

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createddate");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(255)
                    .HasColumnName("external_id__c");


              

                entity.Property(e => e.IsDeleted).HasColumnName("isdeleted");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("lastmodifieddate");

                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .HasColumnName("name");

                entity.Property(e => e.SfId)
                    .HasMaxLength(18)
                    .HasColumnName("sfid")
                    .UseCollation("ucs_basic");

                // entity.HasMany(x => x.Products)
                //     .WithOne(x => x.Category)
                //     .HasForeignKey(x => x.CategoryExternalId)
                //     .HasPrincipalKey(x => x.ExternalId);
                //



            });

        

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product__c", "salesforce");

                entity.HasIndex(e => e.CategoryId, "hc_idx_product__c_categoryid__c");


                entity.HasIndex(e => e.ExternalId, "hcu_idx_product__c_external_id__c")
                    .IsUnique();

                entity.HasIndex(e => e.SfId, "hcu_idx_product__c_sfid")
                    .IsUnique()
                    .UseCollation(new[] { "ucs_basic" });

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(18)
                    .HasColumnName("categoryid__c");

                entity.Property(e => e.CategoryExternalId)
                    .HasMaxLength(255)
                    .HasColumnName("categoryid__r__external_id__c");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createddate");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(255)
                    .HasColumnName("external_id__c");

               
                entity.Property(e => e.IsDeleted).HasColumnName("isdeleted");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("lastmodifieddate");

                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .HasColumnName("name");

                entity.Property(e => e.SfId)
                    .HasMaxLength(18)
                    .HasColumnName("sfid")
                    .UseCollation("ucs_basic");
                entity.Property(e => e.Price)
                    .HasColumnName("price__c")
                    .HasColumnType("double")
                    .HasMaxLength(18);

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryExternalId)
                    .HasPrincipalKey(p => p.ExternalId);
                // entity.Navigation("Category");

            });
            
        }
    }
}
