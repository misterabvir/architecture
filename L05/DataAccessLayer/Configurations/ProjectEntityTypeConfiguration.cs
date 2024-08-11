using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations;

internal class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");
        builder.HasKey(e => e.Id).HasName("pk_project");
        builder.Property(e => e.Id).HasColumnName("project_id");

        builder.OwnsMany(e => e.Settings, SettingConfigure);
        builder.OwnsMany(e => e.Models, ModelConfigure);

        builder.HasData([
                new Project() { Id = 1, Name = "Project Alpha"},
                new Project() { Id = 2, Name = "Project Beta"},
                new Project() { Id = 3, Name = "Project Delta"},
            ]);
    }

    private void SettingConfigure(OwnedNavigationBuilder<Project, Setting> builder)
    {
        builder.ToTable("settings");
        builder.WithOwner().HasForeignKey(e => e.ProjectId);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("setting_id");

        builder.HasData([
                new Setting() { Id = 1, ProjectId = 1, Parameter = "Resolution", Value = "FullHd" },
                new Setting() { Id = 2, ProjectId = 1, Parameter = "Environment", Value = "DevelopMode" },
                new Setting() { Id = 3, ProjectId = 2, Parameter = "Resolution", Value = "2K" },
                new Setting() { Id = 4, ProjectId = 3, Parameter = "Resolution", Value = "1080" },
                new Setting() { Id = 5, ProjectId = 3, Parameter = "Environment", Value = "Production" }
            ]);
    }

    private void ModelConfigure(OwnedNavigationBuilder<Project, Model> builder)
    {
        builder.ToTable("models");
        builder.WithOwner().HasForeignKey(e => e.ProjectId);
        builder.HasKey(e => e.Id);
        builder.OwnsMany(e => e.Textures, TextureConfigure);
        builder.Property(e => e.Id).HasColumnName("model_id");

        builder.HasData([
                new Model(){ Id = 1, ProjectId = 1, Name = "Box" },
                new Model(){ Id = 2, ProjectId = 1, Name = "Sphere" },
                new Model(){ Id = 3, ProjectId = 1, Name = "Cylinder" },
                new Model(){ Id = 4, ProjectId = 2, Name = "Cylinder1" },
                new Model(){ Id = 5, ProjectId = 2, Name = "Cylinder2" },
                new Model(){ Id = 6, ProjectId = 2, Name = "Sphere" },
                new Model(){ Id = 7, ProjectId = 3, Name = "Box1" },
                new Model(){ Id = 8, ProjectId = 3, Name = "Box2" },
            ]);
    }

    private void TextureConfigure(OwnedNavigationBuilder<Model, Texture> builder)
    {
        builder.ToTable("textures");
        builder.WithOwner().HasForeignKey(e => new { e.ModelId });
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("texture_id");
        builder.HasData([
                new Texture() {Id = 1, ModelId = 1, Pattern = "Solid", Color = "Black"},
                new Texture() {Id = 2, ModelId = 1, Pattern = "Stripes", Color = "Red"},
                new Texture() {Id = 3, ModelId = 2, Pattern = "Solid", Color = "White"},
                new Texture() {Id = 4, ModelId = 3, Pattern = "Solid", Color = "Blue"},
                new Texture() {Id = 5, ModelId = 4, Pattern = "Solid", Color = "Yellow"},
                new Texture() {Id = 6, ModelId = 4, Pattern = "Stripes", Color = "Green"},
                new Texture() {Id = 7, ModelId = 5, Pattern = "Solid", Color = "Black"},
                new Texture() {Id = 8, ModelId = 6, Pattern = "Solid", Color = "White"},
                new Texture() {Id = 9, ModelId = 7, Pattern = "Solid", Color = "Red"},
                new Texture() {Id = 10, ModelId = 8, Pattern = "Solid", Color = "Blue"}
            ]);
    }
}
