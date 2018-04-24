using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.Model.Impl
{
    public class Arc : IArc
    {
        public IEnumerable<ILine> Lines { get; set; }
        public IEnumerable<INode> Nodes { get; set; }
        public IEnumerable<INode> StartPoints { get; set; }
        public IEnumerable<INode> EndPoints { get; set; }
    }
}
