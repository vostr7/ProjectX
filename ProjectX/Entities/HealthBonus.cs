using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProjectX.Mangers;
using ProjectX.Properties;

namespace ProjectX.Entities
{
    public class HealthBonus
    {
        public static List<HealthBonus> Objects;
        public Rectangle Bounds => View.Bounds;
        public Point Position => View.Location;
        public bool IsPoison = false;
        public Image Image => View.Image;
        
        private PictureBox View = new()
        {
            Image = Resources.HealthBonus,
            Size = Resources.HealthBonus.Size
        };

        public readonly int Bonus;

        public HealthBonus(Rectangle r, bool isPoison)
        {
            View.Bounds = r;
            IsPoison = isPoison;
            if (IsPoison)
            {
                Bonus = -300;
                View.Image = Resources.PoisonBerry;
            }
            else
            {
                Bonus = int.Parse(Resources.HealthBonusValue);
                View.Image = Resources.HealthBonus;
            }
        }
        public static void Update(Player player)
        {
            if (player.Stage == PlayerStage.Normal)
                for (var i = 0; i < Objects.Count; i++)
                {
                    var b = Objects[i];
                    if (b.Bounds.IntersectsWith(player.Bounds))
                    {
                        player.TakeHealth(b.Bonus);
                        Objects.RemoveAt(i);
                        Objects.Add(new HealthBonus(SpawnManager.Spawn(1, player.Position).First(), b.IsPoison));
                    }
                }
        }
    }
}
