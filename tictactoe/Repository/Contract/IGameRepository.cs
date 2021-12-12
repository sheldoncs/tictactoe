using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using tictactoe.Data.Entities;
using tictactoe.Dto;

namespace tictactoe.Repository.Contract
{
    public interface IGameRepository
    {
       Task<Game> CreateNewGame(GameDto gameDto, CancellationToken cancellationToken = default);
        Task<TokenDto> PlaceToken(int gameId, TokenDto tokenDto, CancellationToken cancellationToken = default);
        Task<GameStatusDto> GetGameStatus(int gameId, CancellationToken cancellationToken);
    }
}
