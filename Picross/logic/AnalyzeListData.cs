using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.logic
{
    class AnalyzeListData
    {
        private bool _analyzed;
        public List<AnalyzeData> AnalyzeDatas;

        public AnalyzeListData()
        {
            _analyzed = false;
            AnalyzeDatas = new List<AnalyzeData>();
        }

        public bool IsAnalyzed()
        {
            return _analyzed;
        }

        public void Analyzed()
        {
            foreach(var data in AnalyzeDatas)
            {
                data.Analyzed();
            }
            _analyzed = true;
        }

        public void CheckAnalyze()
        {
            int count = 0;
            foreach(var data in AnalyzeDatas)
            {
                if (data.IsAnalyzed())
                {
                    count++;
                }
            }
            if(count == AnalyzeDatas.Count)
            {
                _analyzed = true;
            }
        }

        public AnalyzeListData Clone()
        {
            var clone = new AnalyzeListData();
            clone.AnalyzeDatas = new List<AnalyzeData>();
            foreach (var data in AnalyzeDatas)
            {
                clone.AnalyzeDatas.Add(data.Clone());
            }
            if (_analyzed)
            {
                clone.Analyzed();
            }
            return clone;
        }
    }
}
