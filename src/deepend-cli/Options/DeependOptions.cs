using Microsoft.Extensions.Options;

namespace Deepend.Options
{
    public class DeependOptions
    {
        public const string Deepend = "Deepend";

        public string RootDirectory { get; set; }
        public string FilesToInclude { get; set; }
        public FileAnalysisOptions FileAnalysis { get; set; }
        public string OutputFilePath { get; set; }
    }
}
