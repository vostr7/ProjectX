using System.Drawing;
using System.Linq;
using ProjectX.Entities;
using ProjectX.Mangers;
using ProjectX.Properties;
using ProjectX.Views;

namespace ProjectX.Model
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
            new(new Point(player.Position.X - player.Radius, player.Position.Y - player.Radius),
                player.Bounds.Size + new Size(player.Radius * 2, player.Radius * 2));

        public Game()
        {
            Enemy.Objects = SpawnManager.Spawn(int.Parse(Resources.EnemyCount), Point.Empty)
                .Select(x => new Enemy(x))
                .ToList();
            HealthBonus.Objects = SpawnManager.Spawn(int.Parse(Resources.BonusCount), Point.Empty)
                .Select(x => new HealthBonus(x, false))
                .ToList();

        }

        public void Update()
        {
            switch (GameStage)
            {
                case GameStage.Play:
                    if (player.Health <= 0 || player.Energy <= 0)
                        GameStage = GameStage.Lose;
                    else if (Controller.IsPaused)
                        GameStage = GameStage.Pause;
                    if (Enemy.Objects.Count == 0)
                        GameStage = GameStage.Win;
                    Enemy.Update(player);
                    HealthBonus.Update(player);
                    Bush.Update(player);
                    EnergyBonus.Update(player);
                    TimeBonus.Update(player);
                    player.Update();

                    break;
                case GameStage.Pause:
                    player.Pause();
                    if (!Controller.IsPaused)
                    {
                        player.Start();
                        GameStage = GameStage.Play;
                    }

                    break;
                case GameStage.Lose:
                    player.Die();
                    break;
                case GameStage.Win:
                    player.Pause();
                    break;
            }
        }
    }
}