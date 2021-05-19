using System;
using System.Collections.Generic;

namespace Deepend.Analysis
{
    internal class AnalysisAction
    {
        private string _word;
        private LinkedListNode<string> _node;
        private readonly Dictionary<string, List<LinkedListNode<string>>> _ix;

        public AnalysisAction(string word, LinkedListNode<string> node, Dictionary<string, List<LinkedListNode<string>>> ix)
        {
            _word = word;
            _node = node;
            _ix = ix;
        }

        internal void Into(Dictionary<string, int> words)
        {
            if (!words.ContainsKey(_word))
            {
                words.Add(_word, 1);
                _ix.Add(_word, new List<LinkedListNode<string>>() { _node });
            }
            else
            {
                words[_word] += 1;
                _ix[_word].Add(_node);
            }
        }
    }
}