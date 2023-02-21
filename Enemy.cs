using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceShooter
{
    class Enemy
    {
        //===================================
        //       CLASS PROPERTIES
        //===================================
        public int health { get; set; }
        public Point centre { get; set; }
        public int radius { get; set; }
        public double speed { get; set; }
        public Point[] vertices { get; private set; }

        //===================================
        //       CLASS METHODS
        //===================================
        public Enemy(Point enemyCentre, int centreX, int centreY)
        {
            //Properties are given the parameter values.
            health = 5;
            centre = enemyCentre;
            radius = 25;
            speed = 3;

            //Travel directionv vector is calculated
            double vx = 0;
            double vy = 0;
            double mag = 0;
            vx = centreX - centre.X;
            vy = centreY - centre.Y;
            mag = Math.Sqrt(vx * vx + vy * vy);
            vx /= mag;
            vy /= mag;

            /* Vertices of the enemy are calculated based on the location of
            the player. (Enemies face the centre of the player.) */
            int px, py, px2, py2, px3, py3;
            double angle;

            mag = Math.Sqrt(vx * vx + vy * vy); // length
            px = (int)(centre.X + vx * 20);
            py = (int)(centre.Y + vy * 20);
            angle = Math.Atan2(centreY - centre.Y, centreX - centre.X);
            px2 = (int)(px + (-30 * Math.Cos(0.785398163 + angle)));
            py2 = (int)(py + (-30 * Math.Sin(0.785398163 + angle)));
            px3 = (int)(px + (-30 * Math.Cos(-0.785398163 + angle)));
            py3 = (int)(py + (-30 * Math.Sin(-0.785398163 + angle)));

            /* Temporary variable used to transfer calculated coordinates to
            properties. */
            Point[] points = new Point[3];
            points[0] = new Point(px, py);
            points[1] = new Point(px2, py2);
            points[2] = new Point(px3, py3);

            //Transfer.
            vertices = points;
        }

        public void adjustPosition(int centreX, int centreY)
        {
            double vx = 0;
            double vy = 0;
            double mag = 0;
            int x, y;

            if (centre.X != 0)
            {
                /* Recalculates the vector. moves the centre of the enemy along it, and
                updates its vertices to correspond to it.. */
                vx = centreX - centre.X;
                vy = centreY - centre.Y;
                mag = Math.Sqrt(vx * vx + vy * vy); // length
                vx /= mag;
                vy /= mag;
                x = (int)((double)centre.X + vx * speed);
                y = (int)((double)centre.Y + vy * speed);
                centre = new Point(x, y);

                int px, py, px2, py2, px3, py3;
                double angle;

                vx = centreX - x;
                vy = centreY - y;
                mag = Math.Sqrt(vx * vx + vy * vy);
                vx /= mag;
                vy /= mag;
                px = (int)((double)x + vx * 20);
                py = (int)((double)y + vy * 20);
                angle = Math.Atan2(centreY - y, centreX - x);
                px2 = (int)(px + (-30 * Math.Cos(0.785398163 + angle)));
                py2 = (int)(py + (-30 * Math.Sin(0.785398163 + angle)));
                px3 = (int)(px + (-30 * Math.Cos(-0.785398163 + angle)));
                py3 = (int)(py + (-30 * Math.Sin(-0.785398163 + angle)));

                Point[] points = new Point[3];
                points[0] = new Point(px, py);
                points[1] = new Point(px2, py2);
                points[2] = new Point(px3, py3);

                vertices = points;
            }
        }
    }
}
