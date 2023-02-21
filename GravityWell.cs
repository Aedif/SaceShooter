using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceShooter
{
    class GravityWell
    {
        //===================================
        //       CLASS PROPERTIES
        //===================================
        public Point coordinates { get; set; }
        public Point pulseCoordinates { get; set; }
        public int pulsePhase { get; set; }
        public double pullStrength { get; set; }
        public double speed { get; set; }
        public int pulseRadius { get; set; }
        private double vx { get; set; }
        private double vy { get; set; }

        //===================================
        //       CLASS METHODS
        //===================================
        public GravityWell(Point wellCoord, double wellPullStrength, Point travelDirection, double wellSpeed)
        {
            //Sets initial values.
            coordinates = wellCoord;
            pulseCoordinates = wellCoord;
            pullStrength = wellPullStrength;
            speed = wellSpeed;
            pulseRadius = 170;

            //Travel deirection vector is calculated.
            double mag;
            vx = travelDirection.X - coordinates.X;
            vy = travelDirection.Y - coordinates.Y;
            mag = Math.Sqrt(vx * vx + vy * vy);
            vx /= mag;
            vy /= mag;
        }

        public void adjustPosition()
        {
            //The well is moves along the vector.
            Point point = new Point(0,0);

            point.X = (int)(coordinates.X + vx * speed);
            point.Y = (int)(coordinates.Y + vy * speed);
            coordinates = point;

            point.X = (int)(pulseCoordinates.X + vx * speed);
            point.Y = (int)(pulseCoordinates.Y + vy * speed);
            pulseCoordinates = point;

            //Deceases the size of the secondary circle until phase value equal 25.
            if (pulsePhase < 25)
            {
                pulsePhase++;
                pulseRadius -= 2;
                point = new Point(pulseCoordinates.X + 1, pulseCoordinates.Y + 1);
                pulseCoordinates = point;
            }
            else //Resets the secodnary circle size and phase.
            {
                pulsePhase = 0;
                pulseRadius = 170;
                pulseCoordinates = coordinates;
            }
        }
    }
}
