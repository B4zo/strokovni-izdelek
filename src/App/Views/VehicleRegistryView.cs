using App.DTOs.Vehicles;
using App.Infras;

namespace App.Views;

public partial class VehicleRegistryView : UserControl
{
    public VehicleRegistryView()
    {
        InitializeComponent();
        ConfigureVehiclesGrid();
        ConfigureOwnershipGrid();
        ConfigureRegistrationsGrid();
    }

    public async Task SearchAsync()
    {
        var url = BuildUrl();
        var items = await Client.SendAsync<List<VehicleDto>>(HttpMethod.Get, url);
        gridVehicles.DataSource = items;
        gridVehicles.ClearSelection();
        ClearDetails();
    }

    private void ConfigureVehiclesGrid()
    {
        gridVehicles.AutoGenerateColumns = false;
        gridVehicles.Columns.Clear();
        gridVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "VIN", DataPropertyName = nameof(VehicleDto.Vin), Width = 160 });
        gridVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Znamka", DataPropertyName = nameof(VehicleDto.Make), Width = 100 });
        gridVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Model", DataPropertyName = nameof(VehicleDto.Model), Width = 100 });
        gridVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Kat.", DataPropertyName = nameof(VehicleDto.CategoryCode), Width = 70 });
        gridVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Lastnik", DataPropertyName = nameof(VehicleDto.CurrentOwner), Width = 150 });
        gridVehicles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tablica", DataPropertyName = nameof(VehicleDto.CurrentPlate), Width = 90 });
    }

    private void ConfigureOwnershipGrid()
    {
        gridOwnership.AutoGenerateColumns = false;
        gridOwnership.Columns.Clear();
        gridOwnership.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Lastnik", DataPropertyName = nameof(VehicleOwnerHistoryDto.CustomerDisplay), Width = 180 });
        gridOwnership.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Od", DataPropertyName = nameof(VehicleOwnerHistoryDto.ValidFrom), Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" } });
        gridOwnership.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Do", DataPropertyName = nameof(VehicleOwnerHistoryDto.ValidTo), Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" } });
        gridOwnership.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Trenutni", DataPropertyName = nameof(VehicleOwnerHistoryDto.IsCurrent), Width = 70 });
    }

    private void ConfigureRegistrationsGrid()
    {
        gridRegistrations.AutoGenerateColumns = false;
        gridRegistrations.Columns.Clear();
        gridRegistrations.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Stranka", DataPropertyName = nameof(VehicleRegistrationHistoryDto.CustomerDisplay), Width = 170 });
        gridRegistrations.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Reg.", DataPropertyName = nameof(VehicleRegistrationHistoryDto.RegistrationNo), Width = 90 });
        gridRegistrations.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tablica", DataPropertyName = nameof(VehicleRegistrationHistoryDto.PlateNumber), Width = 90 });
        gridRegistrations.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Od", DataPropertyName = nameof(VehicleRegistrationHistoryDto.ValidFrom), Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" } });
        gridRegistrations.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Do", DataPropertyName = nameof(VehicleRegistrationHistoryDto.ValidTo), Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" } });
        gridRegistrations.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Trenutna", DataPropertyName = nameof(VehicleRegistrationHistoryDto.IsCurrent), Width = 70 });
    }

    private async void btnSearch_Click(object sender, EventArgs e) => await SearchAsync();

    private void btnClear_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        txtVin.Clear();
        txtOwner.Clear();
        gridVehicles.DataSource = null;
        ClearDetails();
    }

    private async void btnRefresh_Click(object sender, EventArgs e)
        => await SearchAsync();

    private async void gridVehicles_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        if (gridVehicles.Rows[e.RowIndex].DataBoundItem is not VehicleDto selected)
            return;

        var dto = await Client.SendAsync<VehicleDetailDto>(HttpMethod.Get, $"api/vehicles/{selected.Id}/details");
        txtVehicleVin.Text = dto.Vehicle.Vin;
        txtCategory.Text = $"{dto.Vehicle.CategoryCode} - {dto.Vehicle.CategoryLabel}";
        txtVehicleDisplay.Text = $"{dto.Vehicle.Make} {dto.Vehicle.Model}".Trim();
        txtCurrentOwner.Text = dto.Vehicle.CurrentOwner ?? "";
        txtCurrentRegistration.Text = dto.Vehicle.CurrentRegistrationNo ?? "";
        txtCurrentPlate.Text = dto.Vehicle.CurrentPlate ?? "";
        gridOwnership.DataSource = dto.OwnershipHistory.ToList();
        gridRegistrations.DataSource = dto.RegistrationHistory.ToList();
        gridOwnership.ClearSelection();
        gridRegistrations.ClearSelection();
    }

    private string BuildUrl()
    {
        var parts = new List<string>();
        Add(parts, "q", txtSearch.Text);
        Add(parts, "vin", txtVin.Text);
        Add(parts, "category", txtOwner.Text);
        return parts.Count == 0 ? "api/vehicles" : $"api/vehicles?{string.Join("&", parts)}";
    }

    private void ClearDetails()
    {
        txtVehicleVin.Clear();
        txtCategory.Clear();
        txtVehicleDisplay.Clear();
        txtCurrentOwner.Clear();
        txtCurrentRegistration.Clear();
        txtCurrentPlate.Clear();
        gridOwnership.DataSource = null;
        gridRegistrations.DataSource = null;
    }

    private static void Add(ICollection<string> values, string key, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;

        values.Add($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value.Trim())}");
    }
}
