using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EngEval.Pages.Test;

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
    [Serializable]
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
            for (int i = 0; i < parts.Length; i++)
            {
                for (int j = i; j < parts.Length; j++)
                {
                    if (parts[j].p_no == i)
                    {
                        Sorter.SwitchObj(parts, i, j);
                        break;
                    }
                }
                //对每个部分排序
                parts[i].Sort();
            }
        }

        //获取整个Question
        internal Question getQuestion(int qn)
        {
            int i = 0;
            foreach (Part part in parts)
            {
                foreach (PartExer pe in part.partExers)
                {
                    Exercise exercise = pe.exercise;
                    foreach (Question que in exercise.questions)
                    {
                        i++;
                        if (qn == i)
                            return que;
                    }
                }
            }
            return null;
        }

        //判断是否是part第一题，确定等待时间
        internal int timeRemain(int qn)
        {
            int i = 0;
            foreach (Part part in parts)
            {
                foreach (PartExer pe in part.partExers)
                {
                    Exercise exercise = pe.exercise;
                    foreach (Question que in exercise.questions)
                    {
                        i++;
                        if (qn == i)
                        {
                            if (exercise.questions.Length <= 1)
                                return 10;
                            if (que.q_num == 0)
                            {
                                return exercise.questions.Length * 5;
                            }
                            else
                            {
                                return 5;
                            }
                        }
                    }
                }
            }
            return 10;
        }

        internal Part getPart(int qn)
        {
            int i = 0;
            foreach (Part part in parts)
            {
                foreach (PartExer pe in part.partExers)
                {
                    Exercise exercise = pe.exercise;
                    foreach (Question que in exercise.questions)
                    {
                        i++;
                        if (qn == i)
                            return part;
                    }
                }
            }
            return null;
        }

        internal bool isPartFirst(int qn)
        {
            int i = 0;
            foreach (Part part in parts)
            {
                foreach (PartExer pe in part.partExers)
                {
                    Exercise exercise = pe.exercise;
                    foreach (Question que in exercise.questions)
                    {
                        i++;
                        if (qn == i && pe.e_no == 0 && que.q_num == 0)
                            return true;
                    }
                }
            }
            return false;
        }

        //获取整个exercise
        internal Exercise getExercise(int qn)
        {
            int i = 0;
            foreach (Part part in parts)
            {
                foreach (PartExer pe in part.partExers)
                {
                    Exercise exercise = pe.exercise;
                    foreach (Question que in exercise.questions)
                    {
                        i++;
                        if (qn == i)
                            return exercise;
                    }
                }
            }
            return null;
        }

        //qn题目最后一个问题题号
        internal int getExerciseMaxQn(int qn)
        {
            int i = 0;
            foreach (Part part in parts)
            {
                foreach (PartExer pe in part.partExers)
                {
                    Exercise exercise = pe.exercise;
                    bool check = false;
                    foreach (Question que in exercise.questions)
                    {
                        i++;
                        if (qn == i)
                            check = true;
                    }
                    if (check)
                        return i;
                }
            }
            return i;
        }

        //获取Qn题号
        internal int getQn(Question question)
        {
            int i = 0;
            foreach (Part part in parts)
            {
                foreach (PartExer pe in part.partExers)
                {
                    Exercise exercise = pe.exercise;
                    foreach (Question que in exercise.questions)
                    {
                        i++;
                        if (question == que)
                            return i;
                    }
                }
            }
            return i;
        }
    }

    //大题
    [Serializable]
    public class Part
    {
        public int id { get; set; }
        public string description { get; set; }
        public int p_no { get; set; }
        public PartExer[] partExers { get; set; }
        public Audio directAudio { get; set; }
        public string directStr { get; set; }

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
    [Serializable]
    public class PartExer
    {
        public int id { get; set; }
        public int e_no { get; set; }
        public Exercise exercise { get; set; }
    }

    //题目
    [Serializable]
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
    [Serializable]
    public class Type
    {
        public int id { get; set; }
        public String direction { get; set; }
        public string name { get; set; }
    }

    //小题
    [Serializable]
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
    [Serializable]
    public class Audio
    {
        public int id { get; set; }
        public string path { get; set; }
        public string src { get; set; } //Url 用此来获取音频
        public int type { get; set; }
    }

    //干预
    [Serializable]
    public class Intervention
    {
        public int id { get; set; }
        public string text { get; set; }
        public int level { get; set; } // 干预级别 0~3
        public Audio audio { get; set; }
    }
}
