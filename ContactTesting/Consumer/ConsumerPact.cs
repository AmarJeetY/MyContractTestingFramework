using PactNet;
using PactNet.Mocks.MockHttpService;
using System;

namespace ContactTesting.Consumer
{
    public class ConsumerPact : IDisposable
    {
        private IPactBuilder PactBuilder { get; }
        public IMockProviderService MockProviderService { get; }
        private int MockServerPort => 9224;
        public string MockProviderServiceBaseUri => $"http://localhost:{MockServerPort}";

        public ConsumerPact()
        {
            //Pact Configuration
            var pactConfig = new PactConfig
            {
                SpecificationVersion = "2.0.0",
                PactDir = @"C:\Pact-Testing-Files\pacts",
                LogDir = @"C:\Pact-Testing-Files\logs"
            };

            PactBuilder = new PactBuilder(pactConfig);
            PactBuilder.ServiceConsumer("Service_Consumer").HasPactWith("EmployeeList");
            MockProviderService = PactBuilder.MockService(MockServerPort);
        }
        public void Dispose()
        {
            PactBuilder.Build();
        }
    }
}