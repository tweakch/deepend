using System.Collections.Generic;

namespace Deepend.Options
{
    public class FileAnalysisOptions
    {
        public string IncludePattern { get; set; }
        public bool CaseSensitive { get; internal set; }
        public string WordsToIgnore { get; set; }
        public bool IgnoreSpecialCharacters { get; set; }
        public string SequencesToInclude { get; set; }

        public Dictionary<string, List<string>> Lemmas { get; set; }
    }
}