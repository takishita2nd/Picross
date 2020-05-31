using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.logic
{
    partial class PicrossAnalyze
    {
        // 解析パターンその３
        // すでに塗りつぶされたマスからマスクをする
        private void pattern3()
        {
            // Row
            pattern3Row();
            // Col
            pattern3Col();
        }

        private void pattern3Row()
        {
            int row = 0;
            foreach (var rowlist in rowNumbers)
            {
                if (rowlist.IsAnalyzed())
                {
                    row++;
                    continue;
                }

                // 塗ったマスを数える
                int col = 0;
                int count = 0;
                List<int> countList = new List<int>();
                while (col < colNumbers.Count)
                {
                    if (_bitmapData[row, col].IsPainted())
                    {
                        count++;
                    }
                    else
                    {
                        if (count != 0)
                        {
                            countList.Add(count);
                            count = 0;
                        }
                    }
                    col++;
                    if (col == colNumbers.Count)
                    {
                        if (count != 0)
                        {
                            countList.Add(count);
                        }
                    }
                }
                // 数えた数字が全て一致すれば確定とする
                bool result = true;
                if (rowlist.AnalyzeDatas.Count != countList.Count)
                {
                    row++;
                    continue;
                }
                countList.Reverse();
                for (int i = 0; i < countList.Count; i++)
                {
                    if (rowlist.AnalyzeDatas[i].Value != countList[i])
                    {
                        result = false;
                    }
                }
                if (result)
                {
                    // 開いているところをマスクする
                    col = 0;
                    while (col < colNumbers.Count)
                    {
                        if (_bitmapData[row, col].IsValid() == false)
                        {
                            _bitmapData[row, col].Mask();
                        }
                        col++;
                    }
                    rowlist.Analyzed();
                }
                row++;
            }
        }

        private void pattern3Col()
        {
            int col = 0;
            foreach (var collist in colNumbers)
            {
                if (collist.IsAnalyzed())
                {
                    col++;
                    continue;
                }

                // 塗ったマスを数える
                int row = 0;
                int count = 0;
                List<int> countList = new List<int>();
                while (row < rowNumbers.Count)
                {
                    if (_bitmapData[row, col].IsPainted())
                    {
                        count++;
                    }
                    else
                    {
                        if (count != 0)
                        {
                            countList.Add(count);
                            count = 0;
                        }
                    }
                    row++;
                    if (row == rowNumbers.Count)
                    {
                        if(count != 0)
                        {
                            countList.Add(count);
                        }
                    }
                }
                // 数えた数字が全て一致すれば確定とする
                bool result = true;
                if (collist.AnalyzeDatas.Count != countList.Count)
                {
                    col++;
                    continue;
                }
                countList.Reverse();
                for (int i = 0; i < countList.Count; i++)
                {
                    if (collist.AnalyzeDatas[i].Value != countList[i])
                    {
                        result = false;
                    }
                }
                if (result)
                {
                    // 開いているところをマスクする
                    row = 0;
                    while (row < rowNumbers.Count)
                    {
                        if (_bitmapData[row, col].IsValid() == false)
                        {
                            _bitmapData[row, col].Mask();
                        }
                        row++;
                    }
                    collist.Analyzed();
                }
                col++;
            }
        }
    }
}
