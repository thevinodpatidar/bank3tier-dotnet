using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bank3Tier.Api.Authorization;
using Bank3Tier.Api.Resources.Transaction;
using Bank3Tier.Core.Models;
using Bank3Tier.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank3Tier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        private readonly IJwtUtils _jwtUtils;

        public TransactionController(ITransactionService transactionService, IMapper mapper, IJwtUtils jwtUtils)
        {
            this._mapper = mapper;
            this._jwtUtils = jwtUtils;
            this._transactionService = transactionService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<TransactionResource>>> GetAllTransaction()
        {
            var transactions = await _transactionService.GetAllTransaction();
            var transactionResources = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionResource>>(transactions);

            return Ok(transactionResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionResource>> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetTransactionById(id);
            var transactionResource = _mapper.Map<Transaction, TransactionResource>(transaction);

            return Ok(transactionResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<TransactionResource>> CreateTransaction([FromBody] CreateTransactionResource createTransactionResource)
        {
            var currentUser = (User)HttpContext.Items["User"];
            //var validator = new SaveMusicResourceValidator();
            //var validationResult = await validator.ValidateAsync(saveMusicResource);

            //if (!validationResult.IsValid)
            //    return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var transactionToCreate = _mapper.Map<CreateTransactionResource, Transaction>(createTransactionResource);
           
            var newTransaction = await _transactionService.CreateTransaction(currentUser.Id,transactionToCreate);

            var transaction = await _transactionService.GetTransactionById(newTransaction.Id);

            var transactionResource = _mapper.Map<Transaction,SuccessTransactionResource>(transaction);

            return Ok(transactionResource);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<MusicResource>> UpdateMusic(int id, [FromBody] SaveMusicResource saveMusicResource)
        //{
        //    var validator = new SaveMusicResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveMusicResource);

        //    var requestIsInvalid = id == 0 || !validationResult.IsValid;

        //    if (requestIsInvalid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var musicToBeUpdate = await _musicService.GetMusicById(id);

        //    if (musicToBeUpdate == null)
        //        return NotFound();

        //    var music = _mapper.Map<SaveMusicResource, Music>(saveMusicResource);

        //    await _musicService.UpdateMusic(musicToBeUpdate, music);

        //    var updatedMusic = await _musicService.GetMusicById(id);
        //    var updatedMusicResource = _mapper.Map<Music, MusicResource>(updatedMusic);

        //    return Ok(updatedMusicResource);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMusic(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var music = await _musicService.GetMusicById(id);

        //    if (music == null)
        //        return NotFound();

        //    await _musicService.DeleteMusic(music);

        //    return NoContent();
        //}
    }
}
