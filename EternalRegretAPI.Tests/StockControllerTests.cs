﻿using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using System.Text.Json;
using Newtonsoft.Json;
using EternalRegret.Cosmos.Model;

namespace EternalRegretAPI.Tests
{
    public class StockControllerTests
    {

        [Fact]
        public async Task TestGetCodeTest()
        {
            var lambdaFunction = new LambdaEntryPoint();

            var requestStr = File.ReadAllText("./SampleRequests/StockController-Get.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            var context = new TestLambdaContext();
            var response = await lambdaFunction.FunctionHandlerAsync(request, context);

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task TestGetMetaDataTest()
        {
            var lambdaFunction = new LambdaEntryPoint();

            var requestStr = File.ReadAllText("./SampleRequests/StockController-GetMeta.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            var context = new TestLambdaContext();
            var response = await lambdaFunction.FunctionHandlerAsync(request, context);

            var stockMeta = System.Text.Json.JsonSerializer.Deserialize<StockMeta[]>(response.Body);

            Assert.Equal(200, response.StatusCode);
        }
    }
}
