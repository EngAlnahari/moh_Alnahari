using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Test.Service
{
    public class TokenCreate : ITokenCreate
    {




        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _secrtkey;
        public TokenCreate(IConfiguration configuration)
        {
            _issuer = configuration["JWT:Issuer"];
            _audience = configuration["Jwt:Audience"];
            _secrtkey = configuration["Jwt:SecretKey"];
        }

        public string GetToken(string Token, string role)
        {
            var tokenhandelor = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secrtkey);
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, Token), 
                    new Claim(ClaimTypes.Role, role) 
                }),
                Issuer = _issuer,
                Audience = _audience,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tok = tokenhandelor.CreateToken(tokenDesc);
            return
             tokenhandelor.WriteToken(tok);
        }

    }
}

//var key = Encoding.ASCII.GetBytes(_secrtkey);
//var tokendes = new SecurityTokenDescriptor
//{
//    //Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, Token) }),

//    Issuer = _issuer,
//    Audience = _audience,
//    Expires = DateTime.UtcNow.AddHours(1),
//    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

//};
//var tokebn = tokenhandelor.CreateToken(tokendes);
//return tokenhandelor.WriteToken(tokebn);