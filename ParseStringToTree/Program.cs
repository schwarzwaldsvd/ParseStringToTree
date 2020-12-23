using System;
using System.Collections.Generic;
using System.Linq;

namespace ParseStringToTree
{
    class Program
    {

        static void Main()
        {
            const string inputString = "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";
            var root = new Node
            {
                Parent = null,
                Value = string.Empty,
                ChildNodes = new List<Node>()
            };
            var node = root;
            var escape = false;
            foreach (var c in inputString)
            {
                if (escape)
                {
                    if (c != ' ') node.Value += c;
                    escape = false;
                }
                else
                {
                    switch (c)
                    {
                        case '(' :
                            node = new Node {Parent = node, Value = string.Empty, ChildNodes = new List<Node>()};
                            node.Parent.ChildNodes.Add(node);
                            break;
                        case ')' :
                            if (node.Parent != null)
                            {
                                node = new Node {Parent = node.Parent.Parent, Value = string.Empty, ChildNodes = new List<Node>()};
                                node.Parent?.ChildNodes.Add(node);
                            }
                            break;
                        case ',':
                            node = new Node { Parent = node.Parent, Value = string.Empty, ChildNodes = new List<Node>() };
                            node.Parent?.ChildNodes.Add(node);
                            escape = true;
                            break;
                        default:
                            node.Value += c;
                            break;
                    }
                }
            }

            Console.WriteLine("Output 1:");
            Print(root, string.Empty, false);
            Console.WriteLine();

            Console.WriteLine("Output 2:");
            Print(root, string.Empty, true);
        }

       
        private static void Print(Node node, string level, bool isOrdered)
        {
            if (node.Value.Length > 0) Console.WriteLine(level + node.Value);
            
            IEnumerable<Node> nodes = null;
            if (isOrdered) 
                nodes = node.ChildNodes.OrderBy(q => q);
            else 
                nodes = node.ChildNodes;
            
            foreach (var n in nodes)
            {
                if (node.Parent == null) 
                    Print(n, "- " + level, isOrdered);
                else 
                    Print(n, "  " + level, isOrdered);
            }
        }
    }
}
