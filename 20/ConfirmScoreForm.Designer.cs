namespace _20
{
    partial class ConfirmScoreForm
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
            this.scoreConfirmPanel = new System.Windows.Forms.Panel();
            this.confirmScoreButton = new System.Windows.Forms.Button();
            this.awayConfirmGroupBox = new System.Windows.Forms.GroupBox();
            this.awayScoreConfirmText = new System.Windows.Forms.TextBox();
            this.awayConfirmLabel = new System.Windows.Forms.Label();
            this.homeConfirmGroupBox = new System.Windows.Forms.GroupBox();
            this.homeScoreConfirmText = new System.Windows.Forms.TextBox();
            this.homeConfirmLabel = new System.Windows.Forms.Label();
            this.scoreConfirmPanel.SuspendLayout();
            this.awayConfirmGroupBox.SuspendLayout();
            this.homeConfirmGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // scoreConfirmPanel
            // 
            this.scoreConfirmPanel.Controls.Add(this.confirmScoreButton);
            this.scoreConfirmPanel.Controls.Add(this.awayConfirmGroupBox);
            this.scoreConfirmPanel.Controls.Add(this.homeConfirmGroupBox);
            this.scoreConfirmPanel.Location = new System.Drawing.Point(1, 0);
            this.scoreConfirmPanel.Name = "scoreConfirmPanel";
            this.scoreConfirmPanel.Size = new System.Drawing.Size(592, 186);
            this.scoreConfirmPanel.TabIndex = 20;
            // 
            // confirmScoreButton
            // 
            this.confirmScoreButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmScoreButton.Location = new System.Drawing.Point(101, 132);
            this.confirmScoreButton.Name = "confirmScoreButton";
            this.confirmScoreButton.Size = new System.Drawing.Size(395, 44);
            this.confirmScoreButton.TabIndex = 5;
            this.confirmScoreButton.Text = "Confirm Score";
            this.confirmScoreButton.UseVisualStyleBackColor = true;
            this.confirmScoreButton.Click += new System.EventHandler(this.confirmScoreButton_Click);
            // 
            // awayConfirmGroupBox
            // 
            this.awayConfirmGroupBox.Controls.Add(this.awayScoreConfirmText);
            this.awayConfirmGroupBox.Controls.Add(this.awayConfirmLabel);
            this.awayConfirmGroupBox.Location = new System.Drawing.Point(299, 3);
            this.awayConfirmGroupBox.Name = "awayConfirmGroupBox";
            this.awayConfirmGroupBox.Size = new System.Drawing.Size(284, 123);
            this.awayConfirmGroupBox.TabIndex = 4;
            this.awayConfirmGroupBox.TabStop = false;
            this.awayConfirmGroupBox.Text = "Away";
            // 
            // awayScoreConfirmText
            // 
            this.awayScoreConfirmText.Font = new System.Drawing.Font("Courier New", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.awayScoreConfirmText.Location = new System.Drawing.Point(97, 50);
            this.awayScoreConfirmText.MaxLength = 3;
            this.awayScoreConfirmText.Name = "awayScoreConfirmText";
            this.awayScoreConfirmText.Size = new System.Drawing.Size(100, 62);
            this.awayScoreConfirmText.TabIndex = 3;
            this.awayScoreConfirmText.Text = "999";
            this.awayScoreConfirmText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // awayConfirmLabel
            // 
            this.awayConfirmLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.awayConfirmLabel.Location = new System.Drawing.Point(6, 15);
            this.awayConfirmLabel.Name = "awayConfirmLabel";
            this.awayConfirmLabel.Size = new System.Drawing.Size(272, 32);
            this.awayConfirmLabel.TabIndex = 1;
            this.awayConfirmLabel.Text = "Away Team Name";
            this.awayConfirmLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // homeConfirmGroupBox
            // 
            this.homeConfirmGroupBox.Controls.Add(this.homeScoreConfirmText);
            this.homeConfirmGroupBox.Controls.Add(this.homeConfirmLabel);
            this.homeConfirmGroupBox.Location = new System.Drawing.Point(9, 3);
            this.homeConfirmGroupBox.Name = "homeConfirmGroupBox";
            this.homeConfirmGroupBox.Size = new System.Drawing.Size(284, 123);
            this.homeConfirmGroupBox.TabIndex = 3;
            this.homeConfirmGroupBox.TabStop = false;
            this.homeConfirmGroupBox.Text = "Home";
            // 
            // homeScoreConfirmText
            // 
            this.homeScoreConfirmText.Font = new System.Drawing.Font("Courier New", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeScoreConfirmText.Location = new System.Drawing.Point(92, 50);
            this.homeScoreConfirmText.MaxLength = 3;
            this.homeScoreConfirmText.Name = "homeScoreConfirmText";
            this.homeScoreConfirmText.Size = new System.Drawing.Size(100, 62);
            this.homeScoreConfirmText.TabIndex = 4;
            this.homeScoreConfirmText.Text = "999";
            this.homeScoreConfirmText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // homeConfirmLabel
            // 
            this.homeConfirmLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeConfirmLabel.Location = new System.Drawing.Point(6, 15);
            this.homeConfirmLabel.Name = "homeConfirmLabel";
            this.homeConfirmLabel.Size = new System.Drawing.Size(272, 32);
            this.homeConfirmLabel.TabIndex = 1;
            this.homeConfirmLabel.Text = "Home Team Name";
            this.homeConfirmLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConfirmScoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 188);
            this.ControlBox = false;
            this.Controls.Add(this.scoreConfirmPanel);
            this.Name = "ConfirmScoreForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confirm Score";
            this.Load += new System.EventHandler(this.ConfirmScoreForm_Load);
            this.scoreConfirmPanel.ResumeLayout(false);
            this.awayConfirmGroupBox.ResumeLayout(false);
            this.awayConfirmGroupBox.PerformLayout();
            this.homeConfirmGroupBox.ResumeLayout(false);
            this.homeConfirmGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel scoreConfirmPanel;
        private System.Windows.Forms.Button confirmScoreButton;
        private System.Windows.Forms.GroupBox awayConfirmGroupBox;
        private System.Windows.Forms.TextBox awayScoreConfirmText;
        private System.Windows.Forms.Label awayConfirmLabel;
        private System.Windows.Forms.GroupBox homeConfirmGroupBox;
        private System.Windows.Forms.TextBox homeScoreConfirmText;
        private System.Windows.Forms.Label homeConfirmLabel;

    }
}