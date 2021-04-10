using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Services.Models;
using WorldYachts.Services.Models.Authenticate;
using WorldYachts.Services.User;

namespace WorldYachts.Services.SalesPerson
{
    public class SalesPersonService:ISalesPersonService
    {
        private readonly WorldYachtsDbContext _dbContext;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public SalesPersonService(WorldYachtsDbContext dbContext, IUserService userService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<Data.Entities.SalesPerson> Add(Data.Entities.SalesPerson salesPerson)
        {
            var addedSalesPerson = await _dbContext.SalesPersons.AddAsync(salesPerson);
            await _dbContext.SaveChangesAsync();
            
            return addedSalesPerson.Entity;
        }

        public IEnumerable<Data.Entities.SalesPerson> GetAll()
        {
            return _dbContext.SalesPersons;
        }

        public async Task<Data.Entities.SalesPerson> GetById(int id)
        {
            return await _dbContext.SalesPersons.FindAsync(id);
        }

        public async Task<AuthenticateResponse> Register(SalesPersonModel salesPersonModel)
        {
            var salesPerson = _mapper.Map< Data.Entities.SalesPerson >(salesPersonModel);
            var user = _mapper.Map< Data.Entities.User >(salesPersonModel);
            if (await IsIdenticalEntity(salesPerson)
                || await _userService.IsIdenticalEntity(user))
            {
                return null;
            }
            var addedSalesPerson = await Add(salesPerson);

            user.UserId = addedSalesPerson.Id;
            var addedUser = await _userService.Add(user);

            var response = _userService.Authenticate(new AuthenticateRequest
            {
                Username = addedUser.Username,
                Password = addedUser.Password
            });

            return response;
        }

        public async Task<bool> IsIdenticalEntity(Data.Entities.SalesPerson salesPerson)
        {
            if (await _dbContext.SalesPersons.AnyAsync(sp => sp.Email.ToLower() == salesPerson.Email.ToLower()))
            {
                return true;
            }

            return false;
        }
    }
}
