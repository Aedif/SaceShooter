using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

/* BTEC Level 3 Extended Diploma in IT
 * Unit 22 - Developing Computer Games
 * SpaceShooter Ver 0.55
 * Martynas Vaitkus
 * 11/03/2013 */

namespace SpaceShooter
{
    public partial class Form1 : Form
    {
        //===================================
        //          GLOBAL VARIABLES
        //===================================

        //----------------------------------------------------------------------
        /*Lists are created for the storage of various different object types 
        that will be created during the game time.*/
        List<Enemy> enemies = new List<Enemy>();
        List<Projectile> projectiles = new List<Projectile>();
        List<Asteroid> asteroids = new List<Asteroid>();
        List<Blast> blasts = new List<Blast>();
        List<PowerUp> powerUps = new List<PowerUp>();
        List<GravityWell> gravityWells = new List<GravityWell>();
        /*Since the played object will exist for the entirity of the game and of 
        which only a single instance will need to exist at any given moment it 
        has been created without the use of a list. */
        Player player1 = new Player();
        /* Similarly only a single level object is needed */
        Level level = new Level();
        //----------------------------------------------------------------------
        /* Tracks whether a mouse is currently pressed or not, as to decide 
        whether to spawn projectiles or not. */
        bool mouseDown = false;
        /* Global mouse variables that contain the position of the mouse to be 
        used in the direction control of the player1 object. */
        int mouseX, mouseY;
        //----------------------------------------------------------------------
        /* The number of enemies killed is tracked by the enemiesKilled integer, 
        which will be used to spawn powerUps at appropriate times. */
        int enemiesKilled;
        /* Value used to adjust health reduction after projectile collison. */
        int shotDamageModifier;
        /*powerSpawn will be used to track the postition of the Nth enemy killed, 
        as to be able to spawn a powerUp in that exact location*/
        Point powerSpawn;
        /*Since the Random() function makes use of a time seed to produce a 
        semi-random numbers quick serial declerations of it will result in the
        use of the same seed and hence the same "random" number for each call of 
        it. To avoid this problem the random has been declared at global scope as 
        to avoid the need to keep continualy declearing it. */
        Random random = new Random();
        //----------------------------------------------------------------------
        public Form1()
        {
            //===================================
            //          INITIALISATION
            //===================================
            //----------------------------------------------------------------------
            InitializeComponent();
            /* Player starting position as well as health and radius values are 
            initialised. */
            player1.centreX = 300;
            player1.centreY = 300;
            player1.radius = 25;
            player1.health = 50;
            //Changes the colour of the Energy progress bar.
            prbEnergy.ForeColor = Color.Red;
            /* Sets the maximum Life progress bar value which will prevent the player 
            from stacking too much health. */
            prbLife.Maximum = 50;
            //Life progress bar is given the value of the current health of the player.
            prbLife.Value = player1.health;
            /* Starts the update timer which will execute all the neccessary code wihin 
            the program.*/
            tmrUpdate.Enabled = true;
            //----------------------------------------------------------------------
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            /* All the major methods are called every 60th of a second acting as run
            speed control. */
            spawner();
            objectCleaner();
            objectUpdater();
            collisionDetection();
            pnlPaper.Invalidate();

            //Game over test.
            if (player1.health <= 0)
            {
                tmrUpdate.Enabled = false;
                lblGameOver.Visible = true;
                lblGameOver.Text = "GAME OVER";
            }
            else //prbLife update.
            {
                prbLife.Value = player1.health;
            }

            //Energy bar control.
            if (mouseDown == false && prbEnergy.Value != 0)
            {
                prbEnergy.Value--;
            }

            //Label used for testing purposes.
            /*
            lblTest.Visible = true;
            lblTest.Text = "Remaining: " + level.enemiesRemaining.ToString() + "Spawned: " + level.totalEnemiesSpawned.ToString();
            */

            //Level clear test.
            if (level.enemiesRemaining <= 0)
            {
                /* Game stopping, display of transitional text, and initiation of level
                change. */
                tmrUpdate.Enabled = false;
                lblLevel.Visible = true;
                lblLevel.Text = "Level: " + (level.levelNumber + 1).ToString();
                tmrLevelDelay.Enabled = true;
            }
        }
        private void spawner()
        {
            //===================================
            //          ENEMY SPAWNER
            //===================================

            //Spawn condition (level specific enemy limit)
            if (enemies.Count < level.enemySpawn && level.totalEnemiesSpawned <= level.enemyNumber)
            {
                //Number of enemies needed to be spawned
                int a;
                a = level.enemySpawn - enemies.Count;

                //Spawn condition in conjunction to the enemies to be spawned.
                for (int i = 0; i < a && level.totalEnemiesSpawned <= level.enemyNumber; i++)
                {
                    //Enemy spawn point selection.
                    int x, y, c;
                    c = random.Next(0, 4);

                    if (c == 0)
                    {
                        x = random.Next(0, 839);
                        y = 1;
                    }
                    else if (c == 1)
                    {
                        y = random.Next(0, 647);
                        x = 1;
                    }
                    else if (c == 2)
                    {
                        x = 839;
                        y = random.Next(0, 647);
                    }
                    else
                    {
                        y = 647;
                        x = random.Next(0, 839);
                    }

                    //Enemy object creation using random location coordinates.
                    enemies.Add(new Enemy(new Point(x, y), player1.centreX, player1.centreY));
                    //Enemy spawn number tracker.
                    level.totalEnemiesSpawned++;
                }
            }

            //===================================
            //      PROJECTILE SPAWNER
            //===================================
            //Spawn condition (energy must be available.
            if ((mouseDown == true) && prbEnergy.Value != prbEnergy.Maximum)
            {
                //Projectile object creation using player coordinates and damage modifier.
                projectiles.Add(new Projectile(new Point(player1.centreX, player1.centreY), 50,
                    new Point(mouseX, mouseY), new Point(player1.centreX, player1.centreY)));
                projectiles[projectiles.Count - 1].damage += shotDamageModifier;
                //Energy bar updating.
                prbEnergy.Value++;
                prbEnergy.Value--;
                prbEnergy.Value++;
            }

            //===================================
            //        ASTEROID SPAWNER
            //===================================
            //Spawn condition.
            if (asteroids.Count < level.asteroidNumber)
            {
                //Number f asteroid to be spawned.
                int a;
                a = level.asteroidNumber - asteroids.Count;

                for (int i = 0; i < a; i++)
                {
                    //Random screen edge selection.
                    int x, y, c;
                    int x2, y2;
                    c = random.Next(0, 4);

                    if (c == 0)
                    {
                        x = random.Next(0, 839);
                        y = 1;
                        x2 = random.Next(0, 839);
                        y2 = 647;
                    }
                    else if (c == 1)
                    {
                        y = random.Next(0, 647);
                        x = 1;
                        x2 = 839;
                        y2 = random.Next(0, 647);
                    }
                    else if (c == 2)
                    {
                        x = 839;
                        y = random.Next(0, 647);
                        x2 = 1;
                        y2 = random.Next(0, 647);
                    }
                    else
                    {
                        y = 647;
                        x = random.Next(0, 839);
                        x2 = random.Next(0, 839);
                        y2 = 1;
                    }

                    /* Asteroid object creation using random origin and destination 
                    coordinates */ 
                    asteroids.Add(new Asteroid(new Point(x, y), 2, 30, 50, 
                        new Point(x, y), new Point(x2, y2), random));
                }
            }

            //===================================
            //          POWERUP SPAWNER
            //===================================
            //Spawn condition
            if (enemiesKilled > 5)
            {
                enemiesKilled = 0;
                //PowerUp object creation.
                powerUps.Add(new PowerUp(random, powerSpawn));
            }

            //===================================
            //        GRAVITY WELL SPAWNER
            //===================================
            //Spawn condition.
            if (gravityWells.Count < level.gravityWellNumber)
            {
                //Number of wells to be spawned
                int a;
                a = level.gravityWellNumber - gravityWells.Count;

                for (int i = 0; i < a; i++)
                {
                    //Random edge and direction selection.
                    int x, y, c;
                    int x2, y2;
                    c = random.Next(0, 4);

                    if (c == 0)
                    {
                        x = random.Next(0, 839);
                        y = 1;
                        x2 = random.Next(0, 839);
                        y2 = 647;
                    }
                    else if (c == 1)
                    {
                        y = random.Next(0, 647);
                        x = 1;
                        x2 = 839;
                        y2 = random.Next(0, 647);
                    }
                    else if (c == 2)
                    {
                        x = 839;
                        y = random.Next(0, 647);
                        x2 = 1;
                        y2 = random.Next(0, 647);
                    }
                    else
                    {
                        y = 647;
                        x = random.Next(0, 839);
                        x2 = random.Next(0, 839);
                        y2 = 1;
                    }

                    //Well object creation using origin and direction coordinates.
                    gravityWells.Add(new GravityWell(new Point(x, y), 2.5, new Point(x2, y2), 2));
                }
            }
        }
        private void objectUpdater()
        {
            //===================================
            //          POSITION UPDATING
            //===================================

            //Player position update.
            player1.adjustPosition(mouseX, mouseY);

            //Enemy position update.
            foreach (Enemy enemy in enemies)
            {
                //Uses player position as destination.
                enemy.adjustPosition(player1.centreX, player1.centreY);
            }

            //Projectile position update.
            foreach (Projectile shot in projectiles)
            {
                shot.adjustPosition();
            }

            // Asteroid position update.
            foreach (Asteroid rock in asteroids)
            {
                if (rock.xVertices != null)
                {
                    rock.adjustPosition();
                }
            }

            //Blast/Explosion phase update.
            foreach (Blast explosion in blasts)
            {
                explosion.update();
            }

            //Powerup time-to-live update.
            foreach (PowerUp power in powerUps)
            {
                power.TTL--;
            }

            //GravityWell position update.
            foreach (GravityWell well in gravityWells)
            {
                well.adjustPosition();
            }
        }
        private void objectCleaner()
        {
            //===================================
            //          OBJECT REMOVAL
            //===================================

            //Enemy removal
            if (enemies.Count != 0)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    //No remaining health condition
                    if (enemies[i].health <= 0)
                    {
                        //Enemy death tracker used in for powerUp spawning.
                        enemiesKilled++;
                        //Level property used to trigger level change.
                        level.enemiesRemaining--;
                        //tracker of the enemy centre point. User for powerUp spawning.
                        powerSpawn = enemies[i].centre;
                        //Object removed.
                        enemies.RemoveAt(i);
                        //Loop variable decreased by 1, to account for object removal.
                        i--;
                    }
                }
            }

