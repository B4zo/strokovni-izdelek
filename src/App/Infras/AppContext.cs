using App.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infras
{
    public class AppContext : ApplicationContext
    {
        private bool _switching;

        public AppContext()
        {
            Client.LoggedOut += OnLoggedOut;
            ShowLogin();
        }

        private void ShowLogin()
        {
            var login = new LoginForm();

            login.FormClosed += (_, __) =>
            {
                if (login.DialogResult != DialogResult.OK)
                {
                    ExitThread();
                    return;
                }

                ShowMain();
            };

            SwitchMainForm(login);
        }

        private void ShowMain()
        {
            var main = new MainForm();

            main.FormClosed += (_, __) =>
            {
                if (_switching) return;
                ExitThread();
            };

            SwitchMainForm(main);
        }

        private void OnLoggedOut()
        {
            if (MainForm is null) return;

            MainForm.BeginInvoke(new Action(ShowLogin));
        }

        private void SwitchMainForm(Form next)
        {
            _switching = true;

            var old = MainForm;

            MainForm = next;
            next.Show();

            if (old is not null && old != next)
            {
                // don't call Close() here (can re-enter FormClosed and recurse)
                old.Hide();
                old.Dispose();
            }

            _switching = false;
        }

        protected override void ExitThreadCore()
        {
            Client.LoggedOut -= OnLoggedOut;
            base.ExitThreadCore();
        }
    }
}