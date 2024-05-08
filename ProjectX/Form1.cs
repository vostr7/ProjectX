using System.Windows.Forms;
using ProjectX.Model;
using ProjectX.Views;
using View = ProjectX.Views.View;


namespace ProjectX
{
    public partial class Form1 : Form
    {
        private Game game;
        public View View;

        public Form1()
        {
            Initialize();
        }

        private void Initialize()
        {
            game = new Game();
            View = new View(game);

            //Controller.ControlKeys(Keys.P, true);

            DoubleBuffered = true;
            Load += (_, _) => Controller.GoFullscreen(true, this);

            Paint += (_, args) => View.UpdateGraphics(args.Graphics);
            Paint += (_, _) => Invalidate();

            KeyDown += (_, args) => Controller.ControlKeys(args.KeyCode, true);
            KeyUp += (_, args) => Controller.ControlKeys(args.KeyCode, false);
        }
    }
}
