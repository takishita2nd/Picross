using Picross.ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.logic
{
    partial class PicrossAnalyze
    {
        private List<AnalyzeListData> rowNumbers;
        private List<AnalyzeListData> colNumbers;
        private BitmapData[,] _bitmapData;
        private int oldPaintedCount;

        public PicrossAnalyze(List<List<NumberSquare>> rowNumberSquares, List<List<NumberSquare>> colNumberSquares)
        {
            oldPaintedCount = 0;
            _bitmapData = new BitmapData[rowNumberSquares.Count, colNumberSquares.Count];
            for (int r = 0; r < rowNumberSquares.Count; r++)
            {
                for (int c = 0; c < colNumberSquares.Count; c++)
                {
                    _bitmapData[r, c] = new BitmapData(r, c);
                }
            }

            rowNumbers = new List<AnalyzeListData>();
            foreach (var rowList in rowNumberSquares)
            {
                AnalyzeListData list = new AnalyzeListData();
                foreach (var s in rowList)
                {
                    if (s.GetValue() != 0)
                    {
                        list.AnalyzeDatas.Add(new AnalyzeData(s.GetValue()));
                    }
                }
                rowNumbers.Add(list);
            }

            colNumbers = new List<AnalyzeListData>();
            foreach (var colList in colNumberSquares)
            {
                AnalyzeListData list = new AnalyzeListData();
                foreach (var s in colList)
                {
                    if (s.GetValue() != 0)
                    {
                        list.AnalyzeDatas.Add(new AnalyzeData(s.GetValue()));
                    }
                }
                colNumbers.Add(list);
            }
        }

        public BitmapData[,] Run()
        {
            pattern1();
            while (checkPainedCount())
            {
                pattern2();
                pattern3();
            }
            return _bitmapData;
        }

        private bool checkPainedCount()
        {
            int newPaintedCount = 0;
            for (int row = 0; row < rowNumbers.Count; row++)
            {
                for(int col = 0; col < colNumbers.Count; col++)
                {
                    if(_bitmapData[row, col].IsValid())
                    {
                        newPaintedCount++;
                    }
                }
            }
            if(oldPaintedCount == newPaintedCount)
            {
                return false;
            }
            oldPaintedCount = newPaintedCount;
            return true;
        }
    }
}
