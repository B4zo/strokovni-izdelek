using App.Infras;
using App.Views;

namespace App.Forms;

public partial class MainForm : Form
{
    private readonly PartiesView _partiesView = new();
    private readonly AccountView _accountView = new();
    private readonly VehicleRegistryView _vehicleRegistryView = new();
    private readonly PoliciesView _policiesView = new();
    private bool _isClosingAfterLogout;

    public MainForm()
    {
        InitializeComponent();
        FormClosing += MainForm_FormClosing;
        ShowParties();
    }

    private void ShowView(Control view)
    {
        panelContent.Controls.Clear();
        view.Dock = DockStyle.Fill;
        panelContent.Controls.Add(view);
    }

    private void ShowParties() => ShowView(_partiesView);

    private void btnParties_Click(object sender, EventArgs e) => ShowParties();

    private void btnVehicles_Click(object sender, EventArgs e) => ShowView(_vehicleRegistryView);

    private void btnRegistrations_Click(object sender, EventArgs e) => ShowView(_vehicleRegistryView);

    private void btnPolicies_Click(object sender, EventArgs e) => ShowView(_policiesView);

    private async void btnLogout_Click(object sender, EventArgs e)
        => await Client.LogoutAsync();

    private void btnAccount_Click(object sender, EventArgs e)
        => ShowView(_accountView);

    private async void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (_isClosingAfterLogout || !Client.IsLoggedIn)
            return;

        e.Cancel = true;
        Enabled = false;

        try
        {
            var logoutTask = Client.LogoutAsync(suppressLoggedOutEvent: true);
            var completed = await Task.WhenAny(logoutTask, Task.Delay(1500));

            if (completed == logoutTask)
                await logoutTask;
        }
        finally
        {
            _isClosingAfterLogout = true;
            Close();
        }
    }
}