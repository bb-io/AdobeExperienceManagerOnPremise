using Apps.AEMOnPremise.Events;
using Apps.AEMOnPremise.Events.Models;
using Apps.AEMOnPremise.Models.Responses;
using Blackbird.Applications.Sdk.Common.Polling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Tests.AEMOnPremise.Base;

namespace Tests.AEMOnPremise.Events;

[TestClass]
public class PagePollingListTests : TestBase
{
    private PagePollingList _pollingList;

    [TestInitialize]
    public void Initialize()
    {
        _pollingList = new PagePollingList(InvocationContext);
    }

    [TestMethod]
    public async Task OnPagesCreatedOrUpdatedAsync_WithNullMemory_ShouldReturnCorrectResponse()
    {
        // Arrange
        var request = new PollingEventRequest<PagesMemory>
        {
            Memory = null,
            PollingTime = DateTime.UtcNow
        };
        var optionalRequest = new OnPagesCreatedOrUpdatedRequest();

        // Act
        var response = await _pollingList.OnPagesCreatedOrUpdatedAsync(request, optionalRequest);

        // Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(response.Memory);
        Assert.IsNull(response.Result);
        
        // Log for debugging
        Console.WriteLine($"Response: {JsonConvert.SerializeObject(response, Formatting.Indented)}");
        Console.WriteLine($"Last triggered time: {response.Memory.LastTriggeredTime}");
        
        Assert.IsFalse(response.FlyBird, "FlyBird should be false for first run with null memory");
        
        // Now test with existing memory
        var memoryWithTime = new PagesMemory
        {
            LastTriggeredTime = DateTime.UtcNow.AddMinutes(-10)  // 10 minutes in the past
        };
        
        var secondRequest = new PollingEventRequest<PagesMemory>
        {
            Memory = memoryWithTime,
            PollingTime = DateTime.UtcNow
        };
        
        // Act again with memory
        var secondResponse = await _pollingList.OnPagesCreatedOrUpdatedAsync(secondRequest, optionalRequest);
        
        // Log second response
        Console.WriteLine($"Second Response: {JsonConvert.SerializeObject(secondResponse, Formatting.Indented)}");
        if (secondResponse.Result != null)
        {
            Console.WriteLine($"Found pages: {secondResponse.Result.TotalCount}");
        }
        
        // The FlyBird value will depend on whether any pages were created/updated during the test
        Console.WriteLine($"FlyBird: {secondResponse.FlyBird}");
    }
}
