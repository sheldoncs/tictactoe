using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tictactoe.Data.Entities;
using tictactoe.Dto;

namespace tictactoe.Business
{
    public class LookForWin
    {
        private readonly IReadOnlyCollection<PlaceTokenDto> _placeTokens;
        private readonly int _row;
        private readonly int _col;
        private readonly int _player_id;
        private readonly int _game_id;
        private const int max_cell_cnt = 2;
       
        
        public LookForWin(int game_id, int player_id, int row, int col, IReadOnlyCollection<PlaceTokenDto> placeTokens)
        {
            _placeTokens = placeTokens;
            _row = row;
            _col = col;
            _player_id = player_id;
            _game_id = game_id;
        }
        public bool TestCol()
        {
            bool found = false;
            int place_cnt = 0;

            int temp_col = 0;
            while (temp_col <= max_cell_cnt) 
            { 
                    PlaceTokenDto placeToken = new PlaceTokenDto()
                    {
                        player_id = _player_id,
                        game_id = _game_id,
                        row = _row,
                        col = temp_col
                    };
                    
                    foreach(var item in _placeTokens)
                    {
                        string obj1 = JsonConvert.SerializeObject(item);
                        string obj2 = JsonConvert.SerializeObject(placeToken);
                    
                        if (obj1.Equals(obj2))
                        {
                           place_cnt++;
                           break;
                        }
                    }
                    temp_col++;
            }
            if (place_cnt == 3)
            {
                found = true;
            }  
            return found;
        }
        public bool TestRow()
        {
            bool found = false;
            int place_cnt = 0;

            int temp_row = 0;
            while (temp_row <= max_cell_cnt)
            {
                PlaceTokenDto placeToken = new PlaceTokenDto()
                {
                    player_id = _player_id,
                    game_id = _game_id,
                    row = temp_row,
                    col = _col
                };

                foreach (var item in _placeTokens)
                {
                    string obj1 = JsonConvert.SerializeObject(item);
                    string obj2 = JsonConvert.SerializeObject(placeToken);

                    if (obj1.Equals(obj2))
                    {
                        place_cnt++;
                        break;
                    }
                }
                temp_row++;
            }
            if (place_cnt == 3)
            {
                found = true;
            }
            return found;
        }
        public bool TestRightToLeftDiag()
        {

            int temp_row = 0;
            int temp_col = 0;
            int cell_cnt = 0;
            int place_cnt = 0;
            bool found = false;

            while (cell_cnt < 3)
            {
                PlaceTokenDto placeToken = new PlaceTokenDto()
                {
                    player_id = _player_id,
                    game_id = _game_id,
                    row = temp_row,
                    col = temp_col
                };
                foreach (var item in _placeTokens)
                {

                    string obj1 = JsonConvert.SerializeObject(item);
                    string obj2 = JsonConvert.SerializeObject(placeToken);

                    if (obj1.Equals(obj2))
                    {
                        place_cnt++;
                        break;
                    }

                }
                temp_row++;
                temp_col++;
                cell_cnt++;
            }
            if (place_cnt == 3)
            {
                found = true;
            }
            
            return found;
           
        }
        public bool TestLeftToRightDiag()
        {

            int temp_row = 0;
            int temp_col = 2;
            int cell_cnt = 0;
            int place_cnt = 0;
            bool found = false;

            while (cell_cnt < 3)
            {
                PlaceTokenDto placeToken = new PlaceTokenDto()
                {
                    player_id = _player_id,
                    game_id = _game_id,
                    row = temp_row,
                    col = temp_col
                };
                foreach (var item in _placeTokens)
                {
                    string obj1 = JsonConvert.SerializeObject(item);
                    string obj2 = JsonConvert.SerializeObject(placeToken);

                    if (obj1.Equals(obj2))
                    {
                        place_cnt++;
                        break;
                    }
                }
                temp_row++;
                temp_col--;
                cell_cnt++;
            }
            if (place_cnt == 3)
            {
                found = true;
            }
            return found;
        }
        
    }
}
