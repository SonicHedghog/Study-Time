using StudyTimeAPI;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class AdvanceButton : MonoBehaviour
{
    public GameObject scene;
    public GameObject thisScene;
    public bool isScene = false;
    public bool isBack = false;
    public bool getSubject = false;
    public bool getLesson = false;
    public bool dropEndLocation = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Advance()
    {
        if(isScene)
        {
            SceneManager.LoadScene(this.gameObject.name);
        }
        else
        {
            if(getSubject) FileManager.SetSubject(this.gameObject.name);
            if(getLesson) FileManager.SetLesson(this.gameObject.name);
            if(dropEndLocation) FileManager.path = Directory.GetParent(FileManager.path).FullName;
            scene.SetActive(true);
            thisScene.SetActive(false);
        }


    }
}
