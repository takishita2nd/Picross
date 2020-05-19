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
    }
}
