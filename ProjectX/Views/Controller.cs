using System.Windows.Forms;

namespace ProjectX.Views
{
    internal static class Controller
    {
        public static bool IsInputUp { get; private set; }
        public static bool IsInputLeft { get; private set; }
        public static bool IsInputDown { get; private set; }
        public static bool IsInputRight { get; private set; }

        public static bool IsPaused = true;
        public static bool IsInstruction = true;
        public static bool IsHeal { get; private set; }

        internal static void ControlKeys(Keys key, bool isActive)
        {
            switch (key)
            {
                case Keys.W:
                    IsInputUp = isActive;
                    break;
                case Keys.A:
                    IsInputLeft = isActive;
                    break;
                case Keys.S:
                    IsInputDown = isActive;
                    break;
                case Keys.D:
                    IsInputRight = isActive;
                    break;

                case Keys.Escape:
                    Application.Exit();
                    break;
                case Keys.R:
                    Application.Restart();
                    break;
                case Keys.P:
                    if (!isActive)
                        IsPaused = !IsPaused;
                    if (!IsPaused) IsInstruction = false;
                    break;
                case Keys.I:
                    if (!isActive)
                    {
                        IsPaused = true;
                        IsInstruction = true;
                    }

                    break;
                case Keys.H:
                    IsHeal = isActive;
                    break;
            }
        }
        
        public static void GoFullscreen(bool fullscreen, Form f)
        {
            if (fullscreen)
            {
                f.WindowState = FormWindowState.Normal;
                f.FormBorderStyle = FormBorderStyle.None;
                f.Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                f.WindowState = FormWindowState.Maximized;
                f.FormBorderStyle = FormBorderStyle.Sizable;
            }
        }
    }
}