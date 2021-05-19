using Deepend.Analysis;
using Deepend.Converters;
using Deepend.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Deepend
{

    // Interface
    public interface IDataService
    {
        void Connect();
        void Display();
        void Analyze();
    }

    // Class
    public class DeependService : IDataService
    {
        private readonly DeependOptions _options;
        private readonly ILogger<DeependService> _log;
        private readonly IConfiguration _config;

        public DeependService(ILogger<DeependService> log, IConfiguration config, IOptions<DeependOptions> options)
        {
            _log = log;
            _config = config;
            _options = options.Value;
        }

        public void Analyze()
        {
            _log.LogInformation("Options {opt}", Serialize(_options));

            List<FileAnalysisResult> results = new List<FileAnalysisResult>();

            foreach (var item in Directory.GetFiles(_options.RootDirectory, _options.FilesToInclude, new EnumerationOptions() { RecurseSubdirectories = true }))
            {
                var analysis = new FileAnalysis(new FileInfo(item), _options.FileAnalysis);
                results.Add(analysis.Analyze());
            }

            // create reports
            var reports = results.Select(r => new AnalysisReport(_log, r));

            // cross reference
            foreach (var report in reports)
            {
                var otherResults = reports.Except(new[] { report }).Select(r => r.Result);
            }

            var info = new FileInfo(_options.OutputFilePath);

            if (info.Exists)
            {
                _log.LogWarning("Deleting existing analysis {f}", info.FullName);
                info.Delete();
            }

            foreach (var result in results)
            {
                var report = new AnalysisReport(_log, result);
                //report.Print();
                _log.LogInformation("Writing report for {f}",result.FileInfo.FullName);
                report.WriteTo(_options.OutputFilePath);
            }
            _log.LogInformation("Written to {f}", _options.OutputFilePath);
        }

        public void Display()
        {
            var directory = new DirectoryInfo(_options.RootDirectory);

            _log.LogInformation("Directory {opt}", Serialize(directory));
        }

        private string Serialize(object obj, bool indented = true)
        {
            JsonSerializerOptions options = new() 
                    { WriteIndented = indented };
            options.Converters.Add(new DirectoryInfoConverter(_options.FilesToInclude));
            //options.Converters.Add(new FileInfoConverter());
            return JsonSerializer.Serialize(obj, options);
        }

        public void Connect()
        {
            // Connect to the database
            var connectionString = _config.GetValue<string>("ConnectionStrings:DefaultConnection");
            _log.LogInformation("ConnectionString {cs}", connectionString);
        }
    }
}
