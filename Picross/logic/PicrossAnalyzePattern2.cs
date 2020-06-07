using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.logic
{
    partial class PicrossAnalyze
    {
        // 解析パターンその２
        private void pattern2()
        {
            // Row
            pattern2Row();
            // Col
            pattern2Col();
        }

        private void pattern2Row()
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
                int count = 0;
                List<int> countList = new List<int>();
                while (col < colNumbers.Count)
                {
                    if (_bitmapData[row, col].IsMasked() == false)
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
                        if(count != 0)
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
                    // 開いているところを塗る
                    col = 0;
                    while (col < colNumbers.Count)
                    {
                        if (_bitmapData[row, col].IsValid() == false)
                        {
                            _bitmapData[row, col].Paint();
                        }
                        col++;
                    }
                    rowlist.Analyzed();
                }
                row++;
            }
        }

        private void pattern2Col()
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
                int count = 0;
                List<int> countList = new List<int>();
                while (row < rowNumbers.Count)
                {
                    if (_bitmapData[row, col].IsMasked() == false)
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
                        if (count != 0)
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
                    // 開いているところを塗る
                    row = 0;
                    while (row < rowNumbers.Count)
                    {
                        if (_bitmapData[row, col].IsValid() == false)
                        {
                            _bitmapData[row, col].Paint();
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
