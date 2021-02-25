using ContactTesting.Output;
using PactNet;
using PactNet.Infrastructure.Outputters;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace ContactTesting.Tests
{
    public class ProviderPactTest
    {
        private readonly ITestOutputHelper _output;

        public ProviderPactTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void VerifyContractWithRealService()
        {

            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput> 
                {
                    new XUnitOutput(_output)
                },
                Verbose = true 
            };

            new PactVerifier(config)
                .ServiceProvider("EmployeeList", "http://localhost:60880/api")
                .HonoursPactWith("Service_Consumer")
                .PactUri(@"C:\Pact-Testing-Files\pacts\service_consumer-employeelist.json")
                .Verify();
        }
    }
}
