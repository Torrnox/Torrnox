using System.Text.Json;
using Microsoft.Extensions.Logging;
using Torrnox.Application.Configurations;
using Torrnox.Application.Events;
using Torrnox.Application.Interfaces;
using Torrnox.Core;
using Torrnox.Core.Enums;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Torrnox.Infrastructure.Services;

public sealed class ConfigService
{
    public static AppConfig Config { get => Instance.GetConfig(); set => Instance.SetConfig(value); }
    public static event EventHandler<ConfigChangedEventArgs> OnConfigChanged;

    private static ConfigService _instance;
    private static ConfigService Instance
    {
        get
        {
            if (_instance is not null)
                return _instance;
            return _instance = new ConfigService();
        }
    }
    private readonly string _configFilePath;
    private readonly FileSystemWatcher _fileSystemWatcher;
    private AppConfig? _config;

    private ConfigService()
    {
        _configFilePath = Path.Combine(AppConstants.BasePath, "config.yaml");
        Directory.CreateDirectory(AppConstants.BasePath);

        GetConfig();

        _fileSystemWatcher = new FileSystemWatcher(AppConstants.BasePath, "config.yaml")
        {
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.Attributes | NotifyFilters.CreationTime,
        };
        _fileSystemWatcher.Changed += (_, _) => ReadConfigFile();
        _fileSystemWatcher.EnableRaisingEvents = true;
    }

    private AppConfig GetConfig()
    {
        if (_config is not null)
            return _config;

        if (File.Exists(_configFilePath))
        {
            ReadConfigFile();
            return _config!;
        }

        _config = new AppConfig();

        WriteConfigFile();

        return _config;
    }

    private void SetConfig(AppConfig config)
    {
        _config = config;
        WriteConfigFile();
        OnConfigChanged?.Invoke(this, new ConfigChangedEventArgs { Config = _config });
    }

    public void Dispose()
    {
        _fileSystemWatcher.Dispose();
    }

    private void ReadConfigFile()
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();

        try
        {
            var config = deserializer.Deserialize<AppConfig>(new StreamReader(File.OpenRead(_configFilePath)));

            if (config == _config)
                return;

            _config = config;
            OnConfigChanged?.Invoke(this, new ConfigChangedEventArgs { Config = _config });

            Console.WriteLine("Configuration file reloaded");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error reading configuration file: {ex.Message}");
        }
    }

    private void WriteConfigFile()
    {
        try
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
                .Build();

            File.WriteAllText(_configFilePath, serializer.Serialize(_config));
        
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error writing configuration file: {JsonSerializer.Serialize(ex)}");
        }
    }
}
