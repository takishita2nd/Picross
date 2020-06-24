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
        // 解析パターンその１４
        // 数字と空いているマスを照合して塗る
        private void pattern14()
        {
            // Row
            pattern14Row();
            // Col
            pattern14Col();
        }

        private void pattern14Row()
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
                List<AnalyzeData> aData = new List<AnalyzeData>();
                foreach(var data in rowlist.AnalyzeDatas)
                {
                    if (data.IsAnalyzed())
                    {
                        continue;
                    }
                    aData.Add(data);
                }

                // 対象となるマスを抽出する
                List<List<BitmapData>> bitmapLists = extractTargetBitmapListsCol(row);
                bitmapLists.Reverse();

                // 数字とマスを照合する
                if (bitmapLists.Count != aData.Count)
                {
                    row++;
                    continue;
                }

                AnalyzeData maxData = null;
                int remi = 0;
                for(int i = 0; i < aData.Count; i++)
                {
                    if (maxData == null)
                    {
                        maxData = aData[i];
                    }
                    else
                    {
                        if(maxData.Value < aData[i].Value)
                        {
                            remi = i;
                        }
                    }
                }
                if(maxData != null)
                {
                    if (bitmapLists[remi].Count == maxData.Value)
                    {
                        foreach (var b in bitmapLists[remi])
                        {
                            b.Paint();
                        }
                        maxData.Analyzed();
                    }
                }
                row++;
            }
        }

        private void pattern14Col()
        {
            int col = 0;
            foreach (var collist in colNumbers)
            {
                if (collist.IsAnalyzed())
                {
                    col++;
                    continue;
                }

                // 有効な数字を取り出す
                List<AnalyzeData> aData = new List<AnalyzeData>();
                foreach (var data in collist.AnalyzeDatas)
                {
                    if (data.IsAnalyzed())
                    {
                        continue;
                    }
                    aData.Add(data);
                }

                // 対象となるマスを抽出する
                List<List<BitmapData>> bitmapLists = extractTargetBitmapListsRow(col);
                bitmapLists.Reverse();

                // 数字とマスを照合する
                if (bitmapLists.Count != aData.Count)
                {
                    col++;
                    continue;
                }

                AnalyzeData maxData = null;
                int remi = 0;
                for (int i = 0; i < aData.Count; i++)
                {
                    if (maxData == null)
                    {
                        maxData = aData[i];
                    }
                    else
                    {
                        if (maxData.Value < aData[i].Value)
                        {
                            remi = i;
                        }
                    }
                }
                if (maxData != null)
                {
                    if (bitmapLists[remi].Count == maxData.Value)
                    {
                        foreach (var b in bitmapLists[remi])
                        {
                            b.Paint();
                        }
                        maxData.Analyzed();
                    }
                }
                col++;
            }
        }
    }
}
