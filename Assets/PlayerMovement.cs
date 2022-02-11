using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    public float runSpeed = .3f;
    public float flySpeed = .3f;

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
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        Debug.Log("Horizontal Movement: " + horizontalMove);
    
        verticalMove = Input.GetAxisRaw("Vertical") * flySpeed;
        Debug.Log("Vertical Movement: " + verticalMove);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 viewPos = transform.position;

        controller.Move(new Vector3(horizontalMove, verticalMove - .05f, 0));

        // Check if outside x view
        if(viewPos.x <= screenBounds.x * -1 + (objectWidth/2.5f))
        {
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + (objectWidth/1.5f), screenBounds.x - (objectWidth/1.5f));
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + (objectHeight/1.5f), screenBounds.y - (objectHeight/1.5f));
            transform.position = viewPos;
            return;
        }
        if(viewPos.x >= screenBounds.x - (objectWidth/1.5f)) 
        {
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + (objectWidth/1.5f), screenBounds.x - (objectWidth/1.5f));
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + (objectHeight/1.5f), screenBounds.y - (objectHeight/1.5f));
            transform.position = viewPos;
            return;
        }

        // Check if outside y view
        if(viewPos.y <= screenBounds.y * -1 + (objectHeight/2f)) 
        {
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + (objectWidth/2f), screenBounds.x - (objectWidth/2f));
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + (objectHeight/2f), screenBounds.y - (objectHeight/2f));
            transform.position = viewPos;
            return;
        }
        if(viewPos.y >= screenBounds.y - (objectHeight/2f)) 
        {
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + (objectWidth/2f), screenBounds.x - (objectWidth/2f));
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + (objectHeight/2f), screenBounds.y - (objectHeight/2f));
            transform.position = viewPos;
            return;
        }
    }
}
