using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceShooter
{
    class Asteroid
    {
        //===================================
        //       CLASS PROPERTIES
        //===================================
        public int travelDist { get; set; }
        public Point centre { get; private set; }
        public Point[] vertices { get; set; }
        public double speed { get; set; }
        public int minRadius { get; set; }
        public int maxRadius { get; set; }
        public double vx { get; set; }
        public double vy { get; set; }
        public double[] xVertices { get; set; }
        private double[] yVertices { get; set; }
        private double xcentre { get; set; }
        private double ycentre { get; set; }
        public int[] sectionLife { get; set; }

        //===================================
        //       CLASS METHODS
        //===================================
        //Executed during object creation. Initialises property values.
        public Asteroid(Point astCentre, double astSpeed, int astMinRadius, int astMaxRadius, Point origin, Point destination, Random random)
        {
            //Assigns the provides parameter values to the properties.
            centre = astCentre;
            speed = astSpeed;
            minRadius = astMinRadius;
            maxRadius = astMaxRadius;

            //Travel direction vector is calculated.
            double mag;
            vx = destination.X - origin.X;
            vy = destination.Y - origin.Y;
            mag = Math.Sqrt(vx * vx + vy * vy);

            //Vectors are normalised for object coordiante updating.
            vx /= mag;
            vy /= mag;

            /* Double variables are assigned origin coordinates. These variables
            will be used in calculations unlike Point centre variable. */
            centre = new Point(origin.X, origin.Y);
            xcentre = origin.X;
            ycentre = origin.Y;

            int px, py;
            double angle;
            //60 degrees in radians.
            angle = 1.04719755;

            //Temporary variables used in calculations.
            Point[] points = new Point[6];
            double[] doublePointsx = new double[6];
            double[] doublePointsy = new double[6];
            int randomValue;

            /* All 6 vertices are drawn at an angle from the horizontal. The angle is
            higher depending on the vertice number being drawn. */
            for (int i = 0; i < 6; i++)
            {
                /* This value provides variance in distance from the centre that the 
                vertice is drawn. */
                randomValue = random.Next(-50, -30);
                px = (int)(centre.X + (randomValue * Math.Cos(0.785398163 + angle * i)));
                py = (int)(centre.Y + (randomValue * Math.Sin(0.785398163 + angle * i)));
                doublePointsx[i] = px;
                doublePointsy[i] = py;
                points[i] = new Point(px, py);
            }
            //Temporary variables are transfered to class properties.
            vertices = points;
            xVertices = doublePointsx;
            yVertices = doublePointsy;

            //3 invisible sections of the asteroid are provides life values.
            int[] qlife = { 5, 5, 5 };
            sectionLife = qlife;
        }

        public void adjustPosition()
        {
            //Temporary variables.
            double x, y;
            double px, py;
            Point[] points = new Point[6];
            double[] doublePointsx = new double[6];
            double[] doublePointsy = new double[6];
            x = xcentre;
            y = ycentre;

            //The centre of the asteroid is moves along the vector.
            xcentre = (xcentre + vx * speed);
            ycentre = (ycentre + vy * speed);

            /* All the vertices are rotated 2 degrees (0.034906585 radians), and moved
            in the direction of the vector. */
            for (int i = 0; i < 6; i++)
            {
                px = (((xVertices[i] - x) * Math.Cos(0.034906585)) - ((yVertices[i] - y) * Math.Sin(0.034906585)) + x);
                py = (((xVertices[i] - x) * Math.Sin(0.034906585)) + ((yVertices[i] - y) * Math.Cos(0.034906585)) + y);
                doublePointsx[i] = px + vx * speed;
                doublePointsy[i] = py + vy * speed;
                points[i] = new Point((int)px, (int)py);
            }
            //Temporary variables are transfered to proeprties.
            vertices = points;
            xVertices = doublePointsx;
            yVertices = doublePointsy;
            centre = new Point((int)xcentre, (int)ycentre);
        }

        public void hitTaken(int verticeHit, Point hitCoord, int damage)
        {
            /* Determines which of the asteroid sections have been
            hit based on the vertice parameter provided. */
            int a = 0;
            switch(verticeHit)
            {
                case 1:
                    a = 0;
                    break;
                case 3:
                    a = 1;
                    break;
                case 5:
                    a = 2;
                    break;
            }

            //Damage is applied to the hit section.
            sectionLife[a] = sectionLife[a] - damage;
            if (sectionLife[a] <= 0)
            {
                //If the section has been destroyed the middle vertice is caved in into
                //the asteroid centre.
                xVertices[verticeHit] = xcentre;
                yVertices[verticeHit] = ycentre;

                /* The travel direction of the asteroid is also changed by calcualting a new
                vector going from the hit location to the asteroids centre point. */
                double mag;
                vx = hitCoord.X - xcentre;
                vy = hitCoord.Y - ycentre;
                mag = Math.Sqrt(vx * vx + vy * vy);

                vx /= mag;
                vy /= mag;

                vx = vx * -1;
                vy = vy * -1;
            }

            /* Extra vertices are caved in into the centre depending on the combinations
            of sections that have been destroyed. */
            if ((sectionLife[0] <= 0) && (sectionLife[1] <= 0))
            {
                xVertices[2] = xcentre;
                yVertices[2] = ycentre;
            }

            if ((sectionLife[1] <= 0) && (sectionLife[2] <= 0))
            {
                xVertices[4] = xcentre;
                yVertices[4] = ycentre;
            }

            if ((sectionLife[2] <= 0) && (sectionLife[0] <= 0))
            {
                xVertices[0] = xcentre;
                yVertices[0] = ycentre;
            }
        }
    }
}
