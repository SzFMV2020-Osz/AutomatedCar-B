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

        public static Task<GameOverMessageResult> Show(Window parent)
        {
            var msgbox = new GameOverMessage();

            var buttonPanel = msgbox.FindControl<StackPanel>("Buttons");

            var res = GameOverMessageResult.Ok;

            var btn = new Button { Content = "Ok" };
            btn.Click += (_, __) => {
                msgbox.Close();
            };
            buttonPanel.Children.Add(btn);

            var tcs = new TaskCompletionSource<GameOverMessageResult>();
            msgbox.Closed += delegate { tcs.TrySetResult(res); };
            msgbox.Show();
            return tcs.Task;
        }
    }
}