namespace App.Views;

partial class VehicleRegistryView
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
        gridVehicles = new DataGridView();
        flowSearch = new FlowLayoutPanel();
        txtSearch = new TextBox();
        txtVin = new TextBox();
        txtOwner = new TextBox();
        btnSearch = new Button();
        btnClear = new Button();
        lblSearchTitle = new Label();
        panelDetails = new Panel();
        lblRegistrations = new Label();
        gridRegistrations = new DataGridView();
        lblOwnership = new Label();
        gridOwnership = new DataGridView();
        flowButtons = new FlowLayoutPanel();
        btnRefresh = new Button();
        panelInfo = new Panel();
        txtCurrentPlate = new TextBox();
        txtCurrentRegistration = new TextBox();
        txtCurrentOwner = new TextBox();
        txtVehicleDisplay = new TextBox();
        txtCategory = new TextBox();
        txtVehicleVin = new TextBox();
        lblEditorTitle = new Label();
        tableLayoutPanel1.SuspendLayout();
        panelSearch.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridVehicles).BeginInit();
        flowSearch.SuspendLayout();
        panelDetails.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridRegistrations).BeginInit();
        ((System.ComponentModel.ISupportInitialize)gridOwnership).BeginInit();
        flowButtons.SuspendLayout();
        panelInfo.SuspendLayout();
        SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
        tableLayoutPanel1.Controls.Add(panelSearch, 0, 0);
        tableLayoutPanel1.Controls.Add(panelDetails, 1, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(1000, 745);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // panelSearch
        // 
        panelSearch.BorderStyle = BorderStyle.FixedSingle;
        panelSearch.Controls.Add(gridVehicles);
        panelSearch.Controls.Add(flowSearch);
        panelSearch.Controls.Add(lblSearchTitle);
        panelSearch.Dock = DockStyle.Fill;
        panelSearch.Location = new Point(3, 3);
        panelSearch.Name = "panelSearch";
        panelSearch.Padding = new Padding(12);
        panelSearch.Size = new Size(444, 739);
        panelSearch.TabIndex = 0;
        // 
        // gridVehicles
        // 
        gridVehicles.AllowUserToAddRows = false;
        gridVehicles.AllowUserToDeleteRows = false;
        gridVehicles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridVehicles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridVehicles.Location = new Point(15, 149);
        gridVehicles.Name = "gridVehicles";
        gridVehicles.ReadOnly = true;
        gridVehicles.RowHeadersVisible = false;
        gridVehicles.ScrollBars = ScrollBars.Both;
        gridVehicles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridVehicles.Size = new Size(412, 573);
        gridVehicles.TabIndex = 2;
        gridVehicles.CellClick += gridVehicles_CellClick;
        // 
        // flowSearch
        // 
        flowSearch.Controls.Add(txtSearch);
        flowSearch.Controls.Add(txtVin);
        flowSearch.Controls.Add(txtOwner);
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
        txtSearch.PlaceholderText = "Iskanje po VIN, znamki, modelu";
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
        // txtOwner
        // 
        txtOwner.Location = new Point(189, 32);
        txtOwner.Name = "txtOwner";
        txtOwner.PlaceholderText = "Kategorija";
        txtOwner.Size = new Size(210, 23);
        txtOwner.TabIndex = 2;
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
        lblSearchTitle.Size = new Size(113, 15);
        lblSearchTitle.TabIndex = 0;
        lblSearchTitle.Text = "Vozila in registracije";
        // 
        // panelDetails
        // 
        panelDetails.AutoScroll = true;
        panelDetails.BorderStyle = BorderStyle.FixedSingle;
        panelDetails.Controls.Add(lblRegistrations);
        panelDetails.Controls.Add(gridRegistrations);
        panelDetails.Controls.Add(lblOwnership);
        panelDetails.Controls.Add(gridOwnership);
        panelDetails.Controls.Add(flowButtons);
        panelDetails.Controls.Add(panelInfo);
        panelDetails.Controls.Add(lblEditorTitle);
        panelDetails.Dock = DockStyle.Fill;
        panelDetails.Location = new Point(453, 3);
        panelDetails.Name = "panelDetails";
        panelDetails.Padding = new Padding(12);
        panelDetails.Size = new Size(544, 739);
        panelDetails.TabIndex = 1;
        // 
        // lblRegistrations
        // 
        lblRegistrations.AutoSize = true;
        lblRegistrations.Location = new Point(16, 546);
        lblRegistrations.Name = "lblRegistrations";
        lblRegistrations.Size = new Size(91, 15);
        lblRegistrations.TabIndex = 6;
        lblRegistrations.Text = "Zgod. registracij";
        // 
        // gridRegistrations
        // 
        gridRegistrations.AllowUserToAddRows = false;
        gridRegistrations.AllowUserToDeleteRows = false;
        gridRegistrations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridRegistrations.Location = new Point(16, 567);
        gridRegistrations.Name = "gridRegistrations";
        gridRegistrations.ReadOnly = true;
        gridRegistrations.RowHeadersVisible = false;
        gridRegistrations.ScrollBars = ScrollBars.Both;
        gridRegistrations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridRegistrations.Size = new Size(494, 140);
        gridRegistrations.TabIndex = 5;
        // 
        // lblOwnership
        // 
        lblOwnership.AutoSize = true;
        lblOwnership.Location = new Point(16, 366);
        lblOwnership.Name = "lblOwnership";
        lblOwnership.Size = new Size(101, 15);
        lblOwnership.TabIndex = 4;
        lblOwnership.Text = "Zgod. lastnistva";
        // 
        // gridOwnership
        // 
        gridOwnership.AllowUserToAddRows = false;
        gridOwnership.AllowUserToDeleteRows = false;
        gridOwnership.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridOwnership.Location = new Point(16, 387);
        gridOwnership.Name = "gridOwnership";
        gridOwnership.ReadOnly = true;
        gridOwnership.RowHeadersVisible = false;
        gridOwnership.ScrollBars = ScrollBars.Both;
        gridOwnership.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridOwnership.Size = new Size(494, 140);
        gridOwnership.TabIndex = 3;
        // 
        // flowButtons
        // 
        flowButtons.Controls.Add(btnRefresh);
        flowButtons.Location = new Point(16, 312);
        flowButtons.Name = "flowButtons";
        flowButtons.Size = new Size(120, 40);
        flowButtons.TabIndex = 2;
        // 
        // btnRefresh
        // 
        btnRefresh.Location = new Point(3, 3);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(85, 30);
        btnRefresh.TabIndex = 0;
        btnRefresh.Text = "Osvezi";
        btnRefresh.UseVisualStyleBackColor = true;
        btnRefresh.Click += btnRefresh_Click;
        // 
        // panelInfo
        // 
        panelInfo.BorderStyle = BorderStyle.FixedSingle;
        panelInfo.Controls.Add(txtCurrentPlate);
        panelInfo.Controls.Add(txtCurrentRegistration);
        panelInfo.Controls.Add(txtCurrentOwner);
        panelInfo.Controls.Add(txtVehicleDisplay);
        panelInfo.Controls.Add(txtCategory);
        panelInfo.Controls.Add(txtVehicleVin);
        panelInfo.Location = new Point(16, 38);
        panelInfo.Name = "panelInfo";
        panelInfo.Size = new Size(494, 260);
        panelInfo.TabIndex = 1;
        // 
        // txtCurrentPlate
        // 
        txtCurrentPlate.Location = new Point(15, 160);
        txtCurrentPlate.Name = "txtCurrentPlate";
        txtCurrentPlate.PlaceholderText = "Trenutna tablica";
        txtCurrentPlate.ReadOnly = true;
        txtCurrentPlate.Size = new Size(220, 23);
        txtCurrentPlate.TabIndex = 5;
        // 
        // txtCurrentRegistration
        // 
        txtCurrentRegistration.Location = new Point(15, 131);
        txtCurrentRegistration.Name = "txtCurrentRegistration";
        txtCurrentRegistration.PlaceholderText = "Trenutna registracija";
        txtCurrentRegistration.ReadOnly = true;
        txtCurrentRegistration.Size = new Size(220, 23);
        txtCurrentRegistration.TabIndex = 4;
        // 
        // txtCurrentOwner
        // 
        txtCurrentOwner.Location = new Point(15, 102);
        txtCurrentOwner.Name = "txtCurrentOwner";
        txtCurrentOwner.PlaceholderText = "Trenutni lastnik";
        txtCurrentOwner.ReadOnly = true;
        txtCurrentOwner.Size = new Size(320, 23);
        txtCurrentOwner.TabIndex = 3;
        // 
        // txtVehicleDisplay
        // 
        txtVehicleDisplay.Location = new Point(15, 73);
        txtVehicleDisplay.Name = "txtVehicleDisplay";
        txtVehicleDisplay.PlaceholderText = "Znamka in model";
        txtVehicleDisplay.ReadOnly = true;
        txtVehicleDisplay.Size = new Size(320, 23);
        txtVehicleDisplay.TabIndex = 2;
        // 
        // txtCategory
        // 
        txtCategory.Location = new Point(15, 44);
        txtCategory.Name = "txtCategory";
        txtCategory.PlaceholderText = "Kategorija";
        txtCategory.ReadOnly = true;
        txtCategory.Size = new Size(260, 23);
        txtCategory.TabIndex = 1;
        // 
        // txtVehicleVin
        // 
        txtVehicleVin.Location = new Point(15, 15);
        txtVehicleVin.Name = "txtVehicleVin";
        txtVehicleVin.PlaceholderText = "VIN";
        txtVehicleVin.ReadOnly = true;
        txtVehicleVin.Size = new Size(320, 23);
        txtVehicleVin.TabIndex = 0;
        // 
        // lblEditorTitle
        // 
        lblEditorTitle.AutoSize = true;
        lblEditorTitle.Location = new Point(16, 14);
        lblEditorTitle.Name = "lblEditorTitle";
        lblEditorTitle.Size = new Size(107, 15);
        lblEditorTitle.TabIndex = 0;
        lblEditorTitle.Text = "Podatki o vozilu";
        // 
        // VehicleRegistryView
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(tableLayoutPanel1);
        Name = "VehicleRegistryView";
        Size = new Size(1000, 745);
        tableLayoutPanel1.ResumeLayout(false);
        panelSearch.ResumeLayout(false);
        panelSearch.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)gridVehicles).EndInit();
        flowSearch.ResumeLayout(false);
        flowSearch.PerformLayout();
        panelDetails.ResumeLayout(false);
        panelDetails.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)gridRegistrations).EndInit();
        ((System.ComponentModel.ISupportInitialize)gridOwnership).EndInit();
        flowButtons.ResumeLayout(false);
        panelInfo.ResumeLayout(false);
        panelInfo.PerformLayout();
        ResumeLayout(false);
    }

    private TableLayoutPanel tableLayoutPanel1;
    private Panel panelSearch;
    private DataGridView gridVehicles;
    private FlowLayoutPanel flowSearch;
    private TextBox txtSearch;
    private TextBox txtVin;
    private TextBox txtOwner;
    private Button btnSearch;
    private Button btnClear;
    private Label lblSearchTitle;
    private Panel panelDetails;
    private Label lblRegistrations;
    private DataGridView gridRegistrations;
    private Label lblOwnership;
    private DataGridView gridOwnership;
    private FlowLayoutPanel flowButtons;
    private Button btnRefresh;
    private Panel panelInfo;
    private TextBox txtCurrentPlate;
    private TextBox txtCurrentRegistration;
    private TextBox txtCurrentOwner;
    private TextBox txtVehicleDisplay;
    private TextBox txtCategory;
    private TextBox txtVehicleVin;
    private Label lblEditorTitle;
}
