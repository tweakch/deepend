using Deepend.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Deepend.Analysis
{
    internal class FileAnalysis
    {
        private FileInfo _fileInfo;
        private FileAnalysisOptions _options;
        private Dictionary<bool, StringComparer> _comparer = new() { { true, StringComparer.InvariantCulture }, { false, StringComparer.InvariantCultureIgnoreCase } };
        private List<string> _blacklist;
        private Regex _regex;

        public FileAnalysis(FileInfo fileInfo, FileAnalysisOptions options)
        {
            _fileInfo = fileInfo;
            _options = options;

            _regex = new Regex(_options.IncludePattern);

            var ignoreFileInfo = new FileInfo(_options.WordsToIgnore);
            _blacklist = ignoreFileInfo.OpenText().ReadToEnd().Split(Environment.NewLine).ToList();
        }

        internal FileAnalysisResult Analyze()
        {
            var result = new FileAnalysisResult(_fileInfo);

            using (StreamReader sr = _fileInfo.OpenText())
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    string[] words = line.Split();

                    StringComparer comparer = _comparer[_options.CaseSensitive];

                    foreach (string word in words)
                    {
                        var match = _regex.Match(word);

                        if (!match.Success)
                        {
                            result.Skip(match.Value);
                            continue;
                        }

                        var term = match.Value;
                        var lemma = Lemmatize(term);

                        bool shouldIgnore = _blacklist.Contains(lemma, comparer);

                        if (shouldIgnore)
                        {
                            result.Ignore(lemma);
                            continue;
                        }

                        if (_options.CaseSensitive) result.Include(lemma);
                        else result.Include(lemma.ToLowerInvariant());
                    }
                }
            }

            return result;
        }

        private string Lemmatize(string word)
        {
            var lemma = _options.Lemmas.FirstOrDefault(l => l.Value.Contains(word.ToLower()));

            if (lemma.Key == null)
            {
                return word;
            }

            return lemma.Key;
        }
    }
}