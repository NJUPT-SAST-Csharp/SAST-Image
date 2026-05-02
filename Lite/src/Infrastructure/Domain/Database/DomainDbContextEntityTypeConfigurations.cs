using Domain.AlbumAggregate.AlbumEntity;
using Domain.AlbumAggregate.ImageEntity;
using Domain.CategoryAggregate.CategoryEntity;
using Domain.Database.Extensions;
using Domain.UserAggregate.IdentityEntity;
using Domain.UserAggregate.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Database;

internal sealed class DomainDbContextEntityTypeConfigurations
    : IEntityTypeConfiguration<Album>,
        IEntityTypeConfiguration<User>,
        IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.ToTable("albums");
        builder.Ignore(album => album.DomainEvents);

        builder.HasKey(x => x.Id);

        builder.Field<AccessLevel>("_accessLevel", "access_level");
        builder.Field<bool>("_removed", "removed");
        builder.Field<bool>("_updateCoverAutomatically", "update_cover_automatically");
        builder.Field<UserId>("_author", "author_id");
        builder.HasOne<User>().WithMany().HasForeignKey("_author");

        builder.OwnsMany<Subscribe>(
            "_subscribes",
            entity =>
            {
                entity.HasOne<User>().WithMany().HasForeignKey(e => e.User);
                entity.HasOne<Album>().WithMany().HasForeignKey(e => e.Album);

                entity.Property(e => e.User);
                entity.Property(e => e.Album);

                entity.HasKey(x => new { x.User, x.Album });
                entity.WithOwner().HasForeignKey(e => e.Album);

                entity.ToTable("subscribes");
            }
        );

        builder.OwnsMany<Image>(
            "_images",
            image =>
            {
                image.HasKey(x => x.Id);

                image.HasOne<User>().WithMany().HasForeignKey("_uploader");

                image.Field<UserId>("_uploader", "uploader_id");

                image.OwnsOne<ImageStatus>(
                    "_status",
                    image =>
                    {
                        image.Property(s => s.Value);
                        image.Property(s => s.RemovedAt);
                    }
                );

                image.ToTable("images");
                image.WithOwner().HasForeignKey("album_id");

                image.OwnsMany<Like>(
                    "_likes",
                    entity =>
                    {
                        entity.HasKey(x => new { x.User, x.Image });

                        entity.HasOne<User>().WithMany().HasForeignKey(like => like.User);
                        entity.HasOne<Image>().WithMany().HasForeignKey(like => like.Image);

                        entity.WithOwner().HasForeignKey(like => like.Image);
                        entity.ToTable("likes");
                    }
                );
            }
        );
    }

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);

        builder.Ignore(x => x.DomainEvents);

        builder.HasIndex("_username").IsUnique();
        builder.Field<Username>("_username", "username");
        builder.Field<Email>("_email", "email");
        builder.Field<RefreshToken>("_refreshToken", "refresh_token");
        builder.Field<Roles>("_roles", "roles");

        builder.ComplexProperty<Password>(
            "_password",
            password =>
            {
                password.Property(p => p.Hash).HasColumnName("password_hash").IsRequired();
                password.Property(p => p.Salt).HasColumnName("password_salt").IsRequired();
            }
        );

        builder.OwnsMany<Identity>(
            "_identities",
            entity =>
            {
                entity.ToTable("identities");
                entity.WithOwner().HasForeignKey("user_id");
                entity.HasKey(x => x.Id);
                entity.Field<IdentityProvider>("_provider", "provider");
                entity.Field<IdentityId>("_providerUserId", "provider_user_id");
            }
        );
    }

    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");
        builder.HasKey(x => x.Id);
        builder.Ignore(x => x.DomainEvents);

        builder.Field<CategoryName>("_name", "name");
    }
}
