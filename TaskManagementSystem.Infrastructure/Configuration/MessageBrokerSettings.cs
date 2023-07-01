namespace TaskManagementSystem.Infrastructure.Configuration;

public class MessageBrokerSettings
{
    public string Host { get; set; }
    public string UserName { get; set; }
    
    // In that example password is taken form user secrets.json
    public string Password { get; set; }
    
    // But let's imagine that we have a key by which we are retrieving Password value from Azure Key Vault
    // public string SecretKey { get; set; }
}