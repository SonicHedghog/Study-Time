using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{

    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    private float fallX;
    private float fallY;
    Vector3 viewPos;



    // Start is called before the first frame update
    void Start () {
        Vector3 viewPos = transform.position;

        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2

        fallX = Random.Range(-0.2f, 0.2f);
        fallY = Random.Range(0, 0.2f);

    }

    // Update is called once per frame
    void Update()
    {
        if(!(viewPos.y <= screenBounds.y * -1))
        {
            viewPos.y-= fallY;
            viewPos.x-= fallX;
            transform.position = viewPos;
        }
        else Destroy(gameObject);
    }
}
