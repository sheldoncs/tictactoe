using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using tictactoe.Dto;
using tictactoe.Service.Contract;


namespace tictactoe.Controllers
{

    [Route("api/tictactoe/v1/")]
    [ApiController]

    public class TicToeController : ControllerBase
    {
        private readonly IGameService _gameService;

        public TicToeController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("game/new", Name = "CreateNewGame")]
        public async Task<ActionResult> CreateNewGame(GameDto gameDto, CancellationToken cancellationToken = default)
        {
            if (gameDto == null)
            {
                return BadRequest(ModelState);
            }
            else
            {
                if (gameDto.PlayerA == null)
                {
                    return BadRequest(ModelState);
                    
                }
                if (gameDto.PlayerB == null)
                {
                    return BadRequest(ModelState);
                }
            }

            var result = await _gameService.CreateNewGame(gameDto, cancellationToken);
            
           NewGameDto newGameDto = new NewGameDto()
           {
                    game_id=result.Game_Id
           };
      
           return Ok(newGameDto);
        }
        [HttpGet("game/{gameId}", Name = "GetGameStatus")]
        public async Task<ActionResult> GetGameStatus(int gameId)
        {

            var result = await _gameService.GetGameStatus(gameId);
            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);

        }
        [HttpPost("game/{gameId}/placeToken", Name = "PlaceToken")]
        public async Task<ActionResult> PlaceToken(int gameId, TokenDto tokenDto)
        {
            var result = await _gameService.PlaceToken(gameId, tokenDto);

            if (result == null)
            {
                return BadRequest(ModelState);
            }
            
            

            return Ok(result);
        }
    }
}