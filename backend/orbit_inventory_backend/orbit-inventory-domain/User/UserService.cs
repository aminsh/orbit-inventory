using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using orbit_inventory_domain.Core;
using orbit_inventory_domain.User;

namespace orbit_inventory_domain.user;

public class UserService(
    IRepository<User> userRepository
)
{
    public Task Create(UserDto dto)
    {
        var salt = GenerateSalt();
        
        var user = new User
        {
            Email = dto.Email,
            Name = dto.Name,
            Salt = Convert.ToBase64String(salt),
            Password = GetHashPassword(dto.Password, salt)
        };

        userRepository.Add(user);
        return Task.FromResult(user.Id);
    }

    public async Task<User> Signin(UserSigninDto dto)
    {
        var entity = await userRepository.FindOne(u => u.Email.ToLower() == dto.Email.ToLower());
        
        if (entity == null)
            throw new UnauthorizedAccessException();
        
        var hashed = GetHashPassword(dto.Password, Convert.FromBase64String(entity.Salt));

        if (entity.Password != hashed)
            throw new UnauthorizedAccessException();

        return entity;
    }

    public Task<User?> GetUser(int id)
    {
        return userRepository.FindById(id);
    }

    private static string GetHashPassword(string password, byte[] salt)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
    }

    private static byte[] GenerateSalt()
    {
        return RandomNumberGenerator.GetBytes(128 / 8);
    }
}