using Apps.AEMOnPremise.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.AEMOnPremise;

public class Invocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Credentials =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();
    protected ApiClient Client { get; }
    
    public Invocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new(Credentials.ToList());
    }
}
