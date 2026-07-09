using HotelReservationAPI.Models;
using HotelReservationAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer(Customer customer)
        {
            // Basit E-posta Validasyonu
            if (string.IsNullOrWhiteSpace(customer.Email) || !customer.Email.Contains("@"))
            {
                return BadRequest("Geçersiz e-posta formatı. Lütfen içinde '@' işareti olan geçerli bir adres giriniz.");
            }

            await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.Id) return BadRequest();

            // Güncelleme işleminde de aynı validasyon
            if (string.IsNullOrWhiteSpace(customer.Email) || !customer.Email.Contains("@"))
            {
                return BadRequest("Geçersiz e-posta formatı.");
            }

            await _customerService.UpdateCustomerAsync(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }

}
