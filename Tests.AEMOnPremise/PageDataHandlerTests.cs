using Apps.AEMOnPremise.Handlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Tests.AEMOnPremise.Base;

namespace Tests.AEMOnPremise;

public class PageDataHandlerTests : BaseDataHandlerTests
{
    protected override IAsyncDataSourceItemHandler DataHandler => new PageDataHandler(InvocationContext);

    protected override string SearchString => "Clear";
}
