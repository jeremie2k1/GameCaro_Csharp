using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro
{
    class Player
    {
        private string Name; // Sinh get set nhanh: Ctrl + R + E

        public string Name1 { get => Name; set => Name = value; }
        public Image Mark { get => mark; set => mark = value; }
        public Image Avatar { get => avatar; set => avatar = value; }

        private Image mark;

        private Image avatar;
        // Constructor Player
        public Player(string Name, Image mark, Image avatar)
        {
            this.Name1 = Name;
            this.Mark = mark;
            this.Avatar = avatar;
        }

    }
}
