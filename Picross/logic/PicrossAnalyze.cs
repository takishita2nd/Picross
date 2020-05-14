using Picross.ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.logic
{
    class PicrossAnalyze
    {
        List<List<int>> rowNumbers;
        List<List<int>> cowNumbers;
        BitmapData[,] _bitmapData;

        public PicrossAnalyze(List<List<NumberSquare>> rowNumberSquares, List<List<NumberSquare>> colNumberSquares)
        {
            _bitmapData = new BitmapData[rowNumberSquares.Count, colNumberSquares.Count];
            for(int r = 0; r < rowNumberSquares.Count; r++)
            {
                for(int c = 0; c < colNumberSquares.Count; c++)
                {
                    _bitmapData[r, c] = new BitmapData(r, c);
                }
            }

            rowNumbers = new List<List<int>>();
            foreach(var rowList in rowNumberSquares)
            {
                List<int> list = new List<int>();
                foreach(var s in rowList)
                {
                    list.Add(s.GetValue());
                }
                rowNumbers.Add(list);
            }

            cowNumbers = new List<List<int>>();
            foreach (var colList in colNumberSquares)
            {
                List<int> list = new List<int>();
                foreach (var s in colList)
                {
                    list.Add(s.GetValue());
                }
                cowNumbers.Add(list);
            }
        }

        public bool[,] Run()
        {
            // モック
            bool[,] ret = new bool[10, 10] {
                {true, false, true, false, true, true, false, true, false, true },
                {true, false, true, false, true, true, false, true, false, true },
                {true, false, true, false, true, true, false, true, false, true },
                {true, false, true, false, true, true, false, true, false, true },
                {true, false, true, false, true, true, false, true, false, true },
                {true, false, true, false, true, true, false, true, false, true },
                {true, false, true, false, true, true, false, true, false, true },
                {true, false, true, false, true, true, false, true, false, true },
                {true, false, true, false, true, true, false, true, false, true },
                {true, false, true, false, true, true, false, true, false, true }
            };

            return ret;
        }
    }
}
