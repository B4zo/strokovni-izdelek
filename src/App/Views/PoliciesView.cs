using App.DTOs.Policies;
using App.Infras;

namespace App.Views;

public partial class PoliciesView : UserControl
{
    public PoliciesView()
    {
        InitializeComponent();
        ConfigureGrid();
    }

    private void ConfigureGrid()
    {
        gridPolicies.AutoGenerateColumns = false;
        gridPolicies.Columns.Clear();
        gridPolicies.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Polica", DataPropertyName = nameof(PolicyDto.PolicyNo), Width = 120 });
        gridPolicies.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "VIN", DataPropertyName = nameof(PolicyDto.VehicleVin), Width = 150 });
        gridPolicies.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Stranka", DataPropertyName = nameof(PolicyDto.PartyDisplay), Width = 170 });
        gridPolicies.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Zavarovalnica", DataPropertyName = nameof(PolicyDto.InsurerName), Width = 140 });
        gridPolicies.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tip police", DataPropertyName = nameof(PolicyDto.TemplateName), Width = 150 });
        gridPolicies.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Od", DataPropertyName = nameof(PolicyDto.ValidFrom), Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" } });
        gridPolicies.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Do", DataPropertyName = nameof(PolicyDto.ValidTo), Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" } });
    }

    public async Task SearchAsync()
    {
        var url = BuildUrl();
        var items = await Client.SendAsync<List<PolicyDto>>(HttpMethod.Get, url);
        gridPolicies.DataSource = items;
        gridPolicies.ClearSelection();
        ClearEditor();
    }

    private async void btnSearch_Click(object sender, EventArgs e) => await SearchAsync();

    private async void btnRefresh_Click(object sender, EventArgs e)
        => await SearchAsync();

    private void btnClear_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        txtVin.Clear();
        txtPartyOrInsurer.Clear();
        gridPolicies.DataSource = null;
        ClearEditor();
    }

    private void gridPolicies_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        if (gridPolicies.Rows[e.RowIndex].DataBoundItem is not PolicyDto selected)
            return;

        txtPolicyNo.Text = selected.PolicyNo ?? "";
        txtVehicle.Text = $"{selected.VehicleVin} {selected.VehicleDisplay}".Trim();
        txtParty.Text = selected.PartyDisplay;
        txtInsurer.Text = $"{selected.InsurerName} / {selected.TemplateName}";
    }

    private void ClearEditor()
    {
        txtPolicyNo.Clear();
        txtVehicle.Clear();
        txtParty.Clear();
        txtInsurer.Clear();
    }

    private string BuildUrl()
    {
        var third = txtPartyOrInsurer.Text.Trim();
        var parts = new List<string>();
        Add(parts, "q", txtSearch.Text);
        Add(parts, "vin", txtVin.Text);
        Add(parts, "customer", third);
        Add(parts, "insurer", third);
        return parts.Count == 0 ? "api/policies" : $"api/policies?{string.Join("&", parts)}";
    }

    private static void Add(ICollection<string> values, string key, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;

        values.Add($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value.Trim())}");
    }
}

