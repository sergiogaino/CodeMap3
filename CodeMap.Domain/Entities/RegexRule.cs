using CodeMap.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeMap.Domain.Entities
{
    public class RegexRule
    {
        public Regex Regex { get; set; }
        public string OutputPattern { get; set; }
        public string Expression { get { return Regex.ToString(); } }
    }
}
