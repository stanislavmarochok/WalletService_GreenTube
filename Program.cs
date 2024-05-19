using WalletService.DataService;
using WalletService.Repositories;
using WalletService.Repositories.Interfaces;
using WalletService.UseCases;
using WalletService.UseCases.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPlayerRepository, InMemoryPlayerRepository>();
builder.Services.AddSingleton<ITransactionRepository, InMemoryTransactionRepository>();

builder.Services.AddSingleton<IPlayerDataService, PlayerDataService>();
builder.Services.AddSingleton<ITransactionDataService, TransactionDataService>();

builder.Services.AddSingleton<IRegisterPlayer, RegisterPlayer>();
builder.Services.AddSingleton<IGetBalance, GetBalance>();
builder.Services.AddSingleton<IProcessTransaction, ProcessTransaction>();
builder.Services.AddSingleton<IGetTransactions, GetTransactions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
