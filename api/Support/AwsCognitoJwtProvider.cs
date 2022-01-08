using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;

namespace Api.Support;

/// <summary>
/// Class for initializing the Cognito configuration for JWT authentication.
/// </summary>
public class AwsCognitoJwtProvider : IConfigureNamedOptions<JwtBearerOptions>
{
    private AwsCognitoSettings _cognito;

    /// <summary>
    /// Injection constructor.
    /// </summary>
    /// <param name="settings">Injection constructed instance of the settings.</param>
    public AwsCognitoJwtProvider(IOptions<AwsCognitoSettings> settings)
    {
        _cognito = settings.Value;

        if(string.IsNullOrEmpty(_cognito.AppClientId)
            || string.IsNullOrEmpty(_cognito.Region)
            || string.IsNullOrEmpty(_cognito.UserPoolId))
        {
            throw new Exception("The AWS Cognito configuration failed to load correctly.");
        }
    }

    /// <summary>
    /// Configures the token validation parameters.
    /// </summary>
    /// <param name="name">The name of the authentication scheme.</param>
    /// <param name="options">The JWT options to configure</param>
    public void Configure(string name, JwtBearerOptions options)
    {
        // check that we are currently configuring the options for the correct scheme
        if (name == JwtBearerDefaults.AuthenticationScheme)
        {
            options.TokenValidationParameters = CreateTokenValidationParameters();
        }
    }

    /// <summary>
    /// Configures the token validation parameters.
    /// </summary>
    /// <param name="options">The JWT options to configure</param>
    public void Configure(JwtBearerOptions options)
    {
        Configure(string.Empty, options);
    }

    /// <summary>
    /// Creates the configuration required to initialize Cognito authentication.
    /// </summary>
    /// <returns>The TokenValidationParameters required to support Cognito authentication.</returns>
    public TokenValidationParameters CreateTokenValidationParameters()
    {
        string issuer = $"https://cognito-idp.{_cognito.Region}.amazonaws.com/{_cognito.UserPoolId}";
        string jwtKeySetUrl = $"{issuer}/.well-known/jwks.json";

        Log.Warning("DOING KEY CONVERT");

        return new TokenValidationParameters
        {
            IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
            {
                // get JsonWebKeySet from AWS
                var json = new HttpClient().GetStringAsync(jwtKeySetUrl).Result;

                // serialize the result
                var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(json)?.Keys;

                // cast the result to be the type expected by IssuerSigningKeyResolver
                return (IEnumerable<SecurityKey>?) keys;
            },
            ValidIssuer = issuer,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidAudience = _cognito.AppClientId
        };
    }
}