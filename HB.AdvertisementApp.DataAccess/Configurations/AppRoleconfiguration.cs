using HB.AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HB.AdvertisementApp.DataAccess.Configurations
{
    public class AppRoleconfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.Property(x => x.Definition).HasMaxLength(300).IsRequired();

            builder.HasData(new AppRole[]
            {
                new()
                {
                    Id= 1,
                    Definition="Admin"
                },
                new()
                {
                    Id= 2,
                    Definition="Member"

                }
            });

        }
    }

}
