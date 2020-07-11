using Newtonsoft.Json;
using Picross.logic;
using Picross.ui;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross
{
    class FileAccess
    {
        private const string _filename = "save.dat";

        public static void Save(List<List<NumberSquare>> row, List<List<NumberSquare>> col)
        {
            if (File.Exists(_filename) == true)
            {
                File.Delete(_filename);
            }
            Data data = new Data();
            data.RowData = new List<List<int>>();
            foreach(var r in row)
            {
                List<int> list = new List<int>();
                foreach(var s in r)
                {
                    if(s.GetValue() != 0)
                    {
                        list.Add(s.GetValue());
                    }
                }
                data.RowData.Add(list);
            }
            data.ColData = new List<List<int>>();
            foreach (var c in col)
            {
                List<int> list = new List<int>();
                foreach (var s in c)
                {
                    if (s.GetValue() != 0)
                    {
                        list.Add(s.GetValue());
                    }
                }
                data.ColData.Add(list);
            }
            var str = JsonConvert.SerializeObject(data);
            using (var stream = new StreamWriter(_filename, true))
            {
                stream.Write(str);
            }
        }

        public static void Load(ref List<List<NumberSquare>> rowList, ref List<List<NumberSquare>> colList)
        {
            foreach(var r in rowList)
            {
                foreach(var s in r)
                {
                    asd.Engine.RemoveObject2D(s.getBackTexture());
                    asd.Engine.RemoveObject2D(s.getTextObject());
                }
            }
            foreach (var c in colList)
            {
                foreach (var s in c)
                {
                    asd.Engine.RemoveObject2D(s.getBackTexture());
                    asd.Engine.RemoveObject2D(s.getTextObject());
                }
            }

            string str = string.Empty;
            using (var stream = new StreamReader(_filename, true))
            {
                str = stream.ReadToEnd();
            }
            var data = JsonConvert.DeserializeObject<Data>(str);
            rowList = new List<List<NumberSquare>>();
            {
                int row = 0;
                foreach (var r in data.RowData)
                {
                    List<NumberSquare> list = new List<NumberSquare>();
                    int col = -1;
                    foreach (var v in r)
                    {
                        var s = new NumberSquare(row, col);
                        s.SetValue(v.ToString());
                        asd.Engine.AddObject2D(s.getBackTexture());
                        asd.Engine.AddObject2D(s.getTextObject());
                        list.Add(s);
                        col--;
                    }
                    {
                        var s = new NumberSquare(row, col);
                        s.SetValue("0");
                        asd.Engine.AddObject2D(s.getBackTexture());
                        asd.Engine.AddObject2D(s.getTextObject());
                        list.Add(s);
                    }
                    rowList.Add(list);
                    row++;
                }
            }
            colList = new List<List<NumberSquare>>();
            {
                int col = 0;
                foreach (var c in data.ColData)
                {
                    List<NumberSquare> list = new List<NumberSquare>();
                    int row = -1;
                    foreach (var v in c)
                    {
                        var s = new NumberSquare(row, col);
                        s.SetValue(v.ToString());
                        asd.Engine.AddObject2D(s.getBackTexture());
                        asd.Engine.AddObject2D(s.getTextObject());
                        list.Add(s);
                        row--;
                    }
                    {
                        var s = new NumberSquare(row, col);
                        s.SetValue("0");
                        asd.Engine.AddObject2D(s.getBackTexture());
                        asd.Engine.AddObject2D(s.getTextObject());
                        list.Add(s);
                    }
                    colList.Add(list);
                    col++;
                }
            }
        }

        public static void Load(string filename, ref List<List<NumberSquare>> rowList, ref List<List<NumberSquare>> colList)
        {
            string str = string.Empty;
            using (var stream = new StreamReader(filename, true))
            {
                str = stream.ReadToEnd();
            }
            var data = JsonConvert.DeserializeObject<Data>(str);
            rowList = new List<List<NumberSquare>>();
            {
                int row = 0;
                foreach (var r in data.RowData)
                {
                    List<NumberSquare> list = new List<NumberSquare>();
                    int col = -1;
                    foreach (var v in r)
                    {
                        var s = new NumberSquare(row, col);
                        s.SetValue(v.ToString());
                        list.Add(s);
                        col--;
                    }
                    rowList.Add(list);
                    row++;
                }
            }
            colList = new List<List<NumberSquare>>();
            {
                int col = 0;
                foreach (var c in data.ColData)
                {
                    List<NumberSquare> list = new List<NumberSquare>();
                    int row = -1;
                    foreach (var v in c)
                    {
                        var s = new NumberSquare(row, col);
                        s.SetValue(v.ToString());
                        list.Add(s);
                        row--;
                    }
                    colList.Add(list);
                    col++;
                }
            }
        }

        private const string _outputfile = "output.dat";
        public static void Output(BitmapData[,] bitmapDatas, int row, int col)
        {
            if (File.Exists(_outputfile) == true)
            {
                File.Delete(_outputfile);
            }

            using (var stream = new StreamWriter(_outputfile, true))
            {
                for(int r = 0; r < row; r++)
                {
                    for(int c = 0; c < col; c++)
                    {
                        if(bitmapDatas[r, c].IsPainted())
                        {
                            stream.Write(1);
                        }
                        else
                        {
                            stream.Write(0);
                        }
                    }
                    stream.Write("\r\n");
                }
            }
        }

        public static string AnswerLoad(string filename)
        {
            string str = string.Empty;
            using (var stream = new StreamReader(filename, true))
            {
                str = stream.ReadToEnd();
            }
            return str;
        }
    }
}
