using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

namespace StudyTimeAPI
{
    public class FileManager
    {
        public static string[] lessonFiles;
        public static string[] lessons;
        public static string path;
        public static Dictionary<string, string>  configs;

        // Get List of Subjects
        public static string[] GetSubjects()
        {
            string loc = Path.Combine(new string[]{Application.streamingAssetsPath, "Subjects"});
            // = AssetDatabase.GetSubFolders("Assets");

            Debug.Log(loc);
            var folders =  Directory.GetDirectories(loc).Select(f => f.Remove(f.IndexOf(loc), loc.Length + 1)).ToArray();
            Debug.Log("Retrived " + folders.Length + " Subjects");

            return folders;
        }

        // Get List of Lessonss
        public static void SetSubject(string subject)
        {
            path = Path.Combine(new string[]{Application.streamingAssetsPath, "Subjects", subject});
            // = AssetDatabase.GetSubFolders("Assets");

            lessons =  Directory.GetDirectories(path).Select(f => f.Remove(f.IndexOf(path), path.Length + 1)).ToArray();;
            Debug.Log("Retrived " + lessons.Length + " Lessons");
        }

        // Get Specific Lesson
        public static void SetLesson(string lesson)
        {
            path = Path.Combine(new string[]{path, lesson});
            // = AssetDatabase.GetSubFolders("Assets");

            lessonFiles =  Directory.GetFiles(path);
            Debug.Log("Lesson Files Set");

            LoadConfig();
        }

        private static void LoadConfig()
        {
            List<string> fileLines;
            configs = new Dictionary<string, string>();
            
            if(Application.platform == RuntimePlatform.Android)
            {
                var www = UnityEngine.Networking.UnityWebRequest.Get(path);
                www.SendWebRequest();
                while (!www.isDone)
                {
                }
                fileLines = www.downloadHandler.text.Split('\n').ToList();
                Debug.Log(www.downloadHandler.text);
            }
            else
            {
                fileLines = File.ReadAllLines(Path.Combine(new string[]{path, ".config"})).ToList();
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