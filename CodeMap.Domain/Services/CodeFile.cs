using CodeMap.Domain.Entities;
using CodeMap.Domain.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeMap.Domain.Services
{
    public class CodeFile : ICodeFile
    {
        public CodeFile(string initialPath)
        {
            InitialPath = initialPath;
        }

        public string InitialPath { get; private set; }


        public string GetFullPath(string name)
        {
            List<string> files = new List<string>();

            files = Infra.File.GetAll(InitialPath, name);

            if (files.Count() > 0)
            {
                return files[0];
            }

            return string.Empty;
        }

        public List<string> GetContent(string fullPath)
        {
            List<string> lines = new List<string>();
            lines = Infra.File.GetContent(fullPath);

            return (from string ln in lines
                    where !ln.StartsWith("'")
                    select ln).ToList();
        }

        public List<string> GetReferences(string name, List<string> content, Entities.RegexRule rule)
        {
            List<string> references = new List<string>();

            foreach (string line in content)
            {
                string lineLower = line.ToLower();
                Match match = rule.Regex.Match(lineLower);

                if (match.Success)
                {
                    string reference = match.Value.ToLower();

                    if (name != reference)
                    {
                        references.Add(reference);
                    }
                }
            }

            return references;
        }

        public List<string> CleanContent(List<string> content, List<Entities.RegexRule> cleanRules)
        {
            var CleanedContent = (from line in content
                                  select CleanLine(line, cleanRules)).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            return CleanedContent;
        }

        private string CleanLine(string line, List<Entities.RegexRule> cleanRules)
        {
            foreach (var rule in cleanRules)
            {
                string output = "";

                if (!string.IsNullOrEmpty(rule.OutputPattern))
                {
                    output = rule.OutputPattern;
                }

                line = Regex.Replace(line.ToLower(), rule.Expression, output);
            }

            return line;
        }
    }
}
