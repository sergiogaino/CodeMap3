using CodeMap.Domain.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CodeMap.Domain.Enumerators.RegexRule;

namespace CodeMap.Domain.Entities
{
    public class CodeFile : IDisposable
    {
        private ICodeFile _codeFile;
        private IRegexRule _regexRule;

        private List<RegexRule> _cleaningRules;
        private List<RegexRule> _capureRules;

        public string fullPath { get; private set; }
        public string name { get { return Path.GetFileName(fullPath); } }
        public string extension { get { return Path.GetExtension(fullPath); } }
        public string path { get { return Path.GetDirectoryName(fullPath); } }
        public bool exists { get; private set; }
        public int level { get; private set; }

        public List<string> content { get; private set; }
        public List<CodeFile> references { get { return GetReferences(); } }

        public CodeFile(string fullPath, bool exists, ICodeFile codeFile, int level)
        {
            _regexRule = new Services.RegexRule();

            this.fullPath = fullPath;
            this.exists = exists;
            this.level = level;
            _codeFile = codeFile;

            if (exists)
            {
                content = GetContent();
                _cleaningRules = GetCleaningRules();
                _capureRules = GetCaptureRules();
            }
            else
            {
                content = new List<string>();
                _cleaningRules = new List<RegexRule>();
                _capureRules = new List<RegexRule>();
            }

        }

        private List<string> GetContent()
        {
            if (!exists) return new List<string>();

            return _codeFile.GetContent(fullPath);
        }

        private List<RegexRule> GetCleaningRules()
        {
            return _regexRule.GetRules(extension, RuleType.Cleaning);
        }

        private List<RegexRule> GetCaptureRules()
        {
            return _regexRule.GetRules(extension, RuleType.Capture);
        }

        private List<CodeFile> GetReferences()
        {
            List<string> references = new List<string>();

            foreach (var capureRule in _capureRules)
            {
                List<string> content = _codeFile.CleanContent(_codeFile.GetContent(fullPath), _cleaningRules);

                references.AddRange(_codeFile.GetReferences(name, content, capureRule));
            }

            List<CodeFile> codeFiles = new List<CodeFile>();

            int _level = level + 1;
            CodeFile codeFile;

            foreach (string reference in references)
            {
                string _fullPath = _codeFile.GetFullPath(reference);


                if (_fullPath == "")
                {
                    codeFile = new CodeFile(reference, false, _codeFile, _level);
                }
                else
                {
                    codeFile = new CodeFile(_codeFile.GetFullPath(reference), true, _codeFile, _level);
                }
                codeFiles.Add(codeFile);
            }

            return codeFiles;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        ~CodeFile()
        {
            this.Dispose();
        }
    }
}
