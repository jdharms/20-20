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
            this.turnoverButton = new System.Windows.Forms.Button();
            this.jumpBallButton = new System.Windows.Forms.Button();
            this.periodStartButton = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.historyBox = new System.Windows.Forms.ListBox();
            this.substitutionButton = new System.Windows.Forms.Button();
            this.timeoutButton = new System.Windows.Forms.Button();
            this.buttonBox = new System.Windows.Forms.GroupBox();
            this.deleteEventButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.courtBox)).BeginInit();
            this.SuspendLayout();
            // 
            // courtBox
            // 
            this.courtBox.Image = global::_20.Properties.Resources.BasketballCourt;
            this.courtBox.Location = new System.Drawing.Point(23, 22);
            this.courtBox.Name = "courtBox";
            this.courtBox.Size = new System.Drawing.Size(1213, 731);
            this.courtBox.TabIndex = 0;
            this.courtBox.TabStop = false;
            this.courtBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.courtBox_MouseDown);
            // 
            // reboundButton
            // 
            this.reboundButton.Location = new System.Drawing.Point(23, 759);
            this.reboundButton.Name = "reboundButton";
            this.reboundButton.Size = new System.Drawing.Size(164, 35);
            this.reboundButton.TabIndex = 1;
            this.reboundButton.Text = "Rebound";
            this.reboundButton.UseVisualStyleBackColor = true;
            this.reboundButton.Click += new System.EventHandler(this.reboundButton_Click);
            // 
            // turnoverButton
            // 
            this.turnoverButton.Location = new System.Drawing.Point(193, 759);
            this.turnoverButton.Name = "turnoverButton";
            this.turnoverButton.Size = new System.Drawing.Size(164, 35);
            this.turnoverButton.TabIndex = 2;
            this.turnoverButton.Text = "Turnover";
            this.turnoverButton.UseVisualStyleBackColor = true;
            this.turnoverButton.Click += new System.EventHandler(this.turnoverButton_Click);
            // 
            // jumpBallButton
            // 
            this.jumpBallButton.Location = new System.Drawing.Point(363, 759);
            this.jumpBallButton.Name = "jumpBallButton";
            this.jumpBallButton.Size = new System.Drawing.Size(164, 35);
            this.jumpBallButton.TabIndex = 3;
            this.jumpBallButton.Text = "Jump Ball";
            this.jumpBallButton.UseVisualStyleBackColor = true;
            this.jumpBallButton.Click += new System.EventHandler(this.jumpBallButton_Click);
            // 
            // periodStartButton
            // 
            this.periodStartButton.Location = new System.Drawing.Point(873, 759);
            this.periodStartButton.Name = "periodStartButton";
            this.periodStartButton.Size = new System.Drawing.Size(164, 35);
            this.periodStartButton.TabIndex = 5;
            this.periodStartButton.Text = "Period Start/End";
            this.periodStartButton.UseVisualStyleBackColor = true;
            this.periodStartButton.Click += new System.EventHandler(this.periodStartButton_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(533, 759);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(164, 35);
            this.button6.TabIndex = 6;
            this.button6.Text = "Foul";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // historyBox
            // 
            this.historyBox.FormattingEnabled = true;
            this.historyBox.Items.AddRange(new object[] {
            "Event 1",
            "Event 2",
            "Event 3"});
            this.historyBox.Location = new System.Drawing.Point(1242, 24);
            this.historyBox.Name = "historyBox";
            this.historyBox.Size = new System.Drawing.Size(195, 680);
            this.historyBox.TabIndex = 7;
            this.historyBox.SelectedValueChanged += new System.EventHandler(this.historyBox_SelectedValueChanged);
            this.historyBox.Leave += new System.EventHandler(this.historyBox_Leave);
            // 
            // substitutionButton
            // 
            this.substitutionButton.Location = new System.Drawing.Point(1043, 759);
            this.substitutionButton.Name = "substitutionButton";
            this.substitutionButton.Size = new System.Drawing.Size(164, 35);
            this.substitutionButton.TabIndex = 8;
            this.substitutionButton.Text = "Substitution";
            this.substitutionButton.UseVisualStyleBackColor = true;
            // 
            // timeoutButton
            // 
            this.timeoutButton.Location = new System.Drawing.Point(703, 759);
            this.timeoutButton.Name = "timeoutButton";
            this.timeoutButton.Size = new System.Drawing.Size(164, 35);
            this.timeoutButton.TabIndex = 9;
            this.timeoutButton.Text = "Timeout";
            this.timeoutButton.UseVisualStyleBackColor = true;
            // 
            // buttonBox
            // 
            this.buttonBox.Location = new System.Drawing.Point(12, 749);
            this.buttonBox.Name = "buttonBox";
            this.buttonBox.Size = new System.Drawing.Size(1204, 46);
            this.buttonBox.TabIndex = 10;
            this.buttonBox.TabStop = false;
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
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1445, 807);
            this.Controls.Add(this.deleteEventButton);
            this.Controls.Add(this.timeoutButton);
            this.Controls.Add(this.substitutionButton);
            this.Controls.Add(this.historyBox);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.periodStartButton);
            this.Controls.Add(this.jumpBallButton);
            this.Controls.Add(this.turnoverButton);
            this.Controls.Add(this.reboundButton);
            this.Controls.Add(this.courtBox);
            this.Controls.Add(this.buttonBox);
            this.Name = "GameForm";
            this.Text = "20-20 Basketball";
            this.Load += new System.EventHandler(this.GameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.courtBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox courtBox;
        private System.Windows.Forms.Button reboundButton;
        private System.Windows.Forms.Button turnoverButton;
        private System.Windows.Forms.Button jumpBallButton;
        private System.Windows.Forms.Button periodStartButton;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ListBox historyBox;
        private System.Windows.Forms.Button substitutionButton;
        private System.Windows.Forms.Button timeoutButton;
        private System.Windows.Forms.GroupBox buttonBox;
        private System.Windows.Forms.Button deleteEventButton;
    }
}

