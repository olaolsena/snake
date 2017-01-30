using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace snake
{
    public partial class Form1 : Form
    {
        public snake snake;
        public food food;
        public Form1()
        {
            InitializeComponent();
            snake = new snake();
            food = new food();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            snake.drawMe(e.Graphics);
            food.drawMe(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!snake.alive) {
                timer1.Enabled = false;
                MessageBox.Show("DEAD SNAKE!! Score :" + snake.getScore());
                snake = new snake();
                food = new food();
                timer1.Enabled = true;
            }
            else
            {
                if (snake.ate)
                {
                    snake.ate = false;
                    food = new food();
                }
                snake.move();
                snake.eat(food.pos);
                Refresh();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up: snake.endreRetning(0); break;
                case Keys.Right: snake.endreRetning(1); break;
                case Keys.Down: snake.endreRetning(2); break;
                case Keys.Left: snake.endreRetning(3); break;
                case Keys.P: timer1.Enabled = false; break;
                case Keys.R: timer1.Enabled = true; break;
            }
        }
        
    }
    public class snake
    {
        public ArrayList snakePos = new ArrayList();
        public int retning, retningY, retningX;
        public Boolean alive = true;
        public Boolean ate = false;
        Random r;
        public snake()
        {
            retning = 1;
            retningY = 0;
            retningX = 1;
            snakePos.Add(new Point(6, 0));
        }
        public void drawMe(Graphics g)
        {
            SolidBrush b = new SolidBrush(Color.Green);
            for (int i = 0; i < snakePos.Count; i++)
            {
                Point point = (Point)snakePos[i];
                g.FillEllipse(b,point.X*10, point.Y*10, 10, 10);
            }
        }

        public void move()
        {
            Point nyP = lovligPoint((Point)snakePos[0]);
            Point p = (Point)snakePos[0];
            snakePos.Insert(0, nyP);
            if (snakePos.Count > 1)
            {
                snakePos.RemoveAt(snakePos.Count - 1);
                for (int i = 0; i < snakePos.Count; i++)
                {
                    Point pp = (Point)snakePos[i];
                    if (i > 0 && nyP == pp)
                    {
                        alive = false;
                    }
                }
            }
            
        }


        public Point lovligPoint(Point p)
        {
            Point nyP = new Point(p.X + retningX, p.Y + retningY);
            alive = (nyP.X >= 0 && nyP.X < 50 && nyP.Y >= 0 && nyP.Y < 50);
            return nyP;
        }

        public void endreRetning(int r)
        {
            if (r % 2 == 0 && retning % 2 != 0 || r % 2 != 0 && retning % 2 == 0)
            {
                retning = r;
                switch (r)
                {
                    case 0: retningY = -1; retningX = 0; break;
                    case 1: retningY = 0; retningX = 1; break;
                    case 2: retningY = +1; retningX = 0; break;
                    case 3: retningY = 0; retningX = -1; break;
                }
            }
        }
        public int getScore()
        {
            return snakePos.Count - 1;
        }
        public void eat(Point food)
        {
            Point p = (Point)snakePos[0];
            if (food.X == p.X && food.Y == p.Y)
            {
                Console.WriteLine("nom nom");
                snakePos.Add(new Point(food.X, food.Y));
                ate = true;
            }
        }
    }

    public class food
    {
        Random r;
        public Point pos;
        public food()
        {
            this.r = new Random();
            pos = new Point(r.Next(0, 40), r.Next(0, 40));
        }
        public void drawMe(Graphics g)
        {
            SolidBrush b = new SolidBrush(Color.Red);
            g.FillEllipse(b, pos.X*10, pos.Y*10, 10, 10);
        }
    }
}
