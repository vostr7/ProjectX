using System.Timers;
using ProjectX.Properties;

namespace ProjectX.Model
{
    public class BonusManager
    {
        public static Timer BonusTime = new()
        {
            Interval = 500
        };

        private static int now;
        public static void StartTimer()
        {
            now = 0;
            BonusTime = new Timer()
            {
                Interval = 500
            };
            BonusTime.Elapsed += (_, _) => Update();
            BonusTime.Start();
        }

        private static int Value => int.Parse(Resources.TimeBonusValue);
        private static void Update()
        {
            now++;
            if (now == Value)
                BonusTime.Close();
        }
    }
}