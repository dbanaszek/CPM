using CPM.Model;
using CPM.Model.Impl;
using CPM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.Controller
{
    public class ArcController
    {

        List<int> organized = new List<int>();
        Arc arc = new Arc();
        Interaction interaction = new Interaction();
        ShowGraph graph = new ShowGraph();
        string fileName = null; // = "graf.xml";
        int order = 1;
        public bool isValid = true;

        public void GetFileName()
        {
            fileName = interaction.GetFileName();
        }

        public void GetArc()
        {
            if (fileName == null)
                GetFileName();
            arc = interaction.ReadXML(fileName);
        }

        private bool ReadytoSetInOrder(Node node)
        {
            if (node.Order != 0)
                return false;

            if(node.Predecesors.Count() == 0)
                return true;

            for(int i = 0; i < node.Predecesors.Count(); i++)
            {
                if (node.Predecesors.ElementAt(i).Start.Order == 0)
                    return false;
            }

            return true;
        }

        private bool FillLines()
        {
            try
            {
                foreach (Node node in arc.Nodes)
                {
                    foreach (Line line in node.Predecesors)
                        line.End = node;

                    foreach (Line line in node.Succesors)
                        line.Start = node;
                }
            }catch(Exception ex)
            {
                return false;
            }
            

            return true;
        }

        private bool CheckForLoop(INode node, int counter)
        {
            bool result = true;

            if (counter <= 0)
                return false;

            if (node.Predecesors.Count() == 0 && counter > 0)
                return result;

            if (node.Predecesors.Count() > 0 && counter > 0)
            {
                foreach(Line line in node.Predecesors)
                {
                  result = CheckForLoop(line.Start, --counter);
                    if (result == false)
                        break;
                }
            }

            return result;
        }

        private void ShowGraphAfterinitialization()
        {
            graph.CreateGraph(arc);
        }

        public void ShowGraphAfterOrder()
        {
            graph.ShowOrganizedGraph(arc);
        }

        public void ShowFinalGraph()
        {
            graph.ShowFinalGraph(arc);
        }

        private bool CheckWholeGraph()
        {
            var size = arc.Nodes.Count();
            bool result = true;
            foreach(INode node in arc.Nodes)
            {
                result = CheckForLoop(node, size);
                if (result == false)
                    break;
            }
            isValid = result;
            return result;
        }

        public void OrderNodes()
        {
            FillLines();
            ShowGraphAfterinitialization();
            interaction.CheckForLoop(CheckWholeGraph());
            if (isValid)
            {
                int i = 0;
                interaction.OrganizeBeginEnd(true);
                while (order <= arc.Nodes.Count())
                {
                    if (i >= arc.Nodes.Count())
                        i = 0;

                    if (ReadytoSetInOrder((Node)arc.Nodes.ElementAt(i)))
                    {
                        arc.Nodes.ElementAt(i).Order = order;
                        order++;
                        interaction.ShowOrganize((Node)arc.Nodes.ElementAt(i));
                        organized.Add(i);
                    }
                    i++;
                }
                interaction.OrganizeBeginEnd(false);
            }
        }

        public void CalculateDeadline()
        {
            List<int> copy = organized;
            copy.Reverse();

            double deadline = 0;

            interaction.Deadline(true);

            foreach (int i in copy)
            {
                deadline = 0;
                if(arc.Nodes.ElementAt(i).Succesors.Count() == 0)
                {
                    arc.Nodes.ElementAt(i).Deadline = arc.Nodes.ElementAt(i).Start;
                    interaction.ShowDeadline((Node)arc.Nodes.ElementAt(i));
                    continue;
                }

                foreach(Line line in arc.Nodes.ElementAt(i).Succesors)
                {
                    if (deadline > (line.End.Deadline - line.Time) || deadline == 0)
                        deadline = (line.End.Deadline - line.Time);
                }

                arc.Nodes.ElementAt(i).Deadline = deadline;
                interaction.ShowDeadline((Node)arc.Nodes.ElementAt(i));
            }

            interaction.Deadline(false);
        }

        public void CaluclateExecutionTime()
        {
            double delay = 0;
            double time = 0;

            interaction.StartTime(true);

            foreach (int i in organized)
            {
                delay = 0;
                time = 0;

                foreach (Line line in arc.Nodes.ElementAt(i).Predecesors)
                {
                    if (delay < line.Delay)
                        delay = line.Delay;

                    if (time < (line.Start.Start + line.Time))
                        time = line.Start.Start + line.Time;
                }

                if (time > delay)
                    arc.Nodes.ElementAt(i).Start = time;
                else
                    arc.Nodes.ElementAt(i).Start = delay;

                interaction.ShowStartTime((Node)arc.Nodes.ElementAt(i), time, delay);
            }

            interaction.StartTime(false);
        }

        public void CriticalPath()
        {
            double cmax = 0;
            interaction.CriticalPathComm(true);
            foreach (Node node in arc.Nodes)
            {
                if (node.Start == node.Deadline)
                {
                    node.Critical = true;
                    cmax = node.Start;
                    interaction.ShowDeadline(node);
                }
            }
            interaction.CriticalPath(cmax);
            interaction.CriticalPathComm(false);
        }

        public void WriteNodes()
        {
            if(arc.Nodes.Count() > 0)
                interaction.PrintNodes(arc.Nodes);

        }
    }
}
