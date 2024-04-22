using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Timers;
using ProjectX.Entities;
using ProjectX.Mangers;
using ProjectX.Model;
using ProjectX.Properties;

namespace ProjectX.Views
{
    public class UI
    {
        private readonly Game game;
        private readonly Font mainFont = new (FontFamily.GenericMonospace, 24, FontStyle.Bold);
        public UI(Game g)
        {
            game = g;
            
            iTimer.Elapsed += (_, _) => animate++;
            iTimer.Interval = 400;
            iTimer.Start();
        }

        public void UpdateUi(Graphics g)
        {
            //health and energy
            DrawElement(g, Resources.Interface, -700, -450);
            
            DrawLabel(g, game.player.Health.ToString(), -650, -450, Brushes.MistyRose);
            DrawLabel(g, game.player.Energy.ToString(), -650, -425, Brushes.LightBlue);
            
            //input
            DrawElement(g, Resources.InputInstrucion, 600, 300);
            switch (game.GameStage)
            {
                case GameStage.Lose:
                    DrawElement(g, Resources.DeathScreen, -800, -450);
                    break;
                case GameStage.Win:
                    DrawElement(g, Resources.WinScreen, -800, -450);
                    break;
                case GameStage.Pause:
                    DrawElement(g, Resources.Pause, -30, -40);
                    break;
            }
            
            if (isShowInstruction)
            {
                DrawInstruction(g);
            }
            else animate = 0;
        }
        
        public bool isShowInstruction => Controller.IsInstruction;
        private Timer iTimer = new();

        private List<Bitmap> instraction = new()
        {
            Resources.Rule0, Resources.Rule1, 
            Resources.Rule2, Resources.Rule3, Resources.Rule4, Resources.Rule5
        };
        private int animate;

        public void DrawInstruction(Graphics g)
        {
            if (animate == instraction.Count) iTimer.Stop();
            
            foreach (var s in instraction.Take(animate))
            {
                DrawElement(g, s, -800, -450);
            }   
        }
        private void DrawElement(Graphics g, Image i, int x, int y)
            => g.DrawImage(i, Point.Add(game.player.Position, new Size(x, y)));

        private void DrawLabel(Graphics g, string s, int x, int y, Brush b)
            => g.DrawString(s, mainFont,
                b, PointF.Add(game.player.Position, new Size(x, y)));
    }
}