namespace MuTube.Data.EntityConfig
{
    using MeTube.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TubeConfig : IEntityTypeConfiguration<Tube>
    {
        public void Configure(EntityTypeBuilder<Tube> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.YoutubeId)
                .IsUnique();
        }
    }
}
