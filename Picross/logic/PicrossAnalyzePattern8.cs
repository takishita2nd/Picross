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
        // 解析パターンその８
        // 数字が大きい場合、真ん中の方を塗る
        private void pattern8()
        {
            // Row
            pattern8Row();
            // Col
            pattern8Col();
        }

        private void pattern8Row()
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
                foreach(var data in rowlist.AnalyzeDatas)
                {
                    if (data.IsAnalyzed())
                    {
                        continue;
                    }
                    values.Add(data.Value);
                }
                if(values.Count != 1)
                {
                    row++;
                    continue;
                }

                // 対象となるマスを抽出する
                List<List<BitmapData>> bitmapLists = extractTargetBitmapListsCol(row);
                if(bitmapLists.Count != 1)
                {
                    row++;
                    continue;
                }

                List<BitmapData> bitmaps = bitmapLists[0];
                paintCenter(values[0], ref bitmaps);
                row++;
            }
        }

        private void pattern8Col()
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
                List<int> values = new List<int>();
                foreach (var data in collist.AnalyzeDatas)
                {
                    if (data.IsAnalyzed())
                    {
                        continue;
                    }
                    values.Add(data.Value);
                }
                if (values.Count != 1)
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

                List<BitmapData> bitmaps = bitmapLists[0];
                paintCenter(values[0], ref bitmaps);

                col++;
            }
        }

        private List<List<BitmapData>> extractTargetBitmapListsCol(int row)
        {
            List<List<BitmapData>> bitmapLists = new List<List<BitmapData>>();
            List<BitmapData> bitmaplist = new List<BitmapData>();
            for (int col = 0; col < colNumbers.Count; col++)
            {
                // マスクとマスクの間が全て塗られていたら、そこは対象としない
                if (_bitmapData[row, col].IsMasked())
                {
                    if (bitmaplist.Count != 0)
                    {
                        bool done = true;
                        foreach (var bitmap in bitmaplist)
                        {
                            if (bitmap.IsPainted() == false)
                            {
                                done = false;
                            }
                        }
                        if (done == false)
                        {
                            bitmapLists.Add(bitmaplist);
                        }
                        bitmaplist = new List<BitmapData>();
                    }
                    continue;
                }
                bitmaplist.Add(_bitmapData[row, col]);
            }
            if (bitmaplist.Count != 0)
            {
                bitmapLists.Add(bitmaplist);
            }

            return bitmapLists;
        }

        private List<List<BitmapData>> extractTargetBitmapListsRow(int col)
        {
            List<List<BitmapData>> bitmapLists = new List<List<BitmapData>>();
            List<BitmapData> bitmaplist = new List<BitmapData>();
            for (int row = 0; row < rowNumbers.Count; row++)
            {
                // マスクとマスクの間が全て塗られていたら、そこは対象としない
                if (_bitmapData[row, col].IsMasked())
                {
                    if (bitmaplist.Count != 0)
                    {
                        bool done = true;
                        foreach (var bitmap in bitmaplist)
                        {
                            if (bitmap.IsPainted() == false)
                            {
                                done = false;
                            }
                        }
                        if (done == false)
                        {
                            bitmapLists.Add(bitmaplist);
                        }
                        bitmaplist = new List<BitmapData>();
                    }
                    continue;
                }
                bitmaplist.Add(_bitmapData[row, col]);
            }
            if (bitmaplist.Count != 0)
            {
                bitmapLists.Add(bitmaplist);
            }

            return bitmapLists;
        }

        private void paintCenter(int value, ref List<BitmapData> bitmaps)
        {
            if (value < bitmaps.Count && value * 2 > bitmaps.Count)
            {
                int paintNum = value * 2 - bitmaps.Count;
                int countMax = 0;
                if (paintNum % 2 == 1)
                {
                    countMax = paintNum / 2 + 1;
                }
                else
                {
                    countMax = paintNum / 2;
                }
                for (int count = -paintNum / 2; count < countMax; count++)
                {
                    bitmaps[bitmaps.Count / 2 + count].Paint();
                }
            }
        }
    }
}
