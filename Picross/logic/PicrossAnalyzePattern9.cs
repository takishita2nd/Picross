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
        // 解析パターンその９
        // 中央の塗った場所から、塗れない場所をマスクする
        private void pattern9()
        {
            // Row
            pattern9Row();
            // Col
            pattern9Col();
        }

        private void pattern9Row()
        {
            int row = 0;
            foreach (var rowlist in rowNumbers)
            {
                if (rowlist.IsAnalyzed())
                {
                    row++;
                    continue;
                }

                // 有効な数字で一番大きいものを取り出す
                if(rowlist.AnalyzeDatas.Count != 1)
                {
                    row++;
                    continue;
                }
                int value = rowlist.AnalyzeDatas[0].Value;

                // 対象となるマスを抽出する
                List<List<BitmapData>> bitmapLists = new List<List<BitmapData>>();
                List<BitmapData> bitmaplist = new List<BitmapData>();
                for (int col = 0; col < colNumbers.Count; col++)
                {
                    // マスクとマスクの間が全て塗られていたら、そこは対象としない
                    if (_bitmapData[row, col].IsMasked())
                    {
                        if(bitmaplist.Count != 0)
                        {
                            bool done = true;
                            foreach(var bitmap in bitmaplist)
                            {
                                if(bitmap.IsPainted() == false)
                                {
                                    done = false;
                                }
                            }
                            if(done == false)
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
                if(bitmapLists.Count != 1)
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
                // 左側をマスク
                for(int col = 0; col < rightCol - value; col++)
                {
                    if(_bitmapData[row,col].IsValid() == false)
                    {
                        _bitmapData[row, col].Mask();
                    }
                }

                // 右側をマスク
                for (int col = leftCol + value; col < colNumbers.Count; col++)
                {
                    if (_bitmapData[row, col].IsValid() == false)
                    {
                        _bitmapData[row, col].Mask();
                    }
                }

                row++;
            }
        }

        private void pattern9Col()
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
                if (collist.AnalyzeDatas.Count != 1)
                {
                    col++;
                    continue;
                }
                int value = collist.AnalyzeDatas[0].Value;

                // 対象となるマスを抽出する
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
                // 左側をマスク
                for (int row = 0; row < downRow - value; row++)
                {
                    if (_bitmapData[row, col].IsValid() == false)
                    {
                        _bitmapData[row, col].Mask();
                    }
                }

                // 右側をマスク
                for (int row = topRow + value; row < rowNumbers.Count; row++)
                {
                    if (_bitmapData[row, col].IsValid() == false)
                    {
                        _bitmapData[row, col].Mask();
                    }
                }

                col++;
            }
        }
    }
}
