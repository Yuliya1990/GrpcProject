using Grpc.Core;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "Yuliia";
                output.LastName = "Solomakha";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Vlad";
                output.LastName = "Spotar";
            }
            else if (request.UserId == 3)
            {
                output.FirstName = "Dmytro";
                output.LastName = "Aleksenko";
            }
            else
            {
                output.FirstName = "Julia";
                output.LastName = "Nedavnya";
            }

            return Task.FromResult(output);
        }

        public async override Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Alina",
                    LastName = "Bedenko",
                    EmailAddress = "alina@gmail.com",
                    Age = 21,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Dmytro",
                    LastName = "Lytvynenko",
                    EmailAddress = "d_lytv@gmail.com",
                    Age = 20,
                    IsAlive = true
                },
                 new CustomerModel
                {
                    FirstName = "Karina",
                    LastName = "Gurinenko",
                    EmailAddress = "guri@gmail.com",
                    Age = 22,
                    IsAlive = true
                }

            };

            foreach (var custumer in customers)
            {
                await responseStream.WriteAsync(custumer);
            }
        }
    }

     
    
}
