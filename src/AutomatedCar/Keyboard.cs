namespace AutomatedCar
{
    using System.Collections.Generic;
    using System.Linq;
    using Avalonia.Input;

    public static class Keyboard
    {
        public static IReadOnlyCollection<Key> ToggleKeys = new List<Key>
        {
            Key.E,
            Key.Q,
            Key.A,
            Key.D,
            Key.R,
            Key.U,
            Key.Z,
            Key.I,
            Key.F,
        };

        public static IReadOnlyCollection<Key> PressKeys = new List<Key>
        {
            Key.W,
            Key.S,
            Key.T,
            Key.OemPlus,
            Key.OemMinus,
        };

        public static readonly HashSet<Key> Keys = new HashSet<Key>();

        public static readonly HashSet<Key> ToggleableKeys = new HashSet<Key>();

        public static readonly HashSet<Key> PressableKeys = new HashSet<Key>();

        public static bool IsKeyDown(Key key) => Keys.Contains(key);

        public static bool IsToggleKeyPressed(Key key) => ToggleKeys.Contains(key);

        public static bool IsToggleableKeyDown(Key key) => ToggleableKeys.Contains(key);

        public static bool DeleteToggleableKey(Key key) => ToggleableKeys.Remove(key);

        public static bool IsPressKeysPressed(Key key) => PressKeys.Contains(key);

        public static bool IsPressableKeysDown(Key key) => PressableKeys.Contains(key);

        public static bool DeletePressableKeys(Key key) => PressableKeys.Remove(key);
    }
}