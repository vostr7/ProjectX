using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProjectX.Mangers;
using ProjectX.Model;
using ProjectX.Properties;

namespace ProjectX.Entities
{
    public class TimeBonus
    {
        public static List<TimeBonus> Objects;
        public Rectangle Bounds => View.Bounds;
        public Point Position => View.Location;
        public bool IsAngry = false;
        public Image Image => View.Image;

        private PictureBox View = new()
        {
            Image = Resources.HealthBonus,
            Size = Resources.HealthBonus.Size
        };

        public TimeBonus(Rectangle r, bool isPoison)
        {
            View.Bounds = r;
            IsAngry = isPoison;
            if (IsAngry)
                View.Image = Resources.AngryMushroom;
            else
                View.Image = Resources.HealthBonus;
        }

        public static void Update(Player player)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                var b = Objects[i];
                if (b.Bounds.IntersectsWith(player.Bounds))
                {
                    BonusManager.StartTimer();
                    Objects.RemoveAt(i);
                    Objects.Add(new TimeBonus(SpawnManager.Spawn(1, player.Position).First(), b.IsAngry));
                }
            }
        }
    }
}
