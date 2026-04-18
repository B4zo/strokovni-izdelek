namespace App.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        panelNav = new Panel();
        btnAccount = new Button();
        btnPolicies = new Button();
        btnRegistrations = new Button();
        btnVehicles = new Button();
        btnParties = new Button();
        panelContent = new Panel();
        statusStrip1 = new StatusStrip();
        toolStripStatusLabel1 = new ToolStripStatusLabel();
        panelNav.SuspendLayout();
        statusStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // panelNav
        // 
        panelNav.Controls.Add(btnAccount);
        panelNav.Controls.Add(btnPolicies);
        panelNav.Controls.Add(btnRegistrations);
        panelNav.Controls.Add(btnVehicles);
        panelNav.Controls.Add(btnParties);
        panelNav.Dock = DockStyle.Left;
        panelNav.Location = new Point(0, 0);
        panelNav.Name = "panelNav";
        panelNav.Padding = new Padding(11, 13, 11, 13);
        panelNav.Size = new Size(229, 935);
        panelNav.TabIndex = 0;
        // 
        // btnAccount
        // 
        btnAccount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnAccount.Location = new Point(17, 865);
        btnAccount.Name = "btnAccount";
        btnAccount.Size = new Size(189, 50);
        btnAccount.TabIndex = 9;
        btnAccount.Text = "Racun";
        btnAccount.UseVisualStyleBackColor = true;
        btnAccount.Click += btnAccount_Click;
        // 
        // btnPolicies
        // 
        btnPolicies.Location = new Point(17, 244);
        btnPolicies.Name = "btnPolicies";
        btnPolicies.Size = new Size(189, 50);
        btnPolicies.TabIndex = 4;
        btnPolicies.Text = "Police";
        btnPolicies.UseVisualStyleBackColor = true;
        btnPolicies.Click += btnPolicies_Click;
        // 
        // btnRegistrations
        // 
        btnRegistrations.Location = new Point(17, 184);
        btnRegistrations.Name = "btnRegistrations";
        btnRegistrations.Size = new Size(189, 50);
        btnRegistrations.TabIndex = 3;
        btnRegistrations.Text = "Registracije";
        btnRegistrations.UseVisualStyleBackColor = true;
        btnRegistrations.Click += btnRegistrations_Click;
        // 
        // btnVehicles
        // 
        btnVehicles.Location = new Point(17, 124);
        btnVehicles.Name = "btnVehicles";
        btnVehicles.Size = new Size(189, 50);
        btnVehicles.TabIndex = 2;
        btnVehicles.Text = "Vozila";
        btnVehicles.UseVisualStyleBackColor = true;
        btnVehicles.Click += btnVehicles_Click;
        // 
        // btnParties
        // 
        btnParties.Location = new Point(17, 64);
        btnParties.Name = "btnParties";
        btnParties.Size = new Size(189, 50);
        btnParties.TabIndex = 1;
        btnParties.Text = "Stranke";
        btnParties.UseVisualStyleBackColor = true;
        btnParties.Click += btnParties_Click;
        // 
        // panelContent
        // 
        panelContent.BorderStyle = BorderStyle.FixedSingle;
        panelContent.Dock = DockStyle.Fill;
        panelContent.Location = new Point(229, 0);
        panelContent.Name = "panelContent";
        panelContent.Padding = new Padding(9, 10, 9, 10);
        panelContent.Size = new Size(1120, 935);
        panelContent.TabIndex = 1;
        // 
        // statusStrip1
        // 
        statusStrip1.ImageScalingSize = new Size(24, 24);
        statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
        statusStrip1.Location = new Point(229, 903);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new Size(1120, 32);
        statusStrip1.TabIndex = 2;
        statusStrip1.Text = "statusStrip1";
        // 
        // toolStripStatusLabel1
        // 
        toolStripStatusLabel1.Name = "toolStripStatusLabel1";
        toolStripStatusLabel1.Size = new Size(81, 25);
        toolStripStatusLabel1.Text = "Prijavljen";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1349, 935);
        Controls.Add(statusStrip1);
        Controls.Add(panelContent);
        Controls.Add(panelNav);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Tehnicni Pregledi";
        WindowState = FormWindowState.Maximized;
        panelNav.ResumeLayout(false);
        panelNav.PerformLayout();
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private Panel panelNav;
    private Button btnAccount;
    private Button btnPolicies;
    private Button btnRegistrations;
    private Button btnVehicles;
    private Button btnParties;
    private Panel panelContent;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel toolStripStatusLabel1;
}


