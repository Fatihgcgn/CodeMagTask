using Application.Abstractions;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Printing;

public sealed class MockPrinterService : IPrinterService
{
    private readonly ILogger<MockPrinterService> _log;

    public MockPrinterService(ILogger<MockPrinterService> log)
    {
        _log = log;
    }

    public Task PrintAsync(string payload, CancellationToken ct = default)
    {
        _log.LogInformation("MOCK PRINTER OUTPUT => {Payload}", payload);
        return Task.CompletedTask;
    }
}