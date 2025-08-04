using Torrnox.Application.Configurations;

namespace Torrnox.Application.Events;

public sealed class ConfigChangedEventArgs : EventArgs
{
    public required AppConfig Config { get; init; }
}
