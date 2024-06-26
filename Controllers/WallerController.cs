﻿using Microsoft.AspNetCore.Mvc;
using WalletService.Messages;
using WalletService.UseCases.Interfaces;

namespace WalletService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IRegisterPlayer _registerPlayerUseCase;
        private readonly IGetBalance _getBalanceUseCase;
        private readonly IProcessTransaction _processTransactionUseCase;
        private readonly IGetTransactions _getTransactionsUseCase;

        public WalletController(
            IRegisterPlayer registerPlayerUseCase,
            IGetBalance getBalanceUseCase,
            IProcessTransaction processTransactionUseCase,
            IGetTransactions getTransactionsUseCase)
        {
            _registerPlayerUseCase = registerPlayerUseCase;
            _getBalanceUseCase = getBalanceUseCase;
            _processTransactionUseCase = processTransactionUseCase;
            _getTransactionsUseCase = getTransactionsUseCase;
        }

        /// <summary>
        /// Register wallet for new player with initial balance 0 (should return error if player's wallet is already registered).
        /// </summary>
        [HttpPost("register")]
        public IActionResult RegisterPlayer([FromBody] RegisterPlayerRequest request)
        {
            try
            {
                _registerPlayerUseCase.Execute(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get player's balance.
        /// </summary>
        [HttpGet("{playerId}/balance")]
        public IActionResult GetBalance(Guid playerId)
        {
            try
            {
                var balance = _getBalanceUseCase.Execute(playerId);
                return Ok(balance);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Credit transaction to player's wallet (returns accepted/rejected).
        /// </summary>
        [HttpPost("transaction")]
        public async Task<IActionResult> ProcessTransaction([FromBody] TransactionRequest transactionRequest)
        {
            try
            {
                var result = await _processTransactionUseCase.ExecuteAsync(transactionRequest);
                return result ? Ok() : BadRequest("Transaction failed.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get list of saved transactions (id, amount, type) for given player.
        /// </summary>
        [HttpGet("{playerId}/transactions")]
        public async Task<IActionResult> GetTransactions(Guid playerId)
        {
            try
            {
                return Ok(await _getTransactionsUseCase.ExecuteAsync(playerId).ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
