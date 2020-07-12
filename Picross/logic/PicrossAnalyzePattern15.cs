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
        // 解析パターンその１５
        // 数字と空いているマスを照合して塗る
        private void pattern15()
        {
            // Row
            pattern15Row();
            // Col
            pattern15Col();
        }

        private void pattern15Row()
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

                for(int i = 0; i < aData.Count; i++)
                {
                    int count = 0;
                    // 塗られているマスを数える
                    foreach (var b in bitmapLists[i])
                    {
                        if (b.IsPainted())
                        {
                            count++;
                        }
                    }
                    // 塗られているマスと数字が一定している場合は空きマスをマスクする
                    if(aData[i].Value == count)
                    {
                        foreach(var b in bitmapLists[i])
                        {
                            if(b.IsValid() == false)
                            {
                                b.Mask();
                            }
                        }
                    }
                }
                row++;
            }
        }

        private void pattern15Col()
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

                for (int i = 0; i < aData.Count; i++)
                {
                    int count = 0;
                    // 塗られているマスを数える
                    foreach (var b in bitmapLists[i])
                    {
                        if (b.IsPainted())
                        {
                            count++;
                        }
                    }
                    // 塗られているマスと数字が一定している場合は空きマスをマスクする
                    if (aData[i].Value == count)
                    {
                        foreach (var b in bitmapLists[i])
                        {
                            if (b.IsValid() == false)
                            {
                                b.Mask();
                            }
                        }
                    }
                }
                col++;
            }
        }
    }
}
