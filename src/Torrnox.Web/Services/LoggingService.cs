using System;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using Torrnox.Application.Events;
using Torrnox.Application.Interfaces;
using Torrnox.Core;
using Torrnox.Infrastructure.Services;
using Torrnox.Web.Extensions;

namespace Torrnox.Web.Services;

public sealed class LoggingService
{
    private readonly LoggingLevelSwitch _appLogLevelSwitch;
    private readonly LoggingLevelSwitch _microsoftLogLevelSwitch;
    private readonly LoggingLevelSwitch _systemLogLevelSwitch;

    public LoggingService()
    {
        _appLogLevelSwitch = new LoggingLevelSwitch(ConfigService.Config.Logging.AppLoggingLevel.ToSerilog());
        _microsoftLogLevelSwitch = new LoggingLevelSwitch(ConfigService.Config.Logging.MicrosoftLoggingLevel.ToSerilog());
        _systemLogLevelSwitch = new LoggingLevelSwitch(ConfigService.Config.Logging.SystemLoggingLevel.ToSerilog());

        ConfigService.OnConfigChanged += OnConfigChanged;

        var configuration = new LoggerConfiguration()
            .Destructure.ToMaximumDepth(50)
            .MinimumLevel.ControlledBy(_appLogLevelSwitch)
            .MinimumLevel.Override("Microsoft", _microsoftLogLevelSwitch)
            .MinimumLevel.Override("System", _systemLogLevelSwitch)
            .WriteTo.Console()
            .WriteTo.File(
                new CompactJsonFormatter(),
                Path.Combine(AppConstants.BasePath, "logs", "application.log"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7)
            .Enrich.FromLogContext();

        Log.Logger = configuration.CreateLogger();
    }

    private void OnConfigChanged(object? sender, ConfigChangedEventArgs e)
    {
        _appLogLevelSwitch.MinimumLevel = e.Config.Logging.AppLoggingLevel.ToSerilog();
        _microsoftLogLevelSwitch.MinimumLevel = e.Config.Logging.MicrosoftLoggingLevel.ToSerilog();
        _systemLogLevelSwitch.MinimumLevel = e.Config.Logging.SystemLoggingLevel.ToSerilog();
    }
}
