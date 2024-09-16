
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi.Writers;
using super_exchange.Server.Client;
using super_exchange.Server.Constants;
using super_exchange.Server.Database;
using super_exchange.Server.Document;
using super_exchange.Server.Entity;
using super_exchange.Server.Mapper;

namespace super_exchange.Server.Services;

public class CurrencyHostedService : BackgroundService, IDisposable
{

    private readonly ITableClient _client;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IRateMapper _mapper;
    private readonly ILogger<CurrencyHostedService> _logger;
    private readonly PeriodicTimer _timer;

    public CurrencyHostedService(ITableClient client, IServiceScopeFactory scopeFactory, IRateMapper mapper, ILogger<CurrencyHostedService> logger)
    {
        _client = client;
        _scopeFactory = scopeFactory;
        _mapper = mapper;
        _logger = logger;
        _timer = new PeriodicTimer(TimeSpan.FromDays(1));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting to run backround service to fetch and store currency information");

        await RunService();

        try
        {
            while (await _timer.WaitForNextTickAsync(stoppingToken))
            {
                await RunService();
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Backgound service is to fetch and store information is stopping");
        }
    }

    public async Task RunService()
    {
        var tableA = await FetchInformation(NbpTable.TableA);
        var tableB = await FetchInformation(NbpTable.TableB);
        var tableC = await FetchInformation(NbpTable.TableC);
        var entities = _mapper.Map(tableA, tableC);
        entities.AddRange(_mapper.Map(tableB));
        entities.AddRange(_mapper.Map(tableA));
        await StoreInformation(entities.DistinctBy(d => d.Code).ToList());
    }

    private async Task StoreInformation(List<RateEntity> entities)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ExchangeDatabaseContext>();

            try
            {
                foreach (var entity in entities)
                {
                    if (await context.RateEntities.AnyAsync(r => r.EffectiveDate.Equals(entity.EffectiveDate) && r.Code == entity.Code)) continue;

                    await context.AddAsync(entity);

                }
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var e = ex.GetBaseException() as SqlException;
                if (e == null)
                {
                    _logger.LogError(ex, "There was an error while saveing records");
                }
                if (e.Number == 2627)
                {

                    _logger.LogInformation("Duplicate data fround, no need to save it again");
                }
                else
                {
                    _logger.LogError(e, "There was an error while saveing records");
                }
            }
        }
    }

    private async Task<TableDocument> FetchInformation(string table)
    {
        _logger.LogInformation("Starting to log information for table {0}", table);
        return await _client.GetTableExchangeRates(table);
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}
