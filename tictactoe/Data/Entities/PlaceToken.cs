using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tictactoe.Data.Entities
{
    public class PlaceToken
    {
        [Key]
        public int token_id { get; set; }
        public int player_id { get; set; }
        public int game_id { get; set; }
        public int row { get; set; }
        public int col { get; set; }

    }
}
