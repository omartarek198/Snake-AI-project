using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Threading;
using System.Windows.Forms;

namespace AI_project
{
    class World
    {

        public square[,] squares;
        public Snake snake = new Snake();
        public List<Obstacle> obstacles = new List<Obstacle>();
        public List<Food> foods = new List<Food>();
        public int rows = 15;
        public int cols = 28;
        public int[,] snakecells;
        Random R = new Random();
      public  char direction = ' ';
        public void SpawnSnake()
        {
            snake.snakeHead = squares[8, 13];
            snake.snakeHeadCol = 13;
            snake.snakeHeadRow = 8;
            squares[8, 13].occupiedBy = 'S';
            snakecells = new int[snake.size + 1, 2];
            snakecells[0, 0] = 8;
            snakecells[0, 1] = 13;
            for (int i = 0; i < snake.size; i++)
            {
                squares[snake.snakeHeadRow, snake.snakeHeadCol - 1 - i].occupiedBy = 'S';
                snakecells[i + 1, 0] = snake.snakeHeadRow;
                snakecells[i + 1, 1] = snake.snakeHeadCol - 1 - i;
            }

        }

        public void GrowSnake()
        {
            snake.size++;
            int[,] temp;
            temp = new int[snake.size, 2];
            for (int i = 0; i < snake.size - 1; i++)
            {
                temp[i, 0] = snakecells[i, 0];
                temp[i, 1] = snakecells[i, 1];
            }
            temp[snake.size - 1, 0] = temp[snake.size - 2, 0];
            temp[snake.size - 1, 1] = temp[snake.size - 2, 1] - 1;

            snakecells = new int[snake.size + 1, 2];
            for (int i = 0; i < snake.size; i++)
            {
                snakecells[i, 0] = temp[i, 0];
                snakecells[i, 1] = temp[i, 1];
            }
        }
        public int AutoMoveSnake(node obj)
        {

            char dir = ' ';
            if (obj.r == snake.snakeHeadRow && obj.c > snake.snakeHeadCol)
            {
                dir = 'R';
                MoveSnake(dir);
                return 0;
            }
            if (obj.r == snake.snakeHeadRow && obj.c < snake.snakeHeadCol)
            {
                dir = 'L';
                MoveSnake(dir);
                return 0;
            }
            if (obj.r < snake.snakeHeadRow && obj.c == snake.snakeHeadCol)
            {
                dir = 'U';
                MoveSnake(dir);
                return 0;
            }
            if (obj.r > snake.snakeHeadRow && obj.c == snake.snakeHeadCol)
            {
                dir = 'D';
                MoveSnake(dir);
                return 0;
            }
            return 0;
        }
        public void MoveSnake(char dir)
        {
            for (int i = snake.size; i > 0; i--)
            {
                snakecells[i, 0] = snakecells[i - 1, 0];
                snakecells[i, 1] = snakecells[i - 1, 1];
            }
            if (dir == 'U')
            {
                if (squares[snake.snakeHeadRow - 1, snake.snakeHeadCol].occupiedBy != ' ')
                {
                    if (squares[snake.snakeHeadRow - 1, snake.snakeHeadCol].occupiedBy == 'F')
                    {
                        GrowSnake();
                    }
                    if (squares[snake.snakeHeadRow - 1, snake.snakeHeadCol].occupiedBy == 'O')
                    {
                        SnakeCollid();
                    }
                }
                snakecells[0, 0] = snake.snakeHeadRow - 1;
                snakecells[0, 1] = snake.snakeHeadCol;
            }

            if (dir == 'L')
            {

                if (squares[snake.snakeHeadRow, snake.snakeHeadCol - 1].occupiedBy != ' ')
                {
                    if (squares[snake.snakeHeadRow, snake.snakeHeadCol - 1].occupiedBy == 'F')
                    {
                        GrowSnake();

                    }
                    if (squares[snake.snakeHeadRow, snake.snakeHeadCol - 1].occupiedBy == 'O')
                    {

                        SnakeCollid();
                    }
                }
                snakecells[0, 0] = snake.snakeHeadRow;
                snakecells[0, 1] = snake.snakeHeadCol - 1;
            }
            if (dir == 'R')
            {

                if (squares[snake.snakeHeadRow, snake.snakeHeadCol + 1].occupiedBy != ' ')
                {
                    if (squares[snake.snakeHeadRow, snake.snakeHeadCol + 1].occupiedBy == 'F')
                    {
                        GrowSnake();
                    }
                    if (squares[snake.snakeHeadRow, snake.snakeHeadCol + 1].occupiedBy == 'O')
                    {

                        SnakeCollid();
                    }

                }
                snakecells[0, 0] = snake.snakeHeadRow;
                snakecells[0, 1] = snake.snakeHeadCol + 1;
            }
            if (dir == 'D')
            {

                if (squares[snake.snakeHeadRow + 1, snake.snakeHeadCol].occupiedBy != ' ')
                {
                    if (squares[snake.snakeHeadRow + 1, snake.snakeHeadCol].occupiedBy == 'F')
                    {
                        GrowSnake();

                    }
                    if (squares[snake.snakeHeadRow + 1, snake.snakeHeadCol].occupiedBy == 'O')
                    {

                        SnakeCollid();
                    }
                }
                snakecells[0, 0] = snake.snakeHeadRow + 1;
                snakecells[0, 1] = snake.snakeHeadCol;
            }

            direction = dir;
            UpdateSnakeSquares();
        }
        public void SnakeCollid()
        {
            MessageBox.Show("gameover");
        }

        public void UpdateSnakeSquares()
        {
            int count = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int k = 0; k < cols; k++)
                {
                    if (squares[i, k].occupiedBy == 'S')
                    {
                        squares[i, k].occupiedBy = ' ';
                    }
                }
            }

            for (int i = 0; i < snake.size + 1; i++)
            {
                squares[snakecells[i, 0], snakecells[i, 1]].occupiedBy = 'S';
                count++;
            }
            snake.snakeHeadRow = snakecells[0, 0];
            snake.snakeHeadCol = snakecells[0, 1];
            snake.snakeHead = squares[snake.snakeHeadRow, snake.snakeHeadCol];
        }

        public void GenerateFood()
        {



            for (int i = 0; i < foods.Count; i++)
            {
                if (squares[foods[i].row, foods[i].col].occupiedBy != 'F')
                {
                    foods.RemoveAt(i);
                    i--;
                }
            }
            int random1 = R.Next(0, rows);
            int random2 = R.Next(0, cols);
            if (foods.Count() == 0)
            {
                if (squares[random1, random2].occupiedBy == ' ')
                {
                    Food food = new Food();
                    food.row = random1;
                    food.col = random2;
                    foods.Add(food);
                    squares[random1, random2].occupiedBy = 'F';
                }
                else
                {
                    GenerateFood();
                }
            }




        }
        public int GenerateObstacles(int n)
        {
            for (int i = 0; i < rows; i++)
            {
                squares[i, 0].occupiedBy = 'O';
                squares[i, cols - 1].occupiedBy = 'O';
            }
            if (n == 0)
            {
                return 0;
            }
            int random1 = R.Next(0, rows);
            int random2 = R.Next(0, cols);
            if (squares[random1, random2].occupiedBy == ' ')
            {
                Obstacle obstacle = new Obstacle();
                obstacle.row = random1;
                obstacle.col = random2;
                obstacles.Add(obstacle);

                squares[random1, random2].occupiedBy = 'O';
                n--;
            }
            
            GenerateObstacles(n);
            return 0;



        }



    }
}
