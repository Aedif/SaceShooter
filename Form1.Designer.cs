namespace SpaceShooter
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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlPaper = new System.Windows.Forms.Panel();
            this.lblTest = new System.Windows.Forms.Label();
            this.prbLife = new System.Windows.Forms.ProgressBar();
            this.prbEnergy = new System.Windows.Forms.ProgressBar();
            this.lblGameOver = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.tmrLevelDelay = new System.Windows.Forms.Timer(this.components);
            this.pnlPaper.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPaper
            // 
            this.pnlPaper.BackColor = System.Drawing.Color.White;
            this.pnlPaper.Controls.Add(this.lblTest);
            this.pnlPaper.Controls.Add(this.prbLife);
            this.pnlPaper.Controls.Add(this.prbEnergy);
            this.pnlPaper.Controls.Add(this.lblGameOver);
            this.pnlPaper.Controls.Add(this.lblLevel);
            this.pnlPaper.Location = new System.Drawing.Point(0, 0);
            this.pnlPaper.Name = "pnlPaper";
            this.pnlPaper.Size = new System.Drawing.Size(839, 647);
            this.pnlPaper.TabIndex = 1;
            this.pnlPaper.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPaper_Paint);
            this.pnlPaper.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlPaper_MouseDown);
            this.pnlPaper.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlPaper_MouseMove);
            this.pnlPaper.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlPaper_MouseUp);
            // 
            // lblTest
            // 
            this.lblTest.AutoSize = true;
            this.lblTest.Location = new System.Drawing.Point(615, 78);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(35, 13);
            this.lblTest.TabIndex = 9;
            this.lblTest.Text = "label1";
            this.lblTest.Visible = false;
            // 
            // prbLife
            // 
            this.prbLife.BackColor = System.Drawing.Color.Silver;
            this.prbLife.ForeColor = System.Drawing.Color.Lime;
            this.prbLife.Location = new System.Drawing.Point(12, 624);
            this.prbLife.Maximum = 20;
            this.prbLife.Name = "prbLife";
            this.prbLife.Size = new System.Drawing.Size(53, 13);
            this.prbLife.Step = 1;
            this.prbLife.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prbLife.TabIndex = 7;
            // 
            // prbEnergy
            // 
            this.prbEnergy.BackColor = System.Drawing.Color.Silver;
            this.prbEnergy.ForeColor = System.Drawing.Color.Red;
            this.prbEnergy.Location = new System.Drawing.Point(92, 624);
            this.prbEnergy.Maximum = 20;
            this.prbEnergy.Name = "prbEnergy";
            this.prbEnergy.Size = new System.Drawing.Size(53, 13);
            this.prbEnergy.Step = 1;
            this.prbEnergy.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prbEnergy.TabIndex = 6;
            // 
            // lblGameOver
            // 
            this.lblGameOver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGameOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameOver.Location = new System.Drawing.Point(292, 223);
            this.lblGameOver.Name = "lblGameOver";
            this.lblGameOver.Size = new System.Drawing.Size(274, 98);
            this.lblGameOver.TabIndex = 5;
            this.lblGameOver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGameOver.Visible = false;
            // 
            // lblLevel
            // 
            this.lblLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevel.Location = new System.Drawing.Point(292, 31);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(274, 20);
            this.lblLevel.TabIndex = 4;
            this.lblLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLevel.Visible = false;
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 33;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // tmrLevelDelay
            // 
            this.tmrLevelDelay.Interval = 4000;
            this.tmrLevelDelay.Tick += new System.EventHandler(this.tmrLevelDelay_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 646);
            this.Controls.Add(this.pnlPaper);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "SpaceShooter";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.pnlPaper.ResumeLayout(false);
            this.pnlPaper.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPaper;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblGameOver;
        private System.Windows.Forms.ProgressBar prbEnergy;
        private System.Windows.Forms.ProgressBar prbLife;
        private System.Windows.Forms.Timer tmrLevelDelay;
        private System.Windows.Forms.Label lblTest;
    }
}

