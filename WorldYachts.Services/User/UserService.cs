using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorldYachts.Data;
using WorldYachts.Services.Authenticate;
using WorldYachts.Services.Helpers;
using WorldYachts.Services.Models.Authenticate;

namespace WorldYachts.Services.User
{
    public class UserService : IUserService
    {
        #region Поля
        private readonly IConfiguration _configuration;
        private readonly WorldYachtsDbContext _db;
        private readonly IMapper _mapper;
        #endregion

        public UserService(IConfiguration configuration, WorldYachtsDbContext dbContext, IMapper mapper)
        {
            _configuration = configuration;
            _db = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<AuthenticateResponse>> AuthenticateAsync(AuthenticateRequest model)
        {
            var user = GetAll().FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            
            return new ServiceResponse<AuthenticateResponse>
            {
                IsSuccess = user != null,
                Data = user != null ? new AuthenticateResponse(user, token: _configuration.GenerateJwtToken(user)) : null,
                Message = user != null ? $"Authenticate successful" : "Wrong username or password",
                Time = DateTime.UtcNow,
            };
        }

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<Data.Entities.User>> AddAsync(Data.Entities.User user)
        {
            var now = DateTime.UtcNow;
            try
            {
                if (await IsIdenticalEntityAsync(user))
                {
                    return new ServiceResponse<Data.Entities.User>()
                    {
                        IsSuccess = false,
                        Data = user,
                        Message = "User already exist.",
                        Time = now
                    };
                }

                var addedUser = (await _db.Users.AddAsync(user)).Entity;
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.User>()
                {
                    IsSuccess = true,
                    Data = addedUser,
                    Message = $"User (id:{addedUser.Id} Username:{addedUser.Username} added",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.User>()
                {
                    IsSuccess = false,
                    Data = user,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
            
        }

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Data.Entities.User> GetAll()
        {
            return _db.Users;
        }

        /// <summary>
        /// Получение пользователя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<Data.Entities.User>> GetByIdAsync(int id)
        {
            var user = await _db.Users.FindAsync(id);

            return new ServiceResponse<Data.Entities.User>()
            {
                IsSuccess = user != null,
                Data = user,
                Message = user != null ? $"Got user by id: {id}" : "User not found",
                Time = DateTime.UtcNow,
            };
        }

        public async Task<ServiceResponse<Data.Entities.User>> UpdateAsync(int id, Data.Entities.User user)
        {
            var now = DateTime.UtcNow;
            try
            {
                if (await IsIdenticalEntityAsync(user))
                {
                    return new ServiceResponse<Data.Entities.User>()
                    {
                        IsSuccess = false,
                        Data = user,
                        Message = $"User already exist.",
                        Time = now
                    };
                }

                var updatedUser = await _db.Users.FindAsync(id);
                updatedUser = _mapper.Map(user, updatedUser);
                _db.Users.Update(updatedUser);
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.User>()
                {
                    IsSuccess = true,
                    Data = updatedUser,
                    Message = $"User (id:{updatedUser.Id} Username:{updatedUser.Username}) updated.",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.User>()
                {
                    IsSuccess = false,
                    Data = user,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
            
        }

        public async Task<ServiceResponse<Data.Entities.User>> DeleteAsync(int id)
        {
            var now = DateTime.UtcNow;
            try
            {
                var user = await _db.Users.FindAsync(id);
                var deletedUser = _db.Users.Remove(user).Entity;
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.User>()
                {
                    IsSuccess = deletedUser != null,
                    Data = deletedUser,
                    Message = deletedUser != null ? $"User (id:{id}) deleted" : "User not found",
                    Time = now,
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.User>()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
            
        }

        public async Task<bool> IsIdenticalEntityAsync(Data.Entities.User user)
        {
            if (await _db.Users.FirstOrDefaultAsync(
                u => (u.Email.ToLower() == user.Email.ToLower()
                     || u.Username.ToLower() == user.Username.ToLower()) 
                     && u.Id != user.Id) != null)
            {
                return true;
            }

            return false;
        }
    }
}
