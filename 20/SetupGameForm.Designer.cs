namespace _20
{
    partial class SetupGameForm
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
            this.homeTeamGroupBox = new System.Windows.Forms.GroupBox();
            this.homeStartersLabel = new System.Windows.Forms.Label();
            this.homeStartersListBox = new System.Windows.Forms.ListBox();
            this.homeTeamListBox = new System.Windows.Forms.ListBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.awayStartersListBox = new System.Windows.Forms.ListBox();
            this.awayTeamGroupBox = new System.Windows.Forms.GroupBox();
            this.awayStartersLabel = new System.Windows.Forms.Label();
            this.awayTeamListBox = new System.Windows.Forms.ListBox();
            this.exitButton = new System.Windows.Forms.Button();
            this.homeTeamGroupBox.SuspendLayout();
            this.awayTeamGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // homeTeamGroupBox
            // 
            this.homeTeamGroupBox.Controls.Add(this.homeStartersLabel);
            this.homeTeamGroupBox.Controls.Add(this.homeStartersListBox);
            this.homeTeamGroupBox.Controls.Add(this.homeTeamListBox);
            this.homeTeamGroupBox.Location = new System.Drawing.Point(12, 12);
            this.homeTeamGroupBox.Name = "homeTeamGroupBox";
            this.homeTeamGroupBox.Size = new System.Drawing.Size(254, 488);
            this.homeTeamGroupBox.TabIndex = 0;
            this.homeTeamGroupBox.TabStop = false;
            this.homeTeamGroupBox.Text = "Home Team";
            // 
            // homeStartersLabel
            // 
            this.homeStartersLabel.AutoSize = true;
            this.homeStartersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeStartersLabel.Location = new System.Drawing.Point(6, 355);
            this.homeStartersLabel.Name = "homeStartersLabel";
            this.homeStartersLabel.Size = new System.Drawing.Size(157, 20);
            this.homeStartersLabel.TabIndex = 2;
            this.homeStartersLabel.Text = "Home Team Starters";
            // 
            // homeStartersListBox
            // 
            this.homeStartersListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeStartersListBox.FormattingEnabled = true;
            this.homeStartersListBox.ItemHeight = 20;
            this.homeStartersListBox.Items.AddRange(new object[] {
            "Starter 1",
            "Starter 2",
            "Starter 3",
            "Starter 4",
            "Starter 5"});
            this.homeStartersListBox.Location = new System.Drawing.Point(6, 378);
            this.homeStartersListBox.Name = "homeStartersListBox";
            this.homeStartersListBox.Size = new System.Drawing.Size(241, 104);
            this.homeStartersListBox.Sorted = true;
            this.homeStartersListBox.TabIndex = 1;
            this.homeStartersListBox.Click += new System.EventHandler(this.homeStartersListBox_Click);
            // 
            // homeTeamListBox
            // 
            this.homeTeamListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeTeamListBox.FormattingEnabled = true;
            this.homeTeamListBox.ItemHeight = 20;
            this.homeTeamListBox.Items.AddRange(new object[] {
            "Player 1",
            "Player 10",
            "Player 11",
            "Player 12",
            "Player 13",
            "Player 14",
            "Player 15",
            "Player 16",
            "Player 2",
            "Player 3",
            "Player 4",
            "Player 5",
            "Player 6",
            "Player 7",
            "Player 8",
            "Player 9"});
            this.homeTeamListBox.Location = new System.Drawing.Point(6, 19);
            this.homeTeamListBox.Name = "homeTeamListBox";
            this.homeTeamListBox.Size = new System.Drawing.Size(241, 324);
            this.homeTeamListBox.Sorted = true;
            this.homeTeamListBox.TabIndex = 0;
            this.homeTeamListBox.Click += new System.EventHandler(this.homeTeamListBox_Click);
            // 
            // submitButton
            // 
            this.submitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.Location = new System.Drawing.Point(108, 506);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(158, 34);
            this.submitButton.TabIndex = 2;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // awayStartersListBox
            // 
            this.awayStartersListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.awayStartersListBox.FormattingEnabled = true;
            this.awayStartersListBox.ItemHeight = 20;
            this.awayStartersListBox.Location = new System.Drawing.Point(6, 378);
            this.awayStartersListBox.Name = "awayStartersListBox";
            this.awayStartersListBox.Size = new System.Drawing.Size(241, 104);
            this.awayStartersListBox.Sorted = true;
            this.awayStartersListBox.TabIndex = 1;
            this.awayStartersListBox.Click += new System.EventHandler(this.awayStartersListBox_Click);
            // 
            // awayTeamGroupBox
            // 
            this.awayTeamGroupBox.Controls.Add(this.awayStartersLabel);
            this.awayTeamGroupBox.Controls.Add(this.awayStartersListBox);
            this.awayTeamGroupBox.Controls.Add(this.awayTeamListBox);
            this.awayTeamGroupBox.Location = new System.Drawing.Point(272, 13);
            this.awayTeamGroupBox.Name = "awayTeamGroupBox";
            this.awayTeamGroupBox.Size = new System.Drawing.Size(254, 488);
            this.awayTeamGroupBox.TabIndex = 3;
            this.awayTeamGroupBox.TabStop = false;
            this.awayTeamGroupBox.Text = "Away Team";
            // 
            // awayStartersLabel
            // 
            this.awayStartersLabel.AutoSize = true;
            this.awayStartersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.awayStartersLabel.Location = new System.Drawing.Point(6, 355);
            this.awayStartersLabel.Name = "awayStartersLabel";
            this.awayStartersLabel.Size = new System.Drawing.Size(152, 20);
            this.awayStartersLabel.TabIndex = 2;
            this.awayStartersLabel.Text = "Away Team Starters";
            // 
            // awayTeamListBox
            // 
            this.awayTeamListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.awayTeamListBox.FormattingEnabled = true;
            this.awayTeamListBox.ItemHeight = 20;
            this.awayTeamListBox.Items.AddRange(new object[] {
            "Player 1",
            "Player 10",
            "Player 11",
            "Player 12",
            "Player 13",
            "Player 14",
            "Player 15",
            "Player 16",
            "Player 2",
            "Player 3",
            "Player 4",
            "Player 5",
            "Player 6",
            "Player 7",
            "Player 8",
            "Player 9"});
            this.awayTeamListBox.Location = new System.Drawing.Point(6, 19);
            this.awayTeamListBox.Name = "awayTeamListBox";
            this.awayTeamListBox.Size = new System.Drawing.Size(241, 324);
            this.awayTeamListBox.Sorted = true;
            this.awayTeamListBox.TabIndex = 0;
            this.awayTeamListBox.Click += new System.EventHandler(this.awayTeamListBox_Click);
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(272, 506);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(158, 34);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // SetupGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 549);
            this.ControlBox = false;
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.awayTeamGroupBox);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.homeTeamGroupBox);
            this.Name = "SetupGameForm";
            this.Text = "Setup Game";
            this.Load += new System.EventHandler(this.SetupGameForm_Load);
            this.homeTeamGroupBox.ResumeLayout(false);
            this.homeTeamGroupBox.PerformLayout();
            this.awayTeamGroupBox.ResumeLayout(false);
            this.awayTeamGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox homeTeamListBox;
        private System.Windows.Forms.GroupBox homeTeamGroupBox;
        private System.Windows.Forms.Label homeStartersLabel;
        private System.Windows.Forms.ListBox homeStartersListBox;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.ListBox awayStartersListBox;
        private System.Windows.Forms.GroupBox awayTeamGroupBox;
        private System.Windows.Forms.Label awayStartersLabel;
        private System.Windows.Forms.ListBox awayTeamListBox;
        private System.Windows.Forms.Button exitButton;
    }
}