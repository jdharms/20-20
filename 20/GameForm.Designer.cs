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
            ((System.ComponentModel.ISupportInitialize)(this.courtBox)).BeginInit();
            this.SuspendLayout();
            // 
            // courtBox
            // 
            this.courtBox.Image = global::_20.Properties.Resources.BasketballCourt;
            this.courtBox.Location = new System.Drawing.Point(12, 12);
            this.courtBox.Name = "courtBox";
            this.courtBox.Size = new System.Drawing.Size(944, 504);
            this.courtBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.courtBox.TabIndex = 0;
            this.courtBox.TabStop = false;
            this.courtBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.courtBox_MouseDown);
            // 
            // reboundButton
            // 
            this.reboundButton.Location = new System.Drawing.Point(12, 522);
            this.reboundButton.Name = "reboundButton";
            this.reboundButton.Size = new System.Drawing.Size(164, 35);
            this.reboundButton.TabIndex = 1;
            this.reboundButton.Text = "Rebound";
            this.reboundButton.UseVisualStyleBackColor = true;
            this.reboundButton.Click += new System.EventHandler(this.reboundButton_Click);
            // 
            // periodStartButton
            // 
            this.periodStartButton.Location = new System.Drawing.Point(12, 564);
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
            this.historyBox.Size = new System.Drawing.Size(195, 680);
            this.historyBox.TabIndex = 7;
            this.historyBox.SelectedValueChanged += new System.EventHandler(this.historyBox_SelectedValueChanged);
            this.historyBox.Leave += new System.EventHandler(this.historyBox_Leave);
            // 
            // substitutionButton
            // 
            this.substitutionButton.Location = new System.Drawing.Point(352, 564);
            this.substitutionButton.Name = "substitutionButton";
            this.substitutionButton.Size = new System.Drawing.Size(164, 35);
            this.substitutionButton.TabIndex = 8;
            this.substitutionButton.Text = "Substitution";
            this.substitutionButton.UseVisualStyleBackColor = true;
            this.substitutionButton.Click += new System.EventHandler(this.substitutionButton_Click);
            // 
            // timeoutButton
            // 
            this.timeoutButton.Location = new System.Drawing.Point(182, 564);
            this.timeoutButton.Name = "timeoutButton";
            this.timeoutButton.Size = new System.Drawing.Size(164, 35);
            this.timeoutButton.TabIndex = 9;
            this.timeoutButton.Text = "Timeout";
            this.timeoutButton.UseVisualStyleBackColor = true;
            this.timeoutButton.Click += new System.EventHandler(this.timeoutButton_Click);
            // 
            // deleteEventButton
            // 
            this.deleteEventButton.Location = new System.Drawing.Point(1242, 710);
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
            this.jumpBallButton.Location = new System.Drawing.Point(522, 523);
            this.jumpBallButton.Name = "jumpBallButton";
            this.jumpBallButton.Size = new System.Drawing.Size(164, 35);
            this.jumpBallButton.TabIndex = 3;
            this.jumpBallButton.Text = "Jump Ball";
            this.jumpBallButton.UseVisualStyleBackColor = true;
            this.jumpBallButton.Click += new System.EventHandler(this.jumpBallButton_Click);
            // 
            // turnoverButton
            // 
            this.turnoverButton.Location = new System.Drawing.Point(352, 523);
            this.turnoverButton.Name = "turnoverButton";
            this.turnoverButton.Size = new System.Drawing.Size(164, 35);
            this.turnoverButton.TabIndex = 2;
            this.turnoverButton.Text = "Turnover";
            this.turnoverButton.UseVisualStyleBackColor = true;
            this.turnoverButton.Click += new System.EventHandler(this.turnoverButton_Click);
            // 
            // foulButton
            // 
            this.foulButton.Location = new System.Drawing.Point(182, 523);
            this.foulButton.Name = "foulButton";
            this.foulButton.Size = new System.Drawing.Size(164, 35);
            this.foulButton.TabIndex = 6;
            this.foulButton.Text = "Foul";
            this.foulButton.UseVisualStyleBackColor = true;
            this.foulButton.Click += new System.EventHandler(this.foulButton_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 730);
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
    }
}

