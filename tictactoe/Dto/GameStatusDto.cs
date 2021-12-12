using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using tictactoe.Data.Entities;

namespace tictactoe.Dto
{
    public class GameStatusDto
    {
        public int Game_Id { get; set; }
        public Collection<string> players { get; set; }
        public string Outcome { get; set; }
        public Collection<string> Winners { get; set; }

    }
}
