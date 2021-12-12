using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using tictactoe.Data.Entities;
using tictactoe.Dto;
using tictactoe.Repository.Contract;
using tictactoe.Service.Contract;

namespace tictactoe.Service
{
    public class GameService : IGameService
    {

        private const string LoggerScope = nameof(GameService);
        private readonly IGameRepository _gameRepository;
        private readonly ILogger<GameService> _logger;
        public GameService(IGameRepository gameRepository, ILogger<GameService> logger)
        {
            _gameRepository = gameRepository;
            _logger = logger;
        }

        public async Task<Game> CreateNewGame(GameDto gameDto, CancellationToken cancellationToken = default)
        {
            using (_logger.BeginScope("{Scope}: {Method}", LoggerScope, nameof(CreateNewGame)))
            {
                if (gameDto == null)
                {
                    throw new ArgumentNullException(nameof(gameDto));
                }
                return await _gameRepository.CreateNewGame(gameDto, cancellationToken);
            }
        }

        public async Task<GameStatusDto> GetGameStatus(int gameId, CancellationToken cancellationToken = default)
        {
            using (_logger.BeginScope("{Scope}: {Method}", LoggerScope, nameof(GetGameStatus)))
            {
                var result = await _gameRepository.GetGameStatus(gameId, cancellationToken);

                return result;
            }
        }

        public async Task<TokenDto> PlaceToken(int gameId, TokenDto tokenDto, CancellationToken cancellationToken = default)
        {

            

           using (_logger.BeginScope("{Scope}: {Method}", LoggerScope, nameof(PlaceToken)))
            {
                if (tokenDto == null)
                {
                    throw new ArgumentNullException(nameof(tokenDto));
                }
                return await _gameRepository.PlaceToken(gameId, tokenDto);
            }

        }
    }
}
