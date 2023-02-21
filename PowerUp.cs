using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceShooter
{
    class PowerUp
    {
        //===================================
        //       CLASS PROPERTIES
        //===================================
        public Point coord { get; set; }
        public Color color { get; set; }
        public int type { get; set; }
        public Point[] vertices { get; set; }
        public bool pickedUp { get; set; }
        public int TTL { get; set; }

        //===================================
        //       CLASS METHODS
        //===================================
        public PowerUp(Random random, Point spawnPoint)
        {
            //Properties initialised.
            TTL = 360;
            type = random.Next(0, 4);
            pickedUp = false;
            coord = spawnPoint;

            //Vertices are calculated, and set.
            int px, py;
            double angle;
            angle = 1.04719755;

            Point[] points = new Point[6];
            for (int i = 0; i < 6; i++)
            {
                px = (int)(spawnPoint.X + 5 + (10 * Math.Cos(0.785398163 + angle * i)));
                py = (int)(spawnPoint.Y + 5 + (10 * Math.Sin(0.785398163 + angle * i)));
                points[i] = new Point(px, py);
            }
            vertices = points;

            //The powerUp is assigned colour based on its type.
            switch (type)
            {
                case 0:
                    color = Color.Green; //Health
                    break;
                case 1:
                    color = Color.Red; //Damage increase
                    break;
                case 2:
                    color = Color.Blue; //Shield
                    break;
            }
        }
    }
}
