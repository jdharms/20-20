namespace _20
{
    partial class CustomGame
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
            this.label1 = new System.Windows.Forms.Label();
            this.gameVenueTextBox = new System.Windows.Forms.TextBox();
            this.homeNameLabel = new System.Windows.Forms.Label();
            this.editHomeButton = new System.Windows.Forms.Button();
            this.useGameButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.awayNameLabel = new System.Windows.Forms.Label();
            this.editAwayButton = new System.Windows.Forms.Button();
            this.flipButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Game Venue";
            // 
            // gameVenueTextBox
            // 
            this.gameVenueTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameVenueTextBox.Location = new System.Drawing.Point(123, 10);
            this.gameVenueTextBox.Name = "gameVenueTextBox";
            this.gameVenueTextBox.Size = new System.Drawing.Size(297, 26);
            this.gameVenueTextBox.TabIndex = 1;
            this.gameVenueTextBox.TextChanged += new System.EventHandler(this.gameVenueTextBox_TextChanged);
            // 
            // homeNameLabel
            // 
            this.homeNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeNameLabel.Location = new System.Drawing.Point(12, 34);
            this.homeNameLabel.Name = "homeNameLabel";
            this.homeNameLabel.Size = new System.Drawing.Size(403, 27);
            this.homeNameLabel.TabIndex = 4;
            this.homeNameLabel.Text = "None";
            this.homeNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editHomeButton
            // 
            this.editHomeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editHomeButton.Location = new System.Drawing.Point(118, 64);
            this.editHomeButton.Name = "editHomeButton";
            this.editHomeButton.Size = new System.Drawing.Size(187, 39);
            this.editHomeButton.TabIndex = 6;
            this.editHomeButton.Text = "Edit Home Team";
            this.editHomeButton.UseVisualStyleBackColor = true;
            this.editHomeButton.Click += new System.EventHandler(this.editHomeButton_Click);
            // 
            // useGameButton
            // 
            this.useGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useGameButton.Location = new System.Drawing.Point(17, 305);
            this.useGameButton.Name = "useGameButton";
            this.useGameButton.Size = new System.Drawing.Size(187, 39);
            this.useGameButton.TabIndex = 8;
            this.useGameButton.Text = "Use Game";
            this.useGameButton.UseVisualStyleBackColor = true;
            this.useGameButton.Click += new System.EventHandler(this.useGameButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(233, 305);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(187, 39);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.homeNameLabel);
            this.groupBox1.Controls.Add(this.editHomeButton);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 112);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Home";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.awayNameLabel);
            this.groupBox2.Controls.Add(this.editAwayButton);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(5, 187);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(429, 112);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Away";
            // 
            // awayNameLabel
            // 
            this.awayNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.awayNameLabel.Location = new System.Drawing.Point(12, 34);
            this.awayNameLabel.Name = "awayNameLabel";
            this.awayNameLabel.Size = new System.Drawing.Size(403, 27);
            this.awayNameLabel.TabIndex = 4;
            this.awayNameLabel.Text = "None";
            this.awayNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editAwayButton
            // 
            this.editAwayButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editAwayButton.Location = new System.Drawing.Point(118, 64);
            this.editAwayButton.Name = "editAwayButton";
            this.editAwayButton.Size = new System.Drawing.Size(187, 39);
            this.editAwayButton.TabIndex = 6;
            this.editAwayButton.Text = "Edit Away Team";
            this.editAwayButton.UseVisualStyleBackColor = true;
            this.editAwayButton.Click += new System.EventHandler(this.editAwayButton_Click);
            // 
            // flipButton
            // 
            this.flipButton.Location = new System.Drawing.Point(160, 168);
            this.flipButton.Name = "flipButton";
            this.flipButton.Size = new System.Drawing.Size(116, 23);
            this.flipButton.TabIndex = 12;
            this.flipButton.Text = "Flip Home/Away";
            this.flipButton.UseVisualStyleBackColor = true;
            this.flipButton.Click += new System.EventHandler(this.flipButton_Click);
            // 
            // CustomGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 356);
            this.ControlBox = false;
            this.Controls.Add(this.flipButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.useGameButton);
            this.Controls.Add(this.gameVenueTextBox);
            this.Controls.Add(this.label1);
            this.Name = "CustomGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Custom Game";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox gameVenueTextBox;
        private System.Windows.Forms.Label homeNameLabel;
        private System.Windows.Forms.Button editHomeButton;
        private System.Windows.Forms.Button useGameButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label awayNameLabel;
        private System.Windows.Forms.Button editAwayButton;
        private System.Windows.Forms.Button flipButton;
    }
}