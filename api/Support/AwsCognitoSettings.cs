namespace Api.Support;

/// <summary>
/// POCO object for the AWS Cognito settings.
/// </summary>
public class AwsCognitoSettings
{
    /// <summary>
    /// The AWS User Pool ID.
    /// </summary>
    public string UserPoolId { get; set; } = string.Empty;

    /// <summary>
    /// The AWS User Pool Region.
    /// </summary>
    public string Region { get; set; } = string.Empty;

    /// <summary>
    /// The AWS App Client ID.
    /// </summary>
    public string AppClientId { get; set; } = string.Empty;
}