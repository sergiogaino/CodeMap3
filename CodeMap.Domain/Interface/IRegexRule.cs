using CodeMap.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CodeMap.Domain.Enumerators.RegexRule;

namespace CodeMap.Domain.Interface
{
    public interface IRegexRule
    {
        List<RegexRule> GetRules(string extension, RuleType type);
    }
}
