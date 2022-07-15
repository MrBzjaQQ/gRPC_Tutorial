using Grpc.Core;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            switch (request.UserId)
            {
                case 1:
                    {
                        output.FirstName = "Jamie";
                        output.LastName = "Bright";
                        break;
                    }
                case 2:
                    {
                        output.FirstName = "Alex";
                        output.LastName = "Cooper";
                        break;
                    }
                case 3:
                    {
                        output.FirstName = "Niels";
                        output.LastName = "Brightbrained";
                        break;
                    }
                default:
                    {
                        output.FirstName = "Default";
                        output.LastName = "Customer";
                        break;
                    }
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(
            NewCustomerRequest request,
            IServerStreamWriter<CustomerModel> responseStream,
            ServerCallContext context)
        {
            var customers = new List<CustomerModel> {
                new CustomerModel
                {
                    FirstName = "Bilbo",
                    LastName = "Baggins",
                    Age = 80,
                    EmailAddress = "bilbo.baggins@example.com",
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Max",
                    LastName = "Maximoff",
                    Age = 120,
                    EmailAddress = "max.maximoff@example.com",
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Hello",
                    LastName = "World",
                    Age = 1,
                    EmailAddress = "hello.world@example.com",
                    IsAlive = false
                }
            };

            foreach (var cust in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
