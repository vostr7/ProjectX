using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProjectX.Mangers;
using ProjectX.Properties;

namespace ProjectX.Entities
{
    public class EnergyBonus
    {
        public static List<EnergyBonus> Objects;
        public Rectangle Bounds => View.Bounds;
        public Point Position => View.Location;
        public Image Image => View.Image;
        
        public PictureBox View = new()
        {
            Image = Resources.EnergyBonus,
            Size = Resources.EnergyBonus.Size
        };

        public static readonly int Bonus = int.Parse(Resources.EnergyBonusValue);

        public EnergyBonus(Rectangle r)
        {
            View.Bounds = r;
            View.Size = new Size(50, 50);
        }
        public static void Update(Player player)
        {
            for (int i = 0; i<Objects.Count; i++)
            {
                var b = Objects[i];
                if (b.Bounds.IntersectsWith(player.Bounds))
                {
                    player.TakeEnergy(Bonus);
                    Objects.RemoveAt(i);
                    Objects.Add(new EnergyBonus(SpawnManager.Spawn(1, player.Position).Single()));
                }
            }
        }
    }
}