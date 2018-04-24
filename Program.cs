using CPM.Controller;
using CPM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM
{
    class Program
    {
        static void Main(string[] args)
        {
            ArcController arc = new ArcController();
            
            arc.GetArc();
            arc.WriteNodes();
            arc.OrderNodes();
            
            if (arc.isValid)
            {
                //arc.ShowGraphAfterOrder();
                arc.CaluclateExecutionTime();
                arc.CalculateDeadline();
                arc.CriticalPath();
                arc.ShowFinalGraph();
            }

            Console.WriteLine("Naciśnij dowolny klawisz aby zakończyć...");
            Console.ReadLine();
        }
    }
}
