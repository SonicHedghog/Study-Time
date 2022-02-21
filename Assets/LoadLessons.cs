using System.Collections;
using System.Collections.Generic;
using StudyTimeAPI;
using UnityEngine;
using UnityEngine.UI;

public class LoadLessons : MonoBehaviour
{
    public GameObject prefab;
    public GameObject scene;
    public GameObject thisScene;


    // Start is called before the first frame update
    void OnEnable()
    {
        string[] lessons = FileManager.lessons;

        foreach(string lesson in lessons)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(true);

            obj.gameObject.name = lesson;
            obj.GetComponent<Text>().text = lesson;

            obj.GetComponent<AdvanceButton>().scene = scene;
            obj.GetComponent<AdvanceButton>().thisScene = thisScene;

            obj.transform.SetParent(this.transform, false);
        }
    }
}
