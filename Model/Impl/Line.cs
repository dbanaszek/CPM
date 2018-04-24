using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.Model.Impl
{
    public class Line : ILine
    {
        public Line()
        {
            Id = 0;
            Time = Delay = 0;
        }

        public Line(int id, int time, int delay)
        {
            Id = id;
            Time = time;
            Delay = delay;
        }

        public int Id { get; set; }
        public INode Start { get; set; }
        public INode End { get; set; }
        public double Time { get; set; }
        public double Delay { get; set; }
    }
}
