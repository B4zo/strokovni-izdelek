using App.DTOs.Parties;
using App.Infras;

namespace App.Views;

public partial class PartiesView : UserControl
{
    private bool _suppressSelectionEvents;

    public PartiesView()
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
        var url = string.IsNullOrWhiteSpace(query) ? "api/parties" : $"api/parties?{query}";
        var items = await Client.SendAsync<List<PartyDto>>(HttpMethod.Get, url);
        _suppressSelectionEvents = true;
        gridParty.DataSource = items;
        gridParty.ClearSelection();
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
        gridParty.DataSource = null;
        ClearEditor();
        gridOwnedVehicles.DataSource = null;
        gridRegisteredVehicles.DataSource = null;
    }

    private void radioPerson_CheckedChanged(object sender, EventArgs e) => ApplyTypeUi();

    private async void gridParty_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (_suppressSelectionEvents || e.RowIndex < 0)
            return;

        if (gridParty.Rows[e.RowIndex].DataBoundItem is not PartyDto selected)
            return;

        LoadSelectedPartyIntoEditor(selected);
        await LoadPartyRelationshipsAsync(selected.Id);
    }

    private void ConfigureGrid()
    {
        gridParty.AutoGenerateColumns = false;
        gridParty.Columns.Clear();
        gridParty.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Type",
            HeaderText = "Tip",
            DataPropertyName = nameof(PartyDto.Type),
            Width = 80
        });
        gridParty.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "DisplayName",
            HeaderText = "Naziv",
            DataPropertyName = nameof(PartyDto.DisplayName),
            Width = 220
        });
        gridParty.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Address",
            HeaderText = "Naslov",
            DataPropertyName = nameof(PartyDto.Address),
            Width = 180
        });
        gridParty.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Phone",
            HeaderText = "Telefon",
            DataPropertyName = nameof(PartyDto.Phone),
            Width = 120
        });
        gridParty.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Email",
            HeaderText = "Email",
            DataPropertyName = nameof(PartyDto.Email),
            Width = 180
        });
        gridParty.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "TaxNo",
            HeaderText = "Davcna",
            DataPropertyName = nameof(PartyDto.TaxNo),
            Width = 110
        });
        gridParty.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Emso",
            HeaderText = "EMSO",
            DataPropertyName = nameof(PartyDto.Emso),
            Width = 130
        });
        gridParty.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "CompanyRegNo",
            HeaderText = "Maticna firme",
            DataPropertyName = nameof(PartyDto.CompanyRegNo),
            Width = 120
        });
        gridParty.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "DateOfBirth",
            HeaderText = "Rojen",
            DataPropertyName = nameof(PartyDto.DateOfBirth),
            Width = 90,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" }
        });
        gridParty.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "CreatedAt",
            HeaderText = "Ustvarjen",
            DataPropertyName = nameof(PartyDto.CreatedAt),
            Width = 130,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy HH:mm" }
        });
    }

    private void ClearEditor()
    {
        txtAddress.Text = "";
        txtPhone.Text = "";
        txtEmail.Text = "";
        txtTaxNo.Text = "";
        txtFullName.Text = "";
        txtEmso.Text = "";
        txtCompanyName.Text = "";
        txtCompanyRegNo.Text = "";
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
        gridRegisteredVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tablica", DataPropertyName = "PlateNo", Width = 85 });
        gridRegisteredVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Od", DataPropertyName = "ValidFrom", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" } });
        gridRegisteredVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Do", DataPropertyName = "ValidTo", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" } });
    }

    private async Task LoadPartyRelationshipsAsync(Guid partyId)
    {
        var dto = await Client.SendAsync<PartyDetailDto>(HttpMethod.Get, $"api/parties/{partyId}/details");
        gridOwnedVehicles.DataSource = dto.OwnedVehicles.ToList();
        gridRegisteredVehicles.DataSource = dto.RegisteredVehicles.ToList();
        gridOwnedVehicles.ClearSelection();
        gridRegisteredVehicles.ClearSelection();
    }

    private void LoadSelectedPartyIntoEditor(PartyDto selected)
    {
        txtAddress.Text = selected.Address ?? "";
        txtPhone.Text = selected.Phone ?? "";
        txtEmail.Text = selected.Email ?? "";
        txtTaxNo.Text = "";
        txtFullName.Text = "";
        txtEmso.Text = "";
        txtCompanyName.Text = "";
        txtCompanyRegNo.Text = "";
        dateBirth.Checked = false;

        if (selected.Type == "person" && selected.Person is not null)
        {
            radioPerson.Checked = true;
            txtFullName.Text = selected.Person.FullName;
            dateBirth.Checked = selected.Person.DateOfBirth is not null;
            if (selected.Person.DateOfBirth is not null)
                dateBirth.Value = selected.Person.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue);
            txtTaxNo.Text = selected.Person.TaxNo ?? "";
            txtEmso.Text = selected.Person.Emso ?? "";
        }
        else if (selected.Type == "company" && selected.Company is not null)
        {
            radioCompany.Checked = true;
            txtCompanyName.Text = selected.Company.CompanyName;
            txtTaxNo.Text = selected.Company.TaxNo ?? "";
            txtCompanyRegNo.Text = selected.Company.CompanyRegNo ?? "";
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


