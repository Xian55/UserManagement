using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using UserManagement.Domain.Core;

namespace UserManagement.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id).ValueGeneratedOnAdd();

        builder.Property(user => user.Name).IsRequired();
        builder.Property(user => user.Username).IsRequired();
        builder.Property(user => user.Email).IsRequired();
        builder.Property(user => user.Phone).IsRequired();
        builder.Property(user => user.Website).IsRequired();

        builder.OwnsOne(user => user.Address, address =>
        {
            address.Property(a => a.Street).IsRequired();
            address.Property(a => a.Suite).IsRequired();
            address.Property(a => a.City).IsRequired();
            address.Property(a => a.Zipcode).IsRequired();

            address.OwnsOne(a => a.Geo, geo =>
            {
                geo.Property(g => g.Lat).IsRequired();
                geo.Property(g => g.Lng).IsRequired();
            });
        });

        builder.OwnsOne(user => user.Company, company =>
        {
            company.Property(c => c.Name).IsRequired();
            company.Property(c => c.CatchPhrase).IsRequired();
            company.Property(c => c.Bs).IsRequired();
        });
    }
}
