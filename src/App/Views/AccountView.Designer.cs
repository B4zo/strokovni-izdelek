namespace App.Views;

partial class AccountView
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
        panelCard = new Panel();
        btnLogout = new Button();
        lblText = new Label();
        lblTitle = new Label();
        panelCard.SuspendLayout();
        SuspendLayout();
        // 
        // panelCard
        // 
        panelCard.BorderStyle = BorderStyle.FixedSingle;
        panelCard.Controls.Add(btnLogout);
        panelCard.Controls.Add(lblText);
        panelCard.Controls.Add(lblTitle);
        panelCard.Location = new Point(24, 24);
        panelCard.Name = "panelCard";
        panelCard.Size = new Size(420, 170);
        panelCard.TabIndex = 0;
        // 
        // btnLogout
        // 
        btnLogout.ForeColor = Color.Firebrick;
        btnLogout.Location = new Point(18, 109);
        btnLogout.Name = "btnLogout";
        btnLogout.Size = new Size(120, 32);
        btnLogout.TabIndex = 2;
        btnLogout.Text = "Odjava";
        btnLogout.UseVisualStyleBackColor = true;
        btnLogout.Click += btnLogout_Click;
        // 
        // lblText
        // 
        lblText.AutoSize = true;
        lblText.Location = new Point(18, 55);
        lblText.Name = "lblText";
        lblText.Size = new Size(203, 15);
        lblText.TabIndex = 1;
        lblText.Text = "Nastavitve racuna in odjava uporabnika";
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Location = new Point(18, 18);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(43, 15);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Racun";
        // 
        // AccountView
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(panelCard);
        Name = "AccountView";
        Size = new Size(900, 600);
        panelCard.ResumeLayout(false);
        panelCard.PerformLayout();
        ResumeLayout(false);
    }

    private Panel panelCard;
    private Button btnLogout;
    private Label lblText;
    private Label lblTitle;
}
