using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tictactoe.Data.Entities
{
    /*
      "game_id": "",
        "players": [],
        "outcome": "ACTIVE/GAME_OVER",
        "winner": [],
     */
    public class GameStatus
    {
        [Key]
        public int Status_Id { get; set; }
        public int Game_Id { get; set; }
        public ICollection<Player> players { get; set; }
        public string Outcome { get; set; }
        public ICollection<Winner> Winners { get; set; }
    }
}
