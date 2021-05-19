using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Deepend.Analysis
{
    internal class AnalysisReport
    {
        private readonly ILogger _log;
        private FileAnalysisResult _result;

        public AnalysisReport(ILogger log, FileAnalysisResult result)
        {
            _log = log;
            _result = result;
        }
        public FileAnalysisResult Result => _result;

        public void Print()
        {
            _log.LogInformation("{file} ({n} words)", _result.FileInfo.FullName, _result.TotalWords);

            _log.LogInformation("Top 10 (count, score)");

            for (int i = 0; i < 10; i++)
            {
                var element = _result.WordFrequencies.ElementAt(i);
                int count = element.Value;
                double rx = Math.Round(100.0 * count / _result.TotalWords, 2);
                _log.LogInformation("\t{w} ({c}, {r}%)", element.Key, count, rx);

                var nodes = _result.Nodes[element.Key];
                HashSet<string> correlations = new HashSet<string>();
                foreach (var node in nodes)
                {
                    var cnode = new CorrelationNode(node);
                    var correlated = cnode.Strip(_result.Ignored).Trim();

                    if (correlations.Contains(correlated)) continue;

                    correlations.Add(correlated);
                }
                foreach (var item in correlations)
                {
                    _log.LogInformation("\t\t{a}",item);
                }
            }
        }

        internal void WriteTo(string outputFilePath)
        {
            
            using (var file = new StreamWriter(outputFilePath, append: true))
            {
                file.WriteLine($"{_result.FileInfo.FullName} ({_result.TotalWords} words)");
                file.WriteLine("Top 10 (count, score)");

                for (int i = 0; i < 10; i++)
                {
                    var element = _result.WordFrequencies.ElementAt(i);
                    int count = element.Value;
                    double rx = Math.Round(100.0 * count / _result.TotalWords, 2);
                    file.WriteLine($"\t{element.Key} ({count}, {rx}%)");

                    var nodes = _result.Nodes[element.Key];
                    HashSet<string> correlations = new HashSet<string>();
                    foreach (var node in nodes)
                    {
                        var cnode = new CorrelationNode(node);
                        var correlated = cnode.Strip(_result.Ignored).Trim();

                        if (correlations.Contains(correlated)) continue;

                        correlations.Add(correlated);
                    }
                    foreach (var item in correlations)
                    {
                        file.WriteLine($"\t\t{item}");
                    }
                    
                }
            }
        }
    }
}