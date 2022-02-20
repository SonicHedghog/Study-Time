using System.Collections.Generic;
using StudyTimeAPI;
using UnityEngine;
using System.IO;
using System.Linq;

public class DefaultAnswerGenerator : AnswerGenerator
{
    Dictionary<string, List<string>> Answers;

    public DefaultAnswerGenerator(List<string> questions)
    {
        List<string> filelines = new List<string>();
        
        if(Application.platform == RuntimePlatform.Android)
        {
            var www = UnityEngine.Networking.UnityWebRequest.Get(Path.Combine(FileManager.path, "default_answers.in"));
            www.SendWebRequest();
            while (!www.isDone)
            {
            }
            filelines = www.downloadHandler.text.Split('\n').ToList();
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            filelines = File.ReadAllLines(Path.Combine(FileManager.path, "default_answers.in")).ToList();
        }

        foreach(string question in questions)
        {
            List<string> answers = new List<string>();

            while (filelines.Count > 0 && !filelines[0].Contains("@Question"))
            {
                answers.Add(filelines[0]);
                filelines.RemoveAt(0);
            }

            if(filelines.Count != 0) filelines.RemoveAt(0);
            Answers.Add(question, answers);
        }
    }

    public override Dictionary<string, List<string>> GetAllAnswerLists()
    {
        return Answers;
    }

    public override List<string> GetAnswerList(System.Object o)
    {
        return Answers[(string) o];
    }
}