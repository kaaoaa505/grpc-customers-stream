using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServerProject.Services
{
    public class CustomersService: Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if(request.UserId == 1)
            {
                output.FirstName = "Khaled";
                output.LastName = "Abdalla";
            }
            else
            {
                output.FirstName = "Unkown";
                output.LastName = "User";
            }

            return Task.FromResult(output);
        }

        public override async Task GetCustomers(EmptyRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "F01",
                    LastName = "L01",
                    EmailAddress = "E01@dev.com",
                    Age = 01,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "F02",
                    LastName = "L02",
                    EmailAddress = "E02@dev.com",
                    Age = 02,
                    IsAlive = false
                },
                new CustomerModel
                {
                    FirstName = "F03",
                    LastName = "L03",
                    EmailAddress = "E03@dev.com",
                    Age = 03,
                    IsAlive = false
                }
            };

            foreach(var customer in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(customer);
            }

        }
    }
}
