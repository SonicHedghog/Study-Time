﻿using System.Collections.Generic;
using System;

namespace StudyTimeAPI
{
    public abstract class QuestionGenerator
    {
        public QuestionGenerator() { }
        public abstract string GetQuestion();
        public abstract bool IsComplete();
        public abstract List<string> GetQuestionList();
    }
}