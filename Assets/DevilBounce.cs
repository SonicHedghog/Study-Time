using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilBounce : MonoBehaviour
{
    public float runSpeed = .3f;

    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    void FixedUpdate()
    {
        Vector3 viewPos = transform.position;

        // Check if outside x view
        if(viewPos.x <= screenBounds.x * -1 + (objectWidth/2.5f))
        {
            transform.Rotate(0, 0, Random.Range(180,270));
        }
        else if(viewPos.x >= screenBounds.x - (objectWidth/1.5f)) 
        {
            transform.Rotate(0, 0, Random.Range(180,270));
        }

        // Check if outside y view
        else if(viewPos.y <= screenBounds.y * -1 + (objectHeight/2f)) 
        {
            transform.Rotate(0, 0, Random.Range(180,270));

        }
        else if(viewPos.y >= screenBounds.y - (objectHeight/2f)) 
        {
            transform.Rotate(0, 0, Random.Range(180,270));
        }

        transform.position += transform.right * Time.deltaTime * runSpeed;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Pizza") {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }

        Debug.Log("Devil hit Object");
    }
}
