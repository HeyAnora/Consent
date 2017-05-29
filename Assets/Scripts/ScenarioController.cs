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
    private string[] statQuote = new string[3]; 

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
    [SerializeField]
    private Color boyColor; 

    //girl UI
    [SerializeField]
    private Sprite girlQuestion;
    [SerializeField]
    private Sprite girlAnswer;
    [SerializeField]
    private Sprite girlBackground;
    [SerializeField]
    private Color girlColor; 


    //Stat Values
    private int[] statValue = new int[3];
    private string changeStat;

    //Pass Phone
    [SerializeField]
    private Button passPhoneButton;
    [SerializeField]
    private Text passPhoneText;

    //Stat UI
    [SerializeField]
    private Text statText;
    [SerializeField]
    private Image statBg;
    [SerializeField]
    private Image statButtonImage;
    [SerializeField]
    private Button statButton;
    [SerializeField]
    private Sprite[] statbgSprite;
    [SerializeField]
    private Sprite[] statButtonSprite; 


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

        passPhoneButton.onClick.AddListener(PassPhoneContinue);
        statButton.onClick.AddListener(StatButton);

        answerButton[0].onClick.AddListener(SubmitAnswer_1);
        answerButton[1].onClick.AddListener(SubmitAnswer_2);
        answerButton[2].onClick.AddListener(SubmitAnswer_3);

        PassPhone();
        //SetUI();
        //SetScenario(); 
    }

    #region Question

    //set and enable/disable Question UI
    private void SetUI()
    {
        //boy UI
        if (!gController.CheckPlayer())
        {
            background.sprite = boyBackground;
            questionBox.sprite = boyQuestion;
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i].GetComponent<Image>().sprite = boyAnswer;
                answerButton[i].gameObject.GetComponent<Image>().color = boyColor;
            }
        }

        //girl UI
        else if (gController.CheckPlayer())
        {
            background.sprite = girlBackground;
            questionBox.sprite = girlQuestion;
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i].GetComponent<Image>().sprite = girlAnswer;
                answerButton[i].gameObject.GetComponent<Image>().color = girlColor;


            }
        }
    }
    private void EnableQuestionUI(bool active)
    {
        if (active)
        {
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i].SetActive(true);
            }
            background.gameObject.SetActive(true);
            questionBox.gameObject.SetActive(true);
        }
        else if (!active)
        {
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i].SetActive(false);
            }
            background.gameObject.SetActive(false);
            questionBox.gameObject.SetActive(false);
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
                    statQuote[0] = "";

                    //button 2 text + stat schange
                    answerText[1].text =
                        "";

                    statValue[1] = 0;
                    statQuote[1] = "";
                    //button 3 text + stat schange
                    answerText[2].text =
                        "";

                    statValue[2] = 0;
                    statQuote[2] = "";

                    //Stat Being Changed
                    changeStat = "ChangeSobriety";
                    break;

                case 1:

                    //Dialogue text
                    dialogue.text =
                        "Shots!?";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Bring it on";
                    statValue[0] = 0;
                    statQuote[0] = "";

                    //button 2 text + stat schange
                    answerText[1].text =
                         "Ok but just one";
                    statValue[1] = 0;
                    statQuote[1] = "";

                    //button 3 text + stat schange
                    answerText[2].text =
                         "Maybe later";
                    statValue[2] = 0;
                    statQuote[2] = "";

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
                    statQuote[0] = "";

                    //button 2 text + stat schange
                    answerText[1].text =
                        "Sure, thanks";
                    statValue[1] = 0;
                    statQuote[1] = "";

                    //button 3 text + stat schange
                    answerText[2].text =
                         "Ok, but I shouldn't drink too much, I'm driving";
                    statValue[2] = 0;
                    statQuote[2] = "";

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
        EnableStatUI(true,statQuote[0]);
        EnableQuestionUI(false);
    }

    public void SubmitAnswer_2()
    {
        gController.SendMessage(changeStat, statValue[1]);

        EnableStatUI(true,statQuote[1]);
        EnableQuestionUI(false);
    }

    public void SubmitAnswer_3()
    {
        gController.SendMessage(changeStat, statValue[2]);
        EnableStatUI(true,statQuote[2]);
        EnableQuestionUI(false);
    }
    #endregion

    #region StatUI
    private void EnableStatUI(bool active, string quote)
    {
        if (active)
        {
            //boy
            if (!gController.CheckPlayer())
            {
                statBg.sprite = statbgSprite[0];
                statButtonImage.sprite = statButtonSprite[0];
            }
            //girl
            else if (gController.CheckPlayer())
            {
                statBg.sprite = statbgSprite[1];
                statButtonImage.sprite = statButtonSprite[1];
            }

            statText.text = quote; 
            statText.gameObject.SetActive(true);
            statBg.gameObject.SetActive(true);
            statButton.gameObject.SetActive(true);
        }

        else if (!active)
        {
            statText.gameObject.SetActive(false);
            statBg.gameObject.SetActive(false);
            statButton.gameObject.SetActive(false);
        }
    }

    public void StatButton()
    {
        statText.gameObject.SetActive(false);
        statBg.gameObject.SetActive(false);
        statButton.gameObject.SetActive(false);
        gController.ChangePlayer();
        PassPhone(); 
    }



    #endregion

    #region Pass Phone

    private void PassPhone()
    {
        passPhoneButton.gameObject.SetActive(true);
        if (!gController.CheckPlayer())
        {
            passPhoneText.text =
           "Pass the phone to " + "<color=#0864FFFF>" + gController.PlayerName("boy") + "</color>.";
        }
        else if (gController.CheckPlayer())
            passPhoneText.text =
            "Pass the phone to " + "<color=#FB1E4BFF>" + gController.PlayerName("girl") + "</color>.";

        passPhoneText.gameObject.SetActive(true);
    }

    public void PassPhoneContinue()
    {
        passPhoneButton.gameObject.SetActive(false);
        passPhoneText.gameObject.SetActive(false);

        SetUI();
        SetScenario();
        EnableQuestionUI(true);
    }

    #endregion
}
