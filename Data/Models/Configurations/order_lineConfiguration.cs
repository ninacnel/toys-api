﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Data.Models.Configurations
{
    public partial class order_lineConfiguration : IEntityTypeConfiguration<order_line>
    {
        public void Configure(EntityTypeBuilder<order_line> entity)
        {
            entity.HasKey(e => e.order_line_id)
                .HasName("PK__order_li__8F2B951F2B75390E");

            entity.Property(e => e.price).HasColumnType("decimal(10, 2)");

            entity.Property(e => e.sub_total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.order)
                .WithMany(p => p.order_line)
                .HasForeignKey(d => d.order_id)
                .HasConstraintName("FK__order_lin__order__48CFD27E");

            entity.HasOne(d => d.toy_codeNavigation)
                .WithMany(p => p.order_line)
                .HasForeignKey(d => d.toy_code)
                .HasConstraintName("FK__order_lin__toy_c__49C3F6B7");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<order_line> entity);
    }
}
