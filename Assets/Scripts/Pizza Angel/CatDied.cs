using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDied : MonoBehaviour
{
    public GameObject lemur;
    public Transform prefab;
    public Camera MainCamera;
    public AudioSource theme;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Jump"))
        {
            lemur.SetActive(true);
            lemur.transform.position = new Vector3(-7.74f, 4.63f, 0.6992f);
            lemur.transform.eulerAngles = new Vector3(0,0,0);


            var cat = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity);

            cat.GetComponent<PlayerMovement>().SetCamera(MainCamera);

            var objects = GameObject.FindObjectsOfType<GameObject>();
            foreach (var o  in objects) {
                if(o.tag == "Pizza") Destroy(o);
            }

            theme.enabled = true;
            theme.Play();

            this.gameObject.SetActive(false);
        }
        else theme.Stop();
    }
}
