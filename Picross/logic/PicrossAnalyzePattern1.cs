using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.logic
{
    partial class PicrossAnalyze
    {
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
                foreach (var v in rowlist.AnalyzeDatas)
                {
                    total += v.Value;
                }
                total += rowlist.AnalyzeDatas.Count - 1;
                if (total == colNumbers.Count)
                {
                    // 塗れるマス確定
                    // リストを反転コピー
                    List<int> revRowList = new List<int>();
                    foreach (var v in rowlist.AnalyzeDatas)
                    {
                        revRowList.Add(v.Value);
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
                        if (col + c < colNumbers.Count)
                        {
                            _bitmapData[row, col + c].Mask();
                            c++;
                        }
                        col += c;
                    }
                    rowlist.Analyzed();
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
                foreach (var v in collist.AnalyzeDatas)
                {
                    total += v.Value;
                }
                total += collist.AnalyzeDatas.Count - 1;
                if (total == colNumbers.Count)
                {
                    // 塗れるマス確定
                    // リストを反転コピー
                    List<int> revColList = new List<int>();
                    foreach (var v in collist.AnalyzeDatas)
                    {
                        revColList.Add(v.Value);
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
                    collist.Analyzed();
                }
                col++;
            }
        }
    }
}
