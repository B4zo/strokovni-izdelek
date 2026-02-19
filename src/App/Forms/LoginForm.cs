using App.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Forms
{
    public partial class LoginForm : Form
    {
        private readonly ApiClient _api;
        public LoginForm()
        {
            InitializeComponent();
            _api = new ApiClient("http://localhost:51009/");
        }
        private void LoginForm_Shown(object sender, EventArgs e)
        {
            ActiveControl = null;
        }

        private async void buttonLogIn_Click(object sender, EventArgs e)
        {
            buttonLogIn.Enabled = false;

            if (String.IsNullOrWhiteSpace(textBoxUsername.Text) && String.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                MessageBox.Show("Vnesite uporabniško ime in geslo.", "Napaka!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonLogIn.Enabled = true;
                return;
            }

            try
            {
                var username = textBoxUsername.Text.Trim();
                var password = textBoxPassword.Text;

                await _api.Login(username, password);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Neveljavno uporabniško ime ali geslo.", "Napaka!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Prišlo je do napake pri prijavi:\n{ex.Message}", "Napaka!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonLogIn.Enabled = true;
            }
        }
    }
}
