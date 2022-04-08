using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using StudyTimeAPI;

public class CatWorld : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform prefab;
    public Camera MainCamera;
    public GameObject game;
    public static GameObject panel1;
    public static GameObject panel2;
    public static GameObject ui;
    public static InputField answer;
    public GameObject UEpanel1;
    public GameObject UEpanel2;
    public GameObject UEui;
    public InputField UEanswer;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private static QuestionGenerator questions;
    private static AnswerGenerator answers;

    void Start () {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        ui = UEui;
        panel1 = UEpanel1;
        panel2 = UEpanel2;
        answer = UEanswer;

        if(FileManager.configs["questions"] == "default")
            questions = new DefaultQuestionGenerator();
        
        if(FileManager.configs["answers"] == "default")
            answers = new DefaultAnswerGenerator(questions.GetQuestionList());
}

    // Update is called once per frame
    void Update()
    {
        if (Random.value < .03)
        {
            var pizza = Instantiate(prefab, new Vector3(Random.Range(screenBounds.x * -1, screenBounds.x), screenBounds.y, 0), Quaternion.identity);

            pizza.GetComponent<Fall>().SetCamera(MainCamera);
        }

        if(this.gameObject.tag == "Menu")
        {
            if(Input.GetButton("Jump"))
            {
                game.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }

    public static void AskQuestion()
    {
        if(FileManager.configs["questions"] == "default") AskQuestionDQ();
    }

    public void GetAnswer()
    {
        if(FileManager.configs["answers"] == "default") GetAnswerDA();
    }
    
    public static void AskQuestionDQ()
    {
        Time.timeScale = 0;
        ui.SetActive(true);
        panel1.SetActive(true);
        answer.gameObject.SetActive(true);
        answer.enabled = true;

        panel1.GetComponent<Text>().text = questions.GetQuestion();
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
