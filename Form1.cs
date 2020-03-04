using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class Form1 : Form
    {
        #region
        ChessBoardManage ChessBoard;
        
        #endregion
        public Form1()
        {
            InitializeComponent();

            ChessBoard = new ChessBoardManage(pnlChessBoard, txbnamePlayer, pBMark, avatar);
            // Ủy thác 2 event
            ChessBoard.EndedGame += ChessBoard_EndedGame;
            ChessBoard.PlayerMarked += ChessBoard_PlayerMarked;

            
            prBCountTime.Step = Cons.COUNT_DOWN_STEP;
            prBCountTime.Maximum = Cons.COUNT_DOWN_TIME;
            prBCountTime.Value = 0;

            timeCountDown.Interval = Cons.COUNT_DOWN_INTERVAL;

            ChessBoard.DrawChessBoard();

            NewGame();
            //timeCountDown.Start();  khi người chơi Marked thì reset lại ProcessBar
        }
        void EndGame()
        {
            timeCountDown.Stop(); // dừng lại 
            MessageBox.Show("End Game!");
        }
        private void ChessBoard_PlayerMarked(object sender, EventArgs e)
        {
            //Mỗi khi người chơi đánh, Start time và reset ProcessBar
            timeCountDown.Start(); // Nếu hết time rồi sẽ gọi đến hàm EndGame
            prBCountTime.Value = 0;
        }

        private void ChessBoard_EndedGame(object sender, EventArgs e)
        {
            
            EndGame();
            pnlChessBoard.Enabled = false; // Ban Click Button khi end game
            prBCountTime.Value = 0;
            

        }

        private void timeCountDown_Tick(object sender, EventArgs e)
        {
            prBCountTime.PerformStep();

            if (prBCountTime.Value == prBCountTime.Maximum)
            {
                //timeCountDown.Stop();
                EndGame();
                
            }
        }

        void NewGame()
        {
            prBCountTime.Value = 0;
            timeCountDown.Stop();
            ChessBoard.DrawChessBoard();
        }
        void Undo()
        {

        }

        void Quit()
        {
            //if (MessageBox.Show("Bạn muốn thoát game?", "Alert!", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            Application.Exit();
        }
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                e.Cancel = true;
        }
    
    }
    
}
