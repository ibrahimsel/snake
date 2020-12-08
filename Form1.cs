using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


namespace SnakeGame
{
    public partial class Form1 : Form
    {
        readonly Random rand = new Random();
        readonly Label food = new Label();
        readonly Label[] snake = new Label[2376];
        // readonly (int, int)[] placesVisited = new (int, int)[2376];
        List<(int, int)> placesVisited = new List<(int, int)>();

        public int x, xTemp;
        public int y, yTemp;
        public int particleWidth = 10;
        public int particleHeight = 10;
        public int panelWidth = 660;
        public int panelHeight = 360;
        public int headPositionX = 200;  // snake[0] is our snake's head
        public int headPositionY = 200;
        public int snakeVelocity = 10;  // have to assign this to radiobutton
        public int snakeSize = 5;  // The variable to hold how many particles our snake have
        public string snakeDirection = "right";

        public Form1()
        {
            InitializeComponent();
        }

        private void fillTheList()
        {
            for (int i = 0; i < snakeSize; i++)
            {
                placesVisited.Add((i, i + i));
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            //GameLoop.Enabled = false;
            //fillTheList();
            //label1.Text = placesVisited[4].ToString();
            SpawnTheSnake();
            SpawnFoodParticle();
        }

        private Label AddSnakeParticle(int x, int y)
        {
            Label snake = new Label
            {
                Size = new Size(particleWidth, particleHeight),
                Location = new Point(x, y),
                BackColor = Color.Green
            };
            panel1.Controls.Add(snake);
            return snake;
        }

        private void SpawnTheSnake()
        {
            for (int i = 0; i < snakeSize; i++)
            {
                snake[i] = AddSnakeParticle(headPositionX - i * particleWidth, headPositionY);  // We use - (i * particleWidth) to add labels next to the head, then each other
            }
        }

        private void Grow()
        {
            snake[snakeSize] = AddSnakeParticle(snake[snakeSize - 1].Location.X, snake[snakeSize - 1].Location.Y);
            snakeSize++;
        }

        private void SpawnFoodParticle()
        {
            int foodX = rand.Next(0, panelWidth - particleWidth);
            int foodY = rand.Next(0, panelHeight - particleHeight);

            food.Size = new Size(particleWidth, particleHeight);
            food.Location = new Point(foodX + (snakeVelocity - (foodX % snakeVelocity)), foodY + (snakeVelocity - (foodY % snakeVelocity)));  // We need to spawn the food in places which our snake is able to visit...
            food.BackColor = Color.DarkRed;                                                                                                       // So we only produce random numbers which are multiples of our snake's velocity
            panel1.Controls.Add(food);
        }

        private void RemoveFoodParticle()
        {
            panel1.Controls.Remove(food);
        }


        private bool FoodCollision()
        {
            if ((snake[0].Location.X == food.Location.X) && (snake[0].Location.Y == food.Location.Y))
            {
                RemoveFoodParticle();
                SpawnFoodParticle();
                return true;
            }
            return false;
        }

        private bool WallCollision()
        {
            if (snake[0].Location.X < 0 || snake[0].Location.X > 650 || snake[0].Location.Y < 0 || snake[0].Location.Y > 350)
            {
                return true;
            }
            return false;
        }
        private bool SelfCollision()
        {
            for (int i = 0; i < snakeSize; i++)
            {

            }
            return false;
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    snakeDirection = "right";
                    break;

                case Keys.Left:
                    snakeDirection = "left";
                    break;

                case Keys.Up:
                    snakeDirection = "up";
                    break;

                case Keys.Down:
                    snakeDirection = "down";
                    break;

            }
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            if (FoodCollision())
                Grow();

            if (WallCollision())
            {
                GameLoop.Enabled = false;
                MessageBox.Show("Game Over");
            }

            if (SelfCollision())
                ;

            label2.Text = "headX: " + snake[0].Location.X.ToString();
            label3.Text = "headY: " + snake[0].Location.Y.ToString();
            label4.Text = "foodY: " + food.Location.X.ToString();
            label5.Text = "foodY: " + food.Location.Y.ToString();


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
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

    }
}
