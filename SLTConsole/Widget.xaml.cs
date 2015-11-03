using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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
using Hardcodet.Wpf.TaskbarNotification;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using SLTConsole.Library;

namespace SLTConsole
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>

    public partial class Widget : MetroWindow
    {
        private App app = ((App)Application.Current);
        private LoginControl loginControl = new LoginControl();
        private WidgetControl widgetControl = new WidgetControl();
        private TaskbarIcon tbi = new TaskbarIcon();        

        public Widget()
        {
            InitializeComponent();
            if (app.Key.GetValue("slt_user") != null || app.Key.GetValue("slt_password") != null)
            {                
                loginControl.txtUsername.Text = app.Key.GetValue("slt_user").ToString();
                loginControl.txtPassword.Password = app.Key.GetValue("slt_password").ToString();
                btnLogin_Click(null, null);               
            }
            InitDisplay();
            loginControl.btnLogin.Click += btnLogin_Click;
            widgetControl.btnLogout.Click += btnLogout_Click;                   
            InitTaskBarIcon();
        }

        class TaskBarItemDoubleClick : ICommand
        {
            Widget widget;

            public TaskBarItemDoubleClick(Widget widget)
            {
                this.widget = widget;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                widget.SetVisible(widget.Visibility != Visibility.Visible);
            }
        }

        private void SetVisible(bool visible)
        {
            this.Visibility = (visible) ? Visibility.Visible : Visibility.Collapsed;
        }        

        private void InitTaskBarIcon()
        {
            tbi.Icon = SLTConsole.Properties.Resources.icon;
            tbi.DoubleClickCommand = new TaskBarItemDoubleClick(this);            
            tbi.ContextMenu = this.FindResource("contextMenu") as ContextMenu;
        }

        void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            app.Connection.Logout();
            InitDisplay();
        }

        private void InitDisplay()
        {            
            display.Children.Clear();
            if (!app.Connection.IsLogged)
            {                
                display.Children.Add(loginControl);
                tbi.ToolTipText = "Please login with SLT portal credentials";
            }
            else
            {
                ProfileManager profileManager = new ProfileManager(app.Connection);
                Profile profile = profileManager.GetProfile();
                widgetControl.lblPeakStatus.Content = profile.PeakStatus;
                widgetControl.lblTotalStatus.Content = profile.TotalStatus;
                display.Children.Add(widgetControl);
                tbi.ToolTipText = "Peak: " + profile.PeakStatus + ", Total: " + profile.TotalStatus;
            }
        }

        void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            app.Key.SetValue("slt_user", loginControl.txtUsername.Text);
            app.Key.SetValue("slt_password", loginControl.txtPassword.Password);

            if (app.Connection.Login(loginControl.txtUsername.Text, loginControl.txtPassword.Password))
            {
                InitDisplay();
            }
            else
            {
                (this.FindResource("shakeEffect") as Storyboard).Begin();                                
            }
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width - 40;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }

        private void mainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            SetVisible(false);
        }

        private void menuShow_Click(object sender, RoutedEventArgs e)
        {
            SetVisible(Visibility != Visibility.Visible);
        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            app.Shutdown();
        }

        private void menuStartup_Click(object sender, RoutedEventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (key.GetValue("SLTConsole", null) != null)
                {
                    key.DeleteValue("SLTConsole", false);
                    tbi.ShowBalloonTip("Automatic Startup - Disabled", "SLT Console will not startup with windows.", BalloonIcon.Info);
                }
                else
                {
                    key.SetValue("SLTConsole", "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"");
                    tbi.ShowBalloonTip("Automatic Startup - Enabled", "SLT Console will startup with windows.", BalloonIcon.Info);
                }
            }
        }
    }
}