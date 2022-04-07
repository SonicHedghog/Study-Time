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
            Debug.Log(filelines.Count);
        }

        Answers = new Dictionary<string, List<string>>();
        foreach(string question in questions)
        {
            List<string> answers = new List<string>();

            for (int c = 0; filelines.Count > 0 && c < 1; c++)
            {
                answers.Add(filelines[0]);
                Debug.Log("Lookn " + filelines[0]);
                filelines.RemoveAt(0);
            }

            // if(filelines.Count != 0) filelines.RemoveAt(0);
            Answers.Add(question, answers);
            Debug.Log("Tests a" + Answers[question].Count);
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