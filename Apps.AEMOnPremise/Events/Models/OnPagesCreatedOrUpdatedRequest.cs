using Blackbird.Applications.Sdk.Common;

namespace Apps.AEMOnPremise.Events.Models;

public class OnPagesCreatedOrUpdatedRequest
{
    [Display("Root path")]
    public string? RootPath { get; set; }

    [Display("Root path includes")]
    public IEnumerable<string>? RootPathIncludes { get; set; }
}