            //Projectile removal.
            if (projectiles.Count != 0)
            {
                for (int i = 0; i < projectiles.Count; i++)
                {
                    //Time-to-live expiration condition.
                    if (projectiles[i].TTL == 0)
                    {
                        projectiles.RemoveAt(i);
                        i--;
                    }
                }
            }

            //Asteroid removal.
            if (asteroids.Count != 0)
            {
                for (int i = 0; i < asteroids.Count; i++)
                {
                    //Move beyond the edge of the screen condition.
                    if (asteroids[i].centre.X + 50 < 0)
                    {
                        asteroids.RemoveAt(i);
                        i--;
                    }
                    else if (asteroids[i].centre.Y + 50 < 0)
                    {
                        asteroids.RemoveAt(i);
                        i--;
                    }
                    else if (asteroids[i].centre.X - 50 > 839)
                    {
                        asteroids.RemoveAt(i);
                        i--;
                    }
                    else if (asteroids[i].centre.Y - 50 > 647)
                    {
                        asteroids.RemoveAt(i);
                        i--;
                    }
                }
            }

            //PowerUp removal.
            if (powerUps.Count != 0)
            {
                for (int i = 0; i < powerUps.Count; i++)
                {
                    //PickedUp state and time-to-live expiration condition.
                    if ((powerUps[i].pickedUp) || (powerUps[i].TTL == 0)) 
                    {
                        powerUps.RemoveAt(i);
                        i--;
                    }
                }
            }

