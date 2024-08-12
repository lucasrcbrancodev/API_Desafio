namespace API_Desafio.ApplicationServices.Dtos;
public class RandomUserGeneratorApiResponseDto
{
    public List<RandomUser> results { get; set; } = new();
}

public class RandomUser
{
    public Id id { get; set; } = null!;
    public Name name { get; set; } = null!;
    public Login login { get; set; } = null!;
    public Location location { get; set; } = null!;
    public Registered registered { get; set; } = null!;
    public Dob dob { get; set; } = null!;
    public Picture picture { get; set; } = null!;

    public string gender { get; set; } = null!;
    public string email { get; set; } = null!;
    public string phone { get; set; } = null!;
    public string cell { get; set; } = null!;
    public string nat { get; set; } = null!;
}

public class Name
{
    public string title { get; set; } = null!;
    public string first { get; set; } = null!;
    public string last { get; set; } = null!;
}

public class Location
{
    public Street street { get; set; } = null!;
    public string city { get; set; } = null!;
    public string state { get; set; } = null!;
    public string country { get; set; } = null!;
    public int postcode { get; set; }
}

public class Street
{
    public int number { get; set; }
    public string name { get; set; } = null!;
}

public class Login
{
    public string uuid { get; set; } = null!;
    public string username { get; set; } = null!;
    public string password { get; set; } = null!;
    public string salt { get; set; } = null!;
    public string sha256 { get; set; } = null!;
}

public class Dob
{
    public DateTime date { get; set; }
    public int age { get; set; }
}

public class Registered
{
    public DateTime date { get; set; }
    public int age { get; set; }
}

public class Id
{
    public string name { get; set; } = null!;
    public string value { get; set; } = null!;
}

public class Picture
{
    public string large { get; set; } = null!;
    public string medium { get; set; } = null!;
    public string thumbnail { get; set; } = null!;
}