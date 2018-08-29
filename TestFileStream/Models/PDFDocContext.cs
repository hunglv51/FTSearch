using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestFileStream.Models
{
    public partial class PDFDocContext : DbContext
    {
        public PDFDocContext()
        {
        }

        

        public PDFDocContext(DbContextOptions<PDFDocContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<TestDocument> TestDocument { get; set; }
        public virtual DbSet<WordDocument> WordDocument { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-QH3V092\\SQLEXPRESS;Initial Catalog=PDFDoc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestDocument>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.FileType)
                    .IsRequired()
                    .HasColumnName("file_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<WordDocument>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.HasIndex(e => e.Guid)
                    .HasName("UQ__WordDocu__A2B5777DA3B7279F")
                    .IsUnique();

                entity.Property(e => e.Guid).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.FileType)
                    .IsRequired()
                    .HasColumnName("file_type")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }
    }
}
