using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using OnlineShop.Models.Domain;
using OnlineShop.Repositories;
using OnlineShop.Services.ViewMoldels;
using System.Linq;

namespace OnlineShop.Services
{
    public class AddressService : IAddressService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AddressService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(AddressViewModel addressViewModel, long userId)
        {
            var address = _mapper.Map<Address>(addressViewModel);
            address.UserId = userId;
            _repository.Add(address);
            _repository.Save();
        }

        public IEnumerable<AddressViewModel> GetAddresses(long id)
        {
            var addresses = _repository.GetAll<Address>().Where(x => x.UserId == id);

            return addresses.Select(x => new AddressViewModel
            {
                Id = x.Id,
                Country = x.Country,
                City = x.City,
                Street = x.Street,
                PostalCode = x.PostalCode
            }).ToList(); 
            
        }
    }
}
