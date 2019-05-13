using CodeMap.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMap.Domain.Interface
{
    public interface ICodeFile
    {
        string GetFullPath(string name);
        List<string> GetContent(string fullPath);
        List<string> GetReferences(string name, List<string> content, RegexRule rule);
        List<string> CleanContent(List<string> content, List<RegexRule> cleanRules);
    }
}
