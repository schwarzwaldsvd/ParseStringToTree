using System;
using System.Collections.Generic;

namespace ParseStringToTree
{
    public class Node : IComparable<Node>
    {
        public Node Parent;
        public string Value;
        public List<Node> ChildNodes;

        public int CompareTo(Node that)
        {
            return string.Compare(this.Value, that.Value, StringComparison.Ordinal);
        }
    }
}
