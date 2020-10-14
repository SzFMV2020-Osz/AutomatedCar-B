using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace AutomatedCar.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Keyboard.IsToggleKeyPressed(e.Key))
            {
                Keyboard.ToggleableKeys.Add(e.Key);
            }
            else
            {
                if (Keyboard.IsPressKeysPressed(e.Key))
                {
                    Keyboard.PressableKeys.Add(e.Key);
                }

                Keyboard.Keys.Add(e.Key);
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (!Keyboard.IsToggleKeyPressed(e.Key))
            {
                Keyboard.Keys.Remove(e.Key);
            }

            base.OnKeyUp(e);
        }
    }
}