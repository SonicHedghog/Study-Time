using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvanceButton : MonoBehaviour
{
    public GameObject scene;
    public GameObject thisScene;
    public bool isScene = false;
    public bool isBack = false;

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
            scene.SetActive(true);
            thisScene.SetActive(false);
        }
    }
}
