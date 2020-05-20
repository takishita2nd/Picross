using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.logic
{
    partial class PicrossAnalyze
    {
        class Pattern4Data
        {
            public int Index;
            public int Value;
            public Pattern4Data(int index, int value)
            {
                Index = index;
                Value = value;
            }
        }

        // 解析パターンその４
        // 空いているマスが少なくて塗れないマスをマスクする
        private void pattern4()
        {
            // Row
            pattern4Row();
            // Col
            pattern4Col();
        }

        private void pattern4Row()
        {
            int row = 0;
            foreach (var rowlist in rowNumbers)
            {
                if (rowlist.IsAnalyzed())
                {
                    row++;
                    continue;
                }

                // マスクされていない連続したマスを数える
                int col = 0;
                int rememberCol = colNumbers.Count;
                int count = 0;
                List<Pattern4Data> countList = new List<Pattern4Data>();
                while (col < colNumbers.Count)
                {
                    if (_bitmapData[row, col].IsMasked() == false)
                    {
                        count++;
                        if(rememberCol == colNumbers.Count)
                        {
                            rememberCol = col;
                        }
                    }
                    else
                    {
                        if (count != 0)
                        {
                            countList.Add(new Pattern4Data(rememberCol, count));
                            count = 0;
                            rememberCol = colNumbers.Count;
                        }
                    }
                    col++;
                    if (col == colNumbers.Count)
                    {
                        if (count != 0)
                        {
                            countList.Add(new Pattern4Data(rememberCol, count));
                        }
                    }
                }

                // 数字の中で一番小さい値を取得する
                int val = colNumbers.Count;
                foreach(var s in rowlist.AnalyzeDatas)
                {
                    if(val > s.Value)
                    {
                        val = s.Value;
                    }
                }

                // マスクされていない連続したマスの数が数字より小さい場合はマスクする
                foreach(var data in countList)
                {
                    if(val > data.Value)
                    {
                        for(int i = 0; i< data.Value; i++)
                        {
                            if(_bitmapData[row, data.Index + i].IsValid() == false)
                            {
                                _bitmapData[row, data.Index + i].Mask();
                            }
                        }
                    }
                }
                row++;
            }
        }

        private void pattern4Col()
        {
            int col = 0;
            foreach (var collist in colNumbers)
            {
                if (collist.IsAnalyzed())
                {
                    col++;
                    continue;
                }

                // マスクされていない連続したマスを数える
                int row = 0;
                int rememberRow = rowNumbers.Count;
                int count = 0;
                List<Pattern4Data> countList = new List<Pattern4Data>();
                while (row < rowNumbers.Count)
                {
                    if (_bitmapData[row, col].IsMasked() == false)
                    {
                        count++;
                        if(rememberRow == rowNumbers.Count)
                        {
                            rememberRow = row;
                        }
                    }
                    else
                    {
                        if (count != 0)
                        {
                            countList.Add(new Pattern4Data(rememberRow, count));
                            count = 0;
                            rememberRow = rowNumbers.Count;
                        }
                    }
                    row++;
                    if (row == rowNumbers.Count)
                    {
                        if (count != 0)
                        {
                            countList.Add(new Pattern4Data(rememberRow, count));
                        }
                    }
                }

                // 数字の中で一番小さい値を取得する
                int val = rowNumbers.Count;
                foreach(var s in collist.AnalyzeDatas)
                {
                    if(val > s.Value)
                    {
                        val = s.Value;
                    }
                }

                // マスクされていない連続したマスの数が数字より小さい場合はマスクする
                foreach(var data in countList)
                {
                    if(val > data.Value)
                    {
                        for(int i = 0; i< data.Value; i++)
                        {
                            if(_bitmapData[data.Index + i, col].IsValid() == false)
                            {
                                _bitmapData[data.Index + i, col].Mask();
                            }
                        }
                    }
                }
                col++;
            }
        }
    }
}
