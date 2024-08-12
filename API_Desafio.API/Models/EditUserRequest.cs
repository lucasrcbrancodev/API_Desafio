using API_Desafio.Domain.Models;

namespace API_Desafio.API.Models;

public class EditUserRequest
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string DocumentNumber { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string First { get; set; } = null!;
    public string Last { get; set; } = null!;
    public string Thumbnail { get; set; } = null!;

    public static EditUserRequest CreateFromUser(User user)
    {
        return new EditUserRequest
        {
            Id = user.Id,
            Username = user.LoginInformation.Username,
            DocumentNumber = user.DocumentInformation.Number,
            DateOfBirth = user.DateOfBirthInformation.DateOfBirth,
            First = user.NameInformation.First,
            Last = user.NameInformation.Last,
            Thumbnail = user.ImageInformation.Thumbnail
        };
    }
}
