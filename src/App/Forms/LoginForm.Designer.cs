namespace App.Forms
{
    partial class LoginForm
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
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            buttonLogIn = new Button();
            textBoxPassword = new TextBox();
            textBoxUsername = new TextBox();
            menuStrip1 = new MenuStrip();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            connectionToolStripMenuItem = new ToolStripMenuItem();
            connectionsToolStripMenuItem = new ToolStripMenuItem();
            addConnectionsToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelConnection = new ToolStripStatusLabel();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(panel1, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 12F));
            tableLayoutPanel1.Size = new Size(858, 403);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonLogIn);
            panel1.Controls.Add(textBoxPassword);
            panel1.Controls.Add(textBoxUsername);
            panel1.Location = new Point(287, 109);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(283, 184);
            panel1.TabIndex = 0;
            // 
            // buttonLogIn
            // 
            buttonLogIn.Font = new Font("Segoe UI", 11F);
            buttonLogIn.Location = new Point(181, 134);
            buttonLogIn.Margin = new Padding(2);
            buttonLogIn.Name = "buttonLogIn";
            buttonLogIn.Size = new Size(83, 28);
            buttonLogIn.TabIndex = 4;
            buttonLogIn.Text = "Prijavi se";
            buttonLogIn.UseVisualStyleBackColor = true;
            buttonLogIn.Click += buttonLogIn_Click;
            // 
            // textBoxPassword
            // 
            textBoxPassword.Font = new Font("Segoe UI", 13F);
            textBoxPassword.Location = new Point(24, 77);
            textBoxPassword.Margin = new Padding(2);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PasswordChar = '*';
            textBoxPassword.PlaceholderText = " Geslo";
            textBoxPassword.Size = new Size(194, 31);
            textBoxPassword.TabIndex = 1;
            // 
            // textBoxUsername
            // 
            textBoxUsername.Font = new Font("Segoe UI", 13F);
            textBoxUsername.Location = new Point(24, 22);
            textBoxUsername.Margin = new Padding(2);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.PlaceholderText = " Uporabniško Ime";
            textBoxUsername.Size = new Size(194, 31);
            textBoxUsername.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(858, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { connectionToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(74, 20);
            settingsToolStripMenuItem.Text = "&Nastavitve";
            // 
            // connectionToolStripMenuItem
            // 
            connectionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { connectionsToolStripMenuItem, addConnectionsToolStripMenuItem });
            connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            connectionToolStripMenuItem.Size = new Size(180, 22);
            connectionToolStripMenuItem.Text = "&Povezava";
            connectionToolStripMenuItem.DropDownOpening += connectionToolStripMenuItem_DropDownOpening;
            // 
            // connectionsToolStripMenuItem
            // 
            connectionsToolStripMenuItem.Name = "connectionsToolStripMenuItem";
            connectionsToolStripMenuItem.Size = new Size(180, 22);
            connectionsToolStripMenuItem.Text = "&Povezave";
            // 
            // addConnectionsToolStripMenuItem
            // 
            addConnectionsToolStripMenuItem.Name = "addConnectionsToolStripMenuItem";
            addConnectionsToolStripMenuItem.Size = new Size(180, 22);
            addConnectionsToolStripMenuItem.Text = "&Dodaj";
            addConnectionsToolStripMenuItem.Click += addConnectionsToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelConnection });
            statusStrip1.Location = new Point(0, 381);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(858, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelConnection
            // 
            toolStripStatusLabelConnection.Name = "toolStripStatusLabelConnection";
            toolStripStatusLabelConnection.Size = new Size(66, 17);
            toolStripStatusLabelConnection.Text = "(ni izbrana)";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(858, 403);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoginForm";
            Deactivate += LoginForm_Deactivate;
            Shown += LoginForm_Shown;
            KeyDown += LoginForm_KeyDown;
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private TextBox textBoxPassword;
        private TextBox textBoxUsername;
        private Button buttonLogIn;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem connectionToolStripMenuItem;
        private ToolStripMenuItem connectionsToolStripMenuItem;
        private ToolStripMenuItem addConnectionsToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabelConnection;
    }
}