            //Blast/Explostion removal.
            if (blasts.Count != 0)
            {
                for (int i = 0; i < blasts.Count; i++)
                {
                    //Phase/Time-to-live expiration condition.
                    if (blasts[i].phase == 10)
                    {
                        blasts.RemoveAt(i);
                        i--;
                    }
                }
            }
            
            //GravityWell removal.
            if (gravityWells.Count != 0)
            {
                for (int i = 0; i < gravityWells.Count; i++)
                {
                    //Move beyond the edge of the screen condition.
                    if (gravityWells[i].coordinates.X + 50 < 0)
                    {
                        gravityWells.RemoveAt(i);
                        i--;
                    }
                    else if (gravityWells[i].coordinates.Y + 50 < 0)
                    {
                        gravityWells.RemoveAt(i);
                        i--;
                    }
                    else if (gravityWells[i].coordinates.X - 50 > 839)
                    {
                        gravityWells.RemoveAt(i);
                        i--;
                    }
                    else if (gravityWells[i].coordinates.Y - 50 > 647)
                    {
                        gravityWells.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
        private void collisionDetection()
        {
            //===================================
            //         COLLISION DETECTION
            //===================================

            //Variables to be used in multiple loops.
            int x, y, x2, y2;
            int radius1, radius2;
            bool collision = false;

            //------------ || Collisions between player1 and enemies || -------------
            //Coordinate and radius values of the player are fetched.
            x = player1.centreX;
            y = player1.centreY;
            radius1 = player1.radius;

            //A collision test is caried on every single enemy within the enemies list.
            foreach (Enemy enemy in enemies)
            {
                //Enemy coordinates and radius are fetched.
                x2 = enemy.centre.X;
                y2 = enemy.centre.Y;
                radius2 = enemy.radius;

                /* A test checking whether the distance between the centre of the two objects 
                (player and enemy) is less than their combined radiuses. If so it means that 
                the objects have collided. */
                if (Math.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2)) < (radius1 + radius2))
                {
                    /* The collision method is called with appropriate attributes indicating 
                    between who the collision is taking place, and their index values within 
                    their respective lists. */
                    collisionBehaviour(0, enemies.IndexOf(enemy), 0);
                }
            }

            //------------ || Collisions between enemies and projectiles || ---------
            //A collision test is caried on every single enemy within the enemies list.
            foreach (Enemy enemy in enemies)
            {
                x2 = enemy.centre.X;
                y2 = enemy.centre.Y;
                radius2 = enemy.radius;
                radius1 = 5;

                //Enemy within the enemies list is checked against every singly projectile within the projectiles list.
                foreach (Projectile shot in projectiles)
                {
                    x = shot.coordinates.X;
                    y = shot.coordinates.Y;

                    //Collision test.
                    if (Math.Sqrt((x2 - x) * (x2 - x) + (y2 - y) * (y2 - y)) < (radius1 + radius2))
                    {
                        //A call to execute the behaviour of the collision.
                        collisionBehaviour(1, enemies.IndexOf(enemy), projectiles.IndexOf(shot));
                    }
                }
            }

            //------------ || Collisions between asteroids and enemies || -----------
            //A collision test is caried on every single enemy within the enemies list.
            foreach (Enemy enemy in enemies)
            {
                x2 = enemy.centre.X;
                y2 = enemy.centre.Y;
                radius2 = enemy.radius;

                //Enemy within the enemies list is checked against every singly asteroid within the asteroids list.
                foreach (Asteroid roid in asteroids)
                {
                    x = roid.centre.X;
                    y = roid.centre.Y;
                    radius1 = roid.maxRadius;

                    //Initial collision test.
                    if (Math.Sqrt((x2 - x) * (x2 - x) + (y2 - y) * (y2 - y)) < (radius1 + radius2))
                    {
                        //If the first test has been returned as true a seperate collision test is then performed on the 3 vertices of the enemy.
                        for (int q = 0; q < 3; q++)
                        {
                            collision = false;

                            //The pointInPolyTest() is called providing one of the points of the enemy and the asteroid point array as attributes. 
                            //The test returns a true if the point of the enemy has been found inside the polygon(asteroid).
                            collision = pointInPolyTest(enemy.vertices[q], roid.vertices);

                            if (collision == true)
                            {
                                q = 4; //Stops further collision tests.
                                collisionBehaviour(2, enemies.IndexOf(enemy), asteroids.IndexOf(roid)); //Calls collisionBehaviour method with
                                //apptopriate indexes and type.
                            }
                        }
                    }
                }
            }

            //------------ || Collisions between projectiles and asteroids || -------
            //A collision test is caried on every single projectile within the projectiles list.
            foreach (Projectile shot in projectiles)
            {
                x2 = shot.coordinates.X;
                y2 = shot.coordinates.Y;
                radius2 = 5;

                //Shot within the projectiles list is checked against every singly roid within the asteroids list.
                foreach (Asteroid roid in asteroids)
                {
                    x = roid.centre.X;
                    y = roid.centre.Y;
                    radius1 = roid.maxRadius;

                    //Initital collision test using spherical areas.
                    if (Math.Sqrt((x2 - x) * (x2 - x) + (y2 - y) * (y2 - y)) < (radius1 + radius2))
                    {
                        //If the initial test is true, it means that the projectile is within the collision distance of the asteroid.
                        //The tests to follows will check which part of the asteroid has been impacted.

                        //Declaring variables to store data for collision testing.
                        collision = false;
                        Point p1 = shot.coordinates;
                        Point[] polygon = new Point[3]; //Asteroids consist of 6 vertices and has been divided into 3 'impact areas'
                        //which can be hit by a projectile. To test each area individualy a 3 point array is used to create a triangle
                        //using 3 points from the roid.vertices.

                        //A triangle is created using asteroid vertices coordiantes.
                        polygon[0] = roid.vertices[0];
                        polygon[1] = roid.vertices[1];
                        polygon[2] = roid.vertices[2];
                        //The pointInPolyTest is called supplrying the shot coordinates and the newly created triangle.
                        //The result of the test is stored within the collision variable.
                        collision = pointInPolyTest(p1, polygon);

                        //Checks whether the first of the three areas have been hit by a projectiler.
                        if (collision)
                        {
                            //Asteroid index is coupled with the index of the middle vertice of the triangle identified as being hit
                            //within the same integer variable.
                            //This is done by multiplying the index value by 10 to create free space for a single digit number
                            //to be added at the end of the integer. This number is later retrieved using the modulus 10 (% 10)
                            collisionBehaviour(3, projectiles.IndexOf(shot), asteroids.IndexOf(roid) * 10 + 1);
                        }

                        //The 2nd area of the asteroid is tested.
                        polygon[0] = roid.vertices[2];
                        polygon[1] = roid.vertices[3];
                        polygon[2] = roid.vertices[4];
                        collision = pointInPolyTest(p1, polygon);

                        if (collision)
                        {
                            collisionBehaviour(3, projectiles.IndexOf(shot), asteroids.IndexOf(roid) * 10 + 3);
                        }

                        //The 3rd area is tested
                        polygon[0] = roid.vertices[4];
                        polygon[1] = roid.vertices[5];
                        polygon[2] = roid.vertices[0];
                        collision = pointInPolyTest(p1, polygon);

                        if (collision)
                        {
                            collisionBehaviour(3, projectiles.IndexOf(shot), asteroids.IndexOf(roid) * 10 + 5);
                        }
                    }
                }
            }

            //------------ || Collisions between powerups and the player || ---------
            //A collision test is caried on every single powerup within the powerUps list.
            foreach (PowerUp power in powerUps)
            {
                collision = false;
                Point p1 = power.coord;

                //Collision test between a tirangle(player) and a point(center of the powerup)
                collision = pointInPolyTest(p1, player1.vertices);
                if (collision)
                {
                    //Effects of the powerup applied
                    collisionBehaviour(4, 0, powerUps.IndexOf(power));
                }
            }

            //------------ || Collisions between asteroids and enemies || -----------
            //A collision test is caried on every single enemy within the enemies list.
            foreach (Enemy enemy in enemies)
            {
                /* The collision detection process is identical to projectile - asteroid
                detection. */
                x = enemy.centre.X;
                y = enemy.centre.Y;
                radius1 = enemy.radius;

                //Enemy within the enemies list is checked against every singly asteroid within the asteroids list.
                foreach (Asteroid roid in asteroids)
                {
                    x2 = roid.centre.X;
                    y2 = roid.centre.Y;
                    radius2 = roid.maxRadius;

                    if (Math.Sqrt((x2 - x) * (x2 - x) + (y2 - y) * (y2 - y)) < (radius1 + radius2))
                    {
                        collision = false;
                        Point[] polygon = new Point[3];
                        Point p1 = enemy.centre;
                        polygon[0] = roid.vertices[0];
                        polygon[1] = roid.vertices[1];
                        polygon[2] = roid.vertices[2];
                        collision = pointInPolyTest(p1, polygon);

                        if (collision)
                        {
                            collisionBehaviour(5, enemies.IndexOf(enemy), asteroids.IndexOf(roid) * 10 + 1);
                        }

                        polygon[0] = roid.vertices[2];
                        polygon[1] = roid.vertices[3];
                        polygon[2] = roid.vertices[4];
                        collision = pointInPolyTest(p1, polygon);

                        if (collision)
                        {
                            collisionBehaviour(5, enemies.IndexOf(enemy), asteroids.IndexOf(roid) * 10 + 3);
                        }

                        polygon[0] = roid.vertices[4];
                        polygon[1] = roid.vertices[5];
                        polygon[2] = roid.vertices[0];
                        collision = pointInPolyTest(p1, polygon);

                        if (collision)
                        {
                            collisionBehaviour(5, enemies.IndexOf(enemy), asteroids.IndexOf(roid) * 10 + 5);
                        }
                    }
                }
            }

            //------------ || Collisions between gravityWells and Player1 || --------
            x = player1.centreX;
            y = player1.centreY;
            radius1 = 0;

            //Player1 is checked against every single gravity well within the gravityWells list.
            foreach (GravityWell well in gravityWells)
            {
                x2 = well.coordinates.X + 85;
                y2 = well.coordinates.Y + 85;
                radius2 = 85;
                if (Math.Sqrt((x2 - x) * (x2 - x) + (y2 - y) * (y2 - y)) < (radius1 + radius2))
                {
                    collisionBehaviour(6, 0, gravityWells.IndexOf(well));
                }
            }

            //------------ || Collisions between gravityWells and enemies || --------
            foreach (Enemy enemy in enemies)
            {
                x = enemy.centre.X;
                y = enemy.centre.Y;
                radius1 = 0;
                foreach (GravityWell well in gravityWells)
                {
                    x2 = well.coordinates.X + 85;
                    y2 = well.coordinates.Y + 85;
                    radius2 = 85;
                    if (Math.Sqrt((x2 - x) * (x2 - x) + (y2 - y) * (y2 - y)) < (radius1 + radius2))
                    {
                        collisionBehaviour(8, enemies.IndexOf(enemy), gravityWells.IndexOf(well));
                    }
                }
            }
            //------------ || Collisions between asteroids and Player1 || -----------
            foreach (Asteroid roid in asteroids)
            {
                //Every asteroid is tested against all 3 of the player1 vertices.
                for (int i = 0; i < 3; i++)
                {
                    collision = pointInPolyTest(player1.vertices[i], roid.vertices);

                    if (collision)
                    {
                        collisionBehaviour(8, 0, asteroids.IndexOf(roid));
                        break;
                    }
                }
            }
        }
        public bool pointInPolyTest(Point p, Point[] polygon)
        {
            //===================================
            //       POINT IN POLYGON TEST
            //===================================
            
            Point p1, p2;
            bool inside = false;

            //If vertice count is less than 3 the polygon does not have an area and false is returned.
            if (polygon.Length < 3)
            {
                return inside;
            }

            //The last point within the array is selected.
            Point oldPoint = new Point(polygon[polygon.Length - 1].X, polygon[polygon.Length - 1].Y);

            //The polygon is tested line by line.
            for (int i = 0; i < polygon.Length; i++)
            {
                //The first point within the array is selected.
                Point newPoint = new Point(polygon[i].X, polygon[i].Y);

                /* Test to allow to determine the positive x and y distances between the 2
                points of the line */
                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                /* The first half of the test checks whether the point being checked for collision 
                is within the bounds of the x coordinate range determined by the line. The second
                part calculates the area of 2 rectangles (p.Y-p1.Y times p2.X - p1.X  and   p2.Y-p1.Y
                times p.X - p1.X) comparing their sizes against each other to determine on which side
                of the line the point resides.*/
                if ((newPoint.X < p.X) == (p.X <= oldPoint.X) && 
                   ((long)p.Y - (long)p1.Y) * (long)(p2.X - p1.X) < ((long)p2.Y - (long)p1.Y) * (long)(p.X - p1.X))
                {
                    inside = !inside;
                }

                //The line being testes is shifted along the array.
                oldPoint = newPoint;
            }

            //The result of the test is returned.
            return inside;
        }
        public void collisionBehaviour(int collisionType, int index1, int index2)
        {
            //===================================
            //       BEHAVIOUR SWITCH
            //===================================
            /*  INDEX
                0 - Enemy(i1)/Player(i2)
                1 - Enemy(i1)/Projectile(i2)
                2 - Enemy(i1)/Asteroid(i2)
                3 - Projectile(i1)/Asteroid(i2)
                4 - Player(i1)/PowerUp(i2)
                5 - Enemy(i1)/Asteroid(i2)
                6 - Player(i1)/GravityWell(i2)
                7 - Enemy(i1)/GravityWell(i2)
                8 - Player(i1)/Asteroid(i2) */

            //Used inbetween conditionals.
            double vx;
            double vy;
            double mag;
            int x, y;

            switch (collisionType)
            {
                case 0: //0 - Enemy(i1)/Player(i2)
                    //Reduces either shield or health property values.
                    if (player1.shield > 0)
                    {
                        player1.shield--;
                    }
                    else
                    {
                        player1.health -= 1;
                    }

                    //Kill the enmy.
                    enemies[index1].health = 0;
                    //Add an explosion at where the enemy is present.
                    blasts.Add(new Blast(enemies[index1].centre, 6));
                    break;
                case 1: //1 - Enemy(i1)/Projectile(i2)
                    //Apply projectile damage to enemy.
                    enemies[index1].health -= projectiles[index2].damage;
                    //Expire the projectile.
                    projectiles[index2].TTL = 0;
                    //Add a blast.
                    blasts.Add(new Blast(projectiles[index2].coordinates, 6));
                    break;
                case 2: //2 - Enemy(i1)/Asteroid(i2)
                    //Destoy the enemy and add a blast.
                    enemies[index1].health = 0;
                    blasts.Add(new Blast(enemies[index1].centre, 6));
                    break;
                case 3: //3 - Projectile(i1)/Asteroid(i2)
                    //Expire the projectile.
                    projectiles[index1].TTL = 0;
                    /* Apply damage to a specific section of the asteroid.
                    Handled by hitTaken()*/
                    asteroids[index2 / 10].hitTaken(index2 % 10, projectiles[index1].coordinates, projectiles[index1].damage);
                    //Add a blast;
                    blasts.Add(new Blast(projectiles[index1].coordinates, 6));
                    break;
                case 4: //4 - Player(i1)/PowerUp(i2)
                    //Remove the powerup.
                    powerUps[index2].pickedUp = true;
                    //Apply an effect based on the powerUp type.
                    if (powerUps[index2].type == 0)
                    {
                        //Green type 0 recovers 10 points of health.
                        if (player1.health + 10 <= prbLife.Maximum)
                        {
                            player1.health += 10;
                        }
                        else
                        {
                            player1.health = prbLife.Maximum;
                        }
                    }
                    else if (powerUps[index2].type == 1)
                    {
                        //Red type 1 increases projectile damage.
                        shotDamageModifier++;
                    }
                    else if (powerUps[index2].type == 2)
                    {
                        //Blue type 2 creates or recovers a shield.
                        if (player1.shield <= 15)
                        {
                            player1.shield += 5;
                        }
                        else
                        {
                            player1.shield = 20;
                        }
                    }
                    break;
                case 5: //5 - Enemy(i1)/Asteroid(i2)
                    //Destroy the enemy.
                    enemies[index1].health = 0;
                    //Apply damage to the asteroid.
                    asteroids[index2 / 10].hitTaken(index2 % 10, enemies[index1].centre, 1);
                    break;
                case 6: //6 - Player(i1)/GravityWell(i2)
                    // Creates vectors pointing from the player to the centre of the well.
                    vx = player1.centreX - (gravityWells[index2].coordinates.X + 85);
                    vy = player1.centreY - (gravityWells[index2].coordinates.Y + 85);

                    //Normalises the vector (reduces its length to 1)
                    mag = Math.Sqrt(vx * vx + vy * vy);
                    vx /= mag;
                    vy /= mag;

                    /* Moves the centre of the player along the vector by the specified distance
                    indicated by the the pullStrength of the well. */
                    x = (int)(player1.centreX - vx * gravityWells[index2].pullStrength);
                    y = (int)(player1.centreY - vy * gravityWells[index2].pullStrength);
                    if (x > 0 && y > 0) //player is not moved if the x, or y values are invalid (<0)
                    {
                        player1.centreX = x;
                        player1.centreY = y;
                    }
                    break;
                case 7: //7 - Enemy(i1)/GravityWell(i2)
                    //The same GravityWell collision behaviour as with player1.

                    vx = enemies[index1].centre.X - (gravityWells[index2].coordinates.X + 85);
                    vy = enemies[index1].centre.Y - (gravityWells[index2].coordinates.Y + 85);

                    mag = Math.Sqrt(vx * vx + vy * vy); // length
                    vx /= mag;
                    vy /= mag;

                    x = (int)(enemies[index1].centre.X - vx * gravityWells[index2].pullStrength);
                    y = (int)(enemies[index1].centre.Y - vy * gravityWells[index2].pullStrength);

                    if (x > 0 && y > 0)
                    {
                        Point point = new Point(x, y);
                        enemies[index1].centre = point;
                    }
                    break;
                case 8: //8 - Player(i1)/Asteroid(i2)
                    //Checks if the player immunity against consecutive asteroid strikes has ended.
                    if (player1.roidImmunity == 0)
                    {
                        //Removes the shield and applies health damage.
                        player1.shield = 0;
                        player1.health -= 10;
                        //1 second immunity is given.
                        player1.roidImmunity = 60;
                    }
                    break;
            }
        }
        private void pnlPaper_Paint(object sender, PaintEventArgs e)
        {
            //===================================
            //       OBJECT DRAWING
            //===================================

            //A graphics object is created.
            Graphics paper;
            paper = pnlPaper.CreateGraphics();
            //Double buffering is eneabled to reduce screen flickering after the form is redrawn.
            this.DoubleBuffered = true;
            //Custom effect that has been currently taken out.
            //paper.SmoothingMode = SmoothingMode.AntiAlias;

            //Pens to be used in the drawing of objects.
            Pen blackPen = new Pen(Color.Black);
            Pen redPen = new Pen(Color.Red);
            Pen grayPen = new Pen(Color.DarkGray);
            SolidBrush whiteBrush = new SolidBrush(Color.White);

            //Draw Player if the necessary vertices to do so are not null.
            if (player1.vertices != null)
            {
                //A polygon is drawn using player vertices.
                paper.DrawPolygon(redPen, player1.vertices);
                //If the player has shield (>0) it is drawn.
                if (player1.shield != 0)
                {
                    Pen bluePen = new Pen(Color.Blue);
                    paper.DrawEllipse(bluePen, player1.shieldCoord.X, player1.shieldCoord.Y, 50, 50);
                }
            }

            //Draw Enemies if their count is not zero.
            if (enemies.Count != 0)
            {
                foreach (Enemy attacker in enemies)
                {
                    paper.DrawPolygon(blackPen, attacker.vertices);
                }
            }

            //Draw Projctiles(shots) if their count is not zero.
            if (projectiles.Count != 0)
            {
                foreach (Projectile shot in projectiles)
                {
                    Pen shotPen = new Pen(shot.color);
                    paper.DrawEllipse(shotPen, shot.coordinates.X, shot.coordinates.Y, 2, 2);
                }
            }

            //Draw Asteroids if their count is not zero.
            if (asteroids.Count != 0)
            {
                foreach (Asteroid asteroid in asteroids)
                {
                    paper.DrawPolygon(blackPen, asteroid.vertices);
                    //Asteroid centre is not shown. Has been used for testing purposes.
                    //paper.DrawEllipse(blackPen, asteroid.centre.X, asteroid.centre.Y, 5, 5);
                }
            }

            //Draw PowerUps if their count is not zero.
            if (powerUps.Count != 0)
            {
                foreach (PowerUp power in powerUps)
                {
                    //Custom pen color based on the powerUp type.
                    Pen powerPen = new Pen(power.color);
                    paper.DrawPolygon(powerPen, power.vertices);
                }
            }

            //Draw a blast(explosion) if their count is not zero.
            if (blasts.Count != 0)
            {
                foreach (Blast explosion in blasts)
                {
                    /* Explosions have 2 stages, execution of which is controlled by the phase value. 
                    If the phase is below 5 a white solid circle is drawn, otherwise a hollow black 
                    edged circle is drawn. */
                    if (explosion.phase < 5)
                    {
                        paper.FillEllipse(whiteBrush, explosion.centre.X - 15, explosion.centre.Y - 15, explosion.radius, explosion.radius);
                    }
                    else
                    {
                        paper.DrawEllipse(blackPen, explosion.centre.X - 15, explosion.centre.Y - 15, explosion.radius, explosion.radius);
                    }
                }
            }

            //Draw gravity wells if their count is not zero.
            if (gravityWells.Count != 0)
            {
                foreach (GravityWell well in gravityWells)
                {
                    paper.DrawEllipse(blackPen, well.coordinates.X, well.coordinates.Y, 170, 170);
                    paper.DrawEllipse(grayPen, well.pulseCoordinates.X, well.pulseCoordinates.Y, well.pulseRadius, well.pulseRadius);
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //===================================
            //       KEY TRACKING
            //===================================
            //If statements tracking the state of all the relevant keys on the keyboard.
            if (e.KeyCode == Keys.W)
            {
                player1.keyW = true; ;
            }

            if (e.KeyCode == Keys.A)
            {
                player1.keyA = true;
            }

            if (e.KeyCode == Keys.S)
            {
                player1.keyS = true;
            }

            if (e.KeyCode == Keys.D)
            {
                player1.keyD = true;
            }

            //Pauses and unpauses the game if the level transition is off.
            if (tmrLevelDelay.Enabled == false && e.KeyCode == Keys.P)
            {
                if (tmrUpdate.Enabled)
                {
                    tmrUpdate.Enabled = false;
                }
                else
                {
                    tmrUpdate.Enabled = true;
                }
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //If statements tracking the state of all the relevant keys on the keyboard.
            if (e.KeyCode == Keys.W)
            {
                player1.keyW = false; ;
            }

            if (e.KeyCode == Keys.A)
            {
                player1.keyA = false;
            }

            if (e.KeyCode == Keys.S)
            {
                player1.keyS = false;
            }

            if (e.KeyCode == Keys.D)
            {
                player1.keyD = false;
            }
        }
        private void pnlPaper_MouseMove(object sender, MouseEventArgs e)
        {
            //===================================
            //       MOUSE TRACKING
            //===================================
            //Changes in the mouse coordinates are reflected in the global mouseX and mouseY values.
            mouseX = e.X;
            mouseY = e.Y;
        }
        private void pnlPaper_MouseDown(object sender, MouseEventArgs e)
        {
            //Tracks the state of the mouse button.
            mouseDown = true;
        }
        private void pnlPaper_MouseUp(object sender, MouseEventArgs e)
        {
            //Tracks the state fo the mouse button.
            mouseDown = false;
        }
        private void tmrLevelDelay_Tick(object sender, EventArgs e)
        {
            //===================================
            //       LEVEL CONTROL
            //===================================
            //Stops the time delay after the level transition period has passed.
            tmrLevelDelay.Enabled = false;
            //Triggers the level change.
            levelChange();
        }
        private void levelChange()
        {
            //All the lists are cleared to prepare them for the next level.
            enemies.Clear();
            projectiles.Clear();
            asteroids.Clear();
            blasts.Clear();
            powerUps.Clear();
            gravityWells.Clear();
            //Hides the transitional level label.
            lblLevel.Visible = false;
            //Triggers level value change.
            level.levelChange();
            //Unpauses the game.
            tmrUpdate.Enabled = true;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Closes the whole of the application (including the StartMenu form/s)
            Application.Exit();
        }
    }
}
