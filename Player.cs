using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceShooter
{
    class Player
    {
        //===================================
        //       CLASS PROPERTIES
        //===================================
        public bool keyW { get; set; }
        public bool keyA { get; set; }
        public bool keyS { get; set; }
        public bool keyD { get; set; }
        public int radius { get; set; }
        public int health { get; set; }
        public int score { get; set; }
        public int shield { get; set; }
        public Point shieldCoord { get; set; }
        public int centreX { get; set; }
        public int centreY { get; set; }
        public Point[] vertices { get; set; }
        public int roidImmunity { get; set; }

        //===================================
        //       CLASS METHODS
        //===================================
        public void adjustPosition(int mouseX, int mouseY)
        {
            int px, py;
            int px2, py2;
            int px3, py3;
            double angle;

            //Moves the centre of the player according tot he key being pressed.
            if ((centreY > 0) && (keyW == true))
            {
                centreY -= 2;
            }
            if ((centreX > 0) && (keyA == true))
            {
                centreX -= 2;
            }
            if ((centreY < 647) && (keyS == true))
            {
                centreY += 2;
            }
            if ((centreX < 839) && (keyD == true))
            {
                centreX += 2;
            }

            //The vector between the player centre and the mouse is calculated.
            double vx = mouseX - centreX;
            double vy = mouseY - centreY;
            double mag = Math.Sqrt(vx * vx + vy * vy); // length
            vx /= mag;
            vy /= mag;

            //Player vertices are drawn according to the vector.
            px = (int)((double)centreX + vx * 10);
            py = (int)((double)centreY + vy * 10);

            angle = Math.Atan2(mouseY - centreY, mouseX - centreX);

            px2 = (int)(px + (-30 * Math.Cos(0.785398163 + angle)));
            py2 = (int)(py + (-30 * Math.Sin(0.785398163 + angle)));

            px3 = (int)(px + (-30 * Math.Cos(-0.785398163 + angle)));
            py3 = (int)(py + (-30 * Math.Sin(-0.785398163 + angle)));

            Point[] points = new Point[3];
            points[0] = new Point(px, py);
            points[1] = new Point(px2, py2);
            points[2] = new Point(px3, py3);

            vertices = points;

            /* If the player has shields (>0) the shield is given coordinates to
            be used for drawing. */
            if (shield > 0)
            {
                shieldCoord = new Point(centreX - 25, centreY - 25);
            }

            /* If the player currently has immunity against asteroids the value is
            continualy decreased until the immunity is gone. */
            if (roidImmunity > 0)
            {
                roidImmunity--;
            }
        }

    }
}
