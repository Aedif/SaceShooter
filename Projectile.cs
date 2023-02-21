using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceShooter
{
    class Projectile
    {
        //===================================
        //       CLASS PROPERTIES
        //===================================
        private double vx { get; set; }
        private double vy { get; set; }
        public int damage { get; set; }
        public Point coordinates { get; set; }
        public int TTL { get; set; }
        public Color color { get; set; }

        //===================================
        //       CLASS METHODS
        //===================================
        public Projectile(Point playerCoord, int projectileTTL, Point mouseOri, Point playerOri)
        {
            //Properties initialised.
            coordinates = playerCoord;
            TTL = projectileTTL;
            damage = 1;
            color = Color.Red;

            //Travel vector is calculated.
            double mag = 0;
            vx = mouseOri.X - playerOri.X;
            vy = mouseOri.Y - playerOri.Y;
            mag = Math.Sqrt(vx * vx + vy * vy); // length
            vx /= mag;
            vy /= mag;

            //The projectile is mvoed along the vector.
            int x, y;
            x = (int)(coordinates.X + vx * 12);
            y = (int)(coordinates.Y + vy * 12);
            coordinates = new Point(x, y);
            //Time remaining before the projectile is removed, is decremented.
            TTL--;
        }

        public void adjustPosition()
        {
            //Projectile is moved along its vector.
            int x, y;
            x = (int)(coordinates.X + vx * 12);
            y = (int)(coordinates.Y + vy * 12);
            coordinates = new Point(x, y);
            //Time remaining before the projectile is removed, is decremented.
            TTL--;
        }
    }
}
