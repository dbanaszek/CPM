using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.Model
{
    public interface ILine
    {
        int Id { get; set; }
        INode Start { get; set; }
        INode End { get; set; }
        double Time { get; set; }

        double Delay { get; set; }
    }
}
