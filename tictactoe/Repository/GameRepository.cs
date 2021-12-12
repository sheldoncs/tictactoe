using CSharpTest.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using tictactoe.Business;
using tictactoe.Data.Database;
using tictactoe.Data.Entities;
using tictactoe.Dto;
using tictactoe.Repository.Contract;

namespace tictactoe.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly IGameDbContext _gameDbContext;
        private readonly ILogger<GameRepository> _logger;
        private const string LoggerScope = nameof(GameRepository);
        public GameRepository(IGameDbContext gameDbContext, ILogger<GameRepository> logger)
        {
            _gameDbContext = gameDbContext;
            _logger = logger;
        }
        public async Task<Game> CreateNewGame(GameDto gameDto, CancellationToken cancellationToken = default)
        {

            using (_logger.BeginScope("{Scope}: {Method}", LoggerScope, nameof(CreateNewGame)))
            {
                Game game = new Game();
                _logger.LogTrace("Creating Game new Game: {@GameDto}.", gameDto);

                if (gameDto == null)
                {
                    throw new ArgumentNullException(nameof(gameDto));
                }
                try
                {

                    Collection<Player> players = new Collection<Player>();

                    Player playerA = new Player()
                    {
                        Name = gameDto.PlayerA
                    };
                    Player playerB = new Player()
                    {
                        Name = gameDto.PlayerB
                    };
                    /*Add first Player*/
                    _gameDbContext.AddEntry<Player>(playerA, "Add");
                    players.Add(playerA);

                    /*Add first Player*/

                    /*Add second Player*/
                    _gameDbContext.AddEntry<Player>(playerB, "Add");
                    players.Add(playerB);

                    /*Add second Player*/

                    /*Create a new Game*/

                    _gameDbContext.AddEntry<Game>(game, "Add");
                    /*Create a new Game*/

                    /*initialize game status*/
                    GameStatus gameStatus = new GameStatus()
                    {
                        Game_Id = game.Game_Id,
                        players = players,
                        Outcome = "ACTIVE",

                    };
                    _gameDbContext.AddEntry<GameStatus>(gameStatus, "Add");
                    playerA.Game_Id = game.Game_Id;
                    _gameDbContext.AddEntry<Player>(playerA, "Modified");
                    playerB.Game_Id = game.Game_Id;
                    _gameDbContext.AddEntry<Player>(playerB, "Modified");
                    /*initialize game status*/
                }


                catch (MySqlException sqlEx)
                {
                    _logger.LogCritical(
                       sqlEx,
                       "Unexpected behaviour. Adverse event client data: {@Client}.",
                       gameDto);

                    throw;
                }

                return await Task.FromResult(game);
            }
        }

        public async Task<TokenDto> PlaceToken(int gameId, TokenDto tokenDto, CancellationToken cancellationToken = default)
        {



            using (_logger.BeginScope("{Scope}: {Method}", LoggerScope, nameof(CreateNewGame)))
            {
                var exist = _gameDbContext.gameExist(gameId);

                if (exist.Result == false)
                {
                    return null;
                }

                var value = _gameDbContext.findPlayer(gameId, tokenDto.player);
                bool winner = false;
                _logger.LogTrace("Place Token: {@tokenDto}.", tokenDto);

                if (tokenDto == null || value == null)
                {
                    throw new ArgumentNullException(nameof(tokenDto));
                }


                PlaceToken placeToken = new PlaceToken()
                {
                    player_id = value.Result,
                    game_id = gameId,
                    row = tokenDto.row,
                    col = tokenDto.col
                };

                _gameDbContext.AddEntry<PlaceToken>(placeToken, "Add");

                var result = await _gameDbContext.getPlayerTokens(value.Result, gameId);
                LookForWin lookForWin = new LookForWin(gameId, value.Result,
                                                          tokenDto.row,
                                                              tokenDto.col,
                                                                 result);
                winner = lookForWin.TestCol();
                if (!winner)
                {
                    winner = lookForWin.TestRow();
                }
                if (!winner)
                {
                    int diff = tokenDto.row - tokenDto.col;
                    if (diff < 0)
                    {
                        diff = -1 * diff;
                        if (diff == 2)
                        {
                            winner = lookForWin.TestRightToLeftDiag();
                        }
                    } else if (diff == 0)
                    {
                        if (tokenDto.row == 1)
                        {
                            winner = lookForWin.TestRightToLeftDiag();
                            if (!winner)
                            {
                                winner = lookForWin.TestLeftToRightDiag();
                            }
                        } else
                        {
                            winner = lookForWin.TestRightToLeftDiag();
                        }
                    }
                 
                }
                if (winner)
                {
                    Winner win = new Winner()
                    {
                        Player_Id = value.Result,
                        Game_Id = gameId
                    };
                    _gameDbContext.AddEntry<Winner>(win, "Add");

                    await _gameDbContext.getGameStatusId(gameId, "GAME OVER");
                    
                }

                if (winner == false)
                {

                    var count = _gameDbContext.getAllTokens(gameId);
                    var players = _gameDbContext.getPlayers(gameId);
                    if (count.Result == 9)
                    {
                        foreach (var player in players.Result)
                        {

                            Winner draw = new Winner()
                            {
                                Player_Id = player.Player_Id,
                                Game_Id = gameId
                            };
                            _gameDbContext.AddEntry<Winner>(draw, "Add");

                        }
                    }

                }

                return await Task.FromResult(tokenDto);
            }


        }

        public async Task<GameStatusDto> GetGameStatus(int gameId, CancellationToken cancellationToken)
        {

            using (_logger.BeginScope("{Scope}: {Method}", LoggerScope, nameof(GetGameStatus)))
            {
                GameStatusDto gameStatusDto = new GameStatusDto();
                Collection<string> playerList = new Collection<string>();
                Collection<string> winnerList = new Collection<string>();

                var result = _gameDbContext.gameExist(gameId);

                if (result.Result == false)
                {
                    gameStatusDto = null;
                }
                else
                {
                    gameStatusDto.Game_Id = gameId;
                    var players = _gameDbContext.getPlayers(gameId);
                    var winners = _gameDbContext.getWinners(gameId);


                    foreach (var item in players.Result)
                    {
                        playerList.Add(item.Name);
                    }
                    gameStatusDto.players = playerList;
                    if (winners.Result.Count > 0)
                    {
                        foreach (var winner in winners.Result)
                        {
                            winnerList.Add(winner.Name);
                        }
                        gameStatusDto.Winners = winnerList;
                        gameStatusDto.Outcome = "GAME_OVER";
                    } else
                    {
                        gameStatusDto.Outcome = "ACTIVE";
                    }

                    
                }
                return await Task.FromResult(gameStatusDto);
            }
        }
    }
}
