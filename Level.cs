using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceShooter
{
    class Level
    {
        //===================================
        //       CLASS PROPERTIES
        //===================================
        public int levelNumber { get; set; }
        public int enemyNumber { get; set; }
        public int enemySpawn { get; set; }
        public int enemiesRemaining { get; set; }
        public int totalEnemiesSpawned { get; set; }
        public int asteroidNumber { get; set; }
        public int gravityWellNumber { get; set; }

        //===================================
        //       CLASS METHODS
        //===================================
        public Level()
        {
            /* Sets the initial (level 1) values used to maange the spawning
            of the objects. */
            levelNumber = 1;
            enemyNumber = 40;
            enemySpawn = 10;
            enemiesRemaining = enemyNumber;
            asteroidNumber = 2;
            gravityWellNumber = 0;
        }

        public void levelChange()
        {
            //Increments the property values to increase the difficulty.
            levelNumber++;
            enemyNumber += 20;
            enemySpawn += 5;
            enemiesRemaining = enemyNumber;
            totalEnemiesSpawned = 0;
            asteroidNumber += 1;
            //Gravity well number is increased only during levels 3 and 10.
            if (levelNumber == 3 || levelNumber == 10)
            {
                gravityWellNumber += 1; 
            }
        }
    }
}
