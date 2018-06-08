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
            // ScrabbleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 949);
            this.Controls.Add(this.btnSwap);
            this.Controls.Add(this.btnPass);
            this.Controls.Add(this.btnPlay);
            this.MaximizeBox = false;
            this.Name = "ScrabbleForm";
            this.Text = "Scrabble!";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPass;
        private System.Windows.Forms.Button btnSwap;
    }
}