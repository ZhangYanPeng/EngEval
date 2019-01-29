﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngEval.Model
{
    public class Sorter
    {
        //交换两数据位置
        public static void SwitchObj(Object[] objects, int i, int j)
        {
            Object obj = objects[i];
            objects[i] = objects[j];
            objects[j] = obj;
        }
    }


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

        //对题目进行排序
        public void Sort()
        {
            for(int i=0; i<parts.Length; i++)
            {
                for(int j=i; j<parts.Length; j++)
                {
                    if (parts[j].p_no == i) { 
                        Sorter.SwitchObj(parts, i, j);
                        break;
                    }
                }
                //对每个部分排序
                parts[i].Sort();
            }
        }
    }
    
    //大题
    public class Part
    {
        public int id { get; set; }
        public string description { get; set; }
        public int p_no { get; set; }
        public PartExer[] partExers { get; set; }

        //排序
        internal void Sort()
        {
            for (int i = 0; i < partExers.Length; i++)
            {
                for (int j = i; j < partExers.Length; j++)
                {
                    if (partExers[j].e_no == i)
                    {
                        Sorter.SwitchObj(partExers, i, j);
                        break;
                    }
                }
                //对每个部分排序
                partExers[i].exercise.Sort();
            }
        }
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

        //排序
        internal void Sort()
        {
            for (int i = 0; i < questions.Length; i++)
            {
                for (int j = i; j < questions.Length; j++)
                {
                    if (questions[j].q_num == i)
                    {
                        Sorter.SwitchObj(questions, i, j);
                        break;
                    }
                }
                //对每个部分排序
                questions[i].Sort();
            }
        }
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
        public int q_num { get; set; }
        public string type { get; set; }
        public string level { get; set; }
        public Audio audio { get; set; }
        public Intervention[] interventions { get; set; }

        //对干预排序
        internal void Sort()
        {
            for (int i = 0; i < interventions.Length; i++)
            {
                for (int j = i; j < interventions.Length; j++)
                {
                    if (interventions[j].level == i)
                    {
                        Sorter.SwitchObj(interventions, i, j);
                        break;
                    }
                }
            }
        }
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
