using CPM.Model.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.Model
{
    public interface INode
    {
        int Id { get; set; }
        List<Line> Predecesors { get; set; }
        List<Line> Succesors { get; set; }
        int Order { get; set; }
        double Start { get; set; }
        double Deadline { get; set; }
        bool Critical { get; set; }
    }
}
