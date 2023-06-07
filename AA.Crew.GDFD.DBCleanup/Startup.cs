using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;


[assembly: FunctionsStartup(typeof(AA.Crew.GDFD.DBCleanup.Startup))]
namespace AA.Crew.GDFD.DBCleanup
{
    public class Startup : FunctionsStartup
    {
        private ILoggerFactory _loggerFactory;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
                .SetBasePath(builder.GetContext().ApplicationRootPath)
                .AddJsonFile("local.settings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddLogging();
            _loggerFactory = new LoggerFactory();
            var logger = _loggerFactory.CreateLogger("Startup");
            logger.LogInformation("Got Here in Startup");

            logger.LogInformation($"Reading app settings file");
            
            logger.LogInformation($"ASPNETCORE_ENVIRONMENT value from azure config settings is : {environment}");

            var keyVaultUrl = config.GetValue<string>("Values:azKeyvaultUrl");
            logger.LogInformation($"Values:azKeyvaultUrl from config settings file is : {keyVaultUrl}");

            var keyVaultUrl1 = config.GetValue<string>("azKeyvaultUrl");
            logger.LogInformation($"azKeyvaultUrl from config settings file is : {keyVaultUrl1}");

            var keyVaultUrl2 = config.GetValue<string>("IsEncrypted");
            logger.LogInformation($"IsEncrypted from config settings file is : {keyVaultUrl2}");

            //Read db connection string from keyvault
            SecretClientOptions options = new SecretClientOptions()
            {
                Retry =
                    {
                        Delay= TimeSpan.FromSeconds(2),
                        MaxDelay = TimeSpan.FromSeconds(16),
                        MaxRetries = 5,
                        Mode = RetryMode.Exponential
                    }
            };            

            var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential(), options);
            KeyVaultSecret secret = client.GetSecret("GFD-ConnectionString");
            string secretValue = secret.Value;
            logger.LogInformation($"Final connection string from keyvault is : {secretValue}");

            builder.Services.AddSingleton<IDBLayer>(s => new DBLayer(secretValue));


        }

    }
}
