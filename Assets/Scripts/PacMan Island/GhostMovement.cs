using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    float rotationalMove = 0f;
    public float runSpeed = .3f;
    public float rotationalSpeed = .3f;

    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Use this for initialization
    void Start () {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    // Update is called once per frame
    void Update()
    {
        var i = Random.Range(0f, 150f);
        if(i < 2)
        {
            rotationalMove = (i == 2 ? 90f : 45f) * rotationalSpeed;
        }

        Debug.Log("Horizontal Movement: " + rotationalMove);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 viewPos = transform.position;

        // Check if outside x view
        if(viewPos.x <= screenBounds.x * -1 + (objectWidth/2.5f))
        {
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 , screenBounds.x);
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 , screenBounds.y);
            transform.Rotate(0, 0, 180);
        }
        else if(viewPos.x >= screenBounds.x - (objectWidth/1.5f)) 
        {
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1, screenBounds.x);
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1, screenBounds.y);
            transform.Rotate(0, 0, 180);
        }

        // Check if outside y view
        else if(viewPos.y <= screenBounds.y * -1 + (objectHeight/2f)) 
        {
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1, screenBounds.x);
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1, screenBounds.y);
            transform.Rotate(0, 0, 180);

        }
        else if(viewPos.y >= screenBounds.y - (objectHeight/2f)) 
        {
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1, screenBounds.x);
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1,  screenBounds.y);
            transform.Rotate(0, 0, 180);
        }

        transform.Rotate(0, 0, rotationalMove);
        rotationalMove = 0;
        transform.position += transform.right * Time.deltaTime * runSpeed;
    }

    public void SetCamera(Camera cam)
    {
        MainCamera = cam;
    }
}
