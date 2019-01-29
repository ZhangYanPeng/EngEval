using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngEval.Model
{
    //测试
    public class ListeningTest
    {
        public string id { get; set; }
        public int choose { get; set; } //选定测试 为1
        public int collect { get; set; } // 采集信息 1
        public string title { get; set; } //测试名称
        public String Description { get; set; }
        public String Remarks { get; set; }
        public int testno { get; set; } // 第几次测试 1 2 3 三种
        public Part[] parts { get; set; }
    }
    
    //大题
    public class Part
    {
        public int id { get; set; }
        public string description { get; set; }
        public int p_no { get; set; }
        public PartExer[] partExers { get; set; }
    }

    //对应关系
    public class PartExer
    {
        public int id { get; set; }
        public int e_no { get; set; }
        public Exercise exercise { get; set; }
    }

    //题目
    public class Exercise
    {
        public string id { get; set; }
        public string text { get; set; }
        public string description { get; set; }
        public Exercise exercise { get; set; }
        public Type type { get; set; }
        public Question[] questions { get; set; }
    }

    //题目类型
    public class Type
    {
        public int id { get; set; }
        public String direction { get; set; }
        public string name { get; set; }
    }

    //小题
    public class Question
    {
        public int id { get; set; }
        public String text { get; set; }
        public string options { get; set; }//选项 以 || 为分割
        public int answer { get; set; } //答案 1~5
        public int num { get; set; }
        public string type { get; set; }
        public string level { get; set; }
        public Audio audio { get; set; }
        public Intervention[] interventions { get; set; }
    }

    //音频
    public class Audio
    {
        public int id { get; set; }
        public string path { get; set; }
        public string src { get; set; } //Url 用此来获取音频
        public int type { get; set; }
    }
    public class Intervention
    {
        public int id { get; set; }
        public string text { get; set; }
        public int level { get; set; } // 干预级别 0~3
        public Audio audio { get; set; }
    }
}
