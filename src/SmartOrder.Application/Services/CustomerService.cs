using SmartOrder.Domain.Aggregates;
using SmartOrder.Domain.Repositories;


namespace SmartOrder.Application.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public async Task<Guid> CreateCustomerAsync(string name, string email)
        {
            var customer = Customer.Create(name, email);

            await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveChangesAsync();

            return customer.Id;
        }

        public async Task<Customer> GetEligibleCustomerAsync(Guid customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
                throw new InvalidOperationException("Customer not found");

            if (customer.IsBlocked)
                throw new InvalidOperationException("Customer is Blocked");

            return customer;
        }
    }
}
