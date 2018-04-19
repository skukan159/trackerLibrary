namespace TrackerUI
{
    partial class CreateTournamentForm
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
            this.tournamentNameValue = new System.Windows.Forms.TextBox();
            this.tournamentNameLabel = new System.Windows.Forms.Label();
            this.selectTeamDropdown = new System.Windows.Forms.ComboBox();
            this.addTeamButton = new System.Windows.Forms.Button();
            this.tournamentTeamsListBox = new System.Windows.Forms.ListBox();
            this.entryFeeLabel = new System.Windows.Forms.Label();
            this.entryFeeValue = new System.Windows.Forms.TextBox();
            this.teamsLabel = new System.Windows.Forms.Label();
            this.selectTeamLabel = new System.Windows.Forms.Label();
            this.prizesLabel = new System.Windows.Forms.Label();
            this.prizesListBox = new System.Windows.Forms.ListBox();
            this.createPrizeButton = new System.Windows.Forms.Button();
            this.removeTeamButton = new System.Windows.Forms.Button();
            this.removePrizeButton = new System.Windows.Forms.Button();
            this.createTournamentButton = new System.Windows.Forms.Button();
            this.createNewLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // tournamentNameValue
            // 
            this.tournamentNameValue.Location = new System.Drawing.Point(20, 67);
            this.tournamentNameValue.Name = "tournamentNameValue";
            this.tournamentNameValue.Size = new System.Drawing.Size(361, 43);
            this.tournamentNameValue.TabIndex = 12;
            // 
            // tournamentNameLabel
            // 
            this.tournamentNameLabel.AutoSize = true;
            this.tournamentNameLabel.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tournamentNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.tournamentNameLabel.Location = new System.Drawing.Point(12, 9);
            this.tournamentNameLabel.Name = "tournamentNameLabel";
            this.tournamentNameLabel.Size = new System.Drawing.Size(290, 45);
            this.tournamentNameLabel.TabIndex = 13;
            this.tournamentNameLabel.Text = "Tournament Name";
            // 
            // selectTeamDropdown
            // 
            this.selectTeamDropdown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.selectTeamDropdown.FormattingEnabled = true;
            this.selectTeamDropdown.Location = new System.Drawing.Point(12, 246);
            this.selectTeamDropdown.Name = "selectTeamDropdown";
            this.selectTeamDropdown.Size = new System.Drawing.Size(369, 45);
            this.selectTeamDropdown.TabIndex = 14;
            // 
            // addTeamButton
            // 
            this.addTeamButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.addTeamButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.addTeamButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.addTeamButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addTeamButton.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addTeamButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.addTeamButton.Location = new System.Drawing.Point(44, 306);
            this.addTeamButton.Name = "addTeamButton";
            this.addTeamButton.Size = new System.Drawing.Size(291, 55);
            this.addTeamButton.TabIndex = 15;
            this.addTeamButton.Text = "Add Team";
            this.addTeamButton.UseVisualStyleBackColor = true;
            this.addTeamButton.Click += new System.EventHandler(this.addTeamButton_Click);
            // 
            // tournamentTeamsListBox
            // 
            this.tournamentTeamsListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tournamentTeamsListBox.FormattingEnabled = true;
            this.tournamentTeamsListBox.ItemHeight = 37;
            this.tournamentTeamsListBox.Location = new System.Drawing.Point(445, 67);
            this.tournamentTeamsListBox.Name = "tournamentTeamsListBox";
            this.tournamentTeamsListBox.Size = new System.Drawing.Size(355, 224);
            this.tournamentTeamsListBox.TabIndex = 16;
            // 
            // entryFeeLabel
            // 
            this.entryFeeLabel.AutoSize = true;
            this.entryFeeLabel.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entryFeeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.entryFeeLabel.Location = new System.Drawing.Point(26, 131);
            this.entryFeeLabel.Name = "entryFeeLabel";
            this.entryFeeLabel.Size = new System.Drawing.Size(153, 45);
            this.entryFeeLabel.TabIndex = 17;
            this.entryFeeLabel.Text = "Entry Fee";
            // 
            // entryFeeValue
            // 
            this.entryFeeValue.Location = new System.Drawing.Point(197, 134);
            this.entryFeeValue.Name = "entryFeeValue";
            this.entryFeeValue.Size = new System.Drawing.Size(180, 43);
            this.entryFeeValue.TabIndex = 18;
            this.entryFeeValue.Text = "0";
            // 
            // teamsLabel
            // 
            this.teamsLabel.AutoSize = true;
            this.teamsLabel.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teamsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.teamsLabel.Location = new System.Drawing.Point(437, 9);
            this.teamsLabel.Name = "teamsLabel";
            this.teamsLabel.Size = new System.Drawing.Size(224, 45);
            this.teamsLabel.TabIndex = 19;
            this.teamsLabel.Text = "Teams/Players";
            // 
            // selectTeamLabel
            // 
            this.selectTeamLabel.AutoSize = true;
            this.selectTeamLabel.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectTeamLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.selectTeamLabel.Location = new System.Drawing.Point(18, 198);
            this.selectTeamLabel.Name = "selectTeamLabel";
            this.selectTeamLabel.Size = new System.Drawing.Size(191, 45);
            this.selectTeamLabel.TabIndex = 20;
            this.selectTeamLabel.Text = "Select Team";
            // 
            // prizesLabel
            // 
            this.prizesLabel.AutoSize = true;
            this.prizesLabel.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prizesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.prizesLabel.Location = new System.Drawing.Point(437, 319);
            this.prizesLabel.Name = "prizesLabel";
            this.prizesLabel.Size = new System.Drawing.Size(103, 45);
            this.prizesLabel.TabIndex = 22;
            this.prizesLabel.Text = "Prizes";
            // 
            // prizesListBox
            // 
            this.prizesListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.prizesListBox.FormattingEnabled = true;
            this.prizesListBox.ItemHeight = 37;
            this.prizesListBox.Location = new System.Drawing.Point(445, 377);
            this.prizesListBox.Name = "prizesListBox";
            this.prizesListBox.Size = new System.Drawing.Size(355, 187);
            this.prizesListBox.TabIndex = 21;
            // 
            // createPrizeButton
            // 
            this.createPrizeButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.createPrizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.createPrizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.createPrizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createPrizeButton.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createPrizeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.createPrizeButton.Location = new System.Drawing.Point(44, 402);
            this.createPrizeButton.Name = "createPrizeButton";
            this.createPrizeButton.Size = new System.Drawing.Size(291, 55);
            this.createPrizeButton.TabIndex = 23;
            this.createPrizeButton.Text = "Create Prize";
            this.createPrizeButton.UseVisualStyleBackColor = true;
            this.createPrizeButton.Click += new System.EventHandler(this.createPrizeButton_Click);
            // 
            // removeTeamButton
            // 
            this.removeTeamButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.removeTeamButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.removeTeamButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.removeTeamButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeTeamButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeTeamButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.removeTeamButton.Location = new System.Drawing.Point(806, 134);
            this.removeTeamButton.Name = "removeTeamButton";
            this.removeTeamButton.Size = new System.Drawing.Size(102, 85);
            this.removeTeamButton.TabIndex = 24;
            this.removeTeamButton.Text = "Remove Selected";
            this.removeTeamButton.UseVisualStyleBackColor = true;
            this.removeTeamButton.Click += new System.EventHandler(this.removeTeamButton_Click);
            // 
            // removePrizeButton
            // 
            this.removePrizeButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.removePrizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.removePrizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.removePrizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removePrizeButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removePrizeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.removePrizeButton.Location = new System.Drawing.Point(806, 458);
            this.removePrizeButton.Name = "removePrizeButton";
            this.removePrizeButton.Size = new System.Drawing.Size(102, 75);
            this.removePrizeButton.TabIndex = 25;
            this.removePrizeButton.Text = "Remove Selected";
            this.removePrizeButton.UseVisualStyleBackColor = true;
            this.removePrizeButton.Click += new System.EventHandler(this.removePrizeButton_Click);
            // 
            // createTournamentButton
            // 
            this.createTournamentButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.createTournamentButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.createTournamentButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.createTournamentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createTournamentButton.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createTournamentButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.createTournamentButton.Location = new System.Drawing.Point(280, 570);
            this.createTournamentButton.Name = "createTournamentButton";
            this.createTournamentButton.Size = new System.Drawing.Size(381, 55);
            this.createTournamentButton.TabIndex = 26;
            this.createTournamentButton.Text = "Create Tournament";
            this.createTournamentButton.UseVisualStyleBackColor = true;
            this.createTournamentButton.Click += new System.EventHandler(this.createTournamentButton_Click);
            // 
            // createNewLinkLabel
            // 
            this.createNewLinkLabel.AutoSize = true;
            this.createNewLinkLabel.Location = new System.Drawing.Point(220, 204);
            this.createNewLinkLabel.Name = "createNewLinkLabel";
            this.createNewLinkLabel.Size = new System.Drawing.Size(161, 38);
            this.createNewLinkLabel.TabIndex = 27;
            this.createNewLinkLabel.TabStop = true;
            this.createNewLinkLabel.Text = "Create New";
            this.createNewLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.createNewLinkLabel_LinkClicked);
            // 
            // CreateTournamentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(929, 638);
            this.Controls.Add(this.createNewLinkLabel);
            this.Controls.Add(this.createTournamentButton);
            this.Controls.Add(this.removePrizeButton);
            this.Controls.Add(this.removeTeamButton);
            this.Controls.Add(this.createPrizeButton);
            this.Controls.Add(this.prizesLabel);
            this.Controls.Add(this.prizesListBox);
            this.Controls.Add(this.selectTeamLabel);
            this.Controls.Add(this.teamsLabel);
            this.Controls.Add(this.entryFeeValue);
            this.Controls.Add(this.entryFeeLabel);
            this.Controls.Add(this.tournamentTeamsListBox);
            this.Controls.Add(this.addTeamButton);
            this.Controls.Add(this.selectTeamDropdown);
            this.Controls.Add(this.tournamentNameLabel);
            this.Controls.Add(this.tournamentNameValue);
            this.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "CreateTournamentForm";
            this.Text = "Create Tournament";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tournamentNameValue;
        private System.Windows.Forms.Label tournamentNameLabel;
        private System.Windows.Forms.ComboBox selectTeamDropdown;
        private System.Windows.Forms.Button addTeamButton;
        private System.Windows.Forms.ListBox tournamentTeamsListBox;
        private System.Windows.Forms.Label entryFeeLabel;
        private System.Windows.Forms.TextBox entryFeeValue;
        private System.Windows.Forms.Label teamsLabel;
        private System.Windows.Forms.Label selectTeamLabel;
        private System.Windows.Forms.Label prizesLabel;
        private System.Windows.Forms.ListBox prizesListBox;
        private System.Windows.Forms.Button createPrizeButton;
        private System.Windows.Forms.Button removeTeamButton;
        private System.Windows.Forms.Button removePrizeButton;
        private System.Windows.Forms.Button createTournamentButton;
        private System.Windows.Forms.LinkLabel createNewLinkLabel;
    }
}