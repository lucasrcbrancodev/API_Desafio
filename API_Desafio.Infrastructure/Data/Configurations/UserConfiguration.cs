using API_Desafio.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Desafio.API.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)

    {
        builder.ToTable(nameof(User));
        builder.HasKey(b => b.Id);

        builder.ComplexProperty(e => e.DocumentInformation);
        builder.ComplexProperty(e => e.NameInformation);
        builder.ComplexProperty(e => e.LoginInformation);
        builder.ComplexProperty(e => e.DateOfBirthInformation);
        builder.ComplexProperty(e => e.ImageInformation);
        builder.ComplexProperty(e => e.ContactInformation);

        builder.ComplexProperty(e => e.LocationInformation);

        builder.Property(b => b.Gender).IsRequired();
        builder.Property(b => b.Nat).IsRequired();
    }
}
