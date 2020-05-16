﻿using Picross.ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.logic
{
    class PicrossAnalyze
    {
        private List<AnalyzeListData> rowNumbers;
        private List<AnalyzeListData> colNumbers;
        private BitmapData[,] _bitmapData;

        public PicrossAnalyze(List<List<NumberSquare>> rowNumberSquares, List<List<NumberSquare>> colNumberSquares)
        {
            _bitmapData = new BitmapData[rowNumberSquares.Count, colNumberSquares.Count];
            for (int r = 0; r < rowNumberSquares.Count; r++)
            {
                for (int c = 0; c < colNumberSquares.Count; c++)
                {
                    _bitmapData[r, c] = new BitmapData(r, c);
                }
            }

            rowNumbers = new List<AnalyzeListData>();
            foreach (var rowList in rowNumberSquares)
            {
                AnalyzeListData list = new AnalyzeListData();
                foreach (var s in rowList)
                {
                    if (s.GetValue() != 0)
                    {
                        list.AnalyzeDatas.Add(new AnalyzeData(s.GetValue()));
                    }
                }
                rowNumbers.Add(list);
            }

            colNumbers = new List<AnalyzeListData>();
            foreach (var colList in colNumberSquares)
            {
                AnalyzeListData list = new AnalyzeListData();
                foreach (var s in colList)
                {
                    if (s.GetValue() != 0)
                    {
                        list.AnalyzeDatas.Add(new AnalyzeData(s.GetValue()));
                    }
                }
                colNumbers.Add(list);
            }
        }

        public BitmapData[,] Run()
        {
            pattern1();
            pattern2();
            return _bitmapData;
        }

        // 解析パターンその１
        private void pattern1()
        {
            // Row
            pattern1Row();
            // Col
            pattern1Col();
        }

        // 解析パターンその２
        private void pattern2()
        {
            // Row
            pattern2Row();
            // Col
            pattern2Col();
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
                    if(col == colNumbers.Count)
                    {
                        countList.Add(count);
                    }
                }
                // 数えた数字が全て一致すれば確定とする
                bool result = true;
                if (rowlist.AnalyzeDatas.Count != countList.Count)
                {
                    row++;
                    continue;
                }
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
                        if(_bitmapData[row, col].IsValid() == false)
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
                        countList.Add(count);
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
