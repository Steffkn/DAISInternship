using MeTube.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MuTube.Data.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Username)
                .IsUnique();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.HasMany(x => x.Tubes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
