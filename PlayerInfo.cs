using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro
{
    public class PlayerInfo
    {
        // Thông tin về tọa độ điểm mà người chơi đã đánh dấu
        private Point point;
        public Point Point 
        {
            get {return point; }
            set {point = value; }
        
        }

        //Thông tin về người chơi hiện tại CurrentPlayer
        private int currentPlayer;
        public int CurrentPlayer 
        {
            get {return currentPlayer; }
            set {currentPlayer = value; } 
        }
        // constructor
        public PlayerInfo(Point point, int currentPlayer)
        {
            this.Point = point;
            this.CurrentPlayer = currentPlayer;
        }
    }
}
