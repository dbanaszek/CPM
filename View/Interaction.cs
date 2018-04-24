using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPM.Model.Impl;
using System.IO;
using CPM.Model;
using System.Xml;
using System.Globalization;

namespace CPM.View
{
    public class Interaction
    {
        private bool IsValid = true;
        private INode node = new Node();
        private ILine line = new Line();
        private List<int> lineIds = new List<int>();
        private List<int> nodeIds = new List<int>();
        Arc arc = new Arc
        {
            Nodes = new List<Node>(),
            Lines = new List<Line>()
        };
        
        private Line FindLine(int id)
        {
            return (Line)arc.Lines.First(x => x.Id.Equals(id));
        }

        private void ValueHandel(string name, string value)
        {
            switch (name)
            {
                case "line-id":
                    CheckId(Int32.Parse(value));
                    line.Id = Int32.Parse(value); 
                    lineIds.Add(Int32.Parse(value));
                    break;

                case "time":
                    double time;
                    time = double.Parse(value, CultureInfo.InvariantCulture);
                    CheckTime(time);
                    line.Time = time;
                    break;

                case "delay":
                    CkeckDelay(double.Parse(value, CultureInfo.InvariantCulture));
                    line.Delay = double.Parse(value, CultureInfo.InvariantCulture);
                    break;

                case "id":
                    CheckId(Int32.Parse(value));
                    node.Id = Int32.Parse(value);
                    nodeIds.Add(Int32.Parse(value));
                    break;

                case "line-in-id":
                    try
                    {
                        CheckId(Int32.Parse(value));
                        node.Predecesors.Add(FindLine(Int32.Parse(value)));
                    }
                    catch(Exception)
                    {
                        Console.WriteLine("Line ID is not unique");
                        IsValid = false;
                    }
                    break;

                case "line-out-id":
                    try
                    {
                        CheckId(Int32.Parse(value));
                        node.Succesors.Add(FindLine(Int32.Parse(value)));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Line ID is not unique");
                        IsValid = false;
                    }
                    break;
            }
        }

        private void CheckId(int id)
        {
            if(id < 0)
            {
                Console.WriteLine("ID lower than 0");
                IsValid = false;
            }
        }

        private void CheckTime(double time)
        {
            if (time <= 0)
            {
                Console.WriteLine("Time lower or equal 0");
                IsValid = false;
            }
        }

        private void CkeckDelay(double delay)
        {
            if (delay < 0)
            {
                Console.WriteLine("Delay less than 0");
                IsValid = false;
            }
        }

        private void CloseHandler(string value)
        {
            switch (value)
            {
                case "line":
                    arc.Lines = arc.Lines.Concat(new[] { line });
                    line = new Line();
                    break;

                case "node":
                    arc.Nodes = arc.Nodes.Concat(new[] { node });
                    node = new Node();
                    break;
            }
        }

        public string GetFileName()
        {
            string file;
            Console.Write("Podaj nazwę pliku XML: ");
            file = Console.ReadLine();
            return file;
        }

        private void CheckLineIdUnique()
        {
            int counter = 0;

            foreach (Line line in arc.Lines)
            {
                foreach (int i in lineIds)
                {
                    if (line.Id == i)
                        counter++;
                }

                if (counter > 1)
                    throw new ArgumentException("Line ID is not unique");
            }
        }

        private void CheckNodeIdUnique()
        {
            int counter = 0;

            foreach (Node node in arc.Nodes)
            {
                counter = 0;
                foreach (int i in nodeIds)
                {
                    if (node.Id == i)
                        counter++;
                }

                if (counter > 1)
                    throw new ArgumentException("Node ID is not unique");
            }
        }

        private void CheckValid()
        {
            try
            {
                CheckNodeIdUnique();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowStartTime(Node node, double time, double delay)
        {
            Console.WriteLine("Wieżchołek id: {0}| Porządek: {1}, L(A): {2} = Max: ({3} , {4})", node.Id, node.Order, node.Start, time, delay);
        }
        
        public void CriticalPath(double cmax)
        {
            Console.WriteLine("C max = {0}", cmax);
        }

        public void ShowOrganize(Node node)
        {
            Console.WriteLine("Wieżchołek id: {0}| Porządek: {1}", node.Id, node.Order);
        }

        public void ShowDeadline(Node node)
        {
            Console.WriteLine("Wieżchołek id: {0}| Porządek: {1}, L(A): {2}, Najpóźniejszy moment: {3}", node.Id, node.Order, node.Start, node.Deadline);
        }

        public void CriticalPathComm(bool status)
        {
            if (status)
                Console.WriteLine("Ścieżka krytyczna:\n");
            else
                Console.WriteLine("\nZakończono\n");
        }

        public void OrganizeBeginEnd(bool status)
        {
            if (status)
                Console.WriteLine("Porządzkowanie:\n");
            else
                Console.WriteLine("\nZakończono\n");
        }

        public void StartTime(bool status)
        {
            if (status)
                Console.WriteLine("Wyliczanie L(A):\n");
            else
                Console.WriteLine("\nZakończono\n");
        }

        public void Deadline(bool status)
        {
            if (status)
                Console.WriteLine("Wyliczanie najpóźniejszego momentu:\n");
            else
                Console.WriteLine("\nZakończono\n");
        }

        public void PrintNodes(IEnumerable<INode> nodes)
        {
            foreach (Node node in nodes)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nWieżchołek id: {0}", node.Id);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Zadania poprzedzające: ");
                if (node.Predecesors.Count() > 0)
                {
                    foreach (Line line in node.Predecesors)
                    {
                        Console.WriteLine("     Zadanie id: {0}| Czas wykonywania: {1}, Opóźnienie: {2}", line.Id, line.Time, line.Delay);
                    }
                }
                else
                {
                    Console.WriteLine("     Brak zadań");
                }

                Console.WriteLine("Zadania następujące: ");
                if (node.Succesors.Count() > 0)
                {
                    foreach (Line line in node.Succesors)
                    {
                        Console.WriteLine("     Zadanie id: {0}| Czas wykonywania: {1}, Opóźnienie: {2}", line.Id, line.Time, line.Delay);
                    }
                }
                else
                {
                    Console.WriteLine("     Brak zadań");
                }
            }
            Console.WriteLine("");
        }

        public void CheckForLoop(bool status)
        {
            if (status)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Nie wykryto zapętlenia zależności\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wykryto zapętlenie zależności\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
                
        }

        public Arc ReadXML(string fileName)
        {
           string name = "";

            StringBuilder sb = new StringBuilder();
            string file = @"J:\DotNet\CPM\CPM\Files\";
            sb.Append(file).Append(fileName);

            XmlReaderSettings settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse
            };
            XmlReader reader = XmlReader.Create(sb.ToString(), settings);

            reader.MoveToContent();
            
            while (reader.Read() && IsValid)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        name = reader.Name;
                        break;
                    case XmlNodeType.Text:
                        ValueHandel(name, reader.Value);
                        break;
                    case XmlNodeType.EndElement:
                        CloseHandler(reader.Name);
                        break;
                }
            }

            CheckValid();
            return arc;
        }
    }
}
