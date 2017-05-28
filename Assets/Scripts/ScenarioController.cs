using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioController : MonoBehaviour
{

    /// <summary>
    /// TO DO:
    /// -Hide UI and Change players
    /// -
    /// -Add stat values UI
    ///
    /// </summary>


    #region variables
    //Scenarios
    [SerializeField]
    private List<int> maleScenario = new List<int>();
    [SerializeField]
    private List<int> femaleScenario = new List<int>();
    private int scenario;

    //UI
    [SerializeField]
    private GameObject[] answers;
    private Button[] answerButton = new Button[3];
    private Text[] answerText = new Text[3];
    [SerializeField]
    private Text dialogue;

    [SerializeField]
    private Image background;
    [SerializeField]
    private Image questionBox; 

    //boy UI
    [SerializeField]
    private Sprite boyQuestion;
    [SerializeField]
    private Sprite boyAnswer;
    [SerializeField]
    private Sprite boyBackground;

    //girl UI
    [SerializeField]
    private Sprite girlQuestion;
    [SerializeField]
    private Sprite girlAnswer;
    [SerializeField]
    private Sprite girlBackground;


    //Stat Values
    private int[] statValue = new int[3];
    private string changeStat;

    // 0 = Sob, 1 = Love, 2 = Social
    [SerializeField]
    private Text[] stats; 

    private GameController gController;
    #endregion

    void Start()
    {
        gController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        for (int i = 0; i < maleScenario.Count; i++)
        {
            maleScenario[i] = i + 1; 
        }

        for (int i = 0; i < femaleScenario.Count; i++)
        {
            femaleScenario[i] = i + 1;
        }

        for (int i = 0; i < answers.Length; i++)
        {
            answerButton[i] = answers[i].GetComponentInChildren<Button>();
            answerText[i] = answers[i].GetComponentInChildren<Text>(); 
        }

        answerButton[0].onClick.AddListener(SubmitAnswer_1);
        answerButton[1].onClick.AddListener(SubmitAnswer_2);
        answerButton[2].onClick.AddListener(SubmitAnswer_3);

        SetUI();
        SetScenario(); 
    }

    private void SetUI()
    {
        //boy UI
        if (!gController.CheckPlayer())
        {
            background.sprite = boyBackground;
            questionBox.sprite = boyQuestion; 
            for (int i = 0; i < answers.Length; i++)
                answers[i].GetComponent<Image>().sprite = boyAnswer;    
        }

        //girl UI
        else if (gController.CheckPlayer())
        {
            background.sprite = girlBackground;
            questionBox.sprite = girlQuestion;
            for (int i = 0; i < answers.Length; i++)
                answers[i].GetComponent<Image>().sprite = girlAnswer;
        }
    }

    //creates the scenario
    public void SetScenario()
    {
        //male scenarios
        if (!gController.CheckPlayer())
        {
            scenario = Random.Range(0, maleScenario.Count);

            switch (maleScenario[scenario])
            {

                default:
                    //Dialogue text
                    dialogue.text =
                        "";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "";
                    statValue[0] = 0;

                    //button 2 text + stat schange
                    answerText[1].text =
                        "";

                    statValue[1] = 0;
                    //button 3 text + stat schange
                    answerText[2].text =
                        "";

                    statValue[2] = 0;

                    //Stat Being Changed
                    changeStat = "";
                    break;

                case 1:

                    //Dialogue text
                    dialogue.text =
                        "Shots!?";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Bring it on";
                    statValue[0] = 0;

                    //button 2 text + stat schange
                    answerText[1].text =
                         "Ok but just one";
                    statValue[1] = 0;

                    //button 3 text + stat schange
                    answerText[2].text =
                         "Maybe later";
                    statValue[2] = 0;

                    //Stat Being Changed
                    changeStat = "ChangeSobriety";
                    break;

                case 2:
                    //Dialogue text
                    dialogue.text =
                        "I'm getting another beer. Want one?";
                    //button 1 text + stat schange
                    answerText[0].text = "No thanks, I'm driving";
                    statValue[0] = 0;

                    //button 2 text + stat schange
                    answerText[1].text =
                        "Sure, thanks";
                    statValue[1] = 0;

                    //button 3 text + stat schange
                    answerText[2].text =
                         "Ok, but I shouldn't drink too much, I'm driving";
                    statValue[2] = 0;

                    //Stat Being Changed
                    changeStat = "ChangeSobriety";
                    break;
            }

            maleScenario.RemoveAt(scenario);
        }

        //female scenarios
        else if (gController.CheckPlayer())
        {
            scenario = Random.Range(0, femaleScenario.Count);

            switch (femaleScenario[scenario])
            {
                default:
                    //insert code
                    break;

                case 1:
                    //insert code
                    break;
            }

            femaleScenario.RemoveAt(scenario);
        }
    }


    //Button Presses
    public void SubmitAnswer_1()
    {
        gController.SendMessage(changeStat, statValue[0]);

    }

    public void SubmitAnswer_2()
    {
        gController.SendMessage(changeStat, statValue[1]);

    }

    public void SubmitAnswer_3()
    {
        gController.SendMessage(changeStat, statValue[2]);

    }



    //changes StatUI
    public void ChangeStatUI()
    {

    }
}
