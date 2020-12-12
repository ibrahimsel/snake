using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


/* Tasks: 
 * prevent food from spawning on an unavailable point
 */

namespace SnakeGame
{
    public partial class Snake : Form
    {
        readonly Random rand = new Random();
        readonly Label food = new Label();
        readonly Label[] snake = new Label[2376];
        readonly List<Point> unvisitablePoints = new List<Point>();
        readonly List<string> previousDirection = new List<string>(2);
        public int x, xTemp;
        public int y, yTemp;
        public int particleWidth = 10;
        public int particleHeight = 10;
        public int panelWidth = 660;
        public int panelHeight = 360;
        public int headPositionX = 200;  // snake[0] will be our snake's head
        public int headPositionY = 200;
        public int snakeVelocity = 10;  // have to assign this to radiobutton
        public int snakeSize = 5;  // The variable to hold how many particles our snake have
        public string snakeDirection = "right";  // Snake's first direction

        public Snake()
        {
            InitializeComponent();
        }

        private void InitDirectionList()
        {
            previousDirection.Add("right");
            previousDirection.Add("right");
        }


        private void DetectUnvisitablePoints()
        {
            for (int i = 1; i < snakeSize; i++)
            {
                unvisitablePoints.Add(snake[i].Location);
            }
        }


        private void ClearUnvisitablePoints()
        {
            unvisitablePoints.Clear();
        }


        private Point GetSnakeHeadPos()
        {
            return snake[0].Location;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            InitDirectionList();
            SpawnTheSnake();
            SpawnFood();
        }


        private Label AddSnakeParticle(int x, int y)
        {
            Label snakeLabel = new Label
            {
                Size = new Size(particleWidth, particleHeight),
                Location = new Point(x, y),
                BackColor = Color.Green
            };
            panel1.Controls.Add(snakeLabel);
            return snakeLabel;
        }


        private void SpawnTheSnake()
        {
            for (int i = 0; i < snakeSize; i++)
            {
                snake[i] = AddSnakeParticle(headPositionX - i * particleWidth, headPositionY);  // We use - (i * particleWidth) to add labels next to the head, then each other
            }
        }


        private void IncreaseSnakeSize()
        {
            snake[snakeSize] = AddSnakeParticle(snake[snakeSize - 1].Location.X, snake[snakeSize - 1].Location.Y);
            snakeSize++;
        }


        private void SpawnFood()
        {
            int foodX = rand.Next(0, panelWidth - particleWidth);
            int foodY = rand.Next(0, panelHeight - particleHeight);

            food.Size = new Size(particleWidth, particleHeight);
            food.Location = new Point(foodX + (snakeVelocity - (foodX % snakeVelocity)), foodY + (snakeVelocity - (foodY % snakeVelocity)));  // We need to spawn the food in places which our snake is able to visit...         
            food.BackColor = Color.DarkRed;                                                                                                   // So we only produce random numbers which are multiplies of our snake's velocity
            panel1.Controls.Add(food);
        }


        private void RemoveFoodParticle()
        {
            panel1.Controls.Remove(food);
        }


        private bool FoodCollision()
        {
            return (snake[0].Location.X == food.Location.X) && (snake[0].Location.Y == food.Location.Y);
        }


        private bool WallCollision()
        {
            return snake[0].Location.X < 0 || snake[0].Location.X > 650 || snake[0].Location.Y < 0 || snake[0].Location.Y > 350;
        }


        private bool SelfCollision()
        {
            return unvisitablePoints.Contains(GetSnakeHeadPos());
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    if (previousDirection[1] != "left")
                    {
                        snakeDirection = "right";
                        previousDirection.RemoveAt(0);
                        previousDirection.Add("right");
                    }
                    break;

                case Keys.Left:
                    if (previousDirection[1] != "right")
                    {
                        snakeDirection = "left";
                        previousDirection.RemoveAt(0);
                        previousDirection.Add("left");
                    }
                    break;

                case Keys.Up:
                    if (previousDirection[1] != "down")
                    {
                        snakeDirection = "up";
                        previousDirection.RemoveAt(0);
                        previousDirection.Add("up");
                    }
                    break;

                case Keys.Down:
                    if (previousDirection[1] != "up")
                    {
                        snakeDirection = "down";
                        previousDirection.RemoveAt(0);
                        previousDirection.Add("down");
                    }
                    break;
            }
        }


        private void GameLoop_Tick(object sender, EventArgs e)
        {

            switch (snakeDirection)
            {
                case "down":
                    #region                
                    for (int i = 0; i < snakeSize; i++)
                    {
                        x = snake[i].Location.X;
                        y = snake[i].Location.Y;

                        if (i == 0)
                        {
                            snake[i].Location = new Point(x, y + snakeVelocity);
                            xTemp = x;
                            yTemp = y;
                        }

                        else
                        {
                            snake[i].Location = new Point(xTemp, yTemp);
                            xTemp = x;
                            yTemp = y;
                        }
                    }
                    #endregion
                    break;
                case "up":
                    #region                   
                    for (int i = 0; i < snakeSize; i++)
                    {
                        x = snake[i].Location.X;
                        y = snake[i].Location.Y;

                        if (i == 0)
                        {
                            snake[i].Location = new Point(x, y - snakeVelocity);
                            xTemp = x;
                            yTemp = y;

                        }

                        else
                        {
                            snake[i].Location = new Point(xTemp, yTemp);
                            xTemp = x;
                            yTemp = y;

                        }
                    }
                    #endregion
                    break;
                case "left":
                    #region                                       
                    for (int i = 0; i < snakeSize; i++)
                    {
                        x = snake[i].Location.X;
                        y = snake[i].Location.Y;

                        if (i == 0)
                        {
                            snake[i].Location = new Point(x - snakeVelocity, y);
                            xTemp = x;
                            yTemp = y;
                        }

                        else
                        {
                            snake[i].Location = new Point(xTemp, yTemp);
                            xTemp = x;
                            yTemp = y;
                        }
                    }
                    #endregion
                    break;
                case "right":
                    #region
                    for (int i = 0; i < snakeSize; i++)
                    {
                        x = snake[i].Location.X;
                        y = snake[i].Location.Y;

                        if (i == 0)
                        {
                            snake[i].Location = new Point(x + snakeVelocity, y);
                            xTemp = x;
                            yTemp = y;
                        }
                        else
                        {
                            snake[i].Location = new Point(xTemp, yTemp);
                            xTemp = x;
                            yTemp = y;
                        }
                    }
                    #endregion
                    break;
            }

            DetectUnvisitablePoints();

            if (FoodCollision())
            {
                RemoveFoodParticle();
                SpawnFood();
                IncreaseSnakeSize();
            }

            if (WallCollision())
            {
                GameLoop.Enabled = false;
                MessageBox.Show("Snake collided the wall");
            }


            if (SelfCollision())
            {
                GameLoop.Enabled = false;
                MessageBox.Show("Snake collided itself");
            }

            ClearUnvisitablePoints();
        }
    }
}
