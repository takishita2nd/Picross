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
        // 解析パターンその１１
        // すでに塗っている場所を解析済みにする
        private void pattern11()
        {
            // Row
            pattern11Row();
            // Col
            pattern11Col();
        }

        private void pattern11Row()
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

                // 塗られている連続したマスを数える
                List<List<BitmapData>> data = new List<List<BitmapData>>();
                {
                    List<BitmapData> dataList = new List<BitmapData>();
                    for (int col = 0; col < colNumbers.Count; col++)
                    {
                        if (_bitmapData[row, col].IsPainted() == true)
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
                        rowlist.AnalyzeDatas.Reverse();
                        continue;
                    }
                }

                List<BitmapData> targetDataList = null;
                // 最大の物を取得する
                foreach (var datalist in data)
                {
                    if(targetDataList == null)
                    {
                        targetDataList = datalist;
                    }
                    else if(targetDataList.Count < datalist.Count)
                    {
                        targetDataList = datalist;
                    }
                }

                AnalyzeData targetRowList = null;
                foreach(var rowdata in rowlist.AnalyzeDatas)
                {
                    if(targetRowList == null)
                    {
                        targetRowList = rowdata;
                    }
                    else if(targetRowList.Value < rowdata.Value)
                    {
                        targetRowList = rowdata;
                    }

                }

                if(targetDataList.Count == targetRowList.Value)
                {
                    //解析済みにする
                    targetRowList.Analyzed();
                    if(targetDataList[0].Col > 0)
                    {
                        if (_bitmapData[targetDataList[0].Row, targetDataList[0].Col - 1].IsValid() == false)
                        {
                            _bitmapData[targetDataList[0].Row, targetDataList[0].Col - 1].Mask();
                        }
                    }
                    if (targetDataList[targetDataList.Count - 1].Col < colNumbers.Count - 1)
                    {
                        if (_bitmapData[targetDataList[targetDataList.Count - 1].Row, targetDataList[targetDataList.Count - 1].Col + 1].IsValid() == false)
                        {
                            _bitmapData[targetDataList[targetDataList.Count - 1].Row, targetDataList[targetDataList.Count - 1].Col + 1].Mask();
                        }
                    }
                }

                rowlist.AnalyzeDatas.Reverse();
                row++;
            }
        }

        private void pattern11Col()
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

                // 塗られている連続したマスを数える
                List<List<BitmapData>> data = new List<List<BitmapData>>();
                {
                    List<BitmapData> dataList = new List<BitmapData>();
                    for (int row = 0; row < rowNumbers.Count; row++)
                    {
                        if (_bitmapData[row, col].IsPainted() == true)
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
                        collist.AnalyzeDatas.Reverse();
                        continue;
                    }
                }

                List<BitmapData> targetDataList = null;
                // 最大の物を取得する
                foreach (var datalist in data)
                {
                    if (targetDataList == null)
                    {
                        targetDataList = datalist;
                    }
                    else if (targetDataList.Count < datalist.Count)
                    {
                        targetDataList = datalist;
                    }
                }

                AnalyzeData targetColList = null;
                foreach (var coldata in collist.AnalyzeDatas)
                {
                    if (targetColList == null)
                    {
                        targetColList = coldata;
                    }
                    else if (targetColList.Value < coldata.Value)
                    {
                        targetColList = coldata;
                    }

                }

                if (targetDataList.Count == targetColList.Value)
                {
                    //解析済みにする
                    targetColList.Analyzed();
                    if (targetDataList[0].Row > 0)
                    {
                        if (_bitmapData[targetDataList[0].Row - 1, targetDataList[0].Col].IsValid() == false)
                        {
                            _bitmapData[targetDataList[0].Row - 1, targetDataList[0].Col].Mask();
                        }
                    }
                    if (targetDataList[targetDataList.Count - 1].Row < colNumbers.Count - 1)
                    {
                        if (_bitmapData[targetDataList[targetDataList.Count - 1].Row + 1, targetDataList[targetDataList.Count - 1].Col].IsValid() == false)
                        {
                            _bitmapData[targetDataList[targetDataList.Count - 1].Row + 1, targetDataList[targetDataList.Count - 1].Col].Mask();
                        }
                    }
                }

                collist.AnalyzeDatas.Reverse();
                col++;
            }
        }
    }
}
