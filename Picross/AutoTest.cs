using Picross.ui;
using Picross.logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross
{
    class AutoTest
    {
        private const string QuestionFolder = "q";
        private const string AnswerFolder = "a";
        private const string ResultFolder = "r";
        private const string QuestionFile = "save";
        private const string AnswerFile = "output";
        private const string ResultFile = "result";

        public AutoTest()
        {

        }

        public void Run()
        {
            var files = Directory.EnumerateFiles(QuestionFolder, "*");
            foreach(var file in files)
            {
                string answerFile = file.Replace(QuestionFolder, AnswerFolder).Replace(QuestionFile, AnswerFile);
                string resultFile = file.Replace(QuestionFolder, ResultFolder).Replace(QuestionFile, ResultFile);
                if (File.Exists(resultFile))
                {
                    File.Delete(resultFile);
                }

                if (File.Exists(answerFile))
                {
                    List<List<NumberSquare>> rowNumberSquare = new List<List<NumberSquare>>();
                    List<List<NumberSquare>> colNumberSquare = new List<List<NumberSquare>>();
                    FileAccess.Load(file, ref rowNumberSquare, ref colNumberSquare);
                    PicrossAnalyze picross = new PicrossAnalyze(rowNumberSquare, colNumberSquare);
                    var ret = picross.Run();
                    string answer = string.Empty;
                    for(int r = 0; r < rowNumberSquare.Count; r++)
                    {
                        for(int c = 0; c < colNumberSquare.Count; c++)
                        {
                            if (ret[r, c].IsPainted())
                            {
                                answer += "1";
                            }
                            else
                            {
                                answer += "0";
                            }
                        }
                        answer += "\r\n";
                    }
                    string compare = FileAccess.AnswerLoad(answerFile);
                    string result;
                    if(compare.CompareTo(answer) == 0)
                    {
                        result = "OK";
                    }
                    else
                    {
                        result = answer;
                    }
                    using (var stream = new StreamWriter(resultFile, true))
                    {
                        stream.Write(result);
                    }
                }
            }
        }
    }
}
