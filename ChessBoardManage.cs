using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GameCaro
{
    public class ChessBoardManage
    {
        
            // Ý tưởng: Dùng một grid button
            // C# hỗ trợ đồ họa yếu nên không nên sử dụng thư viện đồ họa, nếu muốn thì học WPF

            // #region để gomcode
            #region Properties
            private Panel chessBoard;
            
            
            public Panel ChessBoard 
            {
                get { return chessBoard; }
                set { chessBoard = value; } 
            }

            internal List<Player> Player { get => player; set => player = value; }
            public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
            public TextBox PlayerName { get => playerName; set => playerName = value; }
            public PictureBox PlayerMark { get => playerMark; set => playerMark = value; }
            public PictureBox Avatar { get => avatar; set => avatar = value; }
            public List<List<Button>> Matrix { get; private set; }

            private List<Player> player;
            private int currentPlayer;

            private TextBox playerName;
            private PictureBox playerMark;
            private PictureBox avatar;

        // Tạo event đếm thời gian khi người chơi click và thông báo kết thúc game
            private event EventHandler playerMarked; // Xem lại phần Event
            public event EventHandler PlayerMarked
            {
                add
                {
                    playerMarked += value;
                }
                remove
                {
                    playerMarked -= value;
                }
            }

            private event EventHandler endedGame; // Xem lại phần Event
            public event EventHandler EndedGame
            {
                add
                {
                    endedGame += value;
                }
                remove
                {
                    endedGame -= value;
                }
            }
        #endregion

        #region Initialize

        public ChessBoardManage(Panel chessBoard, TextBox playerName, PictureBox playerMark, PictureBox avatar)
            {
                this.ChessBoard = chessBoard;
                this.PlayerName = playerName;
                this.PlayerMark = playerMark;
                this.Avatar = avatar;
                this.Player = new List<Player>() //Phát triển: Cho phép thay đổi tên người chơi
                {
                    new Player("Nga", Image.FromFile(Application.StartupPath + "\\Image\\x.png"), 
                        Image.FromFile(Application.StartupPath + "\\Image\\avatar.png")),
                    new Player("Jeremie", Image.FromFile(Application.StartupPath + "\\Image\\o.png"),
                        Image.FromFile(Application.StartupPath + "\\Image\\icon.png"))
                   
                };
                
                

            }
            #endregion

            #region Methods
            public void DrawChessBoard()
            {
                
                
                
                //ChangeAvatar(avatar);
                ChessBoard.Enabled = true; // trở về trạng thái ban đầu khi ban click lúc endgame (xem phần Form1 - void EndGame)
                ChessBoard.Controls.Clear(); // vẽ xong phải clear hết nếu không dính bug point.Y = -1;
                CurrentPlayer = 0;
                //Sau khi vẽ xong bàn cờ thì gọi luôn tên người chơi
                ChangePlayer();
                
                Matrix = new List<List<Button>>(); //khởi tạo Matrix lưu button đã đánh dấu để xử lý thắng thua

                Button oldButton = new Button { Width = 0, Location = new Point(0, 0) };// Height + Cons.CHESS_HEIGHT) };
                for (int i = 0; i < Cons.BOARD_HEIGHT; i++)
                {
                    
                    Matrix.Add(new List<Button>()); // tạo ra một hàng mới để add button

                    for (int j = 0; j < Cons.BOARD_WIDTH; j++)
                    {
                        
                    
                        Button btn = new Button();

                        // Việc tạo size cho button hay những hằng số cố định, nên tạo riêng 1 class để lần sau chỉ cần sửa trong class là được
                        btn.Width = Cons.CHESS_WIDTH;
                        btn.Height = Cons.CHESS_HEIGHT;
                        btn.Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y);
                        //Setup layout cho button
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                        // Add button into Panel
                        ChessBoard.Controls.Add(btn); // pnlChessBoard nằm bên form nên phải xử lý bằng cách tạo hàm dựng
                        btn.Click += btn_Click;
                        btn.Tag = i.ToString();
                        
                        Matrix[i].Add(btn); // add button
                     
                        oldButton = btn;
                        
                        
                    }
                    

                    oldButton.Location = new Point(0, oldButton.Location.Y + Cons.CHESS_HEIGHT);
                    oldButton.Width = 0;
                    oldButton.Height = 0;
                }
                
            }

            private void btn_Click(object sender, EventArgs e)
            {
                Button button = sender as Button;
                //sender gửi event click
            
                //button.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Image\\x.png");
                if (button.BackgroundImage != null)
                {
                    return;
                }
                Mark(button);
            // phải đưa event playermarked rồi mới ktra endgame
                ChangePlayer();
                ChangeAvatar(avatar);
                if (playerMark != null)
                {
                    playerMarked(this, new EventArgs()); // đưa event lên Form1
                }

                

                if (isEndGame(button))
                {
                    EndGame();
                }

                
            }
            // Hàm EndGame: Sẽ được gọi ra từ Form
            // Nếu class ChessBoardManage chứa hàm End Game thì khi kết thúc thời gian đếm, Lớp Form1 sẽ gọi hàm EndGame
            // Hoặc nếu Class Form1 chứa hàm End Game thì khi xử lý thắng thua được kết quả true, lớp ChessBoardManage sẽ gọi tới hàm EndGame nằm trong lớp Form1
            // Hoặc tạo riêng một Class: Game Manage để chứa hàm end game. Howkteam đưa hàm EndGame lên Form1
            public void EndGame()
            {
                //MessageBox.Show("End Game!!!"); //Cụ thể người chơi chiến thắng
                if (endedGame != null)
                {
                    // gọi hàm end game
                    endedGame(this, new EventArgs()); // đưa event lên Form1
                }
            }
            private bool isEndGame(Button button) // tách ra xử lý với các trường hợp EndHorizontal, EndVertical, EndPrimaryDiagonal, EndSubDiagonal
            {
                return isEndHorizontal(button) || isEndVertical(button) || isEndPrimaryDiagonal(button) || isEndSubDiagonal(button);
            }
            private Point GetChessPoint(Button button)
            {
                int vertical = Convert.ToInt32(button.Tag); // lấy tọa độ hàng
                int horizontal = Matrix[vertical].IndexOf(button); // lấy tọa độ cột
                Point point = new Point(horizontal, vertical); // khởi tạo point với tọa độ của button hiện tại
                                                               //point.X = horizontal;
                                                               //point.Y = vertical;
                
                //MessageBox.Show(horizontal.ToString());
                
                return point;
            }
            private bool isEndHorizontal(Button button)
            {
                Point point = GetChessPoint(button);
                int countLeft = 0;
                for (int i = point.X; i >= 0; i--)
                {
                
                if (Matrix[point.Y][i].BackgroundImage == button.BackgroundImage)
                {
                    countLeft++;
                }
                else break;
                }   
                int countRight = 0;
                for (int i = point.X + 1; i < Cons.BOARD_WIDTH; i++)
                {
                if (Matrix[point.Y][i].BackgroundImage == button.BackgroundImage)
                {
                    countRight++;
                }
                else break;
                }
                return countLeft + countRight == 5;
            }
            private bool isEndVertical(Button button)
            {
                Point point = GetChessPoint(button);
                int countUp = 0;
                for (int i = point.Y; i >= 0; i--)
                {
                
                    if (Matrix[i][point.X].BackgroundImage == button.BackgroundImage)
                    {
                        countUp++;
                    }
                    else break;
                }
                int countDown = 0;
                for (int i = point.Y + 1; i < Cons.BOARD_HEIGHT; i++)
                {
                    if (Matrix[i][point.X].BackgroundImage == button.BackgroundImage)
                    {
                        countDown++;
                    }
                else break;    
                }
            return countDown + countUp == 5;
            }
            private bool isEndPrimaryDiagonal(Button button)
            {
                Point point = GetChessPoint(button);
                
                int countLU = 0, countRD = 0;

            for (int i = 0; i <= point.X; i++)
                {
                if (point.X - i < 0 || point.Y - i < 0) break;
                if (Matrix[point.Y - i][point.X - i].BackgroundImage == button.BackgroundImage)
                {
                    countLU++;
                    
                }
                else break;
                }
                
            for (int i = 1; i <= Cons.BOARD_HEIGHT - point.X; i++)
                {
                if (point.X + i >= Cons.BOARD_WIDTH || point.Y + i >= Cons.BOARD_HEIGHT) break;
                if (Matrix[point.Y + i][point.X + i].BackgroundImage == button.BackgroundImage)
                {
                    countRD++;
               
                }
                else break;
                }

            return countRD + countLU == 5;
            }
            private bool isEndSubDiagonal(Button button)
            {
            Point point = GetChessPoint(button);

            int countRU = 0, countLD = 0;

            for (int i = 0; i <= point.Y; i++)
            {
                if (point.Y + i >= Cons.BOARD_HEIGHT || point.X - i < 0) break;
                if (Matrix[point.Y + i][point.X - i].BackgroundImage == button.BackgroundImage)
                {
                    countRU++;

                }
                else break;
            }

            for (int i = 1; i <= Cons.BOARD_WIDTH - point.Y; i++)
            {
                if (point.Y - i < 0 || point.X + i >= Cons.BOARD_WIDTH) break;
                if (Matrix[point.Y - i][point.X + i].BackgroundImage == button.BackgroundImage)
                {
                    countLD++;

                }
                else break;
            }

            return countLD + countRU == 5;
        }
            private void Mark(Button button)
            {
                button.BackgroundImage = Player[CurrentPlayer].Mark;
                CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;

            }
            private void ChangePlayer()
            {
                //đổi tên người chơi và mark;
                PlayerName.Text = Player[CurrentPlayer].Name1;
                PlayerMark.Image = Player[CurrentPlayer].Mark;
                Avatar.Image = Player[CurrentPlayer].Avatar;

            }
            private void ChangeAvatar(PictureBox avatar)
            {
                avatar.BackgroundImage = Player[CurrentPlayer].Avatar;
                //CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
            }
            #endregion
    }   
}
/* Phần đếm thời gian bằng thanh processBar: 
 * Kết hợp processBar với Timer
 * Timer là một luồng riêng giúp đếm thời gian
 * Nếu bỏ vào vòng lặp thì game cũng sẽ bị sleep*/