using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crescent.WinForms.DTO
{
    public class Question
    {
        public Question(byte questionIndex, string questionName)
        {
            QuestionIndex = questionIndex;
            QuestionName = questionName;
        }
        public Question(byte groupIndex, string groupName, byte questionIndex, string questionName)
        {
            GroupIndex = groupIndex;
            GroupName = groupName;

            QuestionIndex = questionIndex;
            QuestionName = questionName;
        }

        public byte GroupIndex { get; set; }
        public string GroupName { get; set; }
        public byte QuestionIndex { get; set; }
        public string QuestionName { get; set; }
    }
}
