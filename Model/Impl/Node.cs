using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.Model.Impl
{
    public class Node : INode
    {
        public Node()
        {
            Id = 0;
            Start = Deadline = 0;
            Predecesors = new List<Line>();
            Succesors = new List<Line>();
            Critical = false;
        }
        public Node(int id, double time)
        {
            Id = id;
            Start = 0;
            Predecesors = new List<Line>();
            Succesors = new List<Line>();
            Critical = false;
        }
        public Node(int id, double time, double start)
        {
            Id = id;
            Start = start;
            Predecesors = new List<Line>();
            Succesors = new List<Line>();
            Critical = false;
        }

        public int Id { get; set; }
        public List<Line> Predecesors { get; set; }
        public List<Line> Succesors { get; set; }
        public int Order { get; set; }
        public double Start { get; set; }
        public double Deadline { get; set; }
        public bool Critical { get; set; }
    }
}
