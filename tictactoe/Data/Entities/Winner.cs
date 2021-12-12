using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tictactoe.Data.Entities
{
    public class Winner
    {
        [Key]
        public int Winner_Id { get; set; }
        public int Game_Id { get; set; }
        public int Player_Id { get; set; }
    }
}
