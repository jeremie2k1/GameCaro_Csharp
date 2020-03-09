using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro
{
    [Serializable] // cho phép Serializable trên data này (cụ thể là Data dạng Point)
    class SocketData
    {
        
        private int command;

        public int Command
        {
            get { return command; }
            set {command = value; }
        }

        private Point point;
        public Point Point
        {
            get { return point; }
            set { point = value; }
        }

        private string message;
        public string Message 
        {
            get {return message; }
            set {message = value; } 
        }

        public SocketData(int command, string message, Point point) // các kiểu dữ liệu không cho phép null thì thêm dấu chấm hỏi vào sau kiểu dữ liệu
        {
            this.Command = command;
            this.Message = message;
            this.Point = point;
        }

        
    }
    public enum SocketCommand // tương ứng với các yêu cầu cần gửi đi
    {
        SEND_POINT, // enum là kiểu liệt kê, đánh dấu thứ tự các yêu cầu nên phải ép kiểu int
        NOTIFY,
        NEW_GAME,
        END_GAME,
        TIME_OUT,
        UNDO,
        QUIT

    }

}
