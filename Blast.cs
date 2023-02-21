using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceShooter
{
    class Blast
    {
        //===================================
        //       CLASS PROPERTIES
        //===================================
        public Point centre { get; set; }
        public int radius { get; set; }
        public int phase { get; set; }
        public int TTL { get; set; }

        //===================================
        //       CLASS METHODS
        //===================================
        //Blast is created by providing it with coordinates, radius and phase.
        public Blast(Point centrePoint, int initialRadius)
        {
            centre = centrePoint;
            radius = initialRadius;
            phase = 0;
        }

        public void update()
        {
            /* The blast is expanded and the phase incremented to produce a
            different effect during object drawing. */
            radius += 5;
            phase++;
        }
    }
}
