using CodeMap.Domain.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using static CodeMap.Domain.Enumerators.RegexRule;

namespace CodeMap.Domain.Services
{
    public class RegexRule : IRegexRule
    {
        const string cleaningRuleFileName = "CFG_CleaningRules.xml";
        const string captureRuleFileName = "CFG_CaptureRules.xml";

        public Regex Regex { get; private set; }

        public List<Entities.RegexRule> GetRules(string extension, RuleType type)
        {
            string ruleFileName = type == RuleType.Cleaning ? cleaningRuleFileName : captureRuleFileName;

            List<Entities.RegexRule> rules = new List<Entities.RegexRule>();

            XmlDocument xmlContent = Infra.File.GetConfiguration(ruleFileName);
            XElement xl = XElement.Load(new XmlNodeReader(xmlContent));

            List<XElement> expressions = (from xe in xl.Elements()
                                          where xe.Element("rule-extension").Value.ToLower() == extension.Remove(0, 1).ToLower()
                                          select xe.Element("rules")).Descendants().ToList();

            foreach (var expression in expressions)
            {
                Entities.RegexRule rr = new Entities.RegexRule();
                rr.Regex = new Regex(expression.Value);

                if (expression.HasAttributes)
                {
                    rr.OutputPattern = expression.Attribute("output").Value;
                }

                rules.Add(rr);
            }

            return rules;
        }
    }
}
