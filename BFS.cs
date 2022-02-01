using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI_project
{
    class BFS   : SearchBehaviour 
    {
        bool[,] visited;
        public List<node> path = new List<node>();
        int R, C;
        square[,] array;
        node[,] prev;
        public BFS(int r, int c, square[,] obj)
        {
            R = r;
            C = c;

            visited = new bool[R, C];
            prev = new node[R, C];
            for (int i = 0; i < R; i++)
            {
                for (int k = 0; k < C; k++)
                {
                    visited[i, k] = false;
                    prev[i, k] = null;
                }
            }
            SetArray(obj);
        }
        public void SetArray(square[,] obj)
        {
                array = new square[R, C];

            for (int i = 0; i < R; i++)
            {
                for (int k = 0; k < C; k++)
                {
                    array[i, k] = new square();
                    array[i, k].occupiedBy = obj[i, k].occupiedBy;
                }
            }
        }
        bool isvalid(int x, int y, bool[,] visited)
        {
            try
            {
                if (array[x, y].occupiedBy == 'O' || array[x,y].occupiedBy == 'S')
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
        public void ConstructPath(int r, int c)
        {
            node pn = new node();
            pn.r = r;
            pn.c = c;
            path.Add(pn);
            for (node ptrav = prev[r, c]; ptrav!= null; ptrav =prev[ptrav.r,ptrav.c])
            {
                path.Add(ptrav);
            }

           
        }

        public void bfs(int x, int y)
        {

            LinkedList<node> queue = new LinkedList<node>();
            // Mark the current node as
            // visited and enqueue it
            visited[x,y] = true;
            node n = new node();
            n.r = x;
            n.c = y;
            node T = new node();
            queue.AddLast(n);
            while (queue.Any())
            {

                node Current = new node();
                Current = queue.First();
                x = Current.r;
                y = Current.c;
                if (array[Current.r,Current.c].occupiedBy == 'F')
                {
                    ConstructPath(Current.r,Current.c);
                    break;
                }
                queue.RemoveFirst();
                if (isvalid(x+1,y,visited))
                {
                    visited[x + 1, y] = true;
                    T = new node();
                    T.r = x + 1;
                    T.c = y;
                    queue.AddLast(T);
                    prev[x + 1, y] = Current;
                }
                if (isvalid(x-1, y, visited))
                {
                    visited[x - 1, y] = true;
                    T = new node();
                    T.r = x - 1;
                    T.c = y;
                    queue.AddLast(T);
                    prev[x - 1, y] = Current;

                }
                if (isvalid(x, y+1, visited))
                {
                    visited[x,y+1] = true;
               
                    T = new node();
                    T.r = x ;
                    T.c = y+1;
                    queue.AddLast(T);
                    prev[x , y+1] = Current;
                }
                if (isvalid(x, y-1, visited))
                {
                    
                    visited[x, y - 1] = true;
                    T = new node();
                    T.r = x ;
                    T.c = y-1;
                    queue.AddLast(T);
                    prev[x , y-1] = Current;
                }

            }
        }

       
        public List<node> FindPath(World world)
        {
            bfs(world.snake.snakeHeadRow, world.snake.snakeHeadCol);
            return path;
        }
    }

  
}
