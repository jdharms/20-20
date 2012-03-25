namespace _20
{
    partial class GameSelectForm
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
            this.FromLabel = new System.Windows.Forms.Label();
            this.toLabel = new System.Windows.Forms.Label();
            this.fromDatePicker = new System.Windows.Forms.DateTimePicker();
            this.toDatePicker = new System.Windows.Forms.DateTimePicker();
            this.searchGameButton = new System.Windows.Forms.Button();
            this.gameListBox = new System.Windows.Forms.ListBox();
            this.selectGameButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FromLabel
            // 
            this.FromLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FromLabel.AutoSize = true;
            this.FromLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromLabel.Location = new System.Drawing.Point(69, 8);
            this.FromLabel.Name = "FromLabel";
            this.FromLabel.Size = new System.Drawing.Size(46, 20);
            this.FromLabel.TabIndex = 0;
            this.FromLabel.Text = "From";
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toLabel.Location = new System.Drawing.Point(87, 38);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(27, 20);
            this.toLabel.TabIndex = 1;
            this.toLabel.Text = "To";
            // 
            // fromDatePicker
            // 
            this.fromDatePicker.Location = new System.Drawing.Point(155, 8);
            this.fromDatePicker.Name = "fromDatePicker";
            this.fromDatePicker.Size = new System.Drawing.Size(200, 20);
            this.fromDatePicker.TabIndex = 2;
            // 
            // toDatePicker
            // 
            this.toDatePicker.Location = new System.Drawing.Point(155, 38);
            this.toDatePicker.Name = "toDatePicker";
            this.toDatePicker.Size = new System.Drawing.Size(200, 20);
            this.toDatePicker.TabIndex = 3;
            // 
            // searchGameButton
            // 
            this.searchGameButton.Location = new System.Drawing.Point(109, 64);
            this.searchGameButton.Name = "searchGameButton";
            this.searchGameButton.Size = new System.Drawing.Size(156, 27);
            this.searchGameButton.TabIndex = 4;
            this.searchGameButton.Text = "Search Games";
            this.searchGameButton.UseVisualStyleBackColor = true;
            this.searchGameButton.Click += new System.EventHandler(this.searchGamesButton_click);
            // 
            // gameListBox
            // 
            this.gameListBox.FormattingEnabled = true;
            this.gameListBox.Location = new System.Drawing.Point(33, 111);
            this.gameListBox.Name = "gameListBox";
            this.gameListBox.Size = new System.Drawing.Size(322, 95);
            this.gameListBox.TabIndex = 5;
            // 
            // selectGameButton
            // 
            this.selectGameButton.Location = new System.Drawing.Point(33, 212);
            this.selectGameButton.Name = "selectGameButton";
            this.selectGameButton.Size = new System.Drawing.Size(156, 27);
            this.selectGameButton.TabIndex = 6;
            this.selectGameButton.Text = "Select Game";
            this.selectGameButton.UseVisualStyleBackColor = true;
            this.selectGameButton.Click += new System.EventHandler(this.selectGameButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(195, 212);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(156, 27);
            this.exitButton.TabIndex = 7;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // GameSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 248);
            this.ControlBox = false;
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.selectGameButton);
            this.Controls.Add(this.gameListBox);
            this.Controls.Add(this.searchGameButton);
            this.Controls.Add(this.toDatePicker);
            this.Controls.Add(this.fromDatePicker);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.FromLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GameSelectForm";
            this.Text = "Select Game";
            this.Load += new System.EventHandler(this.GameSelectForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FromLabel;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.DateTimePicker fromDatePicker;
        private System.Windows.Forms.DateTimePicker toDatePicker;
        private System.Windows.Forms.Button searchGameButton;
        private System.Windows.Forms.ListBox gameListBox;
        private System.Windows.Forms.Button selectGameButton;
        private System.Windows.Forms.Button exitButton;
    }
}