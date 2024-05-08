using System.Drawing;
using System.Windows.Forms;
using ProjectX.Mangers;
using ProjectX.Model;
using ProjectX.Properties;
using ProjectX.Views;

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
        public int Radius => eManager.Energy * 2;

        private readonly EnergyManager eManager;
        public int Energy => eManager.Energy;
        public PlayerStage Stage;
        public Bitmap Star = Resources.Star;

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
            eManager = new EnergyManager();
            eManager.Start();
            
            Position = start;
            Speed = speed;
            startSpeed = speed;
            Health = health;
        }

        public void Pause()
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
            eManager.Start();
        }

        public void TakeHealth(int i)
        {
            Health += i;
        }

        public void TakeEnergy(int i)
        {
            eManager.Energy += i;
        }

        public void Update()
        {
            Move();
            if (Controller.IsHeal && Health >= 200)
                Stage = PlayerStage.Heal;
            if (BonusManager.BonusTime.Enabled)
                Stage = PlayerStage.Angry;
            else if (Stage == PlayerStage.Angry)
            {
                Stage = PlayerStage.Normal;
                Health /= 2;
            }
            
            switch (Stage)
            {
                case PlayerStage.Normal:
                    View.Image = Resources.MainHero;
                    Star = Resources.Star;
                    break;
                case PlayerStage.Died:
                    View.Image = Resources.DeadHero;
                    break;
                case PlayerStage.Heal:
                    View.Image = Resources.SmillingHero;
                    View.Size = Resources.SmillingHero.Size;
                    Star = Resources.Heart;
                    break;
                case PlayerStage.Angry:
                    View.Image = Resources.AngryHero;
                    View.Size = Resources.AngryHero.Size;
                    eManager.Stop();
                    break;
            }
        }
    }
}