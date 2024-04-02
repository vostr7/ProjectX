using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProjectX.Properties;

namespace ProjectX.Entities
{
    public class Bush
    {
        public static List<Bush> Objects;
        public Rectangle Bounds => View.Bounds;
        public Point Position => View.Location;
        public Image Image => View.Image;
        
        public PictureBox View = new()
        {
            Tag = "bush",
            Image = Resources.Bush,
            Size = Resources.Bush.Size
        };

        public Bush(Rectangle r)
        {
            View.Bounds = r;
            View.Size = new Size(100, 100);
        }
        public static void Update(Player player)
        {
            player.Stage = Objects.Select(x => x.Bounds)
                .Any(x => x.IntersectsWith(player.Bounds)) ? PlayerStage.Hidden : PlayerStage.Normal;
        }
    }
}