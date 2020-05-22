using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.logic
{
    class AnalyzeData
    {
        private bool _analyzed;
        public int Value { get; }

        public AnalyzeData(int value)
        {
            Value = value;
            _analyzed = false;
        }

        public bool IsAnalyzed()
        {
            return _analyzed;
        }

        public void Analyzed()
        {
            _analyzed = true;
        }

        public AnalyzeData Clone()
        {
            var clone = new AnalyzeData(Value);
            if (_analyzed)
            {
                clone.Analyzed();
            }
            return clone;
        }
    }
}
