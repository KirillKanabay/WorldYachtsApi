using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorldYachts.Data;
using WorldYachts.Services.Authenticate;
using WorldYachts.Services.Customer;
using WorldYachts.Services.Helpers;
using WorldYachts.Services.Models;
using WorldYachts.Services.Models.Authenticate;
using WorldYachts.Services.SalesPerson;

namespace WorldYachts.Services.User
{
    public class UserService : IUserService
    {
        #region Поля
        private readonly IConfiguration _configuration;
        private readonly IEfRepository<Data.Entities.User> _repository;
        #endregion

        public UserService(IConfiguration configuration, IEfRepository<Data.Entities.User> repository)
        {
            _configuration = configuration;
            _repository = repository;
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<AuthenticateResponse>> AuthenticateAsync(AuthenticateRequest model)
        {
            var user = await _repository.Find(x => x.Username == model.Username && x.Password == model.Password);
            
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

            var addedUser = await _repository.Add(user);

            return new ServiceResponse<Data.Entities.User>()
            {
                IsSuccess = true,
                Data = addedUser,
                Message = $"Customer (id:{addedUser.Id} Username:{addedUser.Username} added",
                Time = now
            };
        }

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Data.Entities.User> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Получение пользователя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<Data.Entities.User>> GetByIdAsync(int id)
        {
            var user = await _repository.GetById(id);

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

            var updatedUser = await _repository.Update(id, user);

            return new ServiceResponse<Data.Entities.User>()
            {
                IsSuccess = true,
                Data = updatedUser,
                Message = $"User (id:{updatedUser.Id} Username:{updatedUser.Username}) updated.",
                Time = now
            };
        }

        public async Task<ServiceResponse<Data.Entities.User>> DeleteAsync(int id)
        {
            var now = DateTime.UtcNow;
            var deletedUser = await _repository.Delete(id);

            return new ServiceResponse<Data.Entities.User>()
            {
                IsSuccess = deletedUser != null,
                Data = deletedUser,
                Message = deletedUser != null ? $"User (id:{id}) deleted" : "User not found",
                Time = now,
            };
        }

        public async Task<bool> IsIdenticalEntityAsync(Data.Entities.User user)
        {
            if (await _repository.Find(
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
