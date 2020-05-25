using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.logic
{
    partial class PicrossAnalyze
    {
        // 解析パターンその７
        // 数字が１個で２マス塗られている場合はその間を塗る
        private void pattern7()
        {
            // Row
            pattern7Row();
            // Col
            pattern7Col();
        }

        private void pattern7Row()
        {
            int row = 0;
            foreach (var rowlist in rowNumbers)
            {
                if (rowlist.IsAnalyzed())
                {
                    row++;
                    continue;
                }

                if(rowlist.AnalyzeDatas.Count == 1)
                {
                    int value = rowlist.AnalyzeDatas[0].Value;
                    int startCol = 0;
                    int endCol = 0;
                    for (int col = 0; col < colNumbers.Count; col++)
                    {
                        if(_bitmapData[row, col].IsPainted() && startCol == 0)
                        {
                            startCol = col;
                        }
                        else if(_bitmapData[row, col].IsMasked() && startCol != 0)
                        {
                            break;
                        }
                        else if(_bitmapData[row, col].IsPainted() && startCol != 0)
                        {
                            endCol = col;
                        }
                    }

                    if(startCol== 0 || endCol == 0)
                    {
                        row++;
                        continue;
                    }

                    for(int col = startCol; col < endCol; col++)
                    {
                        _bitmapData[row, col].Paint();
                    }
                }
                row++;
            }
        }

        private void pattern7Col()
        {
            int col = 0;
            foreach (var collist in colNumbers)
            {
                if (collist.IsAnalyzed())
                {
                    col++;
                    continue;
                }

                if (collist.AnalyzeDatas.Count == 1)
                {
                    int value = collist.AnalyzeDatas[0].Value;
                    int startRow = 0;
                    int endRow = 0;
                    for (int row = 0; row < rowNumbers.Count; row++)
                    {
                        if (_bitmapData[row, col].IsPainted() && startRow == 0)
                        {
                            startRow = row;
                        }
                        else if (_bitmapData[row, col].IsMasked() && startRow != 0)
                        {
                            break;
                        }
                        else if (_bitmapData[row, col].IsPainted() && startRow != 0)
                        {
                            endRow = row;
                        }
                    }

                    if (startRow == 0 || endRow == 0)
                    {
                        col++;
                        continue;
                    }

                    for (int row = startRow; row < endRow; row++)
                    {
                        _bitmapData[row, col].Paint();
                    }
                }
                col++;
            }
        }
    }
}
