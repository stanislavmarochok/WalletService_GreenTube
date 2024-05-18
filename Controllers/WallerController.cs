﻿using Microsoft.AspNetCore.Mvc;
using WalletService.Models;
using WalletService.UseCases;

namespace WalletService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly RegisterPlayer _registerPlayerUseCase;
        private readonly GetBalance _getBalanceUseCase;
        private readonly ProcessTransaction _processTransactionUseCase;
        private readonly GetTransactions _getTransactionsUseCase;

        public WalletController(
            RegisterPlayer registerPlayerUseCase,
            GetBalance getBalanceUseCase,
            ProcessTransaction processTransactionUseCase,
            GetTransactions getTransactionsUseCase)
        {
            _registerPlayerUseCase = registerPlayerUseCase;
            _getBalanceUseCase = getBalanceUseCase;
            _processTransactionUseCase = processTransactionUseCase;
            _getTransactionsUseCase = getTransactionsUseCase;
        }

        [HttpPost("register")]
        public IActionResult RegisterPlayer([FromBody] Guid playerId)
        {
            try
            {
                _registerPlayerUseCase.Execute(playerId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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

        [HttpPost("transaction")]
        public IActionResult ProcessTransaction([FromBody] TransactionDTO transactionDto)
        {
            try
            {
                var result = _processTransactionUseCase.Execute(transactionDto);
                return result ? Ok() : BadRequest("Transaction failed.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{playerId}/transactions")]
        public IActionResult GetTransactions(Guid playerId)
        {
            try
            {
                var transactions = _getTransactionsUseCase.Execute(playerId);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
