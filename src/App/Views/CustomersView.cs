using App.DTOs.Customers;
using App.Infras;

namespace App.Views;

public partial class CustomersView : UserControl
{
    private bool _suppressSelectionEvents;

    public CustomersView()
    {
        InitializeComponent();
        radioPerson.Checked = true;
        ApplyTypeUi();
        cboTypeFilter.Items.AddRange(new object[] { "Vse", "Oseba", "Firma" });
        cboTypeFilter.SelectedIndex = 0;
        ConfigureGrid();
        ConfigureOwnedVehiclesGrid();
        ConfigureRegisteredVehiclesGrid();
        ClearEditor();
    }

    public async Task ReloadAsync()
        => await SearchAsync();

    private async Task SearchAsync()
    {
        var query = BuildQuery();
        var url = string.IsNullOrWhiteSpace(query) ? "api/customers" : $"api/customers?{query}";
        var items = await Client.SendAsync<List<CustomerDto>>(HttpMethod.Get, url);
        _suppressSelectionEvents = true;
        gridCustomers.DataSource = items;
        gridCustomers.ClearSelection();
        _suppressSelectionEvents = false;
        ClearEditor();
        gridOwnedVehicles.DataSource = null;
        gridRegisteredVehicles.DataSource = null;
    }

    private void ApplyTypeUi()
    {
        panelPersonFields.Visible = radioPerson.Checked;
        panelCompanyFields.Visible = radioCompany.Checked;
    }

    private async void btnRefresh_Click(object sender, EventArgs e) => await SearchAsync();

    private async void btnSearch_Click(object sender, EventArgs e) => await SearchAsync();

