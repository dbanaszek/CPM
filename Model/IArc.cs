using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.Model
{
    public interface IArc
    {
        IEnumerable<ILine> Lines { get; set; }
        IEnumerable<INode> Nodes { get; set; }
        IEnumerable<INode> StartPoints { get; set; }
        IEnumerable<INode> EndPoints { get; set; }
    }
}
