using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ProjectX.Properties;

namespace ProjectX.Entities
{
    public class Plant
    {
        public static List<Plant> Objects;

        private static List<Bitmap> view = new()
        {
            Resources.Plant0, Resources.Plant1, 
            Resources.Plant2, Resources.Plant3
        };

        private static Random r = new();
        public PictureBox View = new()
        {
            Image = Resources.Plant0,
            Size = new Size(100, 100)
        };
        public Rectangle Bounds => View.Bounds;
        public Image Image => View.Image;

        public Plant(Rectangle p)
        {
            var i = r.Next(0, view.Count - 1);
            View.Image = view[i];
            View.Location = p.Location;
            View.Size = new Size(50, 50);
        }
    }
}