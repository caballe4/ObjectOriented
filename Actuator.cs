//Author: Anthony Caballero
//Milkowski
//CPSC 3200
//Due: 5/8/2020

//Actuator class that is connected to the robot.

//Interface Invariants
//All variables are protected and cannot be used publicly by the user.
//Any changes made are done by other classes and changed through inheritence. 

using static p3.Robot;

namespace p3
{
    public class Actuator
    {
        private const int MOVE = 1;
        private const uint row = 0;
        private const uint column = 1;
        private bool state;

        protected Direction direction;

        public enum Direction
        {
            Forward,
            Backward,
            Left,
            Right
        }

        public Actuator(uint dir)
        {
            state = true;
            direction = 0;
            
        }

        public Actuator(Actuator prevAct)
		{
            state = prevAct.state;
            direction = prevAct.direction;
		}
        public bool isPoweredUp()
		{
            return state;
		}

        //Pre: Current robot position and direction that the robot is facing
        //Post: Updated position of the robot in the grid, triggering a move.
        public bool moveForward(int[] pos, robotDirection x)
        {
            if (x == robotDirection.Forward)
            {
                pos[row] -= MOVE;
                return true;
            }
            else if (x == robotDirection.Backward)
			{
                pos[row] += MOVE;
                return true;
			}
            else if (x == robotDirection.Left)
			{
                pos[column] -= MOVE;
                return true;
			}
            else if(x == robotDirection.Right)
			{
                pos[column] += MOVE;
                return true;
            }
            else
                return false;
        }

        //Pre:Current direction that the robot is facing
        //Post:Updated actuator direction to match that of the robot. 
        public void changeDirection(robotDirection newDir)
        {
            direction = (Direction)newDir;
        }
    }
}
