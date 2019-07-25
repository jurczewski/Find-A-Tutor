using AutoMapper;
using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.DTO;
using Find_A_Tutor.Core.Repositories;
using NLog;
using System;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public class UserService : IUserService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }

        public async Task<Result<AccountDto>> GetAccountAsync(Guid userId)
        {
            logger.Info($"Fetching user with id: {userId}");
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                return Result<AccountDto>.Error($"User with id: '{userId}' does not exist.");
            }

            return Result<AccountDto>.Ok(_mapper.Map<AccountDto>(user));
        }

        public async Task<Result> RegisterAsync(Guid userId, string email, string firstName, string lastName, string password, string role = "student")
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                return Result.Error($"User with email: {email} already exists.");
            }
            user = new User(userId, role, firstName, lastName, email, password);
            await _userRepository.AddAsync(user);

            logger.Info($"User with id: '{userId}', was successfully created");
            return Result.Ok();
        }

        public async Task<Result<TokenDto>> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                return Result<TokenDto>.Error($"Invalid credentials");
            }

            if (user.Password != password)
            {
                return Result<TokenDto>.Error($"Invalid credentials");
            }

            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);

            logger.Info($"User with email: '{email}', was successfully logged in.");
            return Result<TokenDto>.Ok(
                new TokenDto
                {
                    Token = jwt.Token,
                    Expires = jwt.Expires,
                    Role = user.Role
                });
        }
    }
}
