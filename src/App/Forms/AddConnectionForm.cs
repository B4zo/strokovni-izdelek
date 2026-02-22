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
    public partial class AddConnectionForm : Form
    {
        public AddConnectionForm()
        {
            InitializeComponent();

            AcceptButton = btnAdd;
            CancelButton = btnCancel;

            textBoxURL.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnAdd.PerformClick();
                    e.SuppressKeyPress = true;
                }
            };
        }

        public string ConnectionName => textBoxName.Text.Trim();
        public string BaseUrl => textBoxURL.Text.Trim();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ConnectionName))
            {
                MessageBox.Show("Vnesi naziv povezave.", "Manjkajoči podatki", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(BaseUrl))
            {
                MessageBox.Show("Vnesi URL.", "Manjkajoči podatki", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxURL.Focus();
                return;
            }

            if (!Uri.TryCreate(BaseUrl, UriKind.Absolute, out var uri))
            {
                MessageBox.Show("URL ni veljaven. Primer: http://localhost:8080/", "Neveljaven URL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxURL.Focus();
                return;
            }

            var normalized = uri.ToString();
            if (!normalized.EndsWith("/"))
                normalized += "/";

            textBoxURL.Text = normalized;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
