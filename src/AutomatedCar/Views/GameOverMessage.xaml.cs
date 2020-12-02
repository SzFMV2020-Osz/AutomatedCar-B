using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Threading.Tasks;

namespace AvaloniaAppTemplate.Namespace
{
    public class GameOverMessage : Window
    {
        public enum GameOverMessageButtons
        {
            Ok,
            OkCancel,
            YesNo,
            YesNoCancel
        }

        public enum GameOverMessageResult
        {
            Ok,
            Cancel,
            Yes,
            No
        }

        public GameOverMessage()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public static Task<GameOverMessageResult> Show(Window parent, GameOverMessageButtons buttons)
        {
            var msgbox = new GameOverMessage()
            {
                Title = "Controls"
            };
            var buttonPanel = msgbox.FindControl<StackPanel>("Buttons");

            var res = GameOverMessageResult.Ok;

            void AddButton(string caption, GameOverMessageResult r, bool def = false)
            {
                var btn = new Button { Content = caption };
                btn.Click += (_, __) => {
                    res = r;
                    msgbox.Close();
                };
                buttonPanel.Children.Add(btn);
                if (def)
                    res = r;
            }

            if (buttons == GameOverMessageButtons.Ok || buttons == GameOverMessageButtons.OkCancel)
                AddButton("Ok", GameOverMessageResult.Ok, true);
            if (buttons == GameOverMessageButtons.YesNo || buttons == GameOverMessageButtons.YesNoCancel)
            {
                AddButton("Yes", GameOverMessageResult.Yes);
                AddButton("No", GameOverMessageResult.No, true);
            }

            if (buttons == GameOverMessageButtons.OkCancel || buttons == GameOverMessageButtons.YesNoCancel)
                AddButton("Cancel", GameOverMessageResult.Cancel, true);

            var tcs = new TaskCompletionSource<GameOverMessageResult>();
            msgbox.Closed += delegate { tcs.TrySetResult(res); };
            if (parent != null)
                msgbox.ShowDialog(parent);
            else msgbox.Show();
            return tcs.Task;
        }
    }
}