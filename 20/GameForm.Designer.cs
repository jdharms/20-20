﻿namespace _20
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
            this.components = new System.ComponentModel.Container();
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
            this.homePlayer5Context = new System.Windows.Forms.GroupBox();
            this.homePlayer5Label = new System.Windows.Forms.Label();
            this.homePlayer4Context = new System.Windows.Forms.GroupBox();
            this.homePlayer4Label = new System.Windows.Forms.Label();
            this.homePlayer3Context = new System.Windows.Forms.GroupBox();
            this.homePlayer3Label = new System.Windows.Forms.Label();
            this.homePlayer2Context = new System.Windows.Forms.GroupBox();
            this.homePlayer2Label = new System.Windows.Forms.Label();
            this.homePlayer1Context = new System.Windows.Forms.GroupBox();
            this.homePlayer1Label = new System.Windows.Forms.Label();
            this.homeScore = new System.Windows.Forms.Label();
            this.homeNameLabel = new System.Windows.Forms.Label();
            this.awayBox = new System.Windows.Forms.GroupBox();
            this.awayPlayer5Context = new System.Windows.Forms.GroupBox();
            this.awayPlayer5Label = new System.Windows.Forms.Label();
            this.awayScore = new System.Windows.Forms.Label();
            this.awayPlayer4Context = new System.Windows.Forms.GroupBox();
            this.awayPlayer4Label = new System.Windows.Forms.Label();
            this.awayNameLabel = new System.Windows.Forms.Label();
            this.awayPlayer3Context = new System.Windows.Forms.GroupBox();
            this.awayPlayer3Label = new System.Windows.Forms.Label();
            this.awayPlayer1Context = new System.Windows.Forms.GroupBox();
            this.awayPlayer1Label = new System.Windows.Forms.Label();
            this.awayPlayer2Context = new System.Windows.Forms.GroupBox();
            this.awayPlayer2Label = new System.Windows.Forms.Label();
            this.alpacaButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.courtBox = new System.Windows.Forms.PictureBox();
            this.homeBox.SuspendLayout();
            this.homePlayer5Context.SuspendLayout();
            this.homePlayer4Context.SuspendLayout();
            this.homePlayer3Context.SuspendLayout();
            this.homePlayer2Context.SuspendLayout();
            this.homePlayer1Context.SuspendLayout();
            this.awayBox.SuspendLayout();
            this.awayPlayer5Context.SuspendLayout();
            this.awayPlayer4Context.SuspendLayout();
            this.awayPlayer3Context.SuspendLayout();
            this.awayPlayer1Context.SuspendLayout();
            this.awayPlayer2Context.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.courtBox)).BeginInit();
            this.SuspendLayout();
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
            this.historyBox.Size = new System.Drawing.Size(262, 615);
            this.historyBox.TabIndex = 7;
            this.historyBox.SelectedValueChanged += new System.EventHandler(this.historyBox_SelectedValueChanged);
            this.historyBox.Leave += new System.EventHandler(this.historyBox_Leave);
            this.historyBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.historyBox_MouseMove);
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
            this.homeBox.Controls.Add(this.homePlayer5Context);
            this.homeBox.Controls.Add(this.homePlayer4Context);
            this.homeBox.Controls.Add(this.homePlayer3Context);
            this.homeBox.Controls.Add(this.homePlayer2Context);
            this.homeBox.Controls.Add(this.homePlayer1Context);
            this.homeBox.Controls.Add(this.homeScore);
            this.homeBox.Controls.Add(this.homeNameLabel);
            this.homeBox.Location = new System.Drawing.Point(16, 19);
            this.homeBox.Name = "homeBox";
            this.homeBox.Size = new System.Drawing.Size(470, 99);
            this.homeBox.TabIndex = 12;
            this.homeBox.TabStop = false;
            this.homeBox.Text = "Home";
            // 
            // homePlayer5Context
            // 
            this.homePlayer5Context.BackColor = System.Drawing.SystemColors.Control;
            this.homePlayer5Context.Controls.Add(this.homePlayer5Label);
            this.homePlayer5Context.Location = new System.Drawing.Point(284, 51);
            this.homePlayer5Context.Name = "homePlayer5Context";
            this.homePlayer5Context.Size = new System.Drawing.Size(62, 42);
            this.homePlayer5Context.TabIndex = 4;
            this.homePlayer5Context.TabStop = false;
            // 
            // homePlayer5Label
            // 
            this.homePlayer5Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homePlayer5Label.Location = new System.Drawing.Point(6, 16);
            this.homePlayer5Label.Name = "homePlayer5Label";
            this.homePlayer5Label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.homePlayer5Label.Size = new System.Drawing.Size(50, 23);
            this.homePlayer5Label.TabIndex = 0;
            this.homePlayer5Label.Text = "40";
            this.homePlayer5Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.homePlayer5Label.Click += new System.EventHandler(this.playerSelect_click);
            // 
            // homePlayer4Context
            // 
            this.homePlayer4Context.BackColor = System.Drawing.SystemColors.Control;
            this.homePlayer4Context.Controls.Add(this.homePlayer4Label);
            this.homePlayer4Context.Location = new System.Drawing.Point(216, 51);
            this.homePlayer4Context.Name = "homePlayer4Context";
            this.homePlayer4Context.Size = new System.Drawing.Size(62, 42);
            this.homePlayer4Context.TabIndex = 4;
            this.homePlayer4Context.TabStop = false;
            // 
            // homePlayer4Label
            // 
            this.homePlayer4Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homePlayer4Label.Location = new System.Drawing.Point(6, 16);
            this.homePlayer4Label.Name = "homePlayer4Label";
            this.homePlayer4Label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.homePlayer4Label.Size = new System.Drawing.Size(50, 23);
            this.homePlayer4Label.TabIndex = 0;
            this.homePlayer4Label.Text = "30";
            this.homePlayer4Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.homePlayer4Label.Click += new System.EventHandler(this.playerSelect_click);
            // 
            // homePlayer3Context
            // 
            this.homePlayer3Context.BackColor = System.Drawing.SystemColors.Control;
            this.homePlayer3Context.Controls.Add(this.homePlayer3Label);
            this.homePlayer3Context.Location = new System.Drawing.Point(148, 51);
            this.homePlayer3Context.Name = "homePlayer3Context";
            this.homePlayer3Context.Size = new System.Drawing.Size(62, 42);
            this.homePlayer3Context.TabIndex = 4;
            this.homePlayer3Context.TabStop = false;
            // 
            // homePlayer3Label
            // 
            this.homePlayer3Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homePlayer3Label.Location = new System.Drawing.Point(6, 16);
            this.homePlayer3Label.Name = "homePlayer3Label";
            this.homePlayer3Label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.homePlayer3Label.Size = new System.Drawing.Size(50, 23);
            this.homePlayer3Label.TabIndex = 0;
            this.homePlayer3Label.Text = "20";
            this.homePlayer3Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.homePlayer3Label.Click += new System.EventHandler(this.playerSelect_click);
            // 
            // homePlayer2Context
            // 
            this.homePlayer2Context.BackColor = System.Drawing.SystemColors.Control;
            this.homePlayer2Context.Controls.Add(this.homePlayer2Label);
            this.homePlayer2Context.Location = new System.Drawing.Point(80, 51);
            this.homePlayer2Context.Name = "homePlayer2Context";
            this.homePlayer2Context.Size = new System.Drawing.Size(62, 42);
            this.homePlayer2Context.TabIndex = 4;
            this.homePlayer2Context.TabStop = false;
            // 
            // homePlayer2Label
            // 
            this.homePlayer2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homePlayer2Label.Location = new System.Drawing.Point(6, 16);
            this.homePlayer2Label.Name = "homePlayer2Label";
            this.homePlayer2Label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.homePlayer2Label.Size = new System.Drawing.Size(50, 23);
            this.homePlayer2Label.TabIndex = 0;
            this.homePlayer2Label.Text = "10";
            this.homePlayer2Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.homePlayer2Label.Click += new System.EventHandler(this.playerSelect_click);
            // 
            // homePlayer1Context
            // 
            this.homePlayer1Context.BackColor = System.Drawing.SystemColors.Control;
            this.homePlayer1Context.Controls.Add(this.homePlayer1Label);
            this.homePlayer1Context.Location = new System.Drawing.Point(12, 51);
            this.homePlayer1Context.Name = "homePlayer1Context";
            this.homePlayer1Context.Size = new System.Drawing.Size(62, 42);
            this.homePlayer1Context.TabIndex = 3;
            this.homePlayer1Context.TabStop = false;
            // 
            // homePlayer1Label
            // 
            this.homePlayer1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homePlayer1Label.Location = new System.Drawing.Point(6, 16);
            this.homePlayer1Label.Name = "homePlayer1Label";
            this.homePlayer1Label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.homePlayer1Label.Size = new System.Drawing.Size(50, 23);
            this.homePlayer1Label.TabIndex = 0;
            this.homePlayer1Label.Text = "01";
            this.homePlayer1Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.homePlayer1Label.Click += new System.EventHandler(this.playerSelect_click);
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
            this.awayBox.Controls.Add(this.awayPlayer5Context);
            this.awayBox.Controls.Add(this.awayScore);
            this.awayBox.Controls.Add(this.awayPlayer4Context);
            this.awayBox.Controls.Add(this.awayNameLabel);
            this.awayBox.Controls.Add(this.awayPlayer3Context);
            this.awayBox.Controls.Add(this.awayPlayer1Context);
            this.awayBox.Controls.Add(this.awayPlayer2Context);
            this.awayBox.Location = new System.Drawing.Point(492, 19);
            this.awayBox.Name = "awayBox";
            this.awayBox.Size = new System.Drawing.Size(470, 99);
            this.awayBox.TabIndex = 13;
            this.awayBox.TabStop = false;
            this.awayBox.Text = "Away";
            // 
            // awayPlayer5Context
            // 
            this.awayPlayer5Context.BackColor = System.Drawing.SystemColors.Control;
            this.awayPlayer5Context.Controls.Add(this.awayPlayer5Label);
            this.awayPlayer5Context.Location = new System.Drawing.Point(284, 51);
            this.awayPlayer5Context.Name = "awayPlayer5Context";
            this.awayPlayer5Context.Size = new System.Drawing.Size(62, 42);
            this.awayPlayer5Context.TabIndex = 7;
            this.awayPlayer5Context.TabStop = false;
            // 
            // awayPlayer5Label
            // 
            this.awayPlayer5Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.awayPlayer5Label.Location = new System.Drawing.Point(6, 16);
            this.awayPlayer5Label.Name = "awayPlayer5Label";
            this.awayPlayer5Label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.awayPlayer5Label.Size = new System.Drawing.Size(50, 23);
            this.awayPlayer5Label.TabIndex = 0;
            this.awayPlayer5Label.Text = "40";
            this.awayPlayer5Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.awayPlayer5Label.Click += new System.EventHandler(this.playerSelect_click);
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
            // awayPlayer4Context
            // 
            this.awayPlayer4Context.BackColor = System.Drawing.SystemColors.Control;
            this.awayPlayer4Context.Controls.Add(this.awayPlayer4Label);
            this.awayPlayer4Context.Location = new System.Drawing.Point(216, 51);
            this.awayPlayer4Context.Name = "awayPlayer4Context";
            this.awayPlayer4Context.Size = new System.Drawing.Size(62, 42);
            this.awayPlayer4Context.TabIndex = 8;
            this.awayPlayer4Context.TabStop = false;
            // 
            // awayPlayer4Label
            // 
            this.awayPlayer4Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.awayPlayer4Label.Location = new System.Drawing.Point(6, 16);
            this.awayPlayer4Label.Name = "awayPlayer4Label";
            this.awayPlayer4Label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.awayPlayer4Label.Size = new System.Drawing.Size(50, 23);
            this.awayPlayer4Label.TabIndex = 0;
            this.awayPlayer4Label.Text = "30";
            this.awayPlayer4Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.awayPlayer4Label.Click += new System.EventHandler(this.playerSelect_click);
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
            // awayPlayer3Context
            // 
            this.awayPlayer3Context.BackColor = System.Drawing.SystemColors.Control;
            this.awayPlayer3Context.Controls.Add(this.awayPlayer3Label);
            this.awayPlayer3Context.Location = new System.Drawing.Point(148, 51);
            this.awayPlayer3Context.Name = "awayPlayer3Context";
            this.awayPlayer3Context.Size = new System.Drawing.Size(62, 42);
            this.awayPlayer3Context.TabIndex = 9;
            this.awayPlayer3Context.TabStop = false;
            // 
            // awayPlayer3Label
            // 
            this.awayPlayer3Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.awayPlayer3Label.Location = new System.Drawing.Point(6, 16);
            this.awayPlayer3Label.Name = "awayPlayer3Label";
            this.awayPlayer3Label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.awayPlayer3Label.Size = new System.Drawing.Size(50, 23);
            this.awayPlayer3Label.TabIndex = 0;
            this.awayPlayer3Label.Text = "20";
            this.awayPlayer3Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.awayPlayer3Label.Click += new System.EventHandler(this.playerSelect_click);
            // 
            // awayPlayer1Context
            // 
            this.awayPlayer1Context.BackColor = System.Drawing.SystemColors.Control;
            this.awayPlayer1Context.Controls.Add(this.awayPlayer1Label);
            this.awayPlayer1Context.Location = new System.Drawing.Point(12, 51);
            this.awayPlayer1Context.Name = "awayPlayer1Context";
            this.awayPlayer1Context.Size = new System.Drawing.Size(62, 42);
            this.awayPlayer1Context.TabIndex = 5;
            this.awayPlayer1Context.TabStop = false;
            // 
            // awayPlayer1Label
            // 
            this.awayPlayer1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.awayPlayer1Label.Location = new System.Drawing.Point(6, 16);
            this.awayPlayer1Label.Name = "awayPlayer1Label";
            this.awayPlayer1Label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.awayPlayer1Label.Size = new System.Drawing.Size(50, 23);
            this.awayPlayer1Label.TabIndex = 0;
            this.awayPlayer1Label.Text = "01";
            this.awayPlayer1Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.awayPlayer1Label.Click += new System.EventHandler(this.playerSelect_click);
            // 
            // awayPlayer2Context
            // 
            this.awayPlayer2Context.BackColor = System.Drawing.SystemColors.Control;
            this.awayPlayer2Context.Controls.Add(this.awayPlayer2Label);
            this.awayPlayer2Context.Location = new System.Drawing.Point(80, 51);
            this.awayPlayer2Context.Name = "awayPlayer2Context";
            this.awayPlayer2Context.Size = new System.Drawing.Size(62, 42);
            this.awayPlayer2Context.TabIndex = 6;
            this.awayPlayer2Context.TabStop = false;
            // 
            // awayPlayer2Label
            // 
            this.awayPlayer2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.awayPlayer2Label.Location = new System.Drawing.Point(6, 16);
            this.awayPlayer2Label.Name = "awayPlayer2Label";
            this.awayPlayer2Label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.awayPlayer2Label.Size = new System.Drawing.Size(50, 23);
            this.awayPlayer2Label.TabIndex = 0;
            this.awayPlayer2Label.Text = "10";
            this.awayPlayer2Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.awayPlayer2Label.Click += new System.EventHandler(this.playerSelect_click);
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
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
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
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 730);
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
            this.homeBox.ResumeLayout(false);
            this.homeBox.PerformLayout();
            this.homePlayer5Context.ResumeLayout(false);
            this.homePlayer4Context.ResumeLayout(false);
            this.homePlayer3Context.ResumeLayout(false);
            this.homePlayer2Context.ResumeLayout(false);
            this.homePlayer1Context.ResumeLayout(false);
            this.awayBox.ResumeLayout(false);
            this.awayBox.PerformLayout();
            this.awayPlayer5Context.ResumeLayout(false);
            this.awayPlayer4Context.ResumeLayout(false);
            this.awayPlayer3Context.ResumeLayout(false);
            this.awayPlayer1Context.ResumeLayout(false);
            this.awayPlayer2Context.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox homeBox;
        private System.Windows.Forms.Label homeScore;
        private System.Windows.Forms.Label homeNameLabel;
        private System.Windows.Forms.GroupBox awayBox;
        private System.Windows.Forms.Label awayScore;
        private System.Windows.Forms.Label awayNameLabel;
        private System.Windows.Forms.Button alpacaButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox homePlayer1Context;
        private System.Windows.Forms.Label homePlayer1Label;
        private System.Windows.Forms.GroupBox homePlayer5Context;
        private System.Windows.Forms.Label homePlayer5Label;
        private System.Windows.Forms.GroupBox homePlayer4Context;
        private System.Windows.Forms.Label homePlayer4Label;
        private System.Windows.Forms.GroupBox homePlayer3Context;
        private System.Windows.Forms.Label homePlayer3Label;
        private System.Windows.Forms.GroupBox homePlayer2Context;
        private System.Windows.Forms.Label homePlayer2Label;
        private System.Windows.Forms.GroupBox awayPlayer5Context;
        private System.Windows.Forms.Label awayPlayer5Label;
        private System.Windows.Forms.GroupBox awayPlayer4Context;
        private System.Windows.Forms.Label awayPlayer4Label;
        private System.Windows.Forms.GroupBox awayPlayer3Context;
        private System.Windows.Forms.Label awayPlayer3Label;
        private System.Windows.Forms.GroupBox awayPlayer1Context;
        private System.Windows.Forms.Label awayPlayer1Label;
        private System.Windows.Forms.GroupBox awayPlayer2Context;
        private System.Windows.Forms.Label awayPlayer2Label;
    }
}

