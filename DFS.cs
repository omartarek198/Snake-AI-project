using System;
using System.Collections.Generic;

using System.Drawing;


namespace AI_project
{
    class DFS : SearchBehaviour
    {

        bool[,] visited;
        public List<node> path = new List<node>();
        int R, C;
        square[,] array;
        public DFS(int r, int c, square[,] mapsquares)
        {
            R = r;
            C = c;
            visited = new bool[R, C];
            for (int i = 0; i < R; i++)
            {
                for (int k = 0; k < C; k++)
                {
                    visited[i, k] = false;
                }
            }
            array = new square[R, C];

            for (int i = 0; i < R; i++)
            {
                for (int k = 0; k < C; k++)
                {
                    array[i, k] = new square();
                    array[i, k].occupiedBy = mapsquares[i, k].occupiedBy;
                }
            }
        }
     
        int DFSRec(int x, int y, bool[,] visited)
        {

            if (!isvalid(x, y, visited)) return 0;

            visited[x, y] = true;
            if (array[x, y].occupiedBy == 'F')
            {
                node n = new node();
                n.r = x;
                n.c = y;
                path.Add(n);
                return 1;
            }
       
         
                //down
                if (DFSRec(x + 1, y, visited) == 1)
                {
                    node n = new node();
                    n.r = x;
                    n.c = y;
                    path.Add(n);
                    return 1;
                }


                //up
                if (DFSRec(x - 1, y, visited) == 1)
                {
                    node n = new node();
                    n.r = x;
                    n.c = y;
                    path.Add(n);
                    return 1;
                }
    
                //right
                if (DFSRec(x, y + 1, visited) == 1)
                {
                    node n = new node();
                    n.r = x;
                    n.c = y;
                    path.Add(n);
                    return 1;
                }
          
        
                //left
                int result = DFSRec(x, y - 1, visited);
                if (result == 1)
                {
                    node n = new node();
                    n.r = x;
                    n.c = y;
                    path.Add(n);
                    return 1;
                }


            if (array[x, y].occupiedBy == 'S')
            {
                return -1;
            }






            return 0;


        }



        bool isvalid(int x, int y, bool[,] visited)
        {
            try
            {
                if (array[x, y].occupiedBy == 'O')
                {
                    return false;
                }
            }
            catch
            {
                // out of bounds    
                return false;
            }


            if (visited[x, y])
            {
                return false;
            }

            return true;
        }
        public void dfs(int r, int c,char dir)
        {

            //code to prevent snake from going backwards
            if (dir == 'U')
            {
                r--;
            }
            if (dir == 'D')
            {
                r++;
            }
            if (dir =='L')
            {
                c--;
            }
            if (dir =='R')
            {
                c++;
            }
            DFSRec(r, c, visited);
          
        }
   

        public List<node>FindPath(World world)
        {
            dfs(world.snake.snakeHeadRow, world.snake.snakeHeadCol, world.direction);
            return path;
        }
    }
}
