using System.Timers;
using ProjectX.Properties;

namespace ProjectX.Model
{
    public class EnergyManager
    {
        public int Energy = int.Parse(Resources.EnergyCount);
        public static Timer Timer;

        public EnergyManager()
        {
            Timer = new Timer();
            Timer.Interval = 500;
            Timer.Elapsed += (sender, args) => Energy--;
        }

        public void Start() => Timer.Start();
        public void Stop() => Timer.Stop();
    }
}