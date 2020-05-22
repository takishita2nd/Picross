using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.logic
{
    partial class PicrossAnalyze
    {
        // 解析パターンその６
        // 端っこ（後ろ）が塗られていたら塗る。
        private void pattern6()
        {
            // Row
            pattern6Row();
            // Col
            pattern6Col();
        }

        private void pattern6Row()
        {
            int row = 0;
            foreach (var rowlist in rowNumbers)
            {
                if (rowlist.IsAnalyzed())
                {
                    row++;
                    continue;
                }
                var tempRowList = rowlist.Clone();
                tempRowList.AnalyzeDatas.Reverse();

                // 塗った場所が端っこならそこを塗る
                // マスクされていない部分をリスト化して取得する
                List<List<BitmapData>> data = new List<List<BitmapData>>();
                {
                    List<BitmapData> dataList = new List<BitmapData>();
                    for (int col = 0; col < colNumbers.Count; col++)
                    {
                        if (_bitmapData[row, col].IsMasked() == false)
                        {
                            dataList.Add(_bitmapData[row, col]);
                        }
                        else
                        {
                            if (dataList.Count != 0)
                            {
                                data.Add(dataList);
                                dataList = new List<BitmapData>();
                            }
                        }
                    }
                    if (dataList.Count != 0)
                    {
                        data.Add(dataList);
                    }

                    if (data.Count == 0)
                    {
                        row++;
                        continue;
                    }
                }

                int rowNumberIndex = 0;
                foreach (var dataList in data)
                {
                    // 端っこが塗られているか？
                    dataList.Reverse();
                    if (dataList[0].IsPainted())
                    {
                        // すでに処理済みか？
                        if (tempRowList.AnalyzeDatas[rowNumberIndex].IsAnalyzed())
                        {
                            rowNumberIndex++;
                            if (rowNumberIndex >= tempRowList.AnalyzeDatas.Count)
                            {
                                break;
                            }
                            continue;
                        }

                        // 数字に従ってマスを塗る
                        int count = 0;
                        foreach (var s in dataList)
                        {
                            if (count < tempRowList.AnalyzeDatas[rowNumberIndex].Value)
                            {
                                s.Paint();
                                count++;
                            }
                            else
                            {
                                if (s.IsMasked() == false)
                                {
                                    s.Mask();
                                }
                                tempRowList.AnalyzeDatas[rowNumberIndex].Analyzed();
                                tempRowList.CheckAnalyze();
                                break;
                            }
                        }
                    }
                    rowNumberIndex++;
                    if (rowNumberIndex >= tempRowList.AnalyzeDatas.Count)
                    {
                        break;
                    }
                }
                row++;
            }
        }

        private void pattern6Col()
        {
            int col = 0;
            foreach (var collist in colNumbers)
            {
                if (collist.IsAnalyzed())
                {
                    col++;
                    continue;
                }

                // 塗った場所が端っこならそこを塗る
                // マスクされていない部分をリスト化して取得する
                List<List<BitmapData>> data = new List<List<BitmapData>>();
                {
                    List<BitmapData> dataList = new List<BitmapData>();
                    for (int row = 0; row < rowNumbers.Count; row++)
                    {
                        if (_bitmapData[row, col].IsMasked() == false)
                        {
                            dataList.Add(_bitmapData[row, col]);
                        }
                        else
                        {
                            if (dataList.Count != 0)
                            {
                                data.Add(dataList);
                                dataList = new List<BitmapData>();
                            }
                        }
                    }
                    if (dataList.Count != 0)
                    {
                        data.Add(dataList);
                    }

                    if (data.Count == 0)
                    {
                        col++;
                        continue;
                    }
                }

                int colNumberIndex = 0;
                data.Reverse();
                foreach (var dataList in data)
                {
                    // 端っこが塗られているか？
                    dataList.Reverse();
                    if (dataList[0].IsPainted())
                    {
                        // すでに処理済みか？
                        if (collist.AnalyzeDatas[colNumberIndex].IsAnalyzed())
                        {
                            colNumberIndex++;
                            if (colNumberIndex >= collist.AnalyzeDatas.Count)
                            {
                                break;
                            }
                            continue;
                        }

                        // 数字に従ってマスを塗る
                        int count = 0;
                        foreach (var s in dataList)
                        {
                            if (count < collist.AnalyzeDatas[colNumberIndex].Value)
                            {
                                s.Paint();
                                count++;
                            }
                            else
                            {
                                if (s.IsMasked() == false)
                                {
                                    s.Mask();
                                }
                                collist.AnalyzeDatas[colNumberIndex].Analyzed();
                                collist.CheckAnalyze();
                                break;
                            }
                        }
                    }
                    colNumberIndex++;
                    if(colNumberIndex >= collist.AnalyzeDatas.Count)
                    {
                        break;
                    }
                }
                col++;
            }
        }
    }
}
