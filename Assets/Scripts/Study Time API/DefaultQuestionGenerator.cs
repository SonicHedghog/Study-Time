using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using StudyTimeAPI;


public class DefaultQuestionGenerator : QuestionGenerator
{
    private List<string> Questions;
    private bool RandomSelect;
    private int index = 0;

    public DefaultQuestionGenerator()
    {
        // Change later to RandomSelect = "true" == FileManager.configs["random"].ToLower();
        
        RandomSelect = true;
        
        if(Application.platform == RuntimePlatform.Android)
        {
            var www = UnityEngine.Networking.UnityWebRequest.Get(Path.Combine(FileManager.path, "default_questions.in"));
            www.SendWebRequest();
            while (!www.isDone)
            {
            }
            Questions = www.downloadHandler.text.Split('\n').ToList();
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Questions = File.ReadAllLines(Path.Combine(FileManager.path, "default_questions.in")).ToList();
        }
    }

    public override string GetQuestion(System.Object o = null)
    {
        if(!RandomSelect && index < Questions.Count) 
            return Questions[index++];

        index = Random.Range(0, Questions.Count);

        string question = Questions[index];
        Questions.RemoveAt(index);
        return question;
    }

    public override List<string> GetQuestionList()
    {
        return Questions;
    }

    public override bool IsComplete()
    {
        return index < Questions.Count;
    }
    
}
