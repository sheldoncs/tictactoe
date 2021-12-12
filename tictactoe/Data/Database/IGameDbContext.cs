using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tictactoe.Data.Entities;
using tictactoe.Dto;

namespace tictactoe.Data.Database
{
    public interface IGameDbContext
    {
        TData AddEntry<TData>(TData data, string operation);
        Task<IReadOnlyCollection<GameStatus>>  GetGameStatus(int GameId);
        Task<int> findPlayer(int game_id, string name);
        Task<IReadOnlyCollection<PlaceTokenDto>> getPlayerTokens(int player_id, int game_id);
        Task<int> getAllTokens(int game_id);
        Task getGameStatusId(int game_id, string outcome);
        Task<bool> gameExist(int game_id);
        Task<IReadOnlyCollection<PlayerDto>> getPlayers(int game_id);
        Task<IReadOnlyCollection<WinnerDto>> getWinners(int game_id);


    }

}
