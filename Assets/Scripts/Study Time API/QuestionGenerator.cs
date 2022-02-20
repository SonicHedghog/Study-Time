using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudyTimeAPI
{
    public abstract class QuestionGenerator
    {
        public QuestionGenerator() { }
        public abstract string GetQuestion(Object o);
        public abstract bool IsComplete();
        public abstract List<string> GetQuestionList();
    }
}