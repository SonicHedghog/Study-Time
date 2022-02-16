using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManController : MonoBehaviour
{
    public Animator Inky;
    public Animator Pinky;
    public Animator Blinky;
    public Animator Clyde;
    public Animator PacMan;
    float rotationalMove = 0f;
    public float runSpeed = .3f;
    public float rotationalSpeed = .3f;

    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private int pizzaCount = 0;
    private bool hasPowerPellet = false;

    // Use this for initialization
    void Start () {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    // Update is called once per frame
    void Update()
    {
        rotationalMove = -Input.GetAxisRaw("Horizontal") * rotationalSpeed;
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
        transform.position += transform.right * Time.deltaTime * runSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Pellet") Destroy(other.gameObject);

        if (other.gameObject.tag == "Power Pellet") {
            Destroy(other.gameObject);
            hasPowerPellet = true;

            Inky.SetBool("isScared", true);
            Pinky.SetBool("isScared", true);
            Blinky.SetBool("isScared", true);
            Clyde.SetBool("isScared", true);
            
        }
        if (other.gameObject.tag == "Ghost") {
            if(hasPowerPellet) Destroy(other.gameObject);
            else
            {
                PacMan.SetTrigger("isPacManDead");
                Time.timeScale = 0;
                Destroy(this);
            }
        }
        Debug.Log("PacMan hit Object");
    }

    
    public void SetPublicVariables(Camera cam, Transform inky, Transform pinky, Transform blinky, Transform clyde)
    {
        MainCamera = cam;
        Inky = inky.GetComponent<Animator>();
        Pinky = pinky.GetComponent<Animator>();
        Blinky = blinky.GetComponent<Animator>();
        Clyde = clyde.GetComponent<Animator>();
    }
}
