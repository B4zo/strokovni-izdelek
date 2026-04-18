namespace App.Views;

partial class PartiesView
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
        lblSearchTitle = new Label();
        flowSearch = new FlowLayoutPanel();
        txtSearch = new TextBox();
        cboTypeFilter = new ComboBox();
        txtAddressFilter = new TextBox();
        txtPhoneFilter = new TextBox();
        txtEmailFilter = new TextBox();
        btnSearch = new Button();
        btnClearSearch = new Button();
        gridParty = new DataGridView();
        panelEditor = new Panel();
        lblEditorTitle = new Label();
        panelEditorFields = new Panel();
        lblOwnedVehicles = new Label();
        gridOwnedVehicles = new DataGridView();
        lblRegisteredVehicles = new Label();
        gridRegisteredVehicles = new DataGridView();
        flowType = new FlowLayoutPanel();
        radioPerson = new RadioButton();
        radioCompany = new RadioButton();
        panelPersonFields = new Panel();
        txtFullName = new TextBox();
        dateBirth = new DateTimePicker();
        txtEmso = new TextBox();
        panelCompanyFields = new Panel();
        txtCompanyName = new TextBox();
        txtCompanyRegNo = new TextBox();
        txtTaxNo = new TextBox();
        txtAddress = new TextBox();
        txtPhone = new TextBox();
        txtEmail = new TextBox();
        flowButtons = new FlowLayoutPanel();
        btnRefresh = new Button();
        tableLayoutPanel1.SuspendLayout();
        panelSearch.SuspendLayout();
        flowSearch.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridParty).BeginInit();
        panelEditor.SuspendLayout();
        panelEditorFields.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridOwnedVehicles).BeginInit();
        ((System.ComponentModel.ISupportInitialize)gridRegisteredVehicles).BeginInit();
        flowType.SuspendLayout();
        panelPersonFields.SuspendLayout();
        panelCompanyFields.SuspendLayout();
        flowButtons.SuspendLayout();
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
        tableLayoutPanel1.Size = new Size(1000, 745);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // panelSearch
        // 
        panelSearch.BorderStyle = BorderStyle.FixedSingle;
        panelSearch.Controls.Add(gridParty);
        panelSearch.Controls.Add(flowSearch);
        panelSearch.Controls.Add(lblSearchTitle);
        panelSearch.Dock = DockStyle.Fill;
        panelSearch.Location = new Point(3, 3);
        panelSearch.Name = "panelSearch";
        panelSearch.Padding = new Padding(12);
        panelSearch.Size = new Size(444, 739);
        panelSearch.TabIndex = 0;
        // 
        // lblSearchTitle
        // 
        lblSearchTitle.AutoSize = true;
        lblSearchTitle.Location = new Point(15, 14);
        lblSearchTitle.Name = "lblSearchTitle";
        lblSearchTitle.Size = new Size(104, 15);
        lblSearchTitle.TabIndex = 0;
        lblSearchTitle.Text = "Iskanje in rezultati";
        // 
        // flowSearch
        // 
        flowSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        flowSearch.Controls.Add(txtSearch);
        flowSearch.Controls.Add(cboTypeFilter);
        flowSearch.Controls.Add(txtAddressFilter);
        flowSearch.Controls.Add(txtPhoneFilter);
        flowSearch.Controls.Add(txtEmailFilter);
        flowSearch.Controls.Add(btnSearch);
        flowSearch.Controls.Add(btnClearSearch);
        flowSearch.Location = new Point(15, 38);
        flowSearch.Name = "flowSearch";
        flowSearch.Size = new Size(412, 105);
        flowSearch.TabIndex = 1;
        // 
        // txtSearch
        // 
        txtSearch.Location = new Point(3, 3);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Iskanje po imenu, davcni, maticni, naslovu...";
        txtSearch.Size = new Size(396, 23);
        txtSearch.TabIndex = 0;
        // 
        // cboTypeFilter
        // 
        cboTypeFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTypeFilter.FormattingEnabled = true;
        cboTypeFilter.Location = new Point(3, 32);
        cboTypeFilter.Name = "cboTypeFilter";
        cboTypeFilter.Size = new Size(120, 23);
        cboTypeFilter.TabIndex = 1;
        // 
        // txtAddressFilter
        // 
        txtAddressFilter.Location = new Point(129, 32);
        txtAddressFilter.Name = "txtAddressFilter";
        txtAddressFilter.PlaceholderText = "Naslov";
        txtAddressFilter.Size = new Size(130, 23);
        txtAddressFilter.TabIndex = 2;
        // 
        // txtPhoneFilter
        // 
        txtPhoneFilter.Location = new Point(265, 32);
        txtPhoneFilter.Name = "txtPhoneFilter";
        txtPhoneFilter.PlaceholderText = "Telefon";
        txtPhoneFilter.Size = new Size(134, 23);
        txtPhoneFilter.TabIndex = 3;
        // 
        // txtEmailFilter
        // 
        txtEmailFilter.Location = new Point(3, 61);
        txtEmailFilter.Name = "txtEmailFilter";
        txtEmailFilter.PlaceholderText = "Email";
        txtEmailFilter.Size = new Size(180, 23);
        txtEmailFilter.TabIndex = 4;
        // 
        // btnSearch
        // 
        btnSearch.Location = new Point(189, 61);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(85, 28);
        btnSearch.TabIndex = 5;
        btnSearch.Text = "Isci";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += btnSearch_Click;
        // 
        // btnClearSearch
        // 
        btnClearSearch.Location = new Point(280, 61);
        btnClearSearch.Name = "btnClearSearch";
        btnClearSearch.Size = new Size(85, 28);
        btnClearSearch.TabIndex = 6;
        btnClearSearch.Text = "Pocisti";
        btnClearSearch.UseVisualStyleBackColor = true;
        btnClearSearch.Click += btnClearSearch_Click;
        // 
        // gridParty
        // 
        gridParty.AllowUserToAddRows = false;
        gridParty.AllowUserToDeleteRows = false;
        gridParty.AllowUserToOrderColumns = false;
        gridParty.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridParty.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        gridParty.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridParty.Location = new Point(15, 149);
        gridParty.MultiSelect = false;
        gridParty.Name = "gridParty";
        gridParty.ReadOnly = true;
        gridParty.RowHeadersVisible = false;
        gridParty.ScrollBars = ScrollBars.Both;
        gridParty.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridParty.Size = new Size(412, 573);
        gridParty.TabIndex = 2;
        gridParty.CellClick += gridParty_CellClick;
        // 
        // panelEditor
        // 
        panelEditor.AutoScroll = true;
        panelEditor.BorderStyle = BorderStyle.FixedSingle;
        panelEditor.Controls.Add(gridRegisteredVehicles);
        panelEditor.Controls.Add(lblRegisteredVehicles);
        panelEditor.Controls.Add(gridOwnedVehicles);
        panelEditor.Controls.Add(lblOwnedVehicles);
        panelEditor.Controls.Add(flowButtons);
        panelEditor.Controls.Add(panelEditorFields);
        panelEditor.Controls.Add(lblEditorTitle);
        panelEditor.Dock = DockStyle.Fill;
        panelEditor.Location = new Point(453, 3);
        panelEditor.Name = "panelEditor";
        panelEditor.Padding = new Padding(12);
        panelEditor.Size = new Size(544, 739);
        panelEditor.TabIndex = 1;
        // 
        // lblEditorTitle
        // 
        lblEditorTitle.AutoSize = true;
        lblEditorTitle.Location = new Point(16, 14);
        lblEditorTitle.Name = "lblEditorTitle";
        lblEditorTitle.Size = new Size(133, 15);
        lblEditorTitle.TabIndex = 0;
        lblEditorTitle.Text = "Podatki izbrane stranke";
        // 
        // panelEditorFields
        // 
        panelEditorFields.BorderStyle = BorderStyle.FixedSingle;
        panelEditorFields.Controls.Add(txtEmail);
        panelEditorFields.Controls.Add(txtPhone);
        panelEditorFields.Controls.Add(txtAddress);
        panelEditorFields.Controls.Add(txtTaxNo);
        panelEditorFields.Controls.Add(panelCompanyFields);
        panelEditorFields.Controls.Add(panelPersonFields);
        panelEditorFields.Controls.Add(flowType);
        panelEditorFields.Location = new Point(16, 38);
        panelEditorFields.Name = "panelEditorFields";
        panelEditorFields.Size = new Size(494, 300);
        panelEditorFields.TabIndex = 1;
        // 
        // flowType
        // 
        flowType.Controls.Add(radioPerson);
        flowType.Controls.Add(radioCompany);
        flowType.Location = new Point(12, 12);
        flowType.Name = "flowType";
        flowType.Size = new Size(250, 30);
        flowType.TabIndex = 0;
        // 
        // radioPerson
        // 
        radioPerson.AutoSize = true;
        radioPerson.Location = new Point(3, 3);
        radioPerson.Name = "radioPerson";
        radioPerson.Size = new Size(62, 19);
        radioPerson.TabIndex = 0;
        radioPerson.Text = "Oseba";
        radioPerson.UseVisualStyleBackColor = true;
        radioPerson.CheckedChanged += radioPerson_CheckedChanged;
        // 
        // radioCompany
        // 
        radioCompany.AutoSize = true;
        radioCompany.Location = new Point(71, 3);
        radioCompany.Name = "radioCompany";
        radioCompany.Size = new Size(59, 19);
        radioCompany.TabIndex = 1;
        radioCompany.Text = "Firma";
        radioCompany.UseVisualStyleBackColor = true;
        radioCompany.CheckedChanged += radioPerson_CheckedChanged;
        // 
        // panelPersonFields
        // 
        panelPersonFields.Controls.Add(txtFullName);
        panelPersonFields.Controls.Add(dateBirth);
        panelPersonFields.Controls.Add(txtEmso);
        panelPersonFields.Location = new Point(12, 50);
        panelPersonFields.Name = "panelPersonFields";
        panelPersonFields.Size = new Size(420, 110);
        panelPersonFields.TabIndex = 1;
        // 
        // txtFullName
        // 
        txtFullName.Location = new Point(3, 3);
        txtFullName.Name = "txtFullName";
        txtFullName.PlaceholderText = "Ime in priimek";
        txtFullName.Size = new Size(260, 23);
        txtFullName.TabIndex = 0;
        // 
        // dateBirth
        // 
        dateBirth.Checked = false;
        dateBirth.Format = DateTimePickerFormat.Short;
        dateBirth.Location = new Point(3, 32);
        dateBirth.Name = "dateBirth";
        dateBirth.ShowCheckBox = true;
        dateBirth.Size = new Size(220, 23);
        dateBirth.TabIndex = 1;
        // 
        // txtEmso
        // 
        txtEmso.Location = new Point(3, 61);
        txtEmso.Name = "txtEmso";
        txtEmso.PlaceholderText = "EMŠO";
        txtEmso.Size = new Size(220, 23);
        txtEmso.TabIndex = 2;
        // 
        // panelCompanyFields
        // 
        panelCompanyFields.Controls.Add(txtCompanyName);
        panelCompanyFields.Controls.Add(txtCompanyRegNo);
        panelCompanyFields.Location = new Point(12, 50);
        panelCompanyFields.Name = "panelCompanyFields";
        panelCompanyFields.Size = new Size(420, 80);
        panelCompanyFields.TabIndex = 2;
        // 
        // txtCompanyName
        // 
        txtCompanyName.Location = new Point(3, 3);
        txtCompanyName.Name = "txtCompanyName";
        txtCompanyName.PlaceholderText = "Naziv firme";
        txtCompanyName.Size = new Size(260, 23);
        txtCompanyName.TabIndex = 0;
        // 
        // txtCompanyRegNo
        // 
        txtCompanyRegNo.Location = new Point(3, 32);
        txtCompanyRegNo.Name = "txtCompanyRegNo";
        txtCompanyRegNo.PlaceholderText = "Maticna stevilka";
        txtCompanyRegNo.Size = new Size(220, 23);
        txtCompanyRegNo.TabIndex = 1;
        // 
        // txtTaxNo
        // 
        txtTaxNo.Location = new Point(15, 170);
        txtTaxNo.Name = "txtTaxNo";
        txtTaxNo.PlaceholderText = "Davcna stevilka";
        txtTaxNo.Size = new Size(220, 23);
        txtTaxNo.TabIndex = 3;
        // 
        // txtAddress
        // 
        txtAddress.Location = new Point(15, 199);
        txtAddress.Name = "txtAddress";
        txtAddress.PlaceholderText = "Naslov";
        txtAddress.Size = new Size(360, 23);
        txtAddress.TabIndex = 4;
        // 
        // txtPhone
        // 
        txtPhone.Location = new Point(15, 228);
        txtPhone.Name = "txtPhone";
        txtPhone.PlaceholderText = "Telefon";
        txtPhone.Size = new Size(220, 23);
        txtPhone.TabIndex = 5;
        // 
        // txtEmail
        // 
        txtEmail.Location = new Point(15, 257);
        txtEmail.Name = "txtEmail";
        txtEmail.PlaceholderText = "Email";
        txtEmail.Size = new Size(360, 23);
        txtEmail.TabIndex = 6;
        // 
        // flowButtons
        // 
        flowButtons.Controls.Add(btnRefresh);
        flowButtons.Location = new Point(16, 349);
        flowButtons.Name = "flowButtons";
        flowButtons.Size = new Size(120, 40);
        flowButtons.TabIndex = 2;
        // 
        // lblOwnedVehicles
        // 
        lblOwnedVehicles.AutoSize = true;
        lblOwnedVehicles.Location = new Point(16, 404);
        lblOwnedVehicles.Name = "lblOwnedVehicles";
        lblOwnedVehicles.Size = new Size(81, 15);
        lblOwnedVehicles.TabIndex = 3;
        lblOwnedVehicles.Text = "Vozila v lasti";
        // 
        // gridOwnedVehicles
        // 
        gridOwnedVehicles.AllowUserToAddRows = false;
        gridOwnedVehicles.AllowUserToDeleteRows = false;
        gridOwnedVehicles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridOwnedVehicles.Location = new Point(16, 425);
        gridOwnedVehicles.Name = "gridOwnedVehicles";
        gridOwnedVehicles.ReadOnly = true;
        gridOwnedVehicles.RowHeadersVisible = false;
        gridOwnedVehicles.ScrollBars = ScrollBars.Both;
        gridOwnedVehicles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridOwnedVehicles.Size = new Size(494, 120);
        gridOwnedVehicles.TabIndex = 4;
        // 
        // lblRegisteredVehicles
        // 
        lblRegisteredVehicles.AutoSize = true;
        lblRegisteredVehicles.Location = new Point(16, 558);
        lblRegisteredVehicles.Name = "lblRegisteredVehicles";
        lblRegisteredVehicles.Size = new Size(109, 15);
        lblRegisteredVehicles.TabIndex = 5;
        lblRegisteredVehicles.Text = "Registrirana vozila";
        // 
        // gridRegisteredVehicles
        // 
        gridRegisteredVehicles.AllowUserToAddRows = false;
        gridRegisteredVehicles.AllowUserToDeleteRows = false;
        gridRegisteredVehicles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridRegisteredVehicles.Location = new Point(16, 579);
        gridRegisteredVehicles.Name = "gridRegisteredVehicles";
        gridRegisteredVehicles.ReadOnly = true;
        gridRegisteredVehicles.RowHeadersVisible = false;
        gridRegisteredVehicles.ScrollBars = ScrollBars.Both;
        gridRegisteredVehicles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridRegisteredVehicles.Size = new Size(494, 125);
        gridRegisteredVehicles.TabIndex = 6;
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
        // PartiesView
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(tableLayoutPanel1);
        Name = "PartiesView";
        Size = new Size(1000, 745);
        tableLayoutPanel1.ResumeLayout(false);
        panelSearch.ResumeLayout(false);
        panelSearch.PerformLayout();
        flowSearch.ResumeLayout(false);
        flowSearch.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)gridParty).EndInit();
        panelEditor.ResumeLayout(false);
        panelEditor.PerformLayout();
        panelEditorFields.ResumeLayout(false);
        panelEditorFields.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)gridOwnedVehicles).EndInit();
        ((System.ComponentModel.ISupportInitialize)gridRegisteredVehicles).EndInit();
        flowType.ResumeLayout(false);
        flowType.PerformLayout();
        panelPersonFields.ResumeLayout(false);
        panelPersonFields.PerformLayout();
        panelCompanyFields.ResumeLayout(false);
        panelCompanyFields.PerformLayout();
        flowButtons.ResumeLayout(false);
        ResumeLayout(false);
    }

    private TableLayoutPanel tableLayoutPanel1;
    private Panel panelSearch;
    private Label lblSearchTitle;
    private FlowLayoutPanel flowSearch;
    private TextBox txtSearch;
    private ComboBox cboTypeFilter;
    private TextBox txtAddressFilter;
    private TextBox txtPhoneFilter;
    private TextBox txtEmailFilter;
    private Button btnSearch;
    private Button btnClearSearch;
    private DataGridView gridParty;
    private Panel panelEditor;
    private Label lblEditorTitle;
    private Panel panelEditorFields;
    private Label lblOwnedVehicles;
    private DataGridView gridOwnedVehicles;
    private Label lblRegisteredVehicles;
    private DataGridView gridRegisteredVehicles;
    private FlowLayoutPanel flowType;
    private RadioButton radioPerson;
    private RadioButton radioCompany;
    private Panel panelPersonFields;
    private TextBox txtFullName;
    private DateTimePicker dateBirth;
    private TextBox txtEmso;
    private Panel panelCompanyFields;
    private TextBox txtCompanyName;
    private TextBox txtCompanyRegNo;
    private TextBox txtTaxNo;
    private TextBox txtAddress;
    private TextBox txtPhone;
    private TextBox txtEmail;
    private FlowLayoutPanel flowButtons;
    private Button btnRefresh;
}


