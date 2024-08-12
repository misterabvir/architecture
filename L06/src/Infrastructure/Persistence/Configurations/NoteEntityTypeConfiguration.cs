using Domain.Notes;
using Domain.Notes.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class NoteEntityTypeConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable("notes");

        builder.HasKey(e => e.Id).HasName("pk_note");

        builder
            .Property(e => e.Id)
            .HasColumnName("note_id")
            .ValueGeneratedNever();

        builder.Property(e => e.Title)
            .HasColumnName("title")
            .HasMaxLength(Title.MaxLength)
            .HasConversion(title => title.Value, value => Title.Create(value));

        builder
            .Property(e => e.Content)
            .HasColumnName("content")
            .HasMaxLength(Content.MaxLength)
            .HasConversion(content => content.Value, value => Content.Create(value));

        builder
            .Property(e => e.CreatedAt)
            .HasColumnName("created_at");

        builder
            .Property(e => e.UpdatedAt)
            .HasColumnName("updated_at");
    }
}