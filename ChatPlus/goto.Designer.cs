namespace ChatPlus
{
    partial class Goto
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
            this.loginPanel = new System.Windows.Forms.Panel();
            this.card = new System.Windows.Forms.Panel();
            this.avatar = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.inputBorder = new System.Windows.Forms.Panel();
            this.inputInner = new System.Windows.Forms.Panel();
            this.nicknameBox = new System.Windows.Forms.TextBox();
            this.hintLabel = new System.Windows.Forms.Label();
            this.submitButton = new System.Windows.Forms.Button();
            this.loginPanel.SuspendLayout();
            this.card.SuspendLayout();
            this.inputBorder.SuspendLayout();
            this.inputInner.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginPanel
            // 
            this.loginPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.loginPanel.Controls.Add(this.card);
            this.loginPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginPanel.Location = new System.Drawing.Point(0, 0);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(412, 381);
            this.loginPanel.TabIndex = 0;
            this.loginPanel.Resize += new System.EventHandler(this.loginPanel_Resize);
            // 
            // card
            // 
            this.card.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.card.Controls.Add(this.avatar);
            this.card.Controls.Add(this.titleLabel);
            this.card.Controls.Add(this.subtitleLabel);
            this.card.Controls.Add(this.inputBorder);
            this.card.Controls.Add(this.hintLabel);
            this.card.Controls.Add(this.submitButton);
            this.card.Location = new System.Drawing.Point(0, 0);
            this.card.Name = "card";
            this.card.Size = new System.Drawing.Size(396, 344);
            this.card.TabIndex = 0;
            // 
            // avatar
            // 
            this.avatar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.avatar.Location = new System.Drawing.Point(166, 30);
            this.avatar.Name = "avatar";
            this.avatar.Size = new System.Drawing.Size(64, 64);
            this.avatar.TabIndex = 0;
            this.avatar.Paint += new System.Windows.Forms.PaintEventHandler(this.avatar_Paint);
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 16F);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.titleLabel.Location = new System.Drawing.Point(0, 108);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(396, 34);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "What should we call you?";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(94)))), ((int)(((byte)(90)))));
            this.subtitleLabel.Location = new System.Drawing.Point(0, 142);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(396, 24);
            this.subtitleLabel.TabIndex = 2;
            this.subtitleLabel.Text = "Choose a name others will see";
            this.subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // inputBorder
            // 
            this.inputBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(178)))), ((int)(((byte)(169)))));
            this.inputBorder.Controls.Add(this.inputInner);
            this.inputBorder.Location = new System.Drawing.Point(36, 190);
            this.inputBorder.Name = "inputBorder";
            this.inputBorder.Size = new System.Drawing.Size(324, 44);
            this.inputBorder.TabIndex = 3;
            // 
            // inputInner
            // 
            this.inputInner.BackColor = System.Drawing.Color.White;
            this.inputInner.Controls.Add(this.nicknameBox);
            this.inputInner.Location = new System.Drawing.Point(1, 1);
            this.inputInner.Name = "inputInner";
            this.inputInner.Size = new System.Drawing.Size(322, 42);
            this.inputInner.TabIndex = 0;
            this.inputInner.Click += new System.EventHandler(this.inputInner_Click);
            // 
            // nicknameBox
            // 
            this.nicknameBox.BackColor = System.Drawing.Color.White;
            this.nicknameBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nicknameBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.nicknameBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(135)))), ((int)(((byte)(128)))));
            this.nicknameBox.Location = new System.Drawing.Point(14, 10);
            this.nicknameBox.MaxLength = 20;
            this.nicknameBox.Name = "nicknameBox";
            this.nicknameBox.Size = new System.Drawing.Size(296, 22);
            this.nicknameBox.TabIndex = 0;
            this.nicknameBox.Text = "Your nickname";
            this.nicknameBox.TextChanged += new System.EventHandler(this.nicknameBox_TextChanged);
            this.nicknameBox.Enter += new System.EventHandler(this.nicknameBox_Enter);
            this.nicknameBox.Leave += new System.EventHandler(this.nicknameBox_Leave);
            // 
            // hintLabel
            // 
            this.hintLabel.AutoSize = true;
            this.hintLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hintLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(135)))), ((int)(((byte)(128)))));
            this.hintLabel.Location = new System.Drawing.Point(36, 241);
            this.hintLabel.Name = "hintLabel";
            this.hintLabel.Size = new System.Drawing.Size(99, 15);
            this.hintLabel.TabIndex = 4;
            this.hintLabel.Text = "2 to 20 characters";
            // 
            // submitButton
            // 
            this.submitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.submitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submitButton.FlatAppearance.BorderSize = 0;
            this.submitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(19)))));
            this.submitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(65)))));
            this.submitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submitButton.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.submitButton.ForeColor = System.Drawing.Color.White;
            this.submitButton.Location = new System.Drawing.Point(36, 284);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(324, 44);
            this.submitButton.TabIndex = 5;
            this.submitButton.Text = "Join chat  →";
            this.submitButton.UseVisualStyleBackColor = false;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // Goto
            // 
            this.AcceptButton = this.submitButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(412, 381);
            this.Controls.Add(this.loginPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Goto";
            this.Text = "Goto";
            this.loginPanel.ResumeLayout(false);
            this.card.ResumeLayout(false);
            this.card.PerformLayout();
            this.inputBorder.ResumeLayout(false);
            this.inputInner.ResumeLayout(false);
            this.inputInner.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.Panel card;
        private System.Windows.Forms.Panel avatar;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Panel inputBorder;
        private System.Windows.Forms.Panel inputInner;
        private System.Windows.Forms.TextBox nicknameBox;
        private System.Windows.Forms.Label hintLabel;
        private System.Windows.Forms.Button submitButton;
    }
}
