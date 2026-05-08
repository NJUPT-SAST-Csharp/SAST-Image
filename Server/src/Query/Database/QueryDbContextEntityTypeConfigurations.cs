using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Query.Albums;
using Query.Categories;
using Query.Images;
using Query.Users;

namespace Query.Database;

internal sealed class QueryDbContextEntityTypeConfigurations
    : IEntityTypeConfiguration<AlbumModel>,
        IEntityTypeConfiguration<ImageModel>,
        IEntityTypeConfiguration<UserModel>,
        IEntityTypeConfiguration<CategoryModel>
{
    public void Configure(EntityTypeBuilder<AlbumModel> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(album => album.CreatedAt);
        builder.PrimitiveCollection(album => album.Tags);
        builder.PrimitiveCollection(album => album.Subscribes);
        builder.HasOne<CategoryModel>().WithMany().HasForeignKey(album => album.CategoryId);
        builder.HasOne<UserModel>().WithMany().HasForeignKey(album => album.AuthorId);
        builder.HasMany(album => album.Images).WithOne().HasForeignKey(image => image.AlbumId);

        builder.HasIndex(album => album.Title).IsUnique(true);
    }

    public void Configure(EntityTypeBuilder<ImageModel> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(image => image.Title);
        builder.Property(image => image.UploadedAt);
        builder.Property(image => image.AuthorId);
        builder.PrimitiveCollection(image => image.Tags);
        builder.PrimitiveCollection(image => image.Likes);
        builder.HasOne<UserModel>().WithMany().HasForeignKey(image => image.AuthorId);
        builder.HasOne<UserModel>().WithMany().HasForeignKey(image => image.UploaderId);
    }

    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasIndex(user => user.Username).IsUnique(true);

        builder.Property(u => u.RegisteredAt);
    }

    public void Configure(EntityTypeBuilder<CategoryModel> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasIndex(c => c.Name).IsUnique(true);
    }
}
