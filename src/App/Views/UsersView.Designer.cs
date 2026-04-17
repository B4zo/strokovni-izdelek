namespace App.Views;

partial class UsersView
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
        tableLayoutPanel1 = new TableLayoutPanel();
        panelSearch = new Panel();
        gridUsers = new DataGridView();
        flowSearch = new FlowLayoutPanel();
        txtSearch = new TextBox();
        txtRole = new TextBox();
        txtActive = new TextBox();
        btnSearch = new Button();
        btnClear = new Button();
        lblSearchTitle = new Label();
        panelEditor = new Panel();
        flowButtons = new FlowLayoutPanel();
        btnRefresh = new Button();
        panelEditorFields = new Panel();
        txtLastLogin = new TextBox();
        txtEditorRole = new TextBox();
        txtDisplayName = new TextBox();
        txtUsername = new TextBox();
        lblEditorTitle = new Label();
        tableLayoutPanel1.SuspendLayout();
        panelSearch.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridUsers).BeginInit();
        flowSearch.SuspendLayout();
        panelEditor.SuspendLayout();
        flowButtons.SuspendLayout();
        panelEditorFields.SuspendLayout();
        SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
        tableLayoutPanel1.Controls.Add(panelSearch, 0, 0);
        tableLayoutPanel1.Controls.Add(panelEditor, 1, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(1000, 700);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // panelSearch
        // 
        panelSearch.BorderStyle = BorderStyle.FixedSingle;
        panelSearch.Controls.Add(gridUsers);
        panelSearch.Controls.Add(flowSearch);
        panelSearch.Controls.Add(lblSearchTitle);
        panelSearch.Dock = DockStyle.Fill;
        panelSearch.Location = new Point(3, 3);
        panelSearch.Name = "panelSearch";
        panelSearch.Padding = new Padding(12);
        panelSearch.Size = new Size(444, 694);
        panelSearch.TabIndex = 0;
        // 
        // gridUsers
        // 
        gridUsers.AllowUserToAddRows = false;
        gridUsers.AllowUserToDeleteRows = false;
        gridUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridUsers.Location = new Point(15, 149);
        gridUsers.Name = "gridUsers";
        gridUsers.ReadOnly = true;
        gridUsers.RowHeadersVisible = false;
        gridUsers.ScrollBars = ScrollBars.Both;
        gridUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridUsers.Size = new Size(412, 528);
        gridUsers.TabIndex = 2;
        gridUsers.CellClick += gridUsers_CellClick;
        // 
        // flowSearch
        // 
        flowSearch.Controls.Add(txtSearch);
        flowSearch.Controls.Add(txtRole);
        flowSearch.Controls.Add(txtActive);
        flowSearch.Controls.Add(btnSearch);
        flowSearch.Controls.Add(btnClear);
        flowSearch.Location = new Point(15, 38);
        flowSearch.Name = "flowSearch";
        flowSearch.Size = new Size(412, 95);
        flowSearch.TabIndex = 1;
        // 
        // txtSearch
        // 
        txtSearch.Location = new Point(3, 3);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Iskanje uporabnikov";
        txtSearch.Size = new Size(396, 23);
        txtSearch.TabIndex = 0;
        // 
        // txtRole
        // 
        txtRole.Location = new Point(3, 32);
        txtRole.Name = "txtRole";
        txtRole.PlaceholderText = "Vloga";
        txtRole.Size = new Size(180, 23);
        txtRole.TabIndex = 1;
        // 
        // txtActive
        // 
        txtActive.Location = new Point(189, 32);
        txtActive.Name = "txtActive";
        txtActive.PlaceholderText = "Aktiven: da/ne";
        txtActive.Size = new Size(210, 23);
        txtActive.TabIndex = 2;
        // 
        // btnSearch
        // 
        btnSearch.Location = new Point(3, 61);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(85, 28);
        btnSearch.TabIndex = 3;
        btnSearch.Text = "Isci";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += btnSearch_Click;
        // 
        // btnClear
        // 
        btnClear.Location = new Point(94, 61);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(85, 28);
        btnClear.TabIndex = 4;
        btnClear.Text = "Pocisti";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click += btnClear_Click;
        // 
        // lblSearchTitle
        // 
        lblSearchTitle.AutoSize = true;
        lblSearchTitle.Location = new Point(15, 14);
        lblSearchTitle.Name = "lblSearchTitle";
        lblSearchTitle.Size = new Size(67, 15);
        lblSearchTitle.TabIndex = 0;
        lblSearchTitle.Text = "Uporabniki";
        // 
        // panelEditor
        // 
        panelEditor.AutoScroll = true;
        panelEditor.BorderStyle = BorderStyle.FixedSingle;
        panelEditor.Controls.Add(flowButtons);
        panelEditor.Controls.Add(panelEditorFields);
        panelEditor.Controls.Add(lblEditorTitle);
        panelEditor.Dock = DockStyle.Fill;
        panelEditor.Location = new Point(453, 3);
        panelEditor.Name = "panelEditor";
        panelEditor.Padding = new Padding(12);
        panelEditor.Size = new Size(544, 694);
        panelEditor.TabIndex = 1;
        // 
        // flowButtons
        // 
        flowButtons.Controls.Add(btnRefresh);
        flowButtons.Location = new Point(16, 274);
        flowButtons.Name = "flowButtons";
        flowButtons.Size = new Size(120, 40);
        flowButtons.TabIndex = 2;
        // 
        // btnRefresh
        // 
        btnRefresh.Location = new Point(3, 3);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(75, 30);
        btnRefresh.TabIndex = 0;
        btnRefresh.Text = "Osvezi";
        btnRefresh.UseVisualStyleBackColor = true;
        btnRefresh.Click += btnRefresh_Click;
        // 
        // panelEditorFields
        // 
        panelEditorFields.BorderStyle = BorderStyle.FixedSingle;
        panelEditorFields.Controls.Add(txtLastLogin);
        panelEditorFields.Controls.Add(txtEditorRole);
        panelEditorFields.Controls.Add(txtDisplayName);
        panelEditorFields.Controls.Add(txtUsername);
        panelEditorFields.Location = new Point(16, 38);
        panelEditorFields.Name = "panelEditorFields";
        panelEditorFields.Size = new Size(494, 220);
        panelEditorFields.TabIndex = 1;
        // 
        // txtLastLogin
        // 
        txtLastLogin.Location = new Point(15, 102);
        txtLastLogin.Name = "txtLastLogin";
        txtLastLogin.PlaceholderText = "Zadnja prijava";
        txtLastLogin.ReadOnly = true;
        txtLastLogin.Size = new Size(280, 23);
        txtLastLogin.TabIndex = 3;
        // 
        // txtEditorRole
        // 
        txtEditorRole.Location = new Point(15, 73);
        txtEditorRole.Name = "txtEditorRole";
        txtEditorRole.PlaceholderText = "Vloga";
        txtEditorRole.ReadOnly = true;
        txtEditorRole.Size = new Size(280, 23);
        txtEditorRole.TabIndex = 2;
        // 
        // txtDisplayName
        // 
        txtDisplayName.Location = new Point(15, 44);
        txtDisplayName.Name = "txtDisplayName";
        txtDisplayName.PlaceholderText = "Prikazno ime";
        txtDisplayName.ReadOnly = true;
        txtDisplayName.Size = new Size(280, 23);
        txtDisplayName.TabIndex = 1;
        // 
        // txtUsername
        // 
        txtUsername.Location = new Point(15, 15);
        txtUsername.Name = "txtUsername";
        txtUsername.PlaceholderText = "Uporabnisko ime";
        txtUsername.ReadOnly = true;
        txtUsername.Size = new Size(280, 23);
        txtUsername.TabIndex = 0;
        // 
        // lblEditorTitle
        // 
        lblEditorTitle.AutoSize = true;
        lblEditorTitle.Location = new Point(16, 14);
        lblEditorTitle.Name = "lblEditorTitle";
        lblEditorTitle.Size = new Size(121, 15);
        lblEditorTitle.TabIndex = 0;
        lblEditorTitle.Text = "Podrobnosti uporab.";
        // 
        // UsersView
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(tableLayoutPanel1);
        Name = "UsersView";
        Size = new Size(1000, 700);
        tableLayoutPanel1.ResumeLayout(false);
        panelSearch.ResumeLayout(false);
        panelSearch.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)gridUsers).EndInit();
        flowSearch.ResumeLayout(false);
        flowSearch.PerformLayout();
        panelEditor.ResumeLayout(false);
        panelEditor.PerformLayout();
        flowButtons.ResumeLayout(false);
        panelEditorFields.ResumeLayout(false);
        panelEditorFields.PerformLayout();
        ResumeLayout(false);
    }

    private TableLayoutPanel tableLayoutPanel1;
    private Panel panelSearch;
    private DataGridView gridUsers;
    private FlowLayoutPanel flowSearch;
    private TextBox txtSearch;
    private TextBox txtRole;
    private TextBox txtActive;
    private Button btnSearch;
    private Button btnClear;
    private Label lblSearchTitle;
    private Panel panelEditor;
    private FlowLayoutPanel flowButtons;
    private Button btnRefresh;
    private Panel panelEditorFields;
    private TextBox txtLastLogin;
    private TextBox txtEditorRole;
    private TextBox txtDisplayName;
    private TextBox txtUsername;
    private Label lblEditorTitle;
}
