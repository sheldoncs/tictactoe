using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tictactoe.Dto
{
    public class PlaceTokenDto
    {
        public int player_id { get; set; }
        public int game_id { get; set; }
        public int row { get; set; }
        public int col { get; set; }
    }
}
