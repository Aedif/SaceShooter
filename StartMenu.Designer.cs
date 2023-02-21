namespace SpaceShooter
{
    partial class StartMenu
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
            this.btnStartGame = new System.Windows.Forms.Button();
            this.btnControls = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnObjective = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartGame
            // 
            this.btnStartGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartGame.Location = new System.Drawing.Point(58, 26);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(153, 37);
            this.btnStartGame.TabIndex = 0;
            this.btnStartGame.Text = "Start Game";
            this.btnStartGame.UseVisualStyleBackColor = true;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // btnControls
            // 
            this.btnControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnControls.Location = new System.Drawing.Point(58, 69);
            this.btnControls.Name = "btnControls";
            this.btnControls.Size = new System.Drawing.Size(153, 37);
            this.btnControls.TabIndex = 1;
            this.btnControls.Text = "Controls";
            this.btnControls.UseVisualStyleBackColor = true;
            this.btnControls.Click += new System.EventHandler(this.btnControls_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(58, 220);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(153, 37);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnObjective
            // 
            this.btnObjective.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnObjective.Location = new System.Drawing.Point(58, 112);
            this.btnObjective.Name = "btnObjective";
            this.btnObjective.Size = new System.Drawing.Size(153, 37);
            this.btnObjective.TabIndex = 2;
            this.btnObjective.Text = "Objective";
            this.btnObjective.UseVisualStyleBackColor = true;
            this.btnObjective.Click += new System.EventHandler(this.btnObjective_Click);
            // 
            // StartMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(283, 284);
            this.Controls.Add(this.btnObjective);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnControls);
            this.Controls.Add(this.btnStartGame);
            this.Name = "StartMenu";
            this.Text = "StartMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnControls;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnObjective;
    }
}