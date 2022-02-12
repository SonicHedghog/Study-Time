using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatWorld : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform prefab;
    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start () {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
}

    // Update is called once per frame
    void Update()
    {
        if (Random.value < .03)
        {
            var pizza = Instantiate(prefab, new Vector3(Random.Range(screenBounds.x * -1, screenBounds.x), screenBounds.y, 0), Quaternion.identity);

            pizza.GetComponent<Fall>().SetCamera(MainCamera);
        }
    }
}
