using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Drawing;
using CPM.Model;
using System.Threading;

namespace CPM.View
{
    class ShowGraph
    {
        public void CreateGraph(IArc arc)
        {
            //form
            Form form = new Form();
            //view
            GViewer viewer = new GViewer();
            //graph
            Graph graph = new Graph("graph");

            graph.Attr.LayerDirection = LayerDirection.LR;

            foreach (ILine line in arc.Lines)
            {
                graph.AddEdge(line.Start.Id.ToString(), line.Time.ToString(), line.End.Id.ToString());
            }

            viewer.Graph = graph;

            form.SuspendLayout();
            viewer.Dock = DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();

            Thread thread = new Thread(() => form.ShowDialog());
            thread.Start();
        }

        public void ShowOrganizedGraph(IArc arc)
        {
            //form
            Form form = new Form();
            //view
            GViewer viewer = new GViewer();
            //graph
            Graph graph = new Graph("graph");
            

            graph.Attr.LayerDirection = LayerDirection.LR;
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            sb.Append("Porządek: ");

            foreach (ILine line in arc.Lines)
            {
                sb.Clear();
                sb2.Clear();

                sb.Append("Porządek: ");
                sb.Append(line.Start.Order.ToString());
                sb.Append("\n ID: ");
                sb.Append(line.Start.Id.ToString());

                sb2.Append("Porządek: ");
                sb2.Append(line.End.Order.ToString());
                sb2.Append("\n ID: ");
                sb2.Append(line.End.Id.ToString());
                graph.AddEdge(sb.ToString(), sb2.ToString()).LabelText = line.Time.ToString();
            }

            viewer.Graph = graph;

            form.SuspendLayout();
            viewer.Dock = DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();

            Thread thread = new Thread(() => form.ShowDialog());
            thread.Start();
        }

        public void ShowFinalGraph(IArc arc)
        {
            //form
            Form form = new Form();
            //view
            GViewer viewer = new GViewer();
            //graph
            Graph graph = new Graph("graph");


            graph.Attr.LayerDirection = LayerDirection.LR;
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();

            sb.Append("Porządek: ");

            foreach (ILine line in arc.Lines)
            {
                sb.Clear();
                sb2.Clear();
                sb3.Clear();

                sb.Append("ID: ");
                sb.Append(line.Start.Id.ToString());
                sb.Append("\nPorządek: ");
                sb.Append(line.Start.Order.ToString());
                sb.Append("\nL(A): ");
                sb.Append(line.Start.Start.ToString());
                sb.Append("\nDeadline: ");
                sb.Append(line.Start.Deadline.ToString());

                sb2.Append("ID: ");
                sb2.Append(line.End.Id.ToString());
                sb2.Append("\nPorządek: ");
                sb2.Append(line.End.Order.ToString());
                sb2.Append("\nL(A): ");
                sb2.Append(line.End.Start.ToString());
                sb2.Append("\nDeadline: ");
                sb2.Append(line.End.Deadline.ToString());

                sb3.Append("T:");
                sb3.Append(line.Time.ToString());
                sb3.Append(" D:");
                sb3.Append(line.Delay.ToString());

                if(line.Start.Critical && line.End.Critical)
                    graph.AddEdge(sb.ToString(), sb3.ToString(), sb2.ToString()).Attr.Color = Color.Red;
                else
                    graph.AddEdge(sb.ToString(), sb3.ToString(), sb2.ToString());
            }

            viewer.Graph = graph;

            form.SuspendLayout();
            viewer.Dock = DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();

            Thread thread = new Thread(() => form.ShowDialog());
            thread.Start();
        }

    }
}
