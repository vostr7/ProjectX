using System.Drawing;
using System.Windows.Forms;
using ProjectX.Entities.Control;
using ProjectX.Properties;

namespace ProjectX.Entities
{
    public class Player
    {
        public Point Position
        {
            get => View.Location;
            set => View.Location = value;
        }
        public Rectangle Bounds => View.Bounds;
        public Image Image => View.Image;
        
        public PictureBox View = new()
        {
            Image = Resources.MainHero,
            Size = Resources.MainHero.Size,
            Location = new Point(0, 0)
        };
        
        public int Speed { get; private set; }
        private int startSpeed;
        public int Health { get; private set; }
        public int X => Position.X;

        public int Y => Position.Y;
        public PlayerStage Stage;

        private void Move(int dx, int dy)
        {
            var delta = new Size(dx * Speed, dy * Speed);
            var pos = Point.Add(Position, delta);
            if (pos.X > -1500 && pos.X < 1500 && 
                pos.Y > -1500 && pos.Y < 1500)
                Position = pos;

        }
        
        public void Move()
        {
            if (Controller.IsInputDown)
                Move(0, 1);
            if (Controller.IsInputUp)
                Move(0, -1);
            if (Controller.IsInputRight)
                Move(1, 0);
            if (Controller.IsInputLeft)
                Move(-1, 0);
        }

        public Player(Point start, int speed, int health)
        {
            
        }
        
        private void Pause()
        {
            Stage = PlayerStage.Paused;
            Speed = 0;    
        }
        
        public void Die()
        {
            Health = 0;
            Pause();
            Stage = PlayerStage.Died;
        }
        
        public void Start()
        {
            Stage = PlayerStage.Normal;
            Speed = startSpeed;
        }
        
        public void TakeHealth(int i)
        {
            Health += i;
        }
        
    }
}