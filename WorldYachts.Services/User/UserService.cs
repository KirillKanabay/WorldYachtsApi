using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WorldYachts.Data;
using WorldYachts.Services.Customer;
using WorldYachts.Services.Helpers;
using WorldYachts.Services.Models;
using WorldYachts.Services.Models.Authenticate;
using WorldYachts.Services.SalesPerson;
using WorldYachtsApi.Models.Authenticate;

namespace WorldYachts.Services.User
{
    public class UserService : IUserService
    {
        #region Поля
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly WorldYachtsDbContext _dbContext;
        private readonly ISalesPersonService _salesPersonService;
        private readonly ICustomerService _customerService;
        #endregion

        public UserService(WorldYachtsDbContext dbContext,
            IConfiguration configuration, 
            IMapper mapper, 
            ISalesPersonService salesPersonService,
            ICustomerService customerService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _dbContext = dbContext;
            _salesPersonService = salesPersonService;
            _customerService = customerService;
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

        #region Регистрации пользователей

        /// <summary>
        /// Регистрация покупателя
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        public async Task<AuthenticateResponse> Register(CustomerModel customerModel)
        {
            var customer = _mapper.Map<Data.Models.Customer>(customerModel);
            var addedCustomer = await _customerService.Add(customer);

            customerModel.Id = addedCustomer.Id;

            var user = _mapper.Map<Data.Models.User>(customerModel);
            var addedUser = await Add(user);


            var response = Authenticate(new AuthenticateRequest
            {
                Username = user.Username,
                Password = user.Password
            });

            return response;
        }

        /// <summary>
        /// Регистрация менеджера
        /// </summary>
        /// <param name="salesPersonModel"></param>
        /// <returns></returns>
        public async Task<AuthenticateResponse> Register(SalesPersonModel salesPersonModel)
        {
            var salesPerson = _mapper.Map<Data.Models.SalesPerson>(salesPersonModel);

            var addedSalesPerson = await _salesPersonService.Add(salesPerson);

            salesPersonModel.Id = addedSalesPerson.Id;

            var user = _mapper.Map<Data.Models.User>(salesPersonModel);
            var addedUser = await Add(user);

            var response = Authenticate(new AuthenticateRequest
            {
                Username = user.Username,
                Password = user.Password
            });

            return response;
        }

        #endregion
        
        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Data.Models.User> Add(Data.Models.User user)
        {
            var addedUser = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return addedUser.Entity;
        }

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Data.Models.User> GetAll()
        {
            return _dbContext.Users;
        }

        /// <summary>
        /// Получение пользователя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Data.Models.User GetById(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
