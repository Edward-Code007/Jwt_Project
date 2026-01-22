using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;

public class Program
{
    
    public static void Main(string[] args)
    {
     string[] tokensplit = GenerateToken().Split(".");
     Console.WriteLine("Header: " + tokensplit[0]);
     Console.WriteLine("PayLoad: " + tokensplit[1]);
     Console.WriteLine("Signature: " + tokensplit[2]);
    }

    public static string GenerateToken()
    {
           var secret = "e865bf21bbcbb1a383d0f85d006b7d97";
        var secret_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(secret_key, SecurityAlgorithms.HmacSha256);
        var descriptor = new SecurityTokenDescriptor
        {
          Subject = new ClaimsIdentity(
            [
             new Claim(JwtRegisteredClaimNames.Sub,"(E.g)User_Name"),
             new Claim(JwtRegisteredClaimNames.Email,"User's Email"),
            ]
          )  ,
          Expires = DateTime.UtcNow.AddHours(1),
          SigningCredentials = credentials,
          Issuer = "Your Comany",
          Audience = "Your Audience",
        };
        var tokenHandler = new JsonWebTokenHandler();
        string token = tokenHandler.CreateToken(descriptor);
        return token;
    }

}