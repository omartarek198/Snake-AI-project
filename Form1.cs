using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace AI_project
{
    class square
    {
        public Rectangle rectangle;
        public Color color;
        public char occupiedBy = ' ';
    }
    class Snake 
    {
        
        public int size = 4;
        public Color color = Color.Ivory;
        public square snakeHead;
        public int snakeHeadRow;
        public int snakeHeadCol;
    }


    class Obstacle
    {
        public int row, col;
    }
    class Food
    {
        public int row, col;

    }
    class node
    {
        public int r, c;
    }


    public partial class Form1 : Form
    {
     
        Bitmap offset;
        System.Windows.Forms.Timer T = new System.Windows.Forms.Timer();
        World world;
        char dir = ' ';
 
        SearchBehaviour search;
        List<node> Lpath = new List<node>();
        bool IsDFS = false;
        public Form1()
        {
            this.Load += Form1_Load;
            T.Tick += T_Tick;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            T.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                dir = 'U';
                world.MoveSnake('U');

            }
            if (e.KeyCode == Keys.S)
            {
                dir = 'D';
                world.MoveSnake('D');

            }
            if (e.KeyCode == Keys.A)
            {
                dir = 'L';
                world.MoveSnake('L');

            }
            if (e.KeyCode == Keys.D)
            {
                dir = 'R';
                world.MoveSnake('R');

            }

            if (e.KeyCode == Keys.Space)
            {
                SetSearchBehaviour( new DFS(world.rows, world.cols, world.squares));
                PlayGame();

            }
            if (e.KeyCode == Keys.Enter)
            {
                SetSearchBehaviour(new BFS(world.rows, world.cols, world.squares));
                PlayGame(); 

            }
            DrawDubb(CreateGraphics());

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }

        private void T_Tick(object sender, EventArgs e)
        {
          
        }

        public void MakeActors()
        {
            world = new World();

            int X = 0;
            int Y = 0;
            int W = 49;
            world.squares = new square[world.rows, world.cols];
            for (int i = 0; i < world.rows; i++)
            {

                for (int k = 0; k < world.cols; k++)
                {
                    world.squares[i, k] = new square();
                    world.squares[i, k].rectangle = new Rectangle();
                    world.squares[i, k].color = Color.Black;
                    world.squares[i, k].rectangle.X = X;
                    world.squares[i, k].rectangle.Y = Y;
                    world.squares[i, k].rectangle.Width = W;
                    world.squares[i, k].rectangle.Height = W;
                    X += W;
                }
                X = 0;
                Y += W;
            }
            

            world.SpawnSnake();
            world.GenerateObstacles(25);

            world.GenerateFood();

         
            DrawDubb(CreateGraphics());
          
           
        }
        void SetSearchBehaviour(SearchBehaviour obj)
        {
            search = obj;

        }
        
        void PlayGame()
        {


            List<node> path = new List<node>();
     
            path = search.FindPath(world);
            if (path.Count == 0)
            {
                MessageBox.Show("Snake Is Stuck");
            }
           
          
            for (int i = 0; i < path.Count; i++)
            {
                world.squares[path[i].r, path[i].c].color = Color.Pink;

            }
            for (int i = path.Count -1; i > -1; i--)
            {
          
                world.AutoMoveSnake(path[i]);
                world.squares[path[i].r, path[i].c].color = Color.Black;
                DrawDubb(CreateGraphics());
                Thread.Sleep(10);

            }

            world.GenerateFood();
            DrawDubb(CreateGraphics());

           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            offset = new Bitmap(this.Width, this.Height);
            MakeActors();

        }

       private void DrawDubb(Graphics g)
        {

            Graphics g2 = Graphics.FromImage(offset);
            DrawScene(g2);
            g.DrawImage(offset, 0, 0);
        }
        private void DrawScene(Graphics g)
        {
            int count = 0;
            g.Clear(Color.Black);
            SolidBrush brush;

            
            for (int i = 0; i < world.rows; i++)
            {

                for (int k = 0; k < world.cols; k++)
                {
                    if (world.squares[i, k].occupiedBy == ' ')
                    {
                        brush = new SolidBrush(world.squares[i, k].color);
                        g.FillRectangle(brush, world.squares[i, k].rectangle);
                        g.DrawRectangle(Pens.White, world.squares[i, k].rectangle);
                    }
                    if (world.squares[i, k].occupiedBy == 'S')
                    {
                        count++;
                        if (world.snake.snakeHeadCol == k && world.snake.snakeHeadRow == i)
                        {
                            int w = world.squares[i, k].rectangle.Width;
                            brush = new SolidBrush(Color.DarkOrange);
                            g.FillRectangle(brush, world.squares[i, k].rectangle);
                            if (dir == 'U')
                            {
                                g.FillRectangle(Brushes.Black, world.squares[i, k].rectangle.X + w / 4,
                                    world.squares[i, k].rectangle.Y + w / 3, w / 5, w / 5);

                                g.FillRectangle(Brushes.Black, world.squares[i, k].rectangle.X + (w * 3 / 4) - 5,
                              world.squares[i, k].rectangle.Y + w / 3, w / 5, w / 5);
                            }
                            if (dir == 'R')
                            {
                                g.FillRectangle(Brushes.Black,
                                    world.squares[i, k].rectangle.X + w / 4,

                                    world.squares[i, k].rectangle.Y + w / 3
                                    , w / 5, w / 5);

                                g.FillRectangle(Brushes.Black, world.squares[i, k].rectangle.X + (w * 3 / 4) - 5,
                              world.squares[i, k].rectangle.Y + w / 3, w / 5, w / 5);
                            }
                         
                        }
                        else
                        {
                            brush = new SolidBrush(Color.Green);
                            g.FillRectangle(brush, world.squares[i, k].rectangle);
                        }

                    }
                    if (world.squares[i, k].occupiedBy == 'F')
                    {
                        brush = new SolidBrush(Color.Red);
                        g.FillRectangle(brush, world.squares[i, k].rectangle);
                    }
                    if (world.squares[i, k].occupiedBy == 'O')
                    {
                        brush = new SolidBrush(Color.Yellow);
                        g.FillRectangle(brush, world.squares[i, k].rectangle);
                    }
                    g.DrawRectangle(Pens.White, world.squares[i, k].rectangle);
                 
              
                }




            }
        }
    }
}
