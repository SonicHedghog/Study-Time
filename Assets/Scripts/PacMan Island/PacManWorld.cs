using System.Collections;
using StudyTimeAPI;
using UnityEngine;
using UnityEngine.UI;

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
    public static bool canGo = true;
    private static QuestionGenerator questions;
    private static AnswerGenerator answers;
    public static GameObject panel1;
    public static GameObject panel2;
    public static GameObject panel3;
    public static GameObject ui;
    public static InputField answer;
    public GameObject UEpanel1;
    public GameObject UEpanel2;
    public GameObject UEpanel3;
    public GameObject UEui;
    public InputField UEanswer;
    private Transform pacman;
    void OnEnable()
    {
        canGo = true;
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

        InvokeRepeating("AskQuestion", 7.0f, 7.0f);
        
        ui = UEui;
        panel1 = UEpanel1;
        panel2 = UEpanel2;
        panel3 = UEpanel3;
        answer = UEanswer;

        if(FileManager.configs["questions"] == "default")
            questions = new DefaultQuestionGenerator();
        
        if(FileManager.configs["answers"] == "default")
            answers = new DefaultAnswerGenerator(questions.GetQuestionList());
        else if(FileManager.configs["answers"] == "multiple_choice")
            answers = new MultipleChoiceAnswerGenerator(questions.GetQuestionList());
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

        if(pacman && pacman.GetComponent<PacManController>() == null)
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

    public void AskQuestion()
    {
        if(canGo)
            if(FileManager.configs["answers"] == "default") AskQuestionDQ();
    }

    public void GetAnswer()
    {
        if(FileManager.configs["answers"] == "default") GetAnswerDA();
    }
    
    public void AskQuestionDQ()
    {
        panel1.GetComponent<Text>().text = questions.GetQuestion();
        Debug.Log(panel1.GetComponent<Text>().text);

        if(panel1.GetComponent<Text>().text == "")
        {
            if(GameObject.Find("Pinky(Clone)")) GameObject.Find("Pinky(Clone)").SetActive(false);
            if(GameObject.Find("Blinky(Clone)")) GameObject.Find("Blinky(Clone)").SetActive(false);
            if(GameObject.Find("Clyde(Clone)")) GameObject.Find("Clyde(Clone)").SetActive(false);
            if(GameObject.Find("Inky(Clone)")) GameObject.Find("Inky(Clone)").SetActive(false);
            if(GameObject.Find("PacMan(Clone)")) Destroy(GameObject.Find("PacMan(Clone)"));
            panel3.SetActive(true);
            CancelInvoke();
            canGo = false;

            if(FileManager.configs["questions"] == "default")
                questions = new DefaultQuestionGenerator();
            
            if(FileManager.configs["answers"] == "default")
                answers = new DefaultAnswerGenerator(questions.GetQuestionList());
            else if(FileManager.configs["answers"] == "multiple_choice")
                answers = new MultipleChoiceAnswerGenerator(questions.GetQuestionList());
            return;
        }

        Time.timeScale = 0;
        ui.SetActive(true);
        panel1.SetActive(true);
        answer.gameObject.SetActive(true);
        answer.enabled = true;

        Debug.Log("Correct Answer: " + answers.GetAllAnswerLists()[panel1.GetComponent<Text>().text][0]);
    }

    public void GetAnswerDA()
    {
        answer.gameObject.SetActive(false);

        panel1.SetActive(false);
        panel2.SetActive(true);

         if(answers.CheckAnswer(panel1.GetComponent<Text>().text, answer.text))
            panel2.GetComponent<Text>().text = "Correct";
        else
            panel2.GetComponent<Text>().text = "Incorrect. The answer is:\n" + answers.GetAnswerList(panel1.GetComponent<Text>().text)[0];

        answer.text = "";
        StartCoroutine(Function());
    }

    IEnumerator Function(){
        float startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup-startTime < 7) {
            yield return null;
        }
        panel2.SetActive(false);
        ui.SetActive(false);
        Time.timeScale = 1;
    }
}
