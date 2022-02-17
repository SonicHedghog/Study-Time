using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManWorld : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform PacMan;
    public Transform PowerPellet;
    public Transform Pellet;
    public Transform Inky;
    public Transform Pinky;
    public Transform Blinky;
    public Transform Clyde;
    public Camera MainCamera;
    public GameObject menu;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private float pauseEndTime = 0;
    private bool starteEndTime = true;
    private Transform pacman;
    void OnEnable()
    {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        
        var inky = Instantiate(Inky, new Vector3(Random.Range(3, screenBounds.x - 1) * (Random.Range(0,2)*2-1), Random.Range(3, screenBounds.y - 1) * (Random.Range(0,2)*2-1)), Quaternion.identity);
        var pinky = Instantiate(Pinky, new Vector3(Random.Range(3, screenBounds.x - 1) * (Random.Range(0,2)*2-1), Random.Range(3, screenBounds.y - 1) * (Random.Range(0,2)*2-1)), Quaternion.identity);
        var blinky = Instantiate(Blinky, new Vector3(Random.Range(3, screenBounds.x - 1) * (Random.Range(0,2)*2-1), Random.Range(3, screenBounds.y - 1) * (Random.Range(0,2)*2-1)), Quaternion.identity);
        var clyde = Instantiate(Clyde, new Vector3(Random.Range(3, screenBounds.x - 1) * (Random.Range(0,2)*2-1), Random.Range(3, screenBounds.y - 1) * (Random.Range(0,2)*2-1)), Quaternion.identity);
        var power = Instantiate(PowerPellet, new Vector3(Random.Range(3, screenBounds.x - 1) * (Random.Range(0,2)*2-1), Random.Range(3, screenBounds.y - 1) * (Random.Range(0,2)*2-1)), Quaternion.identity);
        
        for(int x = 0; x < 30; x++)
            Instantiate(Pellet, new Vector3(Random.Range(0, screenBounds.x) * (Random.Range(0,2)*2-1), Random.Range(3, screenBounds.y - 1) * (Random.Range(0,2)*2-1)), Quaternion.identity);
        inky.GetComponent<GhostMovement>().SetCamera(MainCamera);
        pinky.GetComponent<GhostMovement>().SetCamera(MainCamera);
        blinky.GetComponent<GhostMovement>().SetCamera(MainCamera);
        clyde.GetComponent<GhostMovement>().SetCamera(MainCamera);

        pacman = Instantiate(PacMan, new Vector3(0,0,0), Quaternion.identity);
        pacman.GetComponent<PacManController>().SetPublicVariables(MainCamera, inky, pinky, blinky, clyde);

        Time.timeScale = 0f;
        pauseEndTime = 0;
        starteEndTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(starteEndTime)
        {
            pacman.GetComponent<Animator>().enabled = false;
            pauseEndTime = pauseEndTime == 0 ? Time.realtimeSinceStartup + 3 : pauseEndTime;
            while(pauseEndTime >= Time.realtimeSinceStartup )
            {
                Debug.Log("don't go");
                return;
            }
            Time.timeScale = 1;
            pauseEndTime = 1;
            starteEndTime = false;
            pacman.GetComponent<Animator>().enabled = true;
        }

        if(pacman.GetComponent<PacManController>() == null)
        {
            Time.timeScale = 0f;
            pauseEndTime = pauseEndTime == 1 ? Time.realtimeSinceStartup + 3 : pauseEndTime;
            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                return;
            }

            foreach(GameObject ghost in GameObject.FindGameObjectsWithTag("Ghost")) Destroy(ghost);
            foreach(GameObject pellet in GameObject.FindGameObjectsWithTag("Pellet")) Destroy(pellet);
            foreach(GameObject pellet in GameObject.FindGameObjectsWithTag("Power Pellet")) Destroy(pellet);
            foreach(GameObject pacmen in GameObject.FindGameObjectsWithTag("Player")) Destroy(pacmen);
            
            Time.timeScale = 1;

            menu.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
