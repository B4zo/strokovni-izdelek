using App.Auth;
using App.Infras;
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
        private bool _altMode;
        private ApiClient _api;
        public LoginForm()
        {
            InitializeComponent();

            AcceptButton = buttonLogIn;

            menuStrip1.Visible = false;

            ApplySelectedConnectionToUi();

            HookClickToCloseMenu(this);

            foreach (ToolStripMenuItem top in menuStrip1.Items)
                top.DropDownClosed += (_, __) => HideMenuStrip();
        }
        private void ApplySelectedConnectionToUi()
        {
            var selected = Connection.GetSelected();

            toolStripStatusLabelConnection.Text = selected is null ? "(ni izbrana)" : $"{selected.Name}";

            if (selected is not null)
            {
                _api = new ApiClient(selected.BaseUrl);

                Session.Clear();
            }
        }
        private void HookClickToCloseMenu(Control root)
        {
            root.MouseDown += (_, e) =>
            {
                if (menuStrip1.Visible && e.Y > menuStrip1.Height)
                    HideMenuStrip();
            };

            foreach (Control child in root.Controls)
                HookClickToCloseMenu(child);
        }
        private void ShowMenuStrip(bool focusFirstMenu)
        {
            menuStrip1.Visible = true;

            if (focusFirstMenu)
            {
                _altMode = true;
                menuStrip1.Focus();
                // Select first top-level item
                if (menuStrip1.Items.Count > 0)
                    menuStrip1.Items[0].Select();
            }
        }

        private void HideMenuStrip()
        {
            menuStrip1.Visible = false;
            _altMode = false;
        }
        private bool TryOpenTopMenuForKey(Keys key)
        {
            ToolStripMenuItem? target = key switch
            {
                Keys.N => settingsToolStripMenuItem,
                _ => null
            };

            if (target is null) return false;

            menuStrip1.Visible = true;
            menuStrip1.Focus();
            target.Select();
            target.ShowDropDown();

            _altMode = false;
            return true;
        }
        private void LoginForm_Shown(object sender, EventArgs e)
        {
            ActiveControl = null;
        }

        private async void buttonLogIn_Click(object sender, EventArgs e)
        {
            buttonLogIn.Enabled = false;

            if (_api is null)
            {
                MessageBox.Show("Najprej izberi ali dodaj povezavo.", "Povezava", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonLogIn.Enabled = true;
                return;
            }

            if (String.IsNullOrWhiteSpace(textBoxUsername.Text))
            {
                MessageBox.Show("Vnesite uporabniško ime.", "Napaka!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxUsername.Focus();
                buttonLogIn.Enabled = true;
                return;
            }

            if (String.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                MessageBox.Show("Vnesite geslo.", "Napaka!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPassword.Focus();
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
        private void addConnectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var dlg = new AddConnectionForm();

            if (dlg.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                Connection.AddOrUpdate(dlg.ConnectionName, dlg.BaseUrl);
                Connection.Select(dlg.ConnectionName);
                ApplySelectedConnectionToUi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && menuStrip1.Visible)
            {
                HideMenuStrip();
                e.Handled = true;
                return;
            }

            if (e.KeyCode == Keys.Menu && !e.Control && !e.Shift)
            {
                if (!menuStrip1.Visible) ShowMenuStrip(focusFirstMenu: true);
                else HideMenuStrip();

                e.Handled = true;
                return;
            }

            if (menuStrip1.Visible)
            {

                if (e.Alt)
                {
                    if (TryOpenTopMenuForKey(e.KeyCode))
                    {
                        e.Handled = true;
                        return;
                    }
                }

                if (_altMode && TryOpenTopMenuForKey(e.KeyCode))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void LoginForm_Deactivate(object sender, EventArgs e)
        {
            HideMenuStrip();
        }

        private void connectionSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException(); // TO DO: add a settings menu, this opens the settings menu with the correct submenu opened
        }

        private void connectionToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            connectionsToolStripMenuItem.DropDownItems.Clear();

            var data = Connection.Load();
            if (data.Items.Count == 0)
            {
                var empty = new ToolStripMenuItem("(Ni shranjenih povezav)") { Enabled = false };
                connectionsToolStripMenuItem.DropDownItems.Add(empty);
                return;
            }

            foreach (var c in data.Items)
            {
                var isSelected = data.Selected != null && data.Selected.Equals(c.Name, StringComparison.OrdinalIgnoreCase);

                var item = new ToolStripMenuItem($"{c.Name}  ({c.BaseUrl})")
                {
                    Checked = isSelected
                };

                item.Click += (_, __) =>
                {
                    Connection.Select(c.Name);
                    ApplySelectedConnectionToUi();
                };
                /*
                var delete = new ToolStripMenuItem("Izbriši");
                delete.Click += (_, __) =>
                {
                    var confirm = MessageBox.Show(
                        $"Izbrišem povezavo '{c.Name}'?",
                        "Potrditev",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirm == DialogResult.Yes)
                    {
                        Connection.Delete(c.Name);
                        ApplySelectedConnectionToUi();
                    }
                };
                item.DropDownItems.Add(delete);
                */

                connectionsToolStripMenuItem.DropDownItems.Add(item);
            }
        }
    }
}
