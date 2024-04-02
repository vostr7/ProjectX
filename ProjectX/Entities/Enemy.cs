using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProjectX.Properties;

namespace ProjectX.Entities
{
    public class Enemy
    {
        public static List<Enemy> Objects;
        private static readonly List<HealthyEnemy> HealthyEnemies = new();
        private Rectangle Bounds => View.Bounds;
        private Point Position => View.Location;
        public Image Image => View.Image;

        private Point StartPosition;

        private PictureBox View = new()
        {
            Image = Resources.Enemy,
            Size = Resources.Enemy.Size
        };

        private static readonly int Damage = int.Parse(Resources.EnemyDamage);
        private static readonly int Speed = int.Parse(Resources.EnemySpeed);
        public Enemy(Rectangle r)
        {
            View.Bounds = r;
            View.Size = new Size(100, 100);
            StartPosition = new Point(r.Location.X, r.Location.Y);
        }

        private Point Move(int dx, int dy)
        {
            return new Point(Position.X + dx * Speed, Position.Y + dy * Speed);
        }

        private void MoveToHero(Point p, int speed)
        {
            var tPos = Position;
            if (Position.X > p.X) tPos.X = Move(-speed, 0).X;
            else if (Position.X < p.X) tPos.X = Move(speed, 0).X;
            if (Position.Y < p.Y) tPos.Y = Move(0, speed).Y;
            else if (Position.Y > p.Y) tPos.Y = Move(0, -speed).Y;

            var tRect = new Rectangle(tPos, View.Size);
            if (Objects.All(x => !x.Bounds.IntersectsWith(tRect) || x.Position == Position))
                View.Location = tPos;
        }

        private void MoveToPosition(Point p) => MoveToHero(p, Speed);

        public static void Update(Player player)
        {
            switch (player.Stage)
            {
                case PlayerStage.Normal:
                    foreach (var e in Objects)
                    {
                        if (e.View.Bounds.IntersectsWith(player.Bounds))
                            player.TakeHealth(-Damage);
                        else
                            e.MoveToPosition(player.Position);
                    }
                    break;
                case PlayerStage.Hidden:
                    foreach (var e in Objects)
                        e.MoveToPosition(e.StartPosition);
                    break;
                case PlayerStage.Heal:
                    for (var i =0; i<Objects.Count;i++)
                    {
                        var e = Objects[i];
                        if (e.View.Bounds.IntersectsWith(player.Bounds))
                        {
                            player.TakeHealth(-150);
                            HealthyEnemies.Add(new HealthyEnemy(e.Position, false));
                            Objects.RemoveAt(i);
                        }
                        else
                            e.MoveToHero(player.Position, -Speed);
                    }
                    break;
                case PlayerStage.Angry:
                    for (var i =0; i<Objects.Count;i++)
                    {
                        var e = Objects[i];
                        if (e.View.Bounds.IntersectsWith(player.Bounds))
                        {
                            player.TakeHealth(0);
                            HealthyEnemies.Add(new HealthyEnemy(e.Position, true));
                            Objects.RemoveAt(i);
                        }
                        else
                            e.MoveToHero(player.Position, -Speed*2);
                    }
                    break;
            }
        }
    }
}
