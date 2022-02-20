using System.Collections.Generic;
using System;

namespace StudyTimeAPI
{
    public abstract class AnswerGenerator
    {
        public AnswerGenerator() { }
        public abstract List<string> GetAnswerList(Object o);
        public abstract Dictionary<string, List<string>> GetAllAnswerLists();
    }
}