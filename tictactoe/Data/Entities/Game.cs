using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tictactoe.Data.Entities
{
    public class Game
    {
        [Key]
        public int Game_Id { get; set; }
        public GameStatus gameStatus { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
