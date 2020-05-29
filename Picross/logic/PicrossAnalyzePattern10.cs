using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Picross.logic
{
    partial class PicrossAnalyze
    {
        // 解析パターンその１０
        // 中央の塗った場所から、塗れない場所をマスクする（数字が２個の場合）
        private void pattern10()
        {
            // Row
            pattern10Row();
            // Col
            pattern10Col();
        }

        private void pattern10Row()
        {
            int row = 0;
            foreach (var rowlist in rowNumbers)
            {
                if (rowlist.IsAnalyzed())
                {
                    row++;
                    continue;
                }

                // 有効な数字を取り出す
                List<int> values = new List<int>();
                foreach (var data in rowlist.AnalyzeDatas)
                {
                    if (data.IsAnalyzed())
                    {
                        continue;
                    }
                    values.Add(data.Value);
                }
                if (values.Count != 2)
                {
                    row++;
                    continue;
                }

                // 対象となるマスを抽出する
                List<List<BitmapData>> bitmapLists = extractTargetBitmapListsCol(row);
                if (bitmapLists.Count != 1)
                {
                    row++;
                    continue;
                }

                // 塗られている場所を特定
                int leftCol = colNumbers.Count;
                int rightCol = 0;
                bool painted = false;
                foreach (var bitmap in bitmapLists[0])
                {
                    if(bitmap.IsPainted())
                    {
                        leftCol = bitmap.Col;
                        painted = true;
                    }
                    else
                    {
                        if (painted)
                        {
                            rightCol = bitmap.Col;
                            break;
                        }
                    }
                }

                var bitmaplist = bitmapLists[0];
                if(bitmaplist.Count <= 1)
                {
                    row++;
                    continue;
                }

                // 塗る対象は右側か、左側か？
                int bigValue = 0;
                int smallValue = 0;
                if (values[0] > values[1])
                {
                    bigValue = values[0];
                    smallValue = values[1];
                    bitmaplist.RemoveRange(0, smallValue + 1);
                }
                else
                {
                    bigValue = values[1];
                    smallValue = values[0];
                    bitmaplist.RemoveRange(bitmaplist.Count - (smallValue + 1), smallValue + 1);
                }

                paintCenter(bigValue, ref bitmaplist);

                row++;
            }
        }

        private void pattern10Col()
        {
            int col = 0;
            foreach (var collist in colNumbers)
            {
                if (collist.IsAnalyzed())
                {
                    col++;
                    continue;
                }

                // 有効な数字で一番大きいものを取り出す
                List<int> values = new List<int>();
                foreach (var data in collist.AnalyzeDatas)
                {
                    if (data.IsAnalyzed())
                    {
                        continue;
                    }
                    values.Add(data.Value);
                }
                if (values.Count != 2)
                {
                    col++;
                    continue;
                }

                // 対象となるマスを抽出する
                List<List<BitmapData>> bitmapLists = extractTargetBitmapListsRow(col);
                if (bitmapLists.Count != 1)
                {
                    col++;
                    continue;
                }

                // 塗られている場所を特定
                int topRow = rowNumbers.Count;
                int downRow = 0;
                bool painted = false;
                foreach (var bitmap in bitmapLists[0])
                {
                    if (bitmap.IsPainted())
                    {
                        topRow = bitmap.Row;
                        painted = true;
                    }
                    else
                    {
                        if (painted)
                        {
                            downRow = bitmap.Row;
                            break;
                        }
                    }
                }

                var bitmaplist = bitmapLists[0];
                if (bitmaplist.Count <= 1)
                {
                    col++;
                    continue;
                }

                // 塗る対象は右側か、左側か？
                int bigValue = 0;
                int smallValue = 0;
                if (values[0] > values[1])
                {
                    bigValue = values[0];
                    smallValue = values[1];
                    bitmaplist.RemoveRange(0, smallValue + 1);
                }
                else
                {
                    bigValue = values[1];
                    smallValue = values[0];
                    bitmaplist.RemoveRange(bitmaplist.Count - (smallValue + 1), smallValue + 1);
                }

                paintCenter(bigValue, ref bitmaplist);

                col++;
            }
        }
    }
}
