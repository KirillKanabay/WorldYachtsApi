using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Services.Models;
using WorldYachts.Services.Models.Authenticate;
using WorldYachts.Services.User;

namespace WorldYachts.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly WorldYachtsDbContext _dbContext;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CustomerService(WorldYachtsDbContext dbContext, IUserService userService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Data.Entities.Customer> Add(Data.Entities.Customer customer)
        {
            var addedCustomer = await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            return addedCustomer.Entity;
        }

        public IEnumerable<Data.Entities.Customer> GetAll()
        {
            return _dbContext.Customers;
        }

        public async Task<Data.Entities.Customer> GetById(int id)
        {
            return await _dbContext.Customers.FindAsync(id);
        }

        public async Task<AuthenticateResponse> Register(CustomerModel customerModel)
        {
            var customer = _mapper.Map<Data.Entities.Customer>(customerModel);
            var user = _mapper.Map<Data.Entities.User>(customerModel);

            if (await IsIdenticalEntity(customer) ||
                  await _userService.IsIdenticalEntity(user))
            {
                return null;
            }
            var addedCustomer = await Add(customer);
            
            user.UserId = addedCustomer.Id;
            var addedUser = await _userService.Add(user);

            var response = _userService.Authenticate(new AuthenticateRequest
            {
                Username = addedUser.Username,
                Password = addedUser.Password
            });

            return response;
        }

        public async Task<bool> IsIdenticalEntity(Data.Entities.Customer customer)
        {
            if (await _dbContext.Customers.AnyAsync(
                c => c.Email.ToLower() == customer.Email.ToLower()
                || c.IdNumber.ToLower() == customer.IdNumber.ToLower()))
            {
                return true;
            }

            return false;
        }
    }
}
