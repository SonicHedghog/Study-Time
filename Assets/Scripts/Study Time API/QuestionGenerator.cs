using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudyTimeAPI
{
    public abstract class QuestionGenerator
    {
        public QuestionGenerator() { }
        public abstract string GetQuestion();
    }
}