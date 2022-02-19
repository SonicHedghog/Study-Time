using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

namespace StudyTimeAPI
{
    public class FileManager
    {
        public static string[] lessonFiles;
        public static string[] path;
        public static Dictionary<string, string>  configs;

        // Get List of Subjects
        public string[] GetSubjects()
        {
            string[] paths = {Application.streamingAssetsPath, "Subjects"};
            // = AssetDatabase.GetSubFolders("Assets");

            var folders =  Directory.GetDirectories(Path.Combine(paths));
            Debug.Log("Retrived " + folders.Length + "Subjects");

            return folders;
        }

        // Get List of Lessonss
        public string[] GetLessons(string subject)
        {
            string[] paths = {Application.streamingAssetsPath, "Subjects", subject};
            // = AssetDatabase.GetSubFolders("Assets");

            var folders =  Directory.GetDirectories(Path.Combine(paths));
            Debug.Log("Retrived " + folders.Length + "Lessons");

            return folders;
        }

        // Get Specific Lesson
        public void GetLesson(string subject, string lesson)
        {
            path = new string[]{Application.streamingAssetsPath, "Subjects", subject, lesson};
            // = AssetDatabase.GetSubFolders("Assets");

            lessonFiles =  Directory.GetFiles(Path.Combine(path));
            Debug.Log("Lesson Files Set");

            LoadConfig();
        }

        private void LoadConfig()
        {
            List<string> fileLines;
            configs = new Dictionary<string, string>();
            
            if(Application.platform == RuntimePlatform.Android)
            {
                var www = UnityEngine.Networking.UnityWebRequest.Get(Path.Combine(path));
                www.SendWebRequest();
                while (!www.isDone)
                {
                }
                fileLines = www.downloadHandler.text.Split('\n').ToList();
                Debug.Log(www.downloadHandler.text);
            }
            else
            {
                fileLines = File.ReadAllLines(Path.Combine(path)).ToList();
            }

            foreach(string config in fileLines)
            {
                string[] cutPoint = config.Split('=');
                configs.Add(cutPoint[0], cutPoint[1]);
            }

            Debug.Log("Loaded configs");
        }
    }
}