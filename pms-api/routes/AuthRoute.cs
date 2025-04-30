using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;

namespace Route {

    public class AuthRoute {

        private static string JWT_SECRET_KEY = "";

        public static void Map(WebApplication app) {

            var auth = app.MapGroup("/api/v1/auth");

            JWT_SECRET_KEY = app.Configuration["Authentication:JwtSecretKey"] ?? "";

            auth.MapPost("/login", Login);
            auth.MapPost("/signup", SignUp);
        }

        private static async Task<IResult> SignUp(User input, ProductDb db) {

            var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(input.Password, HashType.SHA512);

            User user = new() {
                Name = input.Name,
                Username = input.Username,
                Password = passwordHash,
                Role = input.Role
            };

            var existingUser = await db.Users.FirstOrDefaultAsync(u => u.Username == input.Username);

            if (existingUser is not null) {

                return TypedResults.BadRequest(new {
                    message = "Username already exists. Please consider other username."
                });
            }

            await db.Users.AddAsync(user);

            await db.SaveChangesAsync();

            return TypedResults.Ok(new {
               input.Name,
               message = "User successfully created!"
            });
        }

        private static async Task<IResult> Login(User input, ProductDb db) {

           var users = await db.Users.Select(user => 
            new User() {
                Id = user.Id, 
                Username = user.Username, 
                Password = user.Password,
                Role = user.Role,
            })
            .Where(user => user.Username == input.Username)
            .ToListAsync();

            if (users.Count == 0) {
                return TypedResults.NotFound(new {
                    message = "User does not exists"
                }); 
            }
            
            var user = users[0];

            var isPasswordMatch = BCrypt.Net.BCrypt.EnhancedVerify(input.Password, user.Password, HashType.SHA512);

            if (!isPasswordMatch) 
                return TypedResults.Unauthorized();

            var token = GenerateJwtToken([
                new ("role", users[0].Role),
                new ("scope", users[0].Role == "admin" ? "admin_scope" : "user_scope")
            ]);

            return TypedResults.Ok(new {
                token
            });
        }

        private static string GenerateJwtToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_SECRET_KEY));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      
            var token = new JwtSecurityToken(
                issuer: "yanyan-pms",
                audience: "pms",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}