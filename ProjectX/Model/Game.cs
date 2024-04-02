using System.Drawing;
using ProjectX.Entities;
using ProjectX.Properties;

namespace ProjectX.Mangers
{
    public class Game
    {
        //objects
        public readonly Player player = new(
            Point.Empty, 
            int.Parse(Resources.HeroStartSpeed), 
            int.Parse(Resources.HeroStartHealth));
        
        public GameStage GameStage;
        
        public Point Center => Point.Add(player.Position, new Size(50, 50));
        
        public Rectangle ViewZone =>
            new (new Point(player.Position.X - player.Radius, player.Position.Y - player.Radius),
                player.Bounds.Size + new Size(player.Radius * 2, player.Radius * 2));
    }
    // public Game()
    // {
    //
    // }
}