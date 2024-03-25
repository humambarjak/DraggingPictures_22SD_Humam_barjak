using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraggingPictures_22SD_Humam_barjak
{
    internal class Fighter
    {
        private string name;
        private bool onBoard;
        private int location;

        public Fighter(string c_name)
        {
            name = c_name;
            onBoard = false;
        }

        public string GetName()
        {
            return name;
        }

        public bool GetOnBoardValue()
        {
            return onBoard;
        }

        public int GetLocation()
        {
            return location;
        }

        public void SetOnBoard(bool onBoardValue, int locationValue)
        {
            onBoard = onBoardValue;
            location = locationValue;
        }
    }
}
