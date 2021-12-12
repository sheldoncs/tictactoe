using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tictactoe.Data.Entities;
using tictactoe.Dto;

namespace tictactoe.Data.Database
{
    public class DatabaseContext : DbContext, IGameDbContext
    {

        public DbSet<Game> games { get; set; }
        public DbSet<Player> players { get; set; }
        public DbSet<GameStatus> gameStatuses { get; set; }
        public DbSet<Winner> winners { get; set; }
        public DbSet<PlaceToken> placeTokens { get; set; }

        
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public TData AddEntry<TData>(TData data, string operation)
        {
            if (operation.Equals("Add"))
            {
                this.Entry(data).State = EntityState.Added;
            } else
            {
                this.Entry(data).State = EntityState.Modified;
            }
            base.SaveChanges();

            return data;
        }
        public Task<int> findPlayer(int game_id, string name)
        {
            var result = (from pl in players


                          select new PlayerDto
                          {
                              Player_Id = pl.Player_Id,
                              Game_Id = pl.Game_Id,
                              Name = pl.Name
                          }).Where(pl => pl.Name == name).
                          Where(pl => pl.Game_Id == game_id).FirstOrDefault ();

            return Task.FromResult(result.Player_Id);

        }
        public Task<IReadOnlyCollection<GameStatus>> GetGameStatus(int GameId)
        {
            throw new NotImplementedException();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Game>().
                HasMany(ad => ad.Players);
            modelBuilder.Entity<Player>().
                HasOne(tk => tk.Token);
            modelBuilder.Entity<Game>().
               HasOne(gm => gm.gameStatus);
           
        }

        public async Task<IReadOnlyCollection<PlaceTokenDto>> getPlayerTokens(int player_id, int game_id)
        {
            var result = await (from pl in placeTokens


                          select new PlaceTokenDto
                          {
                              player_id = player_id,
                              game_id = game_id,
                              row = pl.row,
                              col = pl.col
                          }).Where(pl => pl.player_id == player_id).
                          Where(pl => pl.game_id == game_id).ToListAsync();

            
            return (IReadOnlyCollection<PlaceTokenDto>)result;
        }

        public Task<bool> gameExist(int game_id)
        {
           
            bool found = false;
            var result = (from ps in games

                               select new GameDto
                               {
                                   game_id = ps.Game_Id,
                                  
                               }).Where(ps => ps.game_id == game_id)
                                          .ToList();
            if (result.Count > 0)
            {
                found = true;
            }
            return  Task.FromResult(found);
            
        }

        public async Task<IReadOnlyCollection<PlayerDto>> getPlayers(int game_id)
        {

            var result = await (from pl in players


                          select new PlayerDto
                          {

                              Game_Id = game_id,
                              Player_Id = pl.Player_Id,
                              Name = pl.Name

                          }).Where(pl => pl.Game_Id == game_id)
                                          .ToListAsync();
            
            return (IReadOnlyCollection<PlayerDto>)result;
           
        }

        public async Task<int> getAllTokens(int game_id)
        {

            var result = await(from pt in placeTokens
                               
                               select new PlaceTokenDto
                               {

                                   game_id = game_id,
                                   row = pt.row,
                                   col = pt.col
                                   

                               }).Where(pt => pt.game_id == game_id)
                                          .ToListAsync();
            
            return await Task.FromResult(result.Count);

            
        }

        public async Task<IReadOnlyCollection<WinnerDto>> getWinners(int game_id)
        {
            var result = await(from wn in winners
                               join pl in players on wn.Player_Id equals pl.Player_Id

                               select new WinnerDto
                               {
                                Player_Id = pl.Player_Id,
                                Game_Id = wn.Game_Id,
                                Name = pl.Name
                               }).Where(wn => wn.Game_Id == game_id)
                                         .ToListAsync();

            return (IReadOnlyCollection<WinnerDto>)result;
        }
    }
}
