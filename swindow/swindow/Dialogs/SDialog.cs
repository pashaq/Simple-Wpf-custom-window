using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace swindow
{
    public class DlgCloseButton : Button
    {
        public SDialog Owner { get; set; }
        public DlgCloseButton()
        {
            Loaded += DlgCloseButton_Loaded;
            Click += DlgCloseButton_Click;
        }
        private void DlgCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Owner.Close();
        }
        private void DlgCloseButton_Loaded(object sender, RoutedEventArgs e)
        {
            Owner = (SDialog)TemplatedParent;
        }
    }
    public class DlgToolBar : Grid
    {
        public SDialog Owner { get; set; }
    }
    public class DlgMoveBorder : Border
    {
        public SDialog Owner { get; set; }
        public DlgMoveBorder()
        {
            Loaded += DlgMoveBorder_Loaded;
        }
        private void DlgMoveBorder_Loaded(object sender, RoutedEventArgs e)
        {
            Owner = (SDialog)TemplatedParent;
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (e.ClickCount == 1)
            {
                Owner.ResizeMode = ResizeMode.CanMinimize;
                Owner.DragMove();
                Owner.ResizeMode = ResizeMode.CanResize;
            }
        }
    }
    public class SDialog : Window
    {
        public DlgToolBar DialogToolBar
        {
            get { return (DlgToolBar)GetValue(DialogToolBarProperty); }
            set { SetValue(DialogToolBarProperty, value); }
        }
        public static readonly DependencyProperty DialogToolBarProperty =
            DependencyProperty.Register("DialogToolBar", typeof(Grid), typeof(SDialog), new PropertyMetadata());

        public static bool IsGoDialog { get; private set; }
        public SDialog()
        {
            IsGoDialog = true;
            WindowState = WindowState.Normal;

            Loaded += DialogWindow_Loaded;
        }
        protected override void OnClosed(EventArgs e)
        {
            IsGoDialog = false;
            base.OnClosed(e);
        }
        private void DialogWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DialogToolBar.Owner = this;
        }
    }
}