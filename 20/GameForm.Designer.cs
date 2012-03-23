namespace _20
{
    partial class GameForm
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
            this.courtBox = new System.Windows.Forms.PictureBox();
            this.reboundButton = new System.Windows.Forms.Button();
            this.periodStartButton = new System.Windows.Forms.Button();
            this.historyBox = new System.Windows.Forms.ListBox();
            this.substitutionButton = new System.Windows.Forms.Button();
            this.timeoutButton = new System.Windows.Forms.Button();
            this.deleteEventButton = new System.Windows.Forms.Button();
            this.jumpBallButton = new System.Windows.Forms.Button();
            this.turnoverButton = new System.Windows.Forms.Button();
            this.foulButton = new System.Windows.Forms.Button();
            this.homeBox = new System.Windows.Forms.GroupBox();
            this.homeScore = new System.Windows.Forms.Label();
            this.homeNameLabel = new System.Windows.Forms.Label();
            this.awayBox = new System.Windows.Forms.GroupBox();
            this.awayScore = new System.Windows.Forms.Label();
            this.awayNameLabel = new System.Windows.Forms.Label();
            this.alpacaButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.courtBox)).BeginInit();
            this.homeBox.SuspendLayout();
            this.awayBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // courtBox
            // 
            this.courtBox.Image = global::_20.Properties.Resources.BasketballCourt;
            this.courtBox.Location = new System.Drawing.Point(12, 124);
            this.courtBox.Name = "courtBox";
            this.courtBox.Size = new System.Drawing.Size(944, 504);
            this.courtBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.courtBox.TabIndex = 0;
            this.courtBox.TabStop = false;
            this.courtBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.courtBox_MouseDown);
            // 
            // reboundButton
            // 
            this.reboundButton.Location = new System.Drawing.Point(12, 634);
            this.reboundButton.Name = "reboundButton";
            this.reboundButton.Size = new System.Drawing.Size(164, 35);
            this.reboundButton.TabIndex = 1;
            this.reboundButton.Text = "Rebound";
            this.reboundButton.UseVisualStyleBackColor = true;
            this.reboundButton.Click += new System.EventHandler(this.reboundButton_Click);
            // 
            // periodStartButton
            // 
            this.periodStartButton.Location = new System.Drawing.Point(12, 676);
            this.periodStartButton.Name = "periodStartButton";
            this.periodStartButton.Size = new System.Drawing.Size(164, 35);
            this.periodStartButton.TabIndex = 5;
            this.periodStartButton.Text = "Period Start/End";
            this.periodStartButton.UseVisualStyleBackColor = true;
            this.periodStartButton.Click += new System.EventHandler(this.periodStartButton_Click);
            // 
            // historyBox
            // 
            this.historyBox.FormattingEnabled = true;
            this.historyBox.Items.AddRange(new object[] {
            "Event 1",
            "Event 2",
            "Event 3"});
            this.historyBox.Location = new System.Drawing.Point(962, 12);
            this.historyBox.Name = "historyBox";
            this.historyBox.Size = new System.Drawing.Size(195, 615);
            this.historyBox.TabIndex = 7;
            this.historyBox.SelectedValueChanged += new System.EventHandler(this.historyBox_SelectedValueChanged);
            this.historyBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.historyBox_DragDrop);
            this.historyBox.DragOver += new System.Windows.Forms.DragEventHandler(this.historyBox_DragOver);
            this.historyBox.Leave += new System.EventHandler(this.historyBox_Leave);
            this.historyBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.historyBox_MouseDown);
            // 
            // substitutionButton
            // 
            this.substitutionButton.Location = new System.Drawing.Point(352, 676);
            this.substitutionButton.Name = "substitutionButton";
            this.substitutionButton.Size = new System.Drawing.Size(164, 35);
            this.substitutionButton.TabIndex = 8;
            this.substitutionButton.Text = "Substitution";
            this.substitutionButton.UseVisualStyleBackColor = true;
            this.substitutionButton.Click += new System.EventHandler(this.substitutionButton_Click);
            // 
            // timeoutButton
            // 
            this.timeoutButton.Location = new System.Drawing.Point(182, 676);
            this.timeoutButton.Name = "timeoutButton";
            this.timeoutButton.Size = new System.Drawing.Size(164, 35);
            this.timeoutButton.TabIndex = 9;
            this.timeoutButton.Text = "Timeout";
            this.timeoutButton.UseVisualStyleBackColor = true;
            this.timeoutButton.Click += new System.EventHandler(this.timeoutButton_Click);
            // 
            // deleteEventButton
            // 
            this.deleteEventButton.Location = new System.Drawing.Point(962, 633);
            this.deleteEventButton.Name = "deleteEventButton";
            this.deleteEventButton.Size = new System.Drawing.Size(195, 31);
            this.deleteEventButton.TabIndex = 11;
            this.deleteEventButton.Text = "Delete Event";
            this.deleteEventButton.UseVisualStyleBackColor = true;
            this.deleteEventButton.Visible = false;
            this.deleteEventButton.Click += new System.EventHandler(this.deleteEventButton_Click);
            // 
            // jumpBallButton
            // 
            this.jumpBallButton.Location = new System.Drawing.Point(522, 635);
            this.jumpBallButton.Name = "jumpBallButton";
            this.jumpBallButton.Size = new System.Drawing.Size(164, 35);
            this.jumpBallButton.TabIndex = 3;
            this.jumpBallButton.Text = "Jump Ball";
            this.jumpBallButton.UseVisualStyleBackColor = true;
            this.jumpBallButton.Click += new System.EventHandler(this.jumpBallButton_Click);
            // 
            // turnoverButton
            // 
            this.turnoverButton.Location = new System.Drawing.Point(352, 635);
            this.turnoverButton.Name = "turnoverButton";
            this.turnoverButton.Size = new System.Drawing.Size(164, 35);
            this.turnoverButton.TabIndex = 2;
            this.turnoverButton.Text = "Turnover";
            this.turnoverButton.UseVisualStyleBackColor = true;
            this.turnoverButton.Click += new System.EventHandler(this.turnoverButton_Click);
            // 
            // foulButton
            // 
            this.foulButton.Location = new System.Drawing.Point(182, 635);
            this.foulButton.Name = "foulButton";
            this.foulButton.Size = new System.Drawing.Size(164, 35);
            this.foulButton.TabIndex = 6;
            this.foulButton.Text = "Foul";
            this.foulButton.UseVisualStyleBackColor = true;
            this.foulButton.Click += new System.EventHandler(this.foulButton_Click);
            // 
            // homeBox
            // 
            this.homeBox.Controls.Add(this.homeScore);
            this.homeBox.Controls.Add(this.homeNameLabel);
            this.homeBox.Location = new System.Drawing.Point(16, 19);
            this.homeBox.Name = "homeBox";
            this.homeBox.Size = new System.Drawing.Size(470, 99);
            this.homeBox.TabIndex = 12;
            this.homeBox.TabStop = false;
            this.homeBox.Text = "Home";
            // 
            // homeScore
            // 
            this.homeScore.AutoSize = true;
            this.homeScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.homeScore.Font = new System.Drawing.Font("Courier New", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeScore.Location = new System.Drawing.Point(358, 27);
            this.homeScore.Name = "homeScore";
            this.homeScore.Size = new System.Drawing.Size(108, 71);
            this.homeScore.TabIndex = 1;
            this.homeScore.Text = "00";
            // 
            // homeNameLabel
            // 
            this.homeNameLabel.AutoSize = true;
            this.homeNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeNameLabel.Location = new System.Drawing.Point(6, 16);
            this.homeNameLabel.Name = "homeNameLabel";
            this.homeNameLabel.Size = new System.Drawing.Size(241, 31);
            this.homeNameLabel.TabIndex = 0;
            this.homeNameLabel.Text = "Home Team Name";
            // 
            // awayBox
            // 
            this.awayBox.Controls.Add(this.awayScore);
            this.awayBox.Controls.Add(this.awayNameLabel);
            this.awayBox.Location = new System.Drawing.Point(492, 19);
            this.awayBox.Name = "awayBox";
            this.awayBox.Size = new System.Drawing.Size(470, 99);
            this.awayBox.TabIndex = 13;
            this.awayBox.TabStop = false;
            this.awayBox.Text = "Away";
            // 
            // awayScore
            // 
            this.awayScore.AutoSize = true;
            this.awayScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.awayScore.Font = new System.Drawing.Font("Courier New", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.awayScore.Location = new System.Drawing.Point(356, 25);
            this.awayScore.Name = "awayScore";
            this.awayScore.Size = new System.Drawing.Size(108, 71);
            this.awayScore.TabIndex = 2;
            this.awayScore.Text = "00";
            // 
            // awayNameLabel
            // 
            this.awayNameLabel.AutoSize = true;
            this.awayNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.awayNameLabel.Location = new System.Drawing.Point(6, 16);
            this.awayNameLabel.Name = "awayNameLabel";
            this.awayNameLabel.Size = new System.Drawing.Size(236, 31);
            this.awayNameLabel.TabIndex = 1;
            this.awayNameLabel.Text = "Away Team Name";
            // 
            // alpacaButton
            // 
            this.alpacaButton.Location = new System.Drawing.Point(522, 676);
            this.alpacaButton.Name = "alpacaButton";
            this.alpacaButton.Size = new System.Drawing.Size(164, 35);
            this.alpacaButton.TabIndex = 14;
            this.alpacaButton.Text = "Testing";
            this.alpacaButton.UseVisualStyleBackColor = true;
            this.alpacaButton.Click += new System.EventHandler(this.alpacaButton_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 730);
            this.Controls.Add(this.alpacaButton);
            this.Controls.Add(this.awayBox);
            this.Controls.Add(this.homeBox);
            this.Controls.Add(this.jumpBallButton);
            this.Controls.Add(this.turnoverButton);
            this.Controls.Add(this.foulButton);
            this.Controls.Add(this.substitutionButton);
            this.Controls.Add(this.deleteEventButton);
            this.Controls.Add(this.timeoutButton);
            this.Controls.Add(this.historyBox);
            this.Controls.Add(this.periodStartButton);
            this.Controls.Add(this.reboundButton);
            this.Controls.Add(this.courtBox);
            this.Name = "GameForm";
            this.Text = "20-20 Basketball";
            this.Load += new System.EventHandler(this.GameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.courtBox)).EndInit();
            this.homeBox.ResumeLayout(false);
            this.homeBox.PerformLayout();
            this.awayBox.ResumeLayout(false);
            this.awayBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox courtBox;
        private System.Windows.Forms.Button reboundButton;
        private System.Windows.Forms.Button periodStartButton;
        private System.Windows.Forms.ListBox historyBox;
        private System.Windows.Forms.Button substitutionButton;
        private System.Windows.Forms.Button timeoutButton;
        private System.Windows.Forms.Button deleteEventButton;
        private System.Windows.Forms.Button jumpBallButton;
        private System.Windows.Forms.Button turnoverButton;
        private System.Windows.Forms.Button foulButton;
        private System.Windows.Forms.GroupBox homeBox;
        private System.Windows.Forms.Label homeScore;
        private System.Windows.Forms.Label homeNameLabel;
        private System.Windows.Forms.GroupBox awayBox;
        private System.Windows.Forms.Label awayScore;
        private System.Windows.Forms.Label awayNameLabel;
        private System.Windows.Forms.Button alpacaButton;
    }
}

