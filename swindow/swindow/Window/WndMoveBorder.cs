using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace swindow
{
    public class WndMoveBorder : Border
    {
        public static RoutedCommand CloseCommand = new RoutedCommand();
        private void CloseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Owner.CloseWindow();
        }
        private void CloseCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public static RoutedCommand MinimizeCommand = new RoutedCommand();
        private void MinimizeCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Owner.MinimizeWindow();
        }
        private void MinimizeCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public static RoutedCommand MaximizeCommand = new RoutedCommand();
        private void MaximizeCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Owner.MaximizeWindow();
        }
        private void MaximizeCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Owner == null)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = !Owner.WindowMaximized;
            }
        }

        public static RoutedCommand RestoreCommand = new RoutedCommand();
        private void RestoreCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Owner.RestoreWindow();
        }
        private void RestoreCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Owner == null)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = Owner.WindowMaximized;
            }
        }

        private void AddCommands()
        {
            var closeCommand = new CommandBinding(CloseCommand, CloseCommandExecuted, CloseCommandCanExecute);
            CommandBindings.Add(closeCommand);

            var minimizeCommand = new CommandBinding(MinimizeCommand, MinimizeCommandExecuted, MinimizeCommandCanExecute);
            CommandBindings.Add(minimizeCommand);

            var maximizeCommand = new CommandBinding(MaximizeCommand, MaximizeCommandExecuted, MaximizeCommandCanExecute);
            CommandBindings.Add(maximizeCommand);

            var restoreCommand = new CommandBinding(RestoreCommand, RestoreCommandExecuted, RestoreCommandCanExecute);
            CommandBindings.Add(restoreCommand);
        }

        public SWindow Owner { get; set; }
        private WndToolBar _bar;
        private double _width, _w;
        private bool _loaded = false;
        private bool _context = false;
        public WndMoveBorder()
        {
            Focusable = true;
            AddCommands();
            Loaded += WndMoveBorder_Loaded;
            SizeChanged += WndMoveBorder_SizeChanged;
        }
        private void WndMoveBorder_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!_loaded) return;
            SetToolBarWidth();
        }
        private void WndMoveBorder_Loaded(object sender, RoutedEventArgs e)
        {
            _loaded = true;
            Owner = (SWindow)TemplatedParent;
            _bar = Owner.WindowToolBar;

            var w = (GridLength)Owner.Resources["WindowButtonWidth"];
            _width = w.Value * 4 + 14;
            _w = _bar.ActualWidth;
            Owner.MinWidth = _width;

            SetToolBarWidth();
        }
        private void SetToolBarWidth()
        {
            if (_width + _w > ActualWidth)
            {
                double a = ActualWidth - _width;
                if (a >= 0)
                {
                    _bar.Width = a;
                }
                else
                {
                    _bar.Width = 0;
                }
            }
            else
            {
                if (_bar.Width != _w)
                {
                    _bar.Width = _w;
                }
            }
        }
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (_2click)
            {
                _2click = false;
                Owner.SwitchWindowState();
            }
        }
        private bool _2click = false;
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (_context)
            {
                return;
            }
            if (e.ClickCount == 1)
            {
                if (!Owner.WindowMaximized)
                {
                    Owner.MoveMaximizeWindow();
                }
            }
            else if (e.ClickCount == 2)
            {
                _2click = true;
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_context)
            {
                return;
            }
            if (e.LeftButton == MouseButtonState.Pressed && Owner.WindowMaximized)
            {
                Owner.MoveRestoreWindow();
            }
        }
        protected override void OnContextMenuOpening(ContextMenuEventArgs e)
        {
            base.OnContextMenuOpening(e);
            _context = true;
            Focus();
        }
        protected override void OnContextMenuClosing(ContextMenuEventArgs e)
        {
            base.OnContextMenuClosing(e);
            _context = false;
        }
    }
}
