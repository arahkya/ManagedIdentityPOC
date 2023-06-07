using Microsoft.AspNetCore.Mvc;
using Azure.Identity;
using Azure.Security.KeyVault;
using Azure.Security.KeyVault.Secrets;

namespace ManagedIdentityPOC.Controllers;

[ApiController]
[Route("[controller]")]
public class SecretController : ControllerBase
{
    private readonly ILogger<SecretController> _logger;

    public SecretController(ILogger<SecretController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("{vaultName}/{name}")]
    public string GetSecret(string vaultName, string name)
    {
        SecretClient secretClient = new(new Uri($"https://{vaultName}.vault.azure.net"), new DefaultAzureCredential());
        
        var secretResponse = secretClient.GetSecret(name);
        var secretValue = secretResponse.Value.Value;

        return secretValue;
    }
}
