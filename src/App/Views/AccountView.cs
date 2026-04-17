using App.Infras;

namespace App.Views;

public partial class AccountView : UserControl
{
    public AccountView()
    {
        InitializeComponent();
    }

    private async void btnLogout_Click(object sender, EventArgs e)
    {
        btnLogout.Enabled = false;

        try
        {
            await Client.LogoutAsync();
        }
        finally
        {
            btnLogout.Enabled = true;
        }
    }
}
