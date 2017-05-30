using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

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
        //which stat is being changed. 0 = sobriety, 1 = social, 2 = love
    [SerializeField]
    private string[] changeStat = new string[3];
        //how much each stat is changed (first for button pressed, second for amount)
    private int[,] statValue = new int[3,3];

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
    [SerializeField]
    private Image[] statIcons;
    [SerializeField]
    private Text[] valueText;

    private Color[] boyIconValue = new Color[3];
    private Color[] girlIconValue = new Color[3];

    //to count number of turns
    private int counter;

    private GameController gController;
    #endregion

    void Start()
    {
        gController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gController.randomPlayer(); 

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

        for (int i = 0; i < 3; i++)
        {
            boyIconValue[i] = statIcons[i].color; 
            girlIconValue[i] = statIcons[i].color;
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
        counter++; 

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
                    //sobriety
                    statValue[0,0] = 1;
                    //social
                    statValue[0,1] = 1;
                    //love
                    statValue[0,2] = 1;


                    statQuote[0] = "Hello this is a Quote";

                    //button 2 text + stat schange
                    answerText[1].text =
                        "";

                    //sobriety
                    statValue[1, 0] = -1;
                    //social
                    statValue[1, 1] = -1;
                    //love
                    statValue[1, 2] = -1;

                    statQuote[1] = "This is not a quote";

                    //button 3 text + stat schange
                    answerText[2].text =
                        "";

                    //sobriety
                    statValue[2, 0] = -2;
                    //social
                    statValue[2, 1] = -2;
                    //love
                    statValue[2, 2] = -2;

                    statQuote[2] = "Insert some random shit here";

                    break;

                //case 1:
                    //insert code
                    //break;
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
                    //Dialogue text
                    dialogue.text =
                        "";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "";
                    //sobriety
                    statValue[0, 0] = 1;
                    //social
                    statValue[0, 1] = 1;
                    //love
                    statValue[0, 2] = 1;


                    statQuote[0] = "Hello this is a Quote";

                    //button 2 text + stat schange
                    answerText[1].text =
                        "";

                    //sobriety
                    statValue[1, 0] = -1;
                    //social
                    statValue[1, 1] = -1;
                    //love
                    statValue[1, 2] = -1;

                    statQuote[1] = "This is not a quote";

                    //button 3 text + stat schange
                    answerText[2].text =
                        "";

                    //sobriety
                    statValue[2, 0] = 2;
                    //social
                    statValue[2, 1] = -1;
                    //love
                    statValue[2, 2] = 1;

                    statQuote[2] = "Insert some random shit here";

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
        int[] statNumber = new int[3];
        for (int i = 0; i < changeStat.Length; i++)
        {
            gController.SendMessage("Change" + changeStat[i], statValue[0,i]);
            statNumber[i] = statValue[0, i];
        }

        EnableStatUI(true,statQuote[0],statNumber);
        EnableQuestionUI(false);
    }

    public void SubmitAnswer_2()
    {
        int[] statNumber = new int[3];
        for (int i = 0; i < changeStat.Length; i++)
        {
            gController.SendMessage("Change" + changeStat[i], statValue[1, i]);
            statNumber[i] = statValue[1, i];
        }
        EnableStatUI(true,statQuote[1], statNumber);
        EnableQuestionUI(false);
    }

    public void SubmitAnswer_3()
    {
        int[] statNumber = new int[3];
        for (int i = 0; i < changeStat.Length; i++)
        {
            gController.SendMessage("Change" + changeStat[i], statValue[2, i]);
            statNumber[i] = statValue[2, i];
        }
        EnableStatUI(true,statQuote[2],statNumber);
        EnableQuestionUI(false);
    }
    #endregion

    #region StatUI
    private void EnableStatUI(bool active, string quote, int[] statNumber)
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
            StartCoroutine(UpdateStats());

            //change Number Text
            string[] symbol = new string[3]; 
            for (int i = 0; i < valueText.Length; i++)
            {
                if (statNumber[i] > 0)
                    symbol[i] = "+";
                else if (statNumber[i] <= 0)
                    symbol[i] = "";

                valueText[i].text = symbol[i] + statNumber[i]; 
            }
        }

        else if (!active)
        {
            statText.gameObject.SetActive(false);
            statBg.gameObject.SetActive(false);
            statButton.gameObject.SetActive(false);
        }
    }

    private IEnumerator UpdateStats()
    {
        
        if (!gController.CheckPlayer())
        {
            Color[] statColors = new Color[3];
            for (int i = 0; i < statColors.Length; i++)
                statColors[i] = boyIconValue[i];

            float elapsedTime = 0;
            float totalTime = 3;
            while (elapsedTime < totalTime)
            {
                
                statIcons[0].color = Color.Lerp(statColors[0], new Color(statColors[0].r, statColors[0].g, statColors[0].b, .502f + (gController.CheckSobriety(gController.CheckPlayer()) *.25f)), (elapsedTime / totalTime));
                statIcons[1].color = Color.Lerp(statColors[1], new Color(statColors[1].r, statColors[1].g, statColors[1].b, .502f + (gController.CheckSocial(gController.CheckPlayer()) * .25f)), (elapsedTime / totalTime));
                statIcons[2].color = Color.Lerp(statColors[2], new Color(statColors[2].r, statColors[2].g, statColors[2].b, .502f + (gController.CheckLove(gController.CheckPlayer()) * .25f)), (elapsedTime / totalTime));
                elapsedTime += Time.deltaTime;

                //Debug.Log(elapsedTime);
                yield return null; 
            }

            for (int i = 0; i < 3; i++)
            {
                boyIconValue[i] = new Color(statIcons[i].color.r, statIcons[i].color.g, statIcons[i].color.b, statIcons[i].color.a);
                //Debug.Log("SavingColor" + i);
            }

            gController.ChangeColor(boyIconValue);
        }

        else if (gController.CheckPlayer())
        {
            Color[] statColors = new Color[3];
            for (int i = 0; i < statColors.Length; i++)
                statColors[i] = girlIconValue[i];

            float elapsedTime = 0;
            float totalTime = 3;
            while (elapsedTime < totalTime)
            {
                

                statIcons[0].color = Color.Lerp(statColors[0], new Color(statColors[0].r, statColors[0].g, statColors[0].b, .502f + (gController.CheckSobriety(gController.CheckPlayer()) * .25f)), (elapsedTime / totalTime));
                statIcons[1].color = Color.Lerp(statColors[1], new Color(statColors[1].r, statColors[1].g, statColors[1].b, .502f + (gController.CheckSocial(gController.CheckPlayer()) * .25f)), (elapsedTime / totalTime));
                statIcons[2].color = Color.Lerp(statColors[2], new Color(statColors[2].r, statColors[2].g, statColors[2].b, .502f + (gController.CheckLove(gController.CheckPlayer()) * .25f)), (elapsedTime / totalTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            for (int i = 0; i < 3; i++)
            {
                girlIconValue[i] = new Color(statIcons[i].color.r, statIcons[i].color.g, statIcons[i].color.b, statIcons[i].color.a);
                //Debug.Log("SavingColor" + i);
            }
            gController.ChangeColor(girlIconValue);
        }

        statButton.gameObject.SetActive(true);
    }

    public void StatButton()
    {
        statText.gameObject.SetActive(false);
        statBg.gameObject.SetActive(false);
        statButton.gameObject.SetActive(false);
        gController.ChangePlayer();

        if (counter <= 10)
            PassPhone();
        else
            SceneManager.LoadScene("End");
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
