using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    public float runSpeed = .3f;
    public float flySpeed = .3f;

    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private int pizzaCount = 0;

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
        animator.SetFloat("Speed", horizontalMove);
        Debug.Log("Horizontal Movement: " + horizontalMove);
    
        verticalMove = Input.GetAxisRaw("Vertical") * flySpeed;
        animator.SetFloat("Lift", verticalMove);
        Debug.Log("Vertical Movement: " + verticalMove);

        if(Input.GetButton("Jump"))
            animator.SetTrigger("Eat");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 viewPos = transform.position;

        viewPos+= new Vector3(horizontalMove, verticalMove - .05f, 0);

        // Check if outside x view
        if(viewPos.x <= screenBounds.x * -1 + (objectWidth/2.5f))
        {
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + (objectWidth/1.5f), screenBounds.x - (objectWidth/1.5f));
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + (objectHeight/1.5f), screenBounds.y - (objectHeight/1.5f));
        }
        else if(viewPos.x >= screenBounds.x - (objectWidth/1.5f)) 
        {
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + (objectWidth/1.5f), screenBounds.x - (objectWidth/1.5f));
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + (objectHeight/1.5f), screenBounds.y - (objectHeight/1.5f));
        }

        // Check if outside y view
        else if(viewPos.y <= screenBounds.y * -1 + (objectHeight/2f)) 
        {
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + (objectWidth/2f), screenBounds.x - (objectWidth/2f));
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + (objectHeight/2f), screenBounds.y - (objectHeight/2f));

        }
        else if(viewPos.y >= screenBounds.y - (objectHeight/2f)) 
        {
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + (objectWidth/2f), screenBounds.x - (objectWidth/2f));
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + (objectHeight/2f), screenBounds.y - (objectHeight/2f));
        }

        transform.position = viewPos;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Pizza") {
            Destroy(other.gameObject);
            pizzaCount++;
            animator.SetTrigger("Eat");
            
            if(pizzaCount%3 == 0) animator.SetTrigger("Celebrate");
        }
        Debug.Log("Cat hit Object");
    }

    public void SetCamera(Camera cam)
    {
        MainCamera = cam;
    }
}
