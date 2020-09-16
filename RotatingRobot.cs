//Author: Anthony Caballero
//Milkowski
//CPSC 3200
//Due: 5/8/2020

//Rotating robot implementation. The robot will face the direction that it is
//going. Instead of translating always facing forward, the robot will change
//the direction that it faces in order to navigate the grid.

//Interface Invariants:
//Class is protected and or has private data members. Inherited members from the
//Robot class are also used and can be changed through certain functions.

using static p3.Actuator;

namespace p3
{
    public class RotatingRobot: Robot
    {

        protected Sensor sL;
        protected Sensor sR;
        //protected Sensor sF;

        protected robotDirection facing;
        protected Direction actuatorDirection;
        protected robotDirection nextDirection;
        protected int FRONT = 0;
        protected int BACK = 1;
        protected int LEFT = 2;
        protected int RIGHT = 3;

        public RotatingRobot()
        {
            sL = new Sensor(LEFT);
            sR = new Sensor(RIGHT);
            actuatorDirection = 0;
            facing = 0;
        }

        public RotatingRobot(string filename)
        {
            sL = new Sensor(LEFT);
            sR = new Sensor(RIGHT);
            actuatorDirection = 0;
            facing = 0;
            getGrid(filename);
        }

        public RotatingRobot(RotatingRobot prevRob)
        {
            sL = prevRob.sL;
            sR = prevRob.sR;
            //sF = prevRob.sF;
            a = prevRob.a;
        }

        //Pre:Current direction of sensors and the direction of the robot.
        //Post: Fully updated direction of the robot, actuator, and sensors
        //for the robot to continue moving throughout the grid. 
        public void Rotate(robotDirection change)
        {
            if (facing == robotDirection.Forward)
            {
                if (change == robotDirection.Right)
                {
          
                    facing = robotDirection.Right;
                    s.changeDirection(facing);
                    sR.changeDirection(robotDirection.Backward);
                    sL.changeDirection(robotDirection.Forward);
                    a.changeDirection(facing);
                }
                else if(change == robotDirection.Left){
                    facing = robotDirection.Left;
                    s.changeDirection(facing);
                    sR.changeDirection(robotDirection.Forward);
                    sL.changeDirection(robotDirection.Backward);
                    a.changeDirection(facing);
                }
            }
            else if (facing == robotDirection.Left)
            {
                if (change == robotDirection.Forward)
                {
                    facing = robotDirection.Forward;
                    s.changeDirection(facing);
                    sR.changeDirection(robotDirection.Right);
                    sL.changeDirection(robotDirection.Left);
                    a.changeDirection(facing);
                }
                else if (change == robotDirection.Backward)
                {
                    facing = robotDirection.Backward;
                    s.changeDirection(facing);
                    sR.changeDirection(robotDirection.Left);
                    sL.changeDirection(robotDirection.Right);
                    a.changeDirection(facing);
                }
            }
            else if (facing == robotDirection.Backward)
            {
                if (change == robotDirection.Right)
                {
                    facing = robotDirection.Right;
                    s.changeDirection(facing);
                    sR.changeDirection(robotDirection.Forward);
                    sL.changeDirection(robotDirection.Backward);
                    a.changeDirection(facing);
                }
                else if (change == robotDirection.Left)
                {
                    facing = robotDirection.Left;
                    s.changeDirection(facing);
                    sR.changeDirection(robotDirection.Backward);
                    sL.changeDirection(robotDirection.Forward);
                    a.changeDirection(facing);
                }
            }
            else if (facing == robotDirection.Right)
            {
                if (change == robotDirection.Backward)
                {
                    facing = robotDirection.Backward;
                    s.changeDirection(facing);
                    sR.changeDirection(robotDirection.Left);
                    sL.changeDirection(robotDirection.Right);
                    a.changeDirection(facing);
                }
                else if (change == robotDirection.Forward)
                {
                    facing = robotDirection.Forward;
                    s.changeDirection(facing);
                    sR.changeDirection(robotDirection.Right);
                    sL.changeDirection(robotDirection.Left);
                    a.changeDirection(facing);
                }
            }

        }

        //Special moveOne() function for rotating robot that overrides moveOne()
        //in robot class. 
        public override bool moveOne()
        {
            if (isValid())
            {
                if (nextDirection == facing)
                    return (a.moveForward(position, nextDirection));

                else
                {
                    //nextDirection = robotDir;
                    switch (nextDirection)
                    {
                        case robotDirection.Forward:
                            Rotate(nextDirection);
                            break;
                        //return (a.moveForward(position, robotDir));
                        case robotDirection.Left:
                            Rotate(robotDirection.Left);
                            break;
                        //return (a.moveForward(position, robotDir));
                        case robotDirection.Right:
                            Rotate(robotDirection.Right);
                            break;
                        //return (a.moveForward(position, robotDir));
                        case robotDirection.Backward:
                            Rotate(robotDirection.Backward);
                            break;
                    }
                    return true;
                }
            }
            return false;
        }

        public override bool isValid()
        {
            if (s.isValid(position, grid, this))
            {
                nextDirection = (robotDirection)robotDir;
                return true;
            }
            if (sR.isValid(position, grid, this))
            {
                nextDirection = (robotDirection)robotDir;
                return true;
            }

            if (sL.isValid(position, grid, this))
            {
                nextDirection = (robotDirection)robotDir;
                return true;
            }
            return false;

        }
                
    }
}
