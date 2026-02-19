using App.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infras
{
    internal class AppContext : ApplicationContext
    {
        public AppContext()
        {
            var login = new LoginForm();

            // When login closes, decide what to do next
            login.FormClosed += (_, __) =>
            {
                if (login.DialogResult == DialogResult.OK)
                {
                    var main = new MainForm();

                    // When main closes, quit the app
                    main.FormClosed += (_, __) =>
                    {
                        main.Dispose();
                        ExitThread();
                    };

                    main.Show();
                }
                else
                {
                    ExitThread(); // Quit app if login cancelled/failed
                }

                login.Dispose();
            };

            login.Show();
        }
    }
}
