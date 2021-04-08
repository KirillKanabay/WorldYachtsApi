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
using WorldYachtsApi.Models.Authenticate;

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
        public async Task<Data.Models.SalesPerson> Add(Data.Models.SalesPerson salesPerson)
        {
            var addedSalesPerson = await _dbContext.SalesPersons.AddAsync(salesPerson);
            await _dbContext.SaveChangesAsync();
            
            return addedSalesPerson.Entity;
        }

        public IEnumerable<Data.Models.SalesPerson> GetAll()
        {
            return _dbContext.SalesPersons;
        }

        public async Task<Data.Models.SalesPerson> GetById(int id)
        {
            return await _dbContext.SalesPersons.FirstOrDefaultAsync(sp => sp.Id == id);
        }

        public async Task<AuthenticateResponse> Register(SalesPersonModel salesPersonModel)
        {
            var salesPerson = _mapper.Map<Data.Models.SalesPerson>(salesPersonModel);

            var addedSalesPerson = await Add(salesPerson);

            salesPersonModel.Id = addedSalesPerson.Id;

            var user = _mapper.Map<Data.Models.User>(salesPersonModel);
            var addedUser = await _userService.Add(user);

            var response = _userService.Authenticate(new AuthenticateRequest
            {
                Username = user.Username,
                Password = user.Password
            });

            return response;
        }
    }
}
