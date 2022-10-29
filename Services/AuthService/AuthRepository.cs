using dotnet_rpg.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.AuthService
{
  public class AuthRepository : IAuthRepository
  {
    private readonly DataContext _context;
    private readonly string BadCredentials = "The username and password combination is invalid.";

    public AuthRepository(DataContext context)
    {
      _context = context;
    }


    public async Task<string> Login(string username, string password)
    {
      var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

      if (user == null) { return BadCredentials; }

      return VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt) ?
        $"User {user.Id} is authenticated." : BadCredentials;
    }

    public async Task<int> Register(User user, string password)
    {
      if (await UserExists(user.Username)) { return 0; }
      CreatedPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      _context.Users.Add(user);
      await _context.SaveChangesAsync();
      return user.Id;
    }

    public async Task<bool> UserExists(string username)
    {
      return await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower());
    }

    private void CreatedPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
      {
        var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computeHash.SequenceEqual(passwordHash);
      }
    }
  }
}
