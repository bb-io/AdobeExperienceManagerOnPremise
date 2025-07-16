using Apps.AEMOnPremise.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.AEMOnPremise.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>
    {
        new()
        {
            Name = "Developer API key",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionProperties = new List<ConnectionProperty>
            {
                new(CredNames.BaseUrl) 
                { 
                    DisplayName = "Base URL", 
                    Description = "Base URL for the AEM instance. Example: https://author-xxxxx-xxxxx.adobeaemcloud.com",
                    Sensitive = false
                },
                new(CredNames.Username)
                {
                    DisplayName = "Username",
                    Sensitive = false
                },
                new(CredNames.Password)
                {
                    DisplayName = "Password",
                    Sensitive = true
                }
            }
        }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(Dictionary<string, string> values) 
        => values.Select(x => new AuthenticationCredentialsProvider(x.Key, x.Value)).ToList();
}
