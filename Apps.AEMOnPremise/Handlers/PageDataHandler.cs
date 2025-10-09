using Apps.AEMOnPremise.Models.Responses;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.AEMOnPremise.Handlers;

public class PageDataHandler(InvocationContext invocationContext) : Invocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var request = new RestRequest("/content/services/bb-aem-connector/content/events.json");
        var pages = await Client.Paginate<PageResponse>(request);
        return pages.Where(x => context.SearchString == null || x.Title.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Select(x => new DataSourceItem(x.Path, x.Title));
    }
}