using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class GreetingService : IGreetingService
{
    private readonly ILogger<GreetingService> _log;
    private readonly IConfiguration _config;

    public GreetingService(ILogger<GreetingService> logger,
                           IConfiguration config)
    {
        _log = logger;
        _config = config;
    }
    public void run()
    {
         _log.LogInformation("contando...{contador}",_config.GetValue<int>("LoopTimes"));
        for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
        {
            _log.LogInformation("run nimber {number}", i);
        }
    }
}