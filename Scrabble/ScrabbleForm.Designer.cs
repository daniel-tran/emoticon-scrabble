namespace Scrabble
{
    partial class ScrabbleForm
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
        private void InitializeComponent()
        {
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPass = new System.Windows.Forms.Button();
            this.btnSwap = new System.Windows.Forms.Button();
            this.lblLog = new System.Windows.Forms.Label();
            this.groupBoxPlayers = new System.Windows.Forms.GroupBox();
            this.lblPlayerOne = new System.Windows.Forms.Label();
            this.lblPlayerTwo = new System.Windows.Forms.Label();
            this.lblCurrentTurn = new System.Windows.Forms.Label();
            this.groupBoxPlayers.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnPlay.Location = new System.Drawing.Point(701, 830);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(98, 96);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPass
            // 
            this.btnPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnPass.Location = new System.Drawing.Point(49, 830);
            this.btnPass.Name = "btnPass";
            this.btnPass.Size = new System.Drawing.Size(98, 45);
            this.btnPass.TabIndex = 1;
            this.btnPass.Text = "Pass";
            this.btnPass.UseVisualStyleBackColor = false;
            this.btnPass.Click += new System.EventHandler(this.btnPass_Click);
            // 
            // btnSwap
            // 
            this.btnSwap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSwap.Location = new System.Drawing.Point(49, 881);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(98, 45);
            this.btnSwap.TabIndex = 2;
            this.btnSwap.Text = "Swap";
            this.btnSwap.UseVisualStyleBackColor = false;
            this.btnSwap.Click += new System.EventHandler(this.btnSwap_Click);
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(837, 136);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(56, 13);
            this.lblLog.TabIndex = 4;
            this.lblLog.Text = "Game Log";
            // 
            // groupBoxPlayers
            // 
            this.groupBoxPlayers.Controls.Add(this.lblCurrentTurn);
            this.groupBoxPlayers.Controls.Add(this.lblPlayerTwo);
            this.groupBoxPlayers.Controls.Add(this.lblPlayerOne);
            this.groupBoxPlayers.Location = new System.Drawing.Point(840, 13);
            this.groupBoxPlayers.Name = "groupBoxPlayers";
            this.groupBoxPlayers.Size = new System.Drawing.Size(320, 111);
            this.groupBoxPlayers.TabIndex = 5;
            this.groupBoxPlayers.TabStop = false;
            this.groupBoxPlayers.Text = "Score";
            // 
            // lblPlayerOne
            // 
            this.lblPlayerOne.AutoSize = true;
            this.lblPlayerOne.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerOne.Location = new System.Drawing.Point(7, 20);
            this.lblPlayerOne.Name = "lblPlayerOne";
            this.lblPlayerOne.Size = new System.Drawing.Size(58, 18);
            this.lblPlayerOne.TabIndex = 0;
            this.lblPlayerOne.Text = "label1";
            // 
            // lblPlayerTwo
            // 
            this.lblPlayerTwo.AutoSize = true;
            this.lblPlayerTwo.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerTwo.Location = new System.Drawing.Point(6, 50);
            this.lblPlayerTwo.Name = "lblPlayerTwo";
            this.lblPlayerTwo.Size = new System.Drawing.Size(58, 18);
            this.lblPlayerTwo.TabIndex = 1;
            this.lblPlayerTwo.Text = "label1";
            // 
            // lblCurrentTurn
            // 
            this.lblCurrentTurn.AutoSize = true;
            this.lblCurrentTurn.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentTurn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblCurrentTurn.Location = new System.Drawing.Point(7, 80);
            this.lblCurrentTurn.Name = "lblCurrentTurn";
            this.lblCurrentTurn.Size = new System.Drawing.Size(60, 18);
            this.lblCurrentTurn.TabIndex = 2;
            this.lblCurrentTurn.Text = "label2";
            // 
            // ScrabbleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 949);
            this.Controls.Add(this.groupBoxPlayers);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.btnSwap);
            this.Controls.Add(this.btnPass);
            this.Controls.Add(this.btnPlay);
            this.MaximizeBox = false;
            this.Name = "ScrabbleForm";
            this.Text = "Scrabble!";
            this.groupBoxPlayers.ResumeLayout(false);
            this.groupBoxPlayers.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnPlay;
        public System.Windows.Forms.Button btnPass;
        public System.Windows.Forms.Button btnSwap;
        public System.Windows.Forms.Label lblLog;
        public System.Windows.Forms.GroupBox groupBoxPlayers;
        public System.Windows.Forms.Label lblCurrentTurn;
        public System.Windows.Forms.Label lblPlayerTwo;
        public System.Windows.Forms.Label lblPlayerOne;
    }
}