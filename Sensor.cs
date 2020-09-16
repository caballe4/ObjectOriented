//Author: Anthony Caballero
//Milkowski
//CPSC 3200
//Due: 5/8/2020

//Implementation of the sensor class. Allows for multiple sensors to created
//or just one sensor to navigate the grid. 

//Interface Invariants:
//Parameters are either protected or private and cannot be accessed. If there is
//an class that is using Sensor, it will be able to have access through inheritence. 
using System;
using System.Threading;
using static p3.Robot;

namespace p3
{
    public class Sensor
    {
        private const uint DEFAULT_CAPACITY = 1000;
        private const int TWO_SEC_IN_MS = 2000;
        private const uint THRESHOLD = 100;
        private const uint row = 0;
        private const uint column = 1;
        private const uint VALID = 1;
        private const uint MOVE = 1;
        private bool state;
        private uint battery;
        private uint discharge;
        protected SensorDirection sdir;

        public enum SensorDirection
        {
            front,
            back,
            left,
            right
        }
        

        //Constructor
        public Sensor()
        {
            battery = DEFAULT_CAPACITY;
            Random rnd = new Random();
            discharge = 1;//(uint)rnd.Next(1,100);
            state = true;
            sdir = 0;
        }
        public Sensor(int dir)
        {
            battery = DEFAULT_CAPACITY;
            Random rnd = new Random();
            discharge = (uint)rnd.Next(1, 100);
            state = true;
            sdir = (SensorDirection)dir;
        }
        public Sensor(Sensor prevSensor)
		{
            battery = prevSensor.battery;
            discharge = prevSensor.discharge;
            state = prevSensor.state;
		}

        //Pre: input the current position, the working grid and current robot
        //Post: updated validation if there is a valid place to move next.
        public bool isValid(int[] pos, int[,]grid, Robot r)
		{
            chargeBattery();
            int curr_row = pos[row];
            int curr_column = pos[column];
            if (curr_row - 1 < 0 || curr_column -1 < 0)
                return false;

            if (state && battery <= THRESHOLD)
            {
                state = false;
                return state;
            }
            else if (state && battery > THRESHOLD)
            {
                if (sdir == (SensorDirection)0)
                {

                    if (grid[curr_row - MOVE, curr_column] != VALID)
                        return false;
                    //sdir = (SensorDirection)2;
                    //if (direction == robotDirection.Forward)
                    else
                    {
                        battery -= discharge;
                        r.changeDirection(robotDirection.Forward);
                        return state;
                    }
                    
                }
                if (sdir == (SensorDirection)2)
                {

                    if (grid[curr_row, curr_column - MOVE] != VALID)
                        return false;
                    //sdir = (SensorDirection)3;
                    else
                    {
                        battery -= discharge;
                        r.changeDirection(robotDirection.Left);
                        return state;
                    }
                }
                if (sdir == (SensorDirection)3)
                {
                    if (grid[curr_row, curr_column + MOVE] != VALID)
                        return false;
                        //sdir = (SensorDirection)4;
                    {
                        battery -= discharge;
                        r.changeDirection(robotDirection.Right);
                        return state;
                    }
                }
                else if (grid[curr_row + MOVE, curr_column] == VALID)
                {
                    battery -= discharge;
                    r.changeDirection(robotDirection.Backward);
                    return state;
                }

                else
                    return !state;
			}
			else
			{
                state = false;
                return state;
            }
            //return false;
		}

        //Pre: next valid direction to move in the grid
        //Post: updated direction for the sensor to move with the robot.
        public void changeDirection(robotDirection newDir)
        {
            
            sdir = (SensorDirection)newDir;
        }

        public uint chargeBattery()
		{
            //Thread.Sleep(TWO_SEC_IN_MS);
            state = true;
            return battery = DEFAULT_CAPACITY;
		}
        public bool getState()
		{
            return state;
		}
    }
}