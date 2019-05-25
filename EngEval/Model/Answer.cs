using EngEval.Common.DateTransform;
using Http;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows;

namespace EngEval.Model
{
    //存储答案
    [Serializable]
    public class Answer
    {
        public Answer()
        {
            records = new List<Record>();
        }

        public Account account;//用户
        public long start_time;//开始测试时间
        public long end_time;//结束测试时间
        public List<Record> records;//每道题目答题情况
        public ListeningTest test; //试题
        public string states;

        //生成向后台传递的信息
        public Dictionary<string, string> GetParamUpload()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("tid", test.id);
            parameters.Add("uid", account.id.ToString());
            parameters.Add("type", "0");
            parameters.Add("states", states);

            parameters.Add("records", FormatRecordsString());
            parameters.Add("reasons", FormatReasonsString());
            parameters.Add("timecon", FormatTimeConString());
            parameters.Add("timereact", FormatTimeReactString());

            DateTime st = DateTransform.ConvertLongToDateTime(start_time);
            parameters.Add("start_time",st.ToString("yyyy-M-d H:m:s"));
            DateTime et = DateTransform.ConvertLongToDateTime(end_time);
            parameters.Add("end_time", et.ToString("yyyy-M-d H:m:s"));
            return parameters;
        }

        //生成reasons串
        public string FormatReasonsString()
        {
            List<string> strs = new List<string>();
            foreach (Record record in records)
            {
                strs.Add("");
            }
            string re = JsonConvert.SerializeObject(strs);
            return re.Substring(1, re.Length - 2);
        }

        //生成records串
        public string FormatRecordsString()
        {
            List<string> strs = new List<string>();
            foreach (Record record in records)
            {
                string str = "";
                foreach(int a in record.answers)
                {
                    str += "||" + a.ToString();
                }
                strs.Add(str);
            }
            string re = JsonConvert.SerializeObject(strs);
            return re.Substring(1, re.Length - 2);
        }

        //生成timecon串
        public string FormatTimeConString()
        {
            List<long> timecon = new List<long>();
            foreach(Record record in records)
            {
                timecon.Add(record.answer_time - record.start_time);
            }
            string re = JsonConvert.SerializeObject(timecon);
            return re.Substring(1, re.Length - 2);
        }

        //生成timereact串
        public string FormatTimeReactString()
        {
            List<long> timereact = new List<long>();
            foreach (Record record in records)
            {
                timereact.Add(record.end_time - record.answer_time);
            }
            string re = JsonConvert.SerializeObject(timereact);
            return re.Substring(1, re.Length - 2);
        }

        //保存在本地临时文件
        public void SaveLocal()
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            //提交试题答题记录
            Dictionary<string, string> parameters = GetParamUpload();
            bool substate = false;
            while (!substate)
            {
                substate = SubmitAnswer(parameters);
            }
        }

        //提交答案
        private bool SubmitAnswer(Dictionary<string, string> parameters)
        {
            Boolean isSuccess = false;
            string rtext = HttpRequestHelper.HttpGet(Setting.BASE_URL + "test/tmpFinishTest", parameters, ref isSuccess);
            return isSuccess;
        }

        //读取本地临时文件
        public Answer LoadAnswer()
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("tid", mainwin.listeningTest.id);
            parameters.Add("uid", mainwin.user.id.ToString());
            Boolean isSuccess = false;
            string rtext="";
            while (!isSuccess)
                rtext = HttpRequestHelper.HttpGet(Setting.BASE_URL + "test/testResult", parameters, ref isSuccess);
            Answer answer = JsonConvert.DeserializeObject<Answer>(rtext);
            if (answer == null)
                return null;
            foreach (Record rec in answer.records)
                rec.DeFormatResult();
            answer.account = mainwin.User;
            answer.test = mainwin.ListeningTest;
            return answer;
        }
    }

    //存储答题记录
    [Serializable]
    public class Record
    {
        public long start_time;
        public long answer_time;
        public long timecon;
        public long timereact;
        public long end_time;
        public List<int> answers;
        public string result;

        public Record()
        {
            answers = new List<int>();
        }

        public void DeFormatResult()
        {
            start_time = 0;
            answer_time = timecon;
            end_time = answer_time + timereact;
            string[] sArray = Regex.Split(result, @"|", RegexOptions.IgnoreCase);
            foreach (string i in sArray)
            {
                if(i != "" && i != null)
                {
                    try
                    {
                        answers.Add(int.Parse(i));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
        }
    }
}
