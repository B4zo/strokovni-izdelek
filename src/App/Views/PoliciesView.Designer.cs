namespace App.Views;

partial class PoliciesView
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
        gridPolicies = new DataGridView();
        flowSearch = new FlowLayoutPanel();
        txtSearch = new TextBox();
        txtVin = new TextBox();
        txtPartyOrInsurer = new TextBox();
        btnSearch = new Button();
        btnClear = new Button();
        lblSearchTitle = new Label();
        panelEditor = new Panel();
        flowButtons = new FlowLayoutPanel();
        btnRefresh = new Button();
        panelEditorFields = new Panel();
        txtInsurer = new TextBox();
        txtParty = new TextBox();
        txtVehicle = new TextBox();
        txtPolicyNo = new TextBox();
        lblEditorTitle = new Label();
        tableLayoutPanel1.SuspendLayout();
        panelSearch.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridPolicies).BeginInit();
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
        panelSearch.Controls.Add(gridPolicies);
        panelSearch.Controls.Add(flowSearch);
        panelSearch.Controls.Add(lblSearchTitle);
        panelSearch.Dock = DockStyle.Fill;
        panelSearch.Location = new Point(3, 3);
        panelSearch.Name = "panelSearch";
        panelSearch.Padding = new Padding(12);
        panelSearch.Size = new Size(444, 694);
        panelSearch.TabIndex = 0;
        // 
        // gridPolicies
        // 
        gridPolicies.AllowUserToAddRows = false;
        gridPolicies.AllowUserToDeleteRows = false;
        gridPolicies.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridPolicies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridPolicies.Location = new Point(15, 149);
        gridPolicies.Name = "gridPolicies";
        gridPolicies.ReadOnly = true;
        gridPolicies.RowHeadersVisible = false;
        gridPolicies.ScrollBars = ScrollBars.Both;
        gridPolicies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridPolicies.Size = new Size(412, 528);
        gridPolicies.TabIndex = 2;
        gridPolicies.CellClick += gridPolicies_CellClick;
        // 
        // flowSearch
        // 
        flowSearch.Controls.Add(txtSearch);
        flowSearch.Controls.Add(txtVin);
        flowSearch.Controls.Add(txtPartyOrInsurer);
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
        txtSearch.PlaceholderText = "Iskanje polic";
        txtSearch.Size = new Size(396, 23);
        txtSearch.TabIndex = 0;
        // 
        // txtVin
        // 
        txtVin.Location = new Point(3, 32);
        txtVin.Name = "txtVin";
        txtVin.PlaceholderText = "VIN";
        txtVin.Size = new Size(180, 23);
        txtVin.TabIndex = 1;
        // 
        // txtPartyOrInsurer
        // 
        txtPartyOrInsurer.Location = new Point(189, 32);
        txtPartyOrInsurer.Name = "txtPartyOrInsurer";
        txtPartyOrInsurer.PlaceholderText = "Stranka ali zavarovalnica";
        txtPartyOrInsurer.Size = new Size(210, 23);
        txtPartyOrInsurer.TabIndex = 2;
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
        lblSearchTitle.Size = new Size(39, 15);
        lblSearchTitle.TabIndex = 0;
        lblSearchTitle.Text = "Police";
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
        panelEditorFields.Controls.Add(txtInsurer);
        panelEditorFields.Controls.Add(txtParty);
        panelEditorFields.Controls.Add(txtVehicle);
        panelEditorFields.Controls.Add(txtPolicyNo);
        panelEditorFields.Location = new Point(16, 38);
        panelEditorFields.Name = "panelEditorFields";
        panelEditorFields.Size = new Size(494, 220);
        panelEditorFields.TabIndex = 1;
        // 
        // txtInsurer
        // 
        txtInsurer.Location = new Point(15, 102);
        txtInsurer.Name = "txtInsurer";
        txtInsurer.PlaceholderText = "Zavarovalnica";
        txtInsurer.ReadOnly = true;
        txtInsurer.Size = new Size(280, 23);
        txtInsurer.TabIndex = 3;
        // 
        // txtParty
        // 
        txtParty.Location = new Point(15, 73);
        txtParty.Name = "txtParty";
        txtParty.PlaceholderText = "Stranka";
        txtParty.ReadOnly = true;
        txtParty.Size = new Size(280, 23);
        txtParty.TabIndex = 2;
        // 
        // txtVehicle
        // 
        txtVehicle.Location = new Point(15, 44);
        txtVehicle.Name = "txtVehicle";
        txtVehicle.PlaceholderText = "Vozilo";
        txtVehicle.ReadOnly = true;
        txtVehicle.Size = new Size(320, 23);
        txtVehicle.TabIndex = 1;
        // 
        // txtPolicyNo
        // 
        txtPolicyNo.Location = new Point(15, 15);
        txtPolicyNo.Name = "txtPolicyNo";
        txtPolicyNo.PlaceholderText = "Polica";
        txtPolicyNo.ReadOnly = true;
        txtPolicyNo.Size = new Size(220, 23);
        txtPolicyNo.TabIndex = 0;
        // 
        // lblEditorTitle
        // 
        lblEditorTitle.AutoSize = true;
        lblEditorTitle.Location = new Point(16, 14);
        lblEditorTitle.Name = "lblEditorTitle";
        lblEditorTitle.Size = new Size(113, 15);
        lblEditorTitle.TabIndex = 0;
        lblEditorTitle.Text = "Podrobnosti police";
        // 
        // PoliciesView
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(tableLayoutPanel1);
        Name = "PoliciesView";
        Size = new Size(1000, 700);
        tableLayoutPanel1.ResumeLayout(false);
        panelSearch.ResumeLayout(false);
        panelSearch.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)gridPolicies).EndInit();
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
    private DataGridView gridPolicies;
    private FlowLayoutPanel flowSearch;
    private TextBox txtSearch;
    private TextBox txtVin;
    private TextBox txtPartyOrInsurer;
    private Button btnSearch;
    private Button btnClear;
    private Label lblSearchTitle;
    private Panel panelEditor;
    private FlowLayoutPanel flowButtons;
    private Button btnRefresh;
    private Panel panelEditorFields;
    private TextBox txtInsurer;
    private TextBox txtParty;
    private TextBox txtVehicle;
    private TextBox txtPolicyNo;
    private Label lblEditorTitle;
}

