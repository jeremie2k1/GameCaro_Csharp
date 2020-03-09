using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class Form1 : Form
    {
        #region
        ChessBoardManage ChessBoard;
        SocketManager socket;
        
        #endregion
        public Form1()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false; // không check việc chạy chồng chéo giữa hai luồng,(đây là biện pháp tạm thời)

            ChessBoard = new ChessBoardManage(pnlChessBoard, txbnamePlayer, pBMark, avatar);
            // Ủy thác 2 event
            ChessBoard.EndedGame += ChessBoard_EndedGame;
            ChessBoard.PlayerMarked += ChessBoard_PlayerMarked;

            
            prBCountTime.Step = Cons.COUNT_DOWN_STEP;
            prBCountTime.Maximum = Cons.COUNT_DOWN_TIME;
            prBCountTime.Value = 0;

            timeCountDown.Interval = Cons.COUNT_DOWN_INTERVAL;

            ChessBoard.DrawChessBoard();
            socket = new SocketManager();
            NewGame();
            //timeCountDown.Start();  khi người chơi Marked thì reset lại ProcessBar
            
        }
        //public static int count = 0; //  đếm số undo để refresh processBar
        
        #region Method
        void EndGame()
        {
            timeCountDown.Stop(); // dừng lại 
            
            //MessageBox.Show("End Game!");

            pnlChessBoard.Enabled = false;
            undoToolStripMenuItem.Enabled = false;
        }
        private void ChessBoard_PlayerMarked(object sender, ButtonClickEvent e)
        {
            //Mỗi khi người chơi đánh, Start time và reset ProcessBar
            timeCountDown.Start(); // Nếu hết time rồi sẽ gọi đến hàm EndGame
            prBCountTime.Value = 0;

            socket.Send(new SocketData((int)SocketCommand.SEND_POINT, "", e.ClickedPoint)); // test thử gửi Point
            undoToolStripMenuItem.Enabled = false;
            Listen();
        }

        private void ChessBoard_EndedGame(object sender, EventArgs e)
        {
            socket.Send(new SocketData((int)SocketCommand.END_GAME, "", new Point()));
            EndGame();
            // Ban Click Button khi end game
            prBCountTime.Value = 0;


        }

        private void timeCountDown_Tick(object sender, EventArgs e)
        {
            prBCountTime.PerformStep();

            if (prBCountTime.Value == prBCountTime.Maximum)
            {
                //timeCountDown.Stop();
                socket.Send(new SocketData((int)SocketCommand.TIME_OUT, "", new Point()));
                EndGame();

            }
        }

        void NewGame()
        {
            
            prBCountTime.Value = 0;
            timeCountDown.Stop();
            undoToolStripMenuItem.Enabled = true;
            ChessBoard.DrawChessBoard();
        }

        void Undo()
        {
            // ý tưởng: khởi tạo một stack lưu bước đi của người chơi, stack sẽ lưu Object PlayerInfo
            
            ChessBoard.Undo();
            prBCountTime.Value = 0;
        }

        void Quit()
        {
            //if (MessageBox.Show("Bạn muốn thoát game?", "Alert!", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            Application.Exit();
        }
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
            socket.Send(new SocketData((int)SocketCommand.NEW_GAME, "", new Point()));
            pnlChessBoard.Enabled = true;
            
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            socket.Send(new SocketData((int)SocketCommand.UNDO, "", new Point()));
            Undo();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) // Giường như Button Quit của mình hay nói chính xác hơn là 
                                                                              // Method Application.Exit() là method của button x trên Form. Khi dùng nó ở một Button khác trên thanh menu như là quit thì 
                                                                              // Sử dụng như trên...

        {
            if (MessageBox.Show("Bạn muốn thoát game?", "Alert!", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
            else
            {
                try
                {
                    socket.Send(new SocketData((int)SocketCommand.QUIT, "", new Point())); //không có Point thì new Point() đại
                } catch { Exception ex; }
            }
        }

        private void btnConnectLAN_Click(object sender, EventArgs e)
        {
            socket.ip = txbIP.Text;

            if (!socket.ConnectedServer())
            {
                socket.CreatedServer();
                /*Bỏ khối code dưới và chỉ cần Listen()
                Listen();
                Thread listenThread = new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(500); // ngủ
                        try
                        {
                            Listen();                               // Toàn bộ khối block này chỉ để ngồi đợi kết nối từ Server
                            break;                                 // Không thực sự cần thiết vì còn xử lý người đánh trước, người đánh sau
                        }
                        catch { }

                    }

                });
                listenThread.IsBackground = true;
                listenThread.Start();*/

                // Xử lý cho server đánh trước
                socket.isServer = true;
                pnlChessBoard.Enabled = true;

            }
            else //Nếu không kết nối được thì ngồi Blind (Lắng nghe, chờ đợi các thứ)
            {
                /* để việc lắng nghe này vào một luồng riêng
                Thread listenThread = new Thread(() =>
                {
                    Listen();
                })
                {
                    IsBackground = true
                };
                 listenThread.Start();*/

                ///  Xử lý cho client đánh sau
                socket.isServer = false;
                pnlChessBoard.Enabled = false;

                Listen();
                //socket.Send(new SocketData((int)SocketCommand.NOTIFY, "Client đã kết nối", null)); // các object ko cho null thì thêm dấu chấm hỏi vào


                //socket.Send("Thông tin từ Client");
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            txbIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211); // get theo Internet hoặc Wifi - ở đây set Wifi
            if (string.IsNullOrEmpty(txbIP.Text))
            {
                txbIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
        }

        void Listen()
        {
            // Để Listen vào một luồng riêng
            
            try
            {
                Thread listendThread = new Thread(() =>
                {
                    SocketData data = (SocketData)socket.Receive();
                    ProccessData(data);
                });
                listendThread.IsBackground = true;
                listendThread.Start();
            }
            catch { Exception e; }

            
        }
        private void ProccessData(SocketData data)
        {
            switch(data.Command)
            {
                case (int)SocketCommand.NOTIFY:
                        MessageBox.Show(data.Message);
                    break;
                case (int)SocketCommand.NEW_GAME:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        NewGame();
                        pnlChessBoard.Enabled = false;
                    }));
                    break;
                case (int)SocketCommand.UNDO:
                        Undo();
                        prBCountTime.Value = 0;
                    break;
                case (int)SocketCommand.END_GAME:
                    MessageBox.Show("Trò chơi kết thúc!");
                    break;
                case (int)SocketCommand.TIME_OUT:
                    MessageBox.Show("Hết giờ!");
                    break;
                case (int)SocketCommand.QUIT:
                    // Đối thủ đột nhiên thoát
                    timeCountDown.Stop();
                    MessageBox.Show("Đối thủ đã thoát!");
                    break;
                case (int)SocketCommand.SEND_POINT:
                    this.Invoke((MethodInvoker)(() => // Kỹ thuật mà bạn phải thay đổi ở hai luông khác biệt (timeCountDown.Start() nằm ở Thread vẽ bàn cờ, còn luồng main chính và luồng gửi nhận dữ liệu lại chạy với Thread khác)
                    {
                        prBCountTime.Value = 0;
                        pnlChessBoard.Enabled = true;
                        timeCountDown.Start();
                        ChessBoard.OtherPlayerMark(data.Point);
                        undoToolStripMenuItem.Enabled = true;
                    }));
                        
                        
                    break;
                default:
                    break;
            }

            // Khi nhận được data thì sẽ kết thúc việc lắng nghe, như vậy sẽ không phù hợp với việc tương tác giữa client và server
            // Việc lắng nghe phải xảy ra tuần tự: người nói có người nghe

            Listen(); // trường hợp nếu Client đang lắng nghe Server, rồi Server thoát đột ngột => không còn người lắng nghe => Lỗi
            // Xử lý bằng cách đặt Listen() vào try catch
        }
        #endregion

    }
    
}
