using StudyTimeAPI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvanceButton : MonoBehaviour
{
    public GameObject scene;
    public GameObject thisScene;
    public bool isScene = false;
    public bool isBack = false;
    public bool getSubject = false;
    public bool getLesson = false;

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
            else if(getLesson) FileManager.SetLesson(this.gameObject.name);
            scene.SetActive(true);
            thisScene.SetActive(false);
        }


    }
}
