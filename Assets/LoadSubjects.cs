using StudyTimeAPI;
using UnityEngine;
using UnityEngine.UI;

public class LoadSubjects : MonoBehaviour
{
    public GameObject prefab;
    public GameObject scene;
    public GameObject thisScene;


    // Start is called before the first frame update
    void OnEnable()
    {
        string[] subjeects = FileManager.GetSubjects();

        foreach(string subject in subjeects)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(true);

            obj.gameObject.name = subject;
            obj.GetComponent<Text>().text = subject;

            obj.GetComponent<AdvanceButton>().scene = scene;
            obj.GetComponent<AdvanceButton>().thisScene = thisScene;

            obj.transform.SetParent(this.transform, false);
        }
    }
}
