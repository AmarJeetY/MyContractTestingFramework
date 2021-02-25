using ContactTesting.Consumer;
using ContactTesting.Mock;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using System.Collections.Generic;
using Xunit;

namespace ContactTesting.Tests
{
    public class ConsumerPactTest : IClassFixture<ConsumerPact>
    {

        private readonly IMockProviderService _mockProviderService;
        private readonly string _mockProviderServiceBaseUri;

        public ConsumerPactTest(ConsumerPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderService.ClearInteractions();
            _mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
        }

        [Fact]
        public void GenerateContract()
        {

            //Arrange
            _mockProviderService
                .Given("Employee Details for Id '1'")
                .UponReceiving("A GET request to retrieve the employee 1 details")
                .With(new ProviderServiceRequest
                {

                    Method = HttpVerb.Get,
                    Path = "/employee/1",
                    Headers = new Dictionary<string, object>
                    {
                        { "Accept", "application/json" }
                    }


                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new
                    {
                        id = 1,
                        employeeName = "Developer",
                        email = "developer@covea.co.uk",
                        city = "Halifax"
                    }
                });

            //Act 
            var consumer = new APIClient(_mockProviderServiceBaseUri);
            var result = consumer.GetEmployeeDetails("1");

            //Assert
            Assert.Equal("Developer", result.EmployeeName);

            _mockProviderService.VerifyInteractions();
        }

    }
}
