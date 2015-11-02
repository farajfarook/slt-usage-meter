using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using SLTConsole.Library;

namespace SLTConsole
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>

    public partial class Widget : MetroWindow
    {
        private App app = ((App)Application.Current);
        LoginControl loginControl = new LoginControl();
        WidgetControl widgetControl = new WidgetControl();

        public Widget()
        {
            InitializeComponent();
            InitDisplay();
            (this.FindResource("shakeEffect") as Storyboard).Begin();
            loginControl.btnLogin.Click += btnLogin_Click;
        }

        private void InitDisplay()
        {
            display.Children.Clear();
            if (!app.Connection.IsLogged)
            {                
                display.Children.Add(loginControl);
            }
            else
            {
                ProfileManager profileManager = new ProfileManager(app.Connection);
                Profile profile = profileManager.GetProfile();
                widgetControl.lblPeakStatus.Content = profile.PeakStatus;
                widgetControl.lblTotalStatus.Content = profile.TotalStatus;
                display.Children.Add(widgetControl);
            }
        }

        void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (app.Connection.Login(loginControl.txtUsername.Text, loginControl.txtPassword.Password))
            {
                InitDisplay();
            }
            else
            {
                (this.FindResource("shakeEffect") as Storyboard).Begin();                                
            }
        }
    }
}