    private async void btnClearSearch_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        txtAddressFilter.Clear();
        txtPhoneFilter.Clear();
        txtEmailFilter.Clear();
        cboTypeFilter.SelectedIndex = 0;
        gridCustomers.DataSource = null;
        ClearEditor();
        gridOwnedVehicles.DataSource = null;
        gridRegisteredVehicles.DataSource = null;
    }

    private void radioPerson_CheckedChanged(object sender, EventArgs e) => ApplyTypeUi();

    private async void btnSave_Click(object sender, EventArgs e)
    {
        var req = new CustomerUpsertRequest(
            radioPerson.Checked ? "person" : "company",
            txtAddress.Text.Trim(),
            txtPhone.Text.Trim(),
            txtEmail.Text.Trim(),
            txtFullName.Text.Trim(),
            dateBirth.Checked ? new DateOnly(dateBirth.Value.Year, dateBirth.Value.Month, dateBirth.Value.Day) : (DateOnly?)null,
            txtTaxNumber.Text.Trim(),
            txtNationalNo.Text.Trim(),
            txtCompanyName.Text.Trim(),
            txtRegistrationNo.Text.Trim());

        await Client.SendAsync(HttpMethod.Post, "api/customers", req);
        await ReloadAsync();
    }

    private async void btnDelete_Click(object sender, EventArgs e)
    {
        if (gridCustomers.CurrentRow?.DataBoundItem is not CustomerDto selected)
            return;

        await Client.SendAsync(HttpMethod.Delete, $"api/customers/{selected.Id}");
        await ReloadAsync();
    }

    private async void gridCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (_suppressSelectionEvents || e.RowIndex < 0)
            return;

        if (gridCustomers.Rows[e.RowIndex].DataBoundItem is not CustomerDto selected)
            return;

        LoadSelectedCustomerIntoEditor(selected);
        await LoadCustomerRelationshipsAsync(selected.Id);
    }

    private void ConfigureGrid()
    {
        gridCustomers.AutoGenerateColumns = false;
        gridCustomers.Columns.Clear();
        gridCustomers.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Type",
            HeaderText = "Tip",
            DataPropertyName = nameof(CustomerDto.Type),
            Width = 80
        });
        gridCustomers.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "DisplayName",
            HeaderText = "Naziv",
            DataPropertyName = nameof(CustomerDto.DisplayName),
            Width = 220
        });
        gridCustomers.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Address",
            HeaderText = "Naslov",
            DataPropertyName = nameof(CustomerDto.Address),
            Width = 180
        });
        gridCustomers.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Phone",
            HeaderText = "Telefon",
            DataPropertyName = nameof(CustomerDto.Phone),
            Width = 120
        });
        gridCustomers.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Email",
            HeaderText = "Email",
            DataPropertyName = nameof(CustomerDto.Email),
            Width = 180
        });
        gridCustomers.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "TaxNumber",
            HeaderText = "Davcna",
            DataPropertyName = nameof(CustomerDto.TaxNumber),
            Width = 110
        });
        gridCustomers.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "NationalNo",
            HeaderText = "EMSO",
            DataPropertyName = nameof(CustomerDto.NationalNo),
            Width = 130
        });
        gridCustomers.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "RegistrationNo",
            HeaderText = "Maticna",
            DataPropertyName = nameof(CustomerDto.RegistrationNo),
            Width = 110
        });
        gridCustomers.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "DateOfBirth",
            HeaderText = "Rojen",
            DataPropertyName = nameof(CustomerDto.DateOfBirth),
            Width = 90,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" }
        });
        gridCustomers.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "CreatedAt",
            HeaderText = "Ustvarjen",
            DataPropertyName = nameof(CustomerDto.CreatedAt),
            Width = 130,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy HH:mm" }
        });
    }

    private void ClearEditor()
    {
        txtAddress.Text = "";
        txtPhone.Text = "";
        txtEmail.Text = "";
        txtTaxNumber.Text = "";
        txtFullName.Text = "";
        txtNationalNo.Text = "";
        txtCompanyName.Text = "";
        txtRegistrationNo.Text = "";
        dateBirth.Checked = false;
        radioPerson.Checked = true;
        ApplyTypeUi();
    }

    private void ConfigureOwnedVehiclesGrid()
    {
        gridOwnedVehicles.AutoGenerateColumns = false;
        gridOwnedVehicles.Columns.Clear();
        gridOwnedVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "VIN", DataPropertyName = "Vin", Width = 140 });
        gridOwnedVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Vozilo", DataPropertyName = "VehicleDisplay", Width = 120 });
        gridOwnedVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Od", DataPropertyName = "ValidFrom", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" } });
        gridOwnedVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Do", DataPropertyName = "ValidTo", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" } });
        gridOwnedVehicles.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Trenutno", DataPropertyName = "IsCurrent", Width = 70 });
    }

    private void ConfigureRegisteredVehiclesGrid()
    {
        gridRegisteredVehicles.AutoGenerateColumns = false;
        gridRegisteredVehicles.Columns.Clear();
        gridRegisteredVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "VIN", DataPropertyName = "Vin", Width = 130 });
        gridRegisteredVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Vozilo", DataPropertyName = "VehicleDisplay", Width = 110 });
        gridRegisteredVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Reg.", DataPropertyName = "RegistrationNo", Width = 90 });
        gridRegisteredVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tablica", DataPropertyName = "PlateNumber", Width = 85 });
        gridRegisteredVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Od", DataPropertyName = "ValidFrom", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" } });
        gridRegisteredVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Do", DataPropertyName = "ValidTo", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" } });
    }

    private async Task LoadCustomerRelationshipsAsync(Guid customerId)
    {
        var dto = await Client.SendAsync<CustomerDetailDto>(HttpMethod.Get, $"api/customers/{customerId}/details");
        gridOwnedVehicles.DataSource = dto.OwnedVehicles.ToList();
        gridRegisteredVehicles.DataSource = dto.RegisteredVehicles.ToList();
        gridOwnedVehicles.ClearSelection();
        gridRegisteredVehicles.ClearSelection();
    }

    private void LoadSelectedCustomerIntoEditor(CustomerDto selected)
    {
        txtAddress.Text = selected.Address ?? "";
        txtPhone.Text = selected.Phone ?? "";
        txtEmail.Text = selected.Email ?? "";
        txtTaxNumber.Text = "";
        txtFullName.Text = "";
        txtNationalNo.Text = "";
        txtCompanyName.Text = "";
        txtRegistrationNo.Text = "";
        dateBirth.Checked = false;

        if (selected.Type == "person" && selected.Person is not null)
        {
            radioPerson.Checked = true;
            txtFullName.Text = selected.Person.FullName;
            dateBirth.Checked = selected.Person.DateOfBirth is not null;
            if (selected.Person.DateOfBirth is not null)
                dateBirth.Value = selected.Person.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue);
            txtTaxNumber.Text = selected.Person.TaxNumber ?? "";
            txtNationalNo.Text = selected.Person.NationalNo ?? "";
        }
        else if (selected.Type == "company" && selected.Company is not null)
        {
            radioCompany.Checked = true;
            txtCompanyName.Text = selected.Company.CompanyName;
            txtTaxNumber.Text = selected.Company.TaxNumber ?? "";
            txtRegistrationNo.Text = selected.Company.RegistrationNo ?? "";
        }

        ApplyTypeUi();
    }

    private string BuildQuery()
    {
        var values = new List<string>();

        Add(values, "q", txtSearch.Text);
        Add(values, "type", cboTypeFilter.SelectedItem?.ToString() is "Vse" or null ? null : cboTypeFilter.SelectedItem?.ToString()?.ToLowerInvariant());
        Add(values, "address", txtAddressFilter.Text);
        Add(values, "phone", txtPhoneFilter.Text);
        Add(values, "email", txtEmailFilter.Text);

        return string.Join("&", values);
    }

    private static void Add(ICollection<string> values, string key, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;

        values.Add($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value.Trim())}");
    }
}
