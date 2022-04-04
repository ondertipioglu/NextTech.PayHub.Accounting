using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Core.Shared.Settings
{
    public class ElasticSearchSettings
    {
        public string Url { get; set; }
        public string IndexName { get; set; }
        public string LogIndexFormat { get; set; }
    }
}
