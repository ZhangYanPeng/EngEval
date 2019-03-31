using EngEval.Common.DateTransform;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

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

        //生成向后台传递的信息
        public Dictionary<string, string> GetParamUpload()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("tid", test.id);
            parameters.Add("uid", account.id.ToString());
            parameters.Add("type", "0");

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

        //生成timecon串
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
            MemoryStream stream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, this);
            byte[] data = stream.ToArray();
            stream.Close();

            //创建文件
            string path = "TEMP/"+ test.testno.ToString() + "PROCESS.ini";
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(data);
            bw.Close();
            fs.Close();
        }

        //读取本地临时文件
        public Answer LoadAnswer()
        {
            //打开文件
            string path = "TEMP/" + test.testno.ToString() + "PROCESS.ini";
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read, 4 * 1024 * 1024, true);
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                object obj = bf.Deserialize(fileStream);
                fileStream.Close();
                return obj as Answer;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    //存储答题记录
    [Serializable]
    public class Record
    {
        public long start_time;
        public long answer_time;
        public long end_time;
        public List<int> answers;

        public Record()
        {
            answers = new List<int>();
        }
    }
}
