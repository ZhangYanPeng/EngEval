using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngEval.Model
{
    [Serializable]
    public class Account
    {
        public long id { get; set; }
        public string name { get; set; }
        public int gender { get; set; }
        public string grade { get; set; }
        public int age { get; set; }
        public string username { get; set; }
        public string student_no { get; set; }
        public string password { get; set; }
        public string major { get; set; }
        public string questionaire { get; set; }
        public string questionaireBF { get; set; }
        public string questionaireAF { get; set; }
        public int english_level { get; set; }
        public int status { get; set; }
        public EngClass engClass { get; set; }
        public School school { get; set; }
    }

    [Serializable]
    public class EngClass
    {
        public long id { get; set; }
        public string name { get; set; }
        public int stu_num { get; set; }
        public University university { get; set; }
    }

    [Serializable]
    public class University
    {
        public long id { get; set; }
        public string name { get; set; }
    }

    [Serializable]
    public class School
    {
        public long id { get; set; }
        public string name { get; set; }
    }
}
