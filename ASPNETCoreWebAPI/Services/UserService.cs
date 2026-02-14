using ASPNETCoreWebAPI.Data;
using ASPNETCoreWebAPI.Data.Repository;
using ASPNETCoreWebAPI.Model;
using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace ASPNETCoreWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ICollegeRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(ICollegeRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateUserAsync(UserDTO dto)
        {
            //old way
            //if (dto == null)
            //    throw new ArgumentNullException(nameof(dto));

            //new way
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            var existingUser = await _userRepository.GetAsync(u => u.Username.Equals(dto.Username));
            if (existingUser != null)
            {
                throw new Exception("The username already taken");
            }

            User user = _mapper.Map<User>(dto);
            user.IsDeleted = false;
            user.CreatedDate = DateTime.Now;
            user.ModifiedDate = DateTime.Now;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                var passwordHash = CreatePasswordHashWithSalt(dto.Password);
                user.Password = passwordHash.PasswordHash;
                user.PasswordSalt = passwordHash.Salt;
            }

            await _userRepository.CreateAsync(user);
            return true;
        }

        public async Task<List<UserReadonlyDTO>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllByFilterAsync(u => !u.IsDeleted);

            return _mapper.Map<List<UserReadonlyDTO>>(users);
        }

        public async Task<UserReadonlyDTO> GetUserById(int id)
        {
            var user =await _userRepository.GetAsync(u => !u.IsDeleted && u.Id == id);

            return _mapper.Map<UserReadonlyDTO>(user);
        }

        public async Task<UserReadonlyDTO> GetUserByName(string username)
        {
            var user = await _userRepository.GetAsync(u => !u.IsDeleted && u.Username.Equals(username));

            return _mapper.Map<UserReadonlyDTO>(user);
        }

        public async Task<bool> UpdateUserAsync(UserDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            var existingUser =await _userRepository.GetAsync(u => !u.IsDeleted && u.Id == dto.Id, true);
            if(existingUser == null)
            {
                throw new Exception($"User not found with the id: {dto.Id}");
            }

            var userToUpdate = _mapper.Map<User>(dto);
            userToUpdate.ModifiedDate = DateTime.Now;

            //1. we will update only user information
            //2. we need to provide separate method to update the password
            //for the demo purpose i am updating the password

            if (!string.IsNullOrEmpty(dto.Password))
            {
                var passwordHash = CreatePasswordHashWithSalt(dto.Password);
                userToUpdate.Password = passwordHash.PasswordHash;
                userToUpdate.PasswordSalt = passwordHash.Salt;
            }

            await _userRepository.UpdateAsync(userToUpdate);

            return true;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException(nameof(userId));

            var existingUser = await _userRepository.GetAsync(u => !u.IsDeleted && u.Id == userId, true);
            if (existingUser == null)
            {
                throw new Exception($"User not found with the id: {userId}");
            }

            //1. Hard delete - you can try this
            //2. Soft delete - we will do this now
            existingUser.IsDeleted = true;

            await _userRepository.UpdateAsync(existingUser);
            return true;
        }

        public (string PasswordHash, string Salt) CreatePasswordHashWithSalt(string password)
        {
            //Create a Salt
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            //Create PasswordHash
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt = salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                ));


            return (hash, Convert.ToBase64String(salt));
        }
    }
}
