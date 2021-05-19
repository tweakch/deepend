using System.Collections.Generic;

namespace Deepend.Analysis
{
    internal class CorrelationNode
    {
        private LinkedListNode<string> _node;
        public string Value { get; set; }
        public string Previous { get; set; }
        public string Next { get; set; }
        public bool HasPrevious => _node.Previous != null && !string.IsNullOrWhiteSpace(_node.Previous.Value);
        public bool HasNext => _node.Next != null && !string.IsNullOrWhiteSpace(_node.Next.Value);

        public CorrelationNode(LinkedListNode<string> node)
        {
            _node = node;
            Value = node.Value;

            if (HasPrevious)
            {
                Previous = _node.Previous.Value;
            }

            if (HasNext)
            {
                Next = _node.Next.Value;
            }
        }

        public string Strip(List<string> values)
        {
            if (values.Contains(Previous))
            {
                Previous = null;
            }

            if (values.Contains(Next))
            {
                Next = null;
            }
            return ToString();
        }

        public override string ToString()
        {
            if (HasPrevious && HasNext)
            {
                return string.Format("{0} {1} {2}", Previous, Value, Next);
            }
            if (HasPrevious)
            {
                return string.Format("{0} {1}", Previous, Value);
            }
            if (HasNext)
            {
                return string.Format("{0} {1}", Value, Next);
            }
            return Value;
        }
    }
}