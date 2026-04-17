using App.DTOs.Users;
using App.Infras;

namespace App.Views;

public partial class UsersView : UserControl
{
    public UsersView()
    {
        InitializeComponent();
        ConfigureGrid();
    }

    private void ConfigureGrid()
    {
        gridUsers.AutoGenerateColumns = false;
        gridUsers.Columns.Clear();
        gridUsers.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Uporabnisko ime", DataPropertyName = nameof(UserDto.Username), Width = 140 });
        gridUsers.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Prikazno ime", DataPropertyName = nameof(UserDto.DisplayName), Width = 170 });
        gridUsers.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Vloga", DataPropertyName = nameof(UserDto.Role), Width = 100 });
        gridUsers.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Aktiven", DataPropertyName = nameof(UserDto.IsActivated), Width = 70 });
        gridUsers.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ustvarjen", DataPropertyName = nameof(UserDto.CreatedAt), Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy HH:mm" } });
        gridUsers.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Zadnja prijava", DataPropertyName = nameof(UserDto.LastLoginAt), Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy HH:mm" } });
    }

    public async Task SearchAsync()
    {
        var url = BuildUrl();
        var items = await Client.SendAsync<List<UserDto>>(HttpMethod.Get, url);
        gridUsers.DataSource = items;
        gridUsers.ClearSelection();
        ClearEditor();
    }

    private async void btnSearch_Click(object sender, EventArgs e) => await SearchAsync();

    private async void btnRefresh_Click(object sender, EventArgs e)
        => await SearchAsync();

    private void btnClear_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        txtRole.Clear();
        txtActive.Clear();
        gridUsers.DataSource = null;
        ClearEditor();
    }

    private void gridUsers_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        if (gridUsers.Rows[e.RowIndex].DataBoundItem is not UserDto selected)
            return;

        txtUsername.Text = selected.Username;
        txtDisplayName.Text = selected.DisplayName;
        txtEditorRole.Text = selected.Role;
        txtLastLogin.Text = selected.LastLoginAt?.ToLocalTime().ToString("dd.MM.yyyy HH:mm") ?? "";
    }

    private void ClearEditor()
    {
        txtUsername.Clear();
        txtDisplayName.Clear();
        txtEditorRole.Clear();
        txtLastLogin.Clear();
    }

    private string BuildUrl()
    {
        var parts = new List<string>();
        Add(parts, "q", txtSearch.Text);
        Add(parts, "role", txtRole.Text);
        Add(parts, "isActive", ParseYesNo(txtActive.Text));
        return parts.Count == 0 ? "api/users" : $"api/users?{string.Join("&", parts)}";
    }

    private static string? ParseYesNo(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        return value.Trim().ToLowerInvariant() switch
        {
            "da" or "yes" or "true" or "1" => "true",
            "ne" or "no" or "false" or "0" => "false",
            _ => null
        };
    }

    private static void Add(ICollection<string> values, string key, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;

        values.Add($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value.Trim())}");
    }
}
