﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class FileManager : MonoBehaviour
{
    public static string[] lessonFiles;

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
        string[] paths = {Application.streamingAssetsPath, "Subjects", subject, lesson};
        // = AssetDatabase.GetSubFolders("Assets");

        lessonFiles =  Directory.GetFiles(Path.Combine(paths));
        Debug.Log("Lesson Files Set");
    }
}