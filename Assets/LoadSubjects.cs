using System.Collections.Generic;
using StudyTimeAPI;
using UnityEngine;
using UnityEngine.UI;

public class LoadSubjects : MonoBehaviour
{
    public GameObject prefab;
    public GameObject scene;
    public GameObject thisScene;
    private List<GameObject> objects;


    // Start is called before the first frame update
    void OnEnable()
    {
        objects = new List<GameObject>();
        string[] subjects = FileManager.GetSubjects();

        foreach(string subject in subjects)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(true);

            obj.gameObject.name = subject;
            obj.GetComponent<Text>().text = subject;

            obj.GetComponent<AdvanceButton>().scene = scene;
            obj.GetComponent<AdvanceButton>().thisScene = thisScene;

            obj.transform.SetParent(this.transform, false);

            objects.Add(obj);
        }
    }

    void OnDisable()
    {
        foreach (var o  in objects) 
        {
            if(o.tag == "Subject") Destroy(o);
        }
    }
}
