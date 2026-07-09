

using HotelReservationAPI.Models;
using HotelReservationAPI.Repository;

namespace HotelReservationAPI.Service
{
    public class CustomerService : ICustomerService
    {

        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteAsync(id);
        }

    }
}
