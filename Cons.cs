using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro
{
    class Cons
    {
        //SIZE of Form1
        public static int FORM_WIDTH = 870;
        public static int FORM_HEIGHT = 577;

        // SIZE of one box
        public static int CHESS_WIDTH = 30;
        public static int CHESS_HEIGHT = 27;

        // SIZE of Chess Board

        public static int BOARD_WIDTH = 20;
        public static int BOARD_HEIGHT = 20;

        // Time Count Down
        public static int COUNT_DOWN_STEP = 100; // time increses ProcessBar
        public static int COUNT_DOWN_TIME = 10000;
        public static int COUNT_DOWN_INTERVAL = 100; // time tick

        //Socket Manager IP and Port
        public static string IP = "127.0.0.1"; //IP mặc định
        public static int PORT = 9999;

        //Độ dài Data giới hạn
        public static int BUFFER = 1024;
    }
}
