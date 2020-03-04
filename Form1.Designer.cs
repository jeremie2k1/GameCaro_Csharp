
using System.Windows.Forms;

namespace GameCaro
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 
        //public System.Drawing.Size size { get; set; }
        //private void DrawSizeForm()
        //{
        //    Size size = new Size(Cons.FORM_WIDTH, Cons.FORM_HEIGHT);
        //}
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pnlChessBoard = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.avatar = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnConnectLAN = new System.Windows.Forms.Button();
            this.txbIP = new System.Windows.Forms.TextBox();
            this.pBMark = new System.Windows.Forms.PictureBox();
            this.prBCountTime = new System.Windows.Forms.ProgressBar();
            this.txbnamePlayer = new System.Windows.Forms.TextBox();
            this.timeCountDown = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.avatar)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBMark)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlChessBoard
            // 
            this.pnlChessBoard.BackColor = System.Drawing.SystemColors.Control;
            this.pnlChessBoard.Location = new System.Drawing.Point(12, 40);
            this.pnlChessBoard.Name = "pnlChessBoard";
            this.pnlChessBoard.Size = new System.Drawing.Size(580, 555);
            this.pnlChessBoard.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.avatar);
            this.panel2.Location = new System.Drawing.Point(615, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(305, 303);
            this.panel2.TabIndex = 1;
            // 
            // avatar
            // 
            this.avatar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.avatar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.avatar.Image = global::GameCaro.Properties.Resources.icon;
            this.avatar.Location = new System.Drawing.Point(-3, 17);
            this.avatar.Name = "avatar";
            this.avatar.Size = new System.Drawing.Size(342, 283);
            this.avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.avatar.TabIndex = 0;
            this.avatar.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.btnConnectLAN);
            this.panel3.Controls.Add(this.txbIP);
            this.panel3.Controls.Add(this.pBMark);
            this.panel3.Controls.Add(this.prBCountTime);
            this.panel3.Controls.Add(this.txbnamePlayer);
            this.panel3.Location = new System.Drawing.Point(612, 368);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(330, 248);
            this.panel3.TabIndex = 2;
            // 
            // btnConnectLAN
            // 
            this.btnConnectLAN.Location = new System.Drawing.Point(42, 109);
            this.btnConnectLAN.Name = "btnConnectLAN";
            this.btnConnectLAN.Size = new System.Drawing.Size(71, 23);
            this.btnConnectLAN.TabIndex = 4;
            this.btnConnectLAN.Text = "Connect";
            this.btnConnectLAN.UseVisualStyleBackColor = true;
            // 
            // txbIP
            // 
            this.txbIP.Location = new System.Drawing.Point(3, 73);
            this.txbIP.Name = "txbIP";
            this.txbIP.Size = new System.Drawing.Size(165, 22);
            this.txbIP.TabIndex = 3;
            // 
            // pBMark
            // 
            this.pBMark.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pBMark.Location = new System.Drawing.Point(174, 6);
            this.pBMark.Name = "pBMark";
            this.pBMark.Size = new System.Drawing.Size(128, 126);
            this.pBMark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pBMark.TabIndex = 2;
            this.pBMark.TabStop = false;
            // 
            // prBCountTime
            // 
            this.prBCountTime.Location = new System.Drawing.Point(3, 44);
            this.prBCountTime.MarqueeAnimationSpeed = 10;
            this.prBCountTime.Name = "prBCountTime";
            this.prBCountTime.Size = new System.Drawing.Size(165, 23);
            this.prBCountTime.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prBCountTime.TabIndex = 1;
            this.prBCountTime.Value = 100;
            // 
            // txbnamePlayer
            // 
            this.txbnamePlayer.Location = new System.Drawing.Point(3, 16);
            this.txbnamePlayer.Name = "txbnamePlayer";
            this.txbnamePlayer.Size = new System.Drawing.Size(165, 22);
            this.txbnamePlayer.TabIndex = 0;
            // 
            // timeCountDown
            // 
            this.timeCountDown.Tick += new System.EventHandler(this.timeCountDown_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(954, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.menuToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(165, 26);
            this.newGameToolStripMenuItem.Text = "New Game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(165, 26);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(165, 26);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 620);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlChessBoard);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Game Caro LAN";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.avatar)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBMark)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlChessBoard;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox avatar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnConnectLAN;
        private System.Windows.Forms.TextBox txbIP;
        private System.Windows.Forms.PictureBox pBMark;
        private System.Windows.Forms.ProgressBar prBCountTime;
        private System.Windows.Forms.TextBox txbnamePlayer;
        private System.Windows.Forms.Timer timeCountDown;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem newGameToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
    }
}

