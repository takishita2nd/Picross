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
                rowlist.AnalyzeDatas.Reverse();

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
                    rowlist.AnalyzeDatas.Reverse();
                    row++;
                    continue;
                }

                // 対象となるマスを抽出する
                List<List<BitmapData>> bitmapLists = extractTargetBitmapListsCol(row);
                if (bitmapLists.Count != 1)
                {
                    rowlist.AnalyzeDatas.Reverse();
                    row++;
                    continue;
                }

                var bitmaplist = bitmapLists[0];
                if(bitmaplist.Count <= 1)
                {
                    rowlist.AnalyzeDatas.Reverse();
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
                    if (bitmaplist.Count > smallValue)
                    {
                        bitmaplist.RemoveRange(bitmaplist.Count - (smallValue + 1), smallValue + 1);
                    }
                }
                else
                {
                    bigValue = values[1];
                    smallValue = values[0];
                    if (bitmaplist.Count > smallValue)
                    {
                        bitmaplist.RemoveRange(0, smallValue + 1);
                    }
                }

                paintCenter(bigValue, ref bitmaplist);

                rowlist.AnalyzeDatas.Reverse();
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
                collist.AnalyzeDatas.Reverse();

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
                    collist.AnalyzeDatas.Reverse();
                    col++;
                    continue;
                }

                // 対象となるマスを抽出する
                List<List<BitmapData>> bitmapLists = extractTargetBitmapListsRow(col);
                if (bitmapLists.Count != 1)
                {
                    collist.AnalyzeDatas.Reverse();
                    col++;
                    continue;
                }

                var bitmaplist = bitmapLists[0];
                if (bitmaplist.Count <= 1)
                {
                    collist.AnalyzeDatas.Reverse();
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
                    if (bitmaplist.Count > smallValue)
                    {
                        bitmaplist.RemoveRange(bitmaplist.Count - (smallValue + 1), smallValue + 1);
                    }
                }
                else
                {
                    bigValue = values[1];
                    smallValue = values[0];
                    if(bitmaplist.Count > smallValue)
                    {
                        bitmaplist.RemoveRange(0, smallValue + 1);
                    }
                }

                paintCenter(bigValue, ref bitmaplist);

                collist.AnalyzeDatas.Reverse();
                col++;
            }
        }
    }
}
