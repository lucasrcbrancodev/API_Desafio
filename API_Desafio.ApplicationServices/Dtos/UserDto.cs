using API_Desafio.Domain.Common;
using API_Desafio.Domain.Models;

namespace API_Desafio.ApplicationServices.Dtos;

public class UserDto : BaseDto
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateOfBirthInformation DateOfBirthInformation { get; set; } = null!;
    public ImageInformation ImageInformation { get; set; } = null!;

    public static UserDto CreateFromUser(User user)
    {
        var name = user.NameInformation.First + user.NameInformation.Last;

        var dateOfBirthInformation = new DateOfBirthInformation(
            user.DateOfBirthInformation.DateOfBirth,
            user.DateOfBirthInformation.Age);

        var imageInformation = new ImageInformation(user.ImageInformation.Thumbnail);

        return new UserDto
        {
            Id = user.Id,
            Username = user.LoginInformation.Username,
            Email = user.ContactInformation.Email,
            Name = name,
            DateOfBirthInformation = dateOfBirthInformation,
            ImageInformation = imageInformation
        };
    }
}