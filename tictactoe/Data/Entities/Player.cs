using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tictactoe.Data.Entities
{
    public class Player
    {
        [Key]
        public int Player_Id { get; set; }
        public int Game_Id { get; set; }
        public string Name { get; set; }
        public PlaceToken Token { get; set; }
       
    }
}
