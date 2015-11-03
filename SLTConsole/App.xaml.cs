using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using SLTConsole.Library;

namespace SLTConsole
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private SLTConnection connection = new SLTConnection();
        public SLTConnection Connection { get { return connection; } }

        private RegistryKey key;

        public RegistryKey Key { get { return key; } }

        public App()
        {
            key = Registry.CurrentUser.OpenSubKey("Software", true);
            key.CreateSubKey("SLTConsole");
            key = key.OpenSubKey("SLTConsole", true);
        }
    }
}
