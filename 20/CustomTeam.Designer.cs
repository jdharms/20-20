namespace _20
{
    partial class CustomTeam
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
            this.playersListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.playerNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.playerNumberTextBox = new System.Windows.Forms.TextBox();
            this.savePlayerButton = new System.Windows.Forms.Button();
            this.loadTeamButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.teamNameTextBox = new System.Windows.Forms.TextBox();
            this.deletePlayerButton = new System.Windows.Forms.Button();
            this.saveTeamButton = new System.Windows.Forms.Button();
            this.addPlayerButton = new System.Windows.Forms.Button();
            this.useTeamButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // playersListBox
            // 
            this.playersListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playersListBox.FormattingEnabled = true;
            this.playersListBox.ItemHeight = 20;
            this.playersListBox.Location = new System.Drawing.Point(340, 70);
            this.playersListBox.Name = "playersListBox";
            this.playersListBox.Size = new System.Drawing.Size(229, 304);
            this.playersListBox.Sorted = true;
            this.playersListBox.TabIndex = 1;
            this.playersListBox.Click += new System.EventHandler(this.playersListBox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Player Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerNameTextBox
            // 
            this.playerNameTextBox.Enabled = false;
            this.playerNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerNameTextBox.Location = new System.Drawing.Point(18, 107);
            this.playerNameTextBox.Name = "playerNameTextBox";
            this.playerNameTextBox.Size = new System.Drawing.Size(310, 38);
            this.playerNameTextBox.TabIndex = 3;
            this.playerNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 31);
            this.label2.TabIndex = 4;
            this.label2.Text = "Player Number";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerNumberTextBox
            // 
            this.playerNumberTextBox.Enabled = false;
            this.playerNumberTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerNumberTextBox.Location = new System.Drawing.Point(213, 151);
            this.playerNumberTextBox.MaxLength = 2;
            this.playerNumberTextBox.Name = "playerNumberTextBox";
            this.playerNumberTextBox.Size = new System.Drawing.Size(50, 38);
            this.playerNumberTextBox.TabIndex = 5;
            this.playerNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // savePlayerButton
            // 
            this.savePlayerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savePlayerButton.Location = new System.Drawing.Point(16, 194);
            this.savePlayerButton.Name = "savePlayerButton";
            this.savePlayerButton.Size = new System.Drawing.Size(312, 56);
            this.savePlayerButton.TabIndex = 6;
            this.savePlayerButton.Text = "Save Player";
            this.savePlayerButton.UseVisualStyleBackColor = true;
            this.savePlayerButton.Click += new System.EventHandler(this.savePlayerButton_Click);
            // 
            // loadTeamButton
            // 
            this.loadTeamButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadTeamButton.Location = new System.Drawing.Point(340, 38);
            this.loadTeamButton.Name = "loadTeamButton";
            this.loadTeamButton.Size = new System.Drawing.Size(229, 26);
            this.loadTeamButton.TabIndex = 8;
            this.loadTeamButton.Text = "Load Team";
            this.loadTeamButton.UseVisualStyleBackColor = true;
            this.loadTeamButton.Click += new System.EventHandler(this.loadTeamButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Team Name";
            // 
            // teamNameTextBox
            // 
            this.teamNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teamNameTextBox.Location = new System.Drawing.Point(119, 6);
            this.teamNameTextBox.Name = "teamNameTextBox";
            this.teamNameTextBox.Size = new System.Drawing.Size(215, 26);
            this.teamNameTextBox.TabIndex = 10;
            this.teamNameTextBox.TextChanged += new System.EventHandler(this.teamNameTextBox_TextChanged);
            // 
            // deletePlayerButton
            // 
            this.deletePlayerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deletePlayerButton.Location = new System.Drawing.Point(16, 318);
            this.deletePlayerButton.Name = "deletePlayerButton";
            this.deletePlayerButton.Size = new System.Drawing.Size(312, 56);
            this.deletePlayerButton.TabIndex = 11;
            this.deletePlayerButton.Text = "Delete Player";
            this.deletePlayerButton.UseVisualStyleBackColor = true;
            this.deletePlayerButton.Click += new System.EventHandler(this.deletePlayerButton_Click);
            // 
            // saveTeamButton
            // 
            this.saveTeamButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveTeamButton.Location = new System.Drawing.Point(340, 6);
            this.saveTeamButton.Name = "saveTeamButton";
            this.saveTeamButton.Size = new System.Drawing.Size(229, 26);
            this.saveTeamButton.TabIndex = 12;
            this.saveTeamButton.Text = "Save Team";
            this.saveTeamButton.UseVisualStyleBackColor = true;
            this.saveTeamButton.Click += new System.EventHandler(this.saveTeamButton_Click);
            // 
            // addPlayerButton
            // 
            this.addPlayerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addPlayerButton.Location = new System.Drawing.Point(16, 256);
            this.addPlayerButton.Name = "addPlayerButton";
            this.addPlayerButton.Size = new System.Drawing.Size(312, 56);
            this.addPlayerButton.TabIndex = 13;
            this.addPlayerButton.Text = "Add Player";
            this.addPlayerButton.UseVisualStyleBackColor = true;
            this.addPlayerButton.Click += new System.EventHandler(this.addPlayer_Click);
            // 
            // useTeamButton
            // 
            this.useTeamButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useTeamButton.Location = new System.Drawing.Point(16, 44);
            this.useTeamButton.Name = "useTeamButton";
            this.useTeamButton.Size = new System.Drawing.Size(154, 26);
            this.useTeamButton.TabIndex = 14;
            this.useTeamButton.Text = "Use Team";
            this.useTeamButton.UseVisualStyleBackColor = true;
            this.useTeamButton.Click += new System.EventHandler(this.useTeamButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(180, 44);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(154, 26);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // CustomTeam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 386);
            this.ControlBox = false;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.useTeamButton);
            this.Controls.Add(this.addPlayerButton);
            this.Controls.Add(this.saveTeamButton);
            this.Controls.Add(this.deletePlayerButton);
            this.Controls.Add(this.teamNameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.loadTeamButton);
            this.Controls.Add(this.savePlayerButton);
            this.Controls.Add(this.playerNumberTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.playerNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.playersListBox);
            this.Name = "CustomTeam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Custom Team";
            this.Load += new System.EventHandler(this.CustomTeam_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox playersListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox playerNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox playerNumberTextBox;
        private System.Windows.Forms.Button savePlayerButton;
        private System.Windows.Forms.Button loadTeamButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox teamNameTextBox;
        private System.Windows.Forms.Button deletePlayerButton;
        private System.Windows.Forms.Button saveTeamButton;
        private System.Windows.Forms.Button addPlayerButton;
        private System.Windows.Forms.Button useTeamButton;
        private System.Windows.Forms.Button cancelButton;
    }
}