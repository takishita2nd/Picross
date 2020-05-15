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
        private List<List<int>> rowNumbers;
        private List<List<int>> colNumbers;
        private BitmapData[,] _bitmapData;

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
                    if(s.GetValue() != 0)
                    {
                        list.Add(s.GetValue());
                    }
                }
                rowNumbers.Add(list);
            }

            colNumbers = new List<List<int>>();
            foreach (var colList in colNumberSquares)
            {
                List<int> list = new List<int>();
                foreach (var s in colList)
                {
                    if (s.GetValue() != 0)
                    {
                        list.Add(s.GetValue());
                    }
                }
                colNumbers.Add(list);
            }
        }

        public BitmapData[,] Run()
        {
            pattern1();
            return _bitmapData;
        }

        // 解析パターンその１
        private void pattern1()
        {
            // Row
            pattern1Row();
            // Col
            pattern1Col();
        }

        /**
         * Rowに対して解析パターン１を適用
         */
        private void pattern1Row()
        {
            int row = 0;
            foreach (var rowlist in rowNumbers)
            {
                int total = 0;
                foreach (var v in rowlist)
                {
                    total += v;
                }
                total += rowlist.Count - 1;
                if (total == colNumbers.Count)
                {
                    // 塗れるマス確定
                    // リストを反転コピー
                    List<int> revRowList = new List<int>();
                    foreach (var v in rowlist)
                    {
                        revRowList.Add(v);
                    }
                    revRowList.Reverse();

                    int col = 0;
                    foreach (var v in revRowList)
                    {
                        int c;
                        for (c = 0; c < v; c++)
                        {
                            _bitmapData[row, col + c].Paint();
                        }
                        if(col + c < colNumbers.Count)
                        {
                            _bitmapData[row, col + c].Mask();
                            c++;
                        }
                        col += c;
                    }
                }
                row++;
            }
        }

        /**
         * Colに対して解析パターン１を適用
         */
        private void pattern1Col()
        {
            int col = 0;
            foreach (var collist in colNumbers)
            {
                int total = 0;
                foreach (var v in collist)
                {
                    total += v;
                }
                total += collist.Count - 1;
                if (total == colNumbers.Count)
                {
                    // 塗れるマス確定
                    // リストを反転コピー
                    List<int> revColList = new List<int>();
                    foreach (var v in collist)
                    {
                        revColList.Add(v);
                    }
                    revColList.Reverse();

                    int row = 0;
                    foreach (var v in revColList)
                    {
                        int r;
                        for (r = 0; r < v; r++)
                        {
                            _bitmapData[row + r, col].Paint();
                        }
                        if (row + r < rowNumbers.Count)
                        {
                            _bitmapData[row + r, col].Mask();
                            r++;
                        }
                        row += r;
                    }
                }
                col++;
            }
        }
    }
}
