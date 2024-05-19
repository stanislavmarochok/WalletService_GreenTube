using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Text.Json;
using WalletService.Controllers;
using WalletService.DataService;
using WalletService.Domain;
using WalletService.Messages;
using WalletService.Repositories;
using WalletService.UseCases;

namespace WalletService.Tests
{
    public class WalletControllerWorkflowTests
    {
        private WalletController _controller;
        private Mock<IPlayerRepository> _mockPlayerRepository;
        private Mock<ITransactionRepository> _mockTransactionRepository;

        [SetUp]
        public void Setup()
        {
            _mockPlayerRepository = new Mock<IPlayerRepository>();
            _mockTransactionRepository = new Mock<ITransactionRepository>();

            var playerDataService = new PlayerDataService(_mockPlayerRepository.Object);
            var transactionDataService = new TransactionDataService(_mockTransactionRepository.Object);

            var registerPlayerUseCase = new RegisterPlayer(playerDataService);
            var getBalanceUseCase = new GetBalance(playerDataService);
            var processTransactionUseCase = new ProcessTransaction(playerDataService, transactionDataService);
            var getTransactionsUseCase = new GetTransactions(transactionDataService);

            _controller = new WalletController(
                registerPlayerUseCase,
                getBalanceUseCase,
                processTransactionUseCase,
                getTransactionsUseCase);
        }

        [Test]
        public async Task CompleteScenarioTest()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var depositTransactionId = Guid.NewGuid();
            var stakeTransactionId = Guid.NewGuid();
            var winTransactionId = Guid.NewGuid();

            var player = new Player { Id = playerId, Balance = 0 };

            _mockPlayerRepository.Setup(r => r.GetPlayer(playerId)).Returns(player);
            _mockPlayerRepository.Setup(r => r.AddPlayer(It.IsAny<Player>())).Callback((Player p) => player = p);

            var transactions = new List<Transaction>();
            _mockTransactionRepository.Setup(r => r.GetTransactionsAsync(playerId)).Returns(GetTransactionsAsync(transactions));
            _mockTransactionRepository.Setup(r => r.AddTransaction(It.IsAny<Transaction>())).Callback((Transaction t) => transactions.Add(t));

            var httpContext = new DefaultHttpContext();
            var responseStream = new MemoryStream();
            httpContext.Response.Body = responseStream;
            _controller.ControllerContext.HttpContext = httpContext;

            // Act - Register Player
            var registerPlayerRequest = new RegisterPlayerRequest { playerId = playerId };
            var registerResult = _controller.RegisterPlayer(registerPlayerRequest);
            Assert.That(registerResult, Is.TypeOf<OkResult>());

            // Act - Process Deposit Transaction
            var depositTransaction = new TransactionRequest { Id = depositTransactionId, Type = TransactionTypeEnum.Deposit, Amount = 100 };
            var depositResult = _controller.ProcessTransaction(depositTransaction);
            Assert.That(depositResult, Is.TypeOf<OkResult>());

            // Act - Process Stake Transaction
            var stakeTransaction = new TransactionRequest { Id = stakeTransactionId, Type = TransactionTypeEnum.Stake, Amount = 50 };
            var stakeResult = _controller.ProcessTransaction(stakeTransaction);
            Assert.That(stakeResult, Is.TypeOf<OkResult>());

            // Act - Process Win Transaction
            var winTransaction = new TransactionRequest { Id = winTransactionId, Type = TransactionTypeEnum.Win, Amount = 200 };
            var winResult = _controller.ProcessTransaction(winTransaction);
            Assert.That(winResult, Is.TypeOf<OkResult>());

            // Act - Get Balance
            var balanceResult = _controller.GetBalance(playerId) as OkObjectResult;
            Assert.That(balanceResult, Is.Not.Null);
            Assert.That(balanceResult, Is.TypeOf<OkResult>());
            var balance = (decimal)balanceResult.Value;
            Assert.That(balance, Is.EqualTo(250));

            // Act - Get Transactions
            await _controller.GetTransactions(playerId);

            // Assert - Transactions
            responseStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(responseStream).ReadToEndAsync();
            var resultList = JsonSerializer.Deserialize<List<Transaction>>(responseBody);
            Assert.That(resultList.Count, Is.EqualTo(3));
            Assert.That(resultList[0].Amount, Is.EqualTo(depositTransaction.Amount));
            Assert.That(resultList[1].Amount, Is.EqualTo(stakeTransaction.Amount));
            Assert.That(resultList[2].Amount, Is.EqualTo(winTransaction.Amount));
        }

        private async IAsyncEnumerable<Transaction> GetTransactionsAsync(IEnumerable<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                yield return await Task.FromResult(transaction);
            }
        }
    }
}
