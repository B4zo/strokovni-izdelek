using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infras
{
    public sealed class ConnectionsFile
    {
        public string? Selected { get; set; }
        public List<ConnectionItem> Items { get; set; } = new();
    }
}
