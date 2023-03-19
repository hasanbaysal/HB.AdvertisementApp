using HB.AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HB.AdvertisementApp.DataAccess.Configurations
{
    public class ProvidedServiceconfiguration : IEntityTypeConfiguration<ProvidedService>
    {
        public void Configure(EntityTypeBuilder<ProvidedService> builder)
        {
            builder.Property(x=>x.Title).HasMaxLength(200).HasColumnType("ntext").IsRequired();
            builder.Property(x=>x.Description).HasMaxLength(500).IsRequired();
            builder.Property(x=>x.ImagePath).HasMaxLength(500).IsRequired();

        }
    }

}
