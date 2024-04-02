using System;
using System.Collections.Generic;
using System.Drawing;
using ProjectX.Properties;

namespace ProjectX.Mangers
{
    public class SpawnManager
    {
        private static int SpawnMin => int.Parse(Resources.SpawnLocationMin);
        private static int SpawnMax => int.Parse(Resources.SpawnLocationMax);
        private static readonly Random Random = new();
        public static IEnumerable<Rectangle> Spawn(int count, Point point)
        {
            for(var i = 0; i<count; i++)
                yield return SpawnObjectWithSize(new Size(30, 30), point);
        }
        
        public static Rectangle SpawnObjectWithSize(Size size, Point point) 
            => new ()
            {
                Size = size,
                Location = GetValidSpawnLocation(point)
            };
        
        private static Point GetValidSpawnLocation(Point p)
        {
            var result = new Point(p.X, p.Y);

            while (!(IsValidLocation(result, p)))
            {
                var randomLocationX = Random.Next(p.X - SpawnMax, SpawnMax + p.X);

                var randomLocationY = Random.Next(p.Y - SpawnMax, SpawnMax + p.Y);

                result = new Point(randomLocationX, randomLocationY);
            }

            return result;
        }

        private static bool IsValidLocation(Point currentPosition,Point targetPosition)
        {
            return ((currentPosition.X < targetPosition.X && currentPosition.X < targetPosition.X - SpawnMin) 
                    || (currentPosition.X > targetPosition.X && currentPosition.X > targetPosition.X + SpawnMin))
                   && ((currentPosition.Y < targetPosition.Y && currentPosition.Y < targetPosition.Y - SpawnMin) 
                       || (currentPosition.Y > targetPosition.Y && currentPosition.Y > targetPosition.Y + SpawnMin))
                   && currentPosition.X is < 1500 and > -1500 && currentPosition.Y is < 1500 and > -1500;
        }
    }
}