using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace swindow
{
    public class WndToolBar : Grid
    {
        public SWindow Owner { get; set; }
        public WndToolBar()
        {
            Loaded += WndToolBar_Loaded;
        }
        private void WndToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            Owner = (SWindow)TemplatedParent;
        }
    }

    public class SWindow : Window
    {
        public Image WindowIcon
        {
            get { return (Image)GetValue(WindowIconProperty); }
            set { SetValue(WindowIconProperty, value); }
        }
        public static readonly DependencyProperty WindowIconProperty =
            DependencyProperty.Register("WindowIcon", typeof(Image), typeof(SWindow),
                new PropertyMetadata());
        public WndToolBar WindowToolBar
        {
            get { return (WndToolBar)GetValue(WindowToolBarProperty); }
            set { SetValue(WindowToolBarProperty, value); }
        }
        public static readonly DependencyProperty WindowToolBarProperty =
            DependencyProperty.Register("WindowToolBar", typeof(Grid), typeof(SWindow),
                new PropertyMetadata());
        public bool WindowMaximized
        {
            get { return (bool)GetValue(WindowMaximizedProperty); }
            set { SetValue(WindowMaximizedProperty, value); }
        }
        public static readonly DependencyProperty WindowMaximizedProperty =
            DependencyProperty.Register("WindowMaximized", typeof(bool), typeof(SWindow),
                new PropertyMetadata(false));
        public string DocumentName { get; set; }

        private Rect _restoreBounds;
        private WindowState _windowState;

        public SWindow()
        {
            //LoadResources();
            Loaded += SWindow_Loaded;
        }
        private void LoadResources()
        {
            var res = new ResourceDictionary();
            res.Source = new Uri(
                "pack://application:,,,/swindow;component/swindow/Window/SWindowStyle.xaml");
            Resources = res;

            Style = (Style)res["SWindowStyle"];
        }
        private void SWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Settings.Load();
            _restoreBounds = Settings.RestoreBounds;
            _windowState = Settings.WindowState;
            Settings.Apply();

            InitWindowState();
        }
        public void InitWindowState()
        {
            if (_windowState != WindowState.Maximized)
            {
                RestoreWindow();
            }
            else
            {
                MaximizeWindow(false);
            }
        }
        public void SwitchWindowState()
        {
            if (_windowState == WindowState.Maximized)
            {
                RestoreWindow();
            }
            else
            {
                MaximizeWindow();
            }
        }
        public void MaximizeWindow(bool saveBounds = true)
        {
            if (saveBounds)
            {
                _restoreBounds = new Rect(Left, Top, Width, Height);
            }

            var workArea = SystemParameters.WorkArea;
            Width = workArea.Width;
            Height = workArea.Height;
            Top = workArea.Top;
            Left = workArea.Left;
            _windowState = WindowState.Maximized;

            WindowMaximized = true;
        }
        public void RestoreWindow()
        {
            Left = _restoreBounds.Left;
            Top = _restoreBounds.Top;
            Height = _restoreBounds.Height;
            Width = _restoreBounds.Width;

            _windowState = WindowState.Normal;

            WindowMaximized = false;
        }
        public void MoveRestoreWindow()
        {
            var pos = Mouse.GetPosition(this);

            Left = pos.X * (1 - (_restoreBounds.Width / Width));
            Top = 0;
            Height = _restoreBounds.Height;
            Width = _restoreBounds.Width;

            _windowState = WindowState.Normal;

            WindowMaximized = false;

            ResizeMode = ResizeMode.CanMinimize;

            DragMove();

            ResizeMode = ResizeMode.CanResize;
        }
        public void MoveMaximizeWindow()
        {
            var last = new Rect(Left, Top, Width, Height);

            ResizeMode = ResizeMode.CanMinimize;
            DragMove();
            ResizeMode = ResizeMode.CanResize;

            if (last.Top != Top && Top == 0)
            {
                MaximizeWindow(false);

                _restoreBounds = last;
            }
        }
        public void MinimizeWindow()
        {
            WindowState = WindowState.Minimized;
        }
        public void CloseWindow()
        {
            Rect bounds;
            if (WindowMaximized) bounds = _restoreBounds;
            else bounds = new Rect(Left, Top, Width, Height);

            Settings.Save(bounds, _windowState);

            Close();
        }
    }
}
