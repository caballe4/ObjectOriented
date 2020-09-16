//Author: Anthony Caballero
//Milkowski
//CPSC 3200
//Due: 5/8/2020

//Robot class implementation that allows the robot proper functionality
//to move.

//Interface Invariants:
//Protected or private variables are used and cannot be accessed. Class will
//regardless of where the robot is the sensor.

using System.IO;

namespace p3
{
    public class Robot
    {

        protected const uint ARR_SIZE = 2;
        protected const int START_POSITION = 5;
        protected const uint MOVE = 1;
        protected const int ROW = 0;
        protected const int COLUMN = 1;


        protected int[,] grid = new int[11, 11];
        protected int[] position = new int[2];
        protected int count_moves;
        protected Sensor s;
        protected Actuator a;
        protected robotDirection robotDir;


        public enum robotDirection
        {
            Forward,
            Backward,
            Left,
            Right
        }

        public Robot(string filename)
        {
            s = new Sensor();
            a = new Actuator(0);
            robotDir = 0;
            count_moves = 0;
            getGrid(filename);
        }

        public Robot()
        {
            s = new Sensor();
            a = new Actuator(0);

            for (uint i = 0; i < ARR_SIZE; i++)
                position[i] = START_POSITION;
            robotDir = 0;
            //getGrid();


        }

        public Robot(Robot prevRobot)
        {
            s = prevRobot.s;
            a = prevRobot.a;

            for (uint i = 0; i < ARR_SIZE; i++)
                position[i] = prevRobot.position[i];


        }

        public void getGrid(string filename)
        {

            using (StreamReader sr = new StreamReader(filename))

            {
                string line;
                int row = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    var rowLine = line.Split(" ");
                    for (int i = 0; i < 11; i++)
                        grid[row, i] = int.Parse(rowLine[i]);
                    row++;
                }
            }
        }

        public virtual bool isValid()
		{
            return (s.isValid(position, grid, this));
		}

        public bool move()
		{
            while (isValid())
            {
                if (moveOne())
                    return true;
            }
            return false;
		}

        public virtual bool moveOne()
		{
            if (a.moveForward(position,robotDir))
			{
                return true;
			}
            return false;
		}

        //Pre: Current direction that the robot is facing.
        //Post: Updated direction if the robot needs to rotate. 
        public void changeDirection(robotDirection newDir)
        {
            robotDir = newDir;
        }
        
        public int getRow()
		{
            return position[ROW];
		}

        public int getColumn()
		{
            return position[COLUMN];
		}

        public bool hasPower()
		{
            return a.isPoweredUp();
		}
    }
}
