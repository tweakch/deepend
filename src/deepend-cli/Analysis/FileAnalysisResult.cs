using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Deepend.Analysis
{
    internal class FileAnalysisResult
    {
        public FileAnalysisResult(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }

        private Dictionary<string, List<LinkedListNode<string>>> _ix = new();
        private Dictionary<string, int> _words = new();
        private Dictionary<string, int> _skipped = new();
        private Dictionary<string, int> _ignored = new();

        public List<string> Ignored => _ignored.Keys.ToList();
        public List<string> Skipped => _skipped.Keys.ToList();

        private LinkedList<string> _text = new();

        public Dictionary<string, int> Words { get => _words; set => _words = value; }
        public FileInfo FileInfo { get; }
        public int TotalWords => _text.Count;

        public Dictionary<string, List<LinkedListNode<string>>> Nodes => _ix;

        /// <summary>
        /// The lemmatized text
        /// </summary>
        public LinkedList<string> Text { get; set; }

        public IOrderedEnumerable<KeyValuePair<string, int>> WordFrequencies => Words.OrderByDescending(w => w.Value);

        internal void Include(string word)
        {
            Put(word).Into(_words);
        }

        private AnalysisAction Put(string word)
        {
            var node = ReadWord(word);
            return new AnalysisAction(word, node, _ix);
        }

        private LinkedListNode<string> ReadWord(string word)
        {
            return _text.AddLast(word);
        }

        internal void Skip(string word)
        {
            Put(word).Into(_skipped);
        }

        internal void Ignore(string word)
        {
            Put(word).Into(_ignored);
        }
    }
}