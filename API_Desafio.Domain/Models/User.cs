using API_Desafio.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Desafio.Domain.Models;

public sealed class User : BaseEntity
{
    public DocumentInformation DocumentInformation { get; set; } = null!;
    public NameInformation NameInformation { get; set; } = null!;
    public LoginInformation LoginInformation { get; set; } = null!;
    public LocationInformation LocationInformation { get; set; } = null!;
    public DateOfBirthInformation DateOfBirthInformation { get; set; } = null!;
    public ImageInformation ImageInformation { get; set; } = null!;
    public ContactInformation ContactInformation { get; set; } = null!;

    public string Gender { get; set; } = null!;
    public string Nat { get; set; } = null!;

    private User() : base()
    {

    }

    public User(
        DocumentInformation documentInformation,
        NameInformation nameInformation,
        LoginInformation loginInformation,
        LocationInformation locationInformation,
        DateOfBirthInformation dateOfBirthInformation,
        ImageInformation imageInformation,
        ContactInformation contactInformation,
        string gender,
        string nat) : base()
    {
        DocumentInformation = documentInformation;
        NameInformation = nameInformation;
        LoginInformation = loginInformation;
        LocationInformation = locationInformation;
        DateOfBirthInformation = dateOfBirthInformation;
        ImageInformation = imageInformation;
        ContactInformation = contactInformation;
        Gender = gender;
        Nat = nat;
    }

    public void Update(
        string username,
        string documentNumber,
        DateTime dateOfBirth,
        string firstName,
        string lastName,
        string thumbnail)
    {
        LoginInformation = new LoginInformation(
            username,
            LoginInformation.Password,
            LoginInformation.Salt,
            LoginInformation.SHA256);

        DateOfBirthInformation = new DateOfBirthInformation(
            dateOfBirth,
            age: DateTime.Now.Year - dateOfBirth.Year);

        NameInformation = new NameInformation(
            firstName,
            lastName);

        ImageInformation = new ImageInformation(thumbnail);
    }
}

[ComplexType]
public sealed record ContactInformation(string Email, string Phone, string Cell);

[ComplexType]
public sealed record NameInformation(string First, string Last);

[ComplexType]
public sealed record LocationInformation(int Number, string Name, string City, string State, string Country, int Postcode);

[ComplexType]
public sealed record LoginInformation(string Username, string Password, string Salt, string SHA256);

[ComplexType]
public class DateOfBirthInformation
{
    public DateOfBirthInformation()
    {

    }

    public DateOfBirthInformation(DateTime dateOfBirth, int age)
    {
        DateOfBirth = dateOfBirth;
        Age = age;
    }

    [Column(TypeName = "date")]
    public DateTime DateOfBirth { get; set; }

    public int Age { get; set; }
}

[ComplexType]
public sealed record DocumentInformation(string Type, string Number);

[ComplexType]
public sealed record ImageInformation(string Thumbnail);