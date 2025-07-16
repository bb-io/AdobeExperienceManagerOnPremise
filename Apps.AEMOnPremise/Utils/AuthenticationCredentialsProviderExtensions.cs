using Apps.AEMOnPremise.Constants;
using Apps.AEMOnPremise.Models.Dtos;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Newtonsoft.Json;

namespace Apps.AEMOnPremise.Utils;

public static class AuthenticationCredentialsProviderExtensions
{
    public static string GetBaseUrl(this IEnumerable<AuthenticationCredentialsProvider> credentialsProviders)
    {
        var baseUrl = GetCredentialValue(credentialsProviders, CredNames.BaseUrl, "Base URL");
        return baseUrl.TrimEnd('/');
    }

    public static string GetCredentialValue(this IEnumerable<AuthenticationCredentialsProvider> credentialsProviders, string credName, string credDisplayName)
    {
        var credValue = credentialsProviders.Get(credName);
        if (credValue == null || string.IsNullOrEmpty(credValue.Value))
        {
            throw new ArgumentNullException($"{credDisplayName} is not provided in the credentialsProviders.");
        }
        
        return credValue.Value;
    }
}
