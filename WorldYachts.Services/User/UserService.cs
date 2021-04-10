using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorldYachts.Data;
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
        private readonly WorldYachtsDbContext _dbContext;
        #endregion

        public UserService(WorldYachtsDbContext dbContext,
            IConfiguration configuration, 
            IMapper mapper)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null)
            {
                // todo: need to add logger
                return null;
            }

            var token = _configuration.GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Data.Entities.User> Add(Data.Entities.User user)
        {
            var addedUser = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return addedUser.Entity;
        }

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Data.Entities.User> GetAll()
        {
            return _dbContext.Users;
        }

        /// <summary>
        /// Получение пользователя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Data.Entities.User GetById(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<bool> IsIdenticalEntity(Data.Entities.User user)
        {
            if (await _dbContext.Users.AnyAsync(
                c => c.Email.ToLower() == user.Email.ToLower()
                     || c.Username.ToLower() == user.Username.ToLower()))
            {
                return true;
            }

            return false;
        }
    }
}
