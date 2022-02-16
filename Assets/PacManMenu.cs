using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManMenu : MonoBehaviour
{
    public GameObject game;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Jump"))
        {
            game.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
