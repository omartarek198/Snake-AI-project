using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_project
{
    interface SearchBehaviour
    {
        List<node> FindPath(World world);
    }
}
