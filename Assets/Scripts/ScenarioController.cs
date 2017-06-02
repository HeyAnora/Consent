using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenarioController : MonoBehaviour
{
    #region variables
    //Scenarios
    [SerializeField]
    private List<int> maleScenario = new List<int>();
    [SerializeField]
    private List<int> femaleScenario = new List<int>();
    private int scenario;
    private string[] statQuote = new string[3];
    [SerializeField]
    private string[] sobrietyQuotes;
    [SerializeField]
    private string[] socialQuotes;
    [SerializeField]
    private string[] loveQuotes; 
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
    [SerializeField]
    private Text[] changeText; 
    private string[] symbol = new string[3];

    [SerializeField]
    private Color[] boyIconValue = new Color[3];
    [SerializeField]
    private Color[] girlIconValue = new Color[3];
    private int[] boyStatValue = new int[3];
    private int[] girlStatValue = new int[3]; 


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
            boyStatValue[i] = 50;
            girlStatValue[i] = 50; 
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

    private string RandomQuote(string statType)
    {
        if (statType == "sobriety")
            return sobrietyQuotes[Random.Range(0, sobrietyQuotes.Length)];
        else if (statType == "social")
            return socialQuotes[Random.Range(0, socialQuotes.Length)];
        else if (statType == "love")
            return loveQuotes[Random.Range(0, loveQuotes.Length)];
        else
            return "";
    }

    //creates the scenario
    public void SetScenario()
    {
        counter++;

    #region male scenarios
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


                    statQuote[0] = RandomQuote("sobriety");

                    //button 2 text + stat schange
                    answerText[1].text =
                        "";

                    //sobriety
                    statValue[1, 0] = -1;
                    //social
                    statValue[1, 1] = -1;
                    //love
                    statValue[1, 2] = -1;

                    statQuote[1] = RandomQuote("social");

                    //button 3 text + stat schange
                    answerText[2].text =
                        "";

                    //sobriety
                    statValue[2, 0] = -2;
                    //social
                    statValue[2, 1] = -2;
                    //love
                    statValue[2, 2] = -2;

                    statQuote[2] = RandomQuote("love");

                    break;

    #region Mind Scenarios
                case 1:
                    //Dialogue text
                    dialogue.text =
                        "Shots!?";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Bring it on!";
                    //sobriety
                    statValue[0, 0] = -2;
                    //social
                    statValue[0, 1] = 2;
                    //love
                    statValue[0, 2] = 0;


                    statQuote[0] = RandomQuote("sobriety");

                    //button 2 text + stat schange
                    answerText[1].text =
                        "Alright, but just one.";

                    //sobriety
                    statValue[1, 0] = -1;
                    //social
                    statValue[1, 1] = 1;
                    //love
                    statValue[1, 2] = 0;

                    statQuote[1] = RandomQuote("sobriety");

                    //button 3 text + stat schange
                    answerText[2].text =
                        "Not right now, maybe later";

                    //sobriety
                    statValue[2, 0] = 1;
                    //social
                    statValue[2, 1] = -2;
                    //love
                    statValue[2, 2] = 0;

                    statQuote[2] = RandomQuote("sobriety");

                    break;

                case 2:
                    //Dialogue text
                    dialogue.text =
                        "I'm getting another beer. Want one?";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "No thanks, someone should stay sober.";
                    //sobriety
                    statValue[0, 0] = 2;
                    //social
                    statValue[0, 1] = -1;
                    //love
                    statValue[0, 2] = 0;

                    statQuote[0] = RandomQuote("sobriety");

                    //button 2 text + stat schange
                    answerText[1].text =
                        "Sure, thanks!";

                    //sobriety
                    statValue[1, 0] = -2;
                    //social
                    statValue[1, 1] = 1;
                    //love
                    statValue[1, 2] = 0;

                    statQuote[1] = RandomQuote("sobriety");

                    //button 3 text + stat schange
                    answerText[2].text =
                        "Ok, but I shouldn't drink too much.";

                    //sobriety
                    statValue[2, 0] = -1;
                    //social
                    statValue[2, 1] = 0;
                    //love
                    statValue[2, 2] = 0;

                    statQuote[2] = RandomQuote("sobriety");

                    break;

                case 3:
                    //Dialogue text
                    dialogue.text =
                        "How about some beer pong?";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Hell yeah!";
                    //sobriety
                    statValue[0, 0] = -2;
                    //social
                    statValue[0, 1] = 2;
                    //love
                    statValue[0, 2] = 0;


                    statQuote[0] = RandomQuote("sobriety");

                    //button 2 text + stat schange
                    answerText[1].text =
                        "Just one game.";

                    //sobriety
                    statValue[1, 0] = -1;
                    //social
                    statValue[1, 1] = 1;
                    //love
                    statValue[1, 2] = 0;

                    statQuote[1] = RandomQuote("sobriety");

                    //button 3 text + stat schange
                    answerText[2].text =
                        "I'll just watch.";

                    //sobriety
                    statValue[2, 0] = 1;
                    //social
                    statValue[2, 1] = -1;
                    //love
                    statValue[2, 2] = 0;

                    statQuote[2] = RandomQuote("sobriety");

                    break;

                case 4:
                    //Dialogue text
                    dialogue.text =
                        "You should share a drink with "+ gController.PlayerName("girl")+"!";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "I don't want either of us to drink too much.";
                    //sobriety
                    statValue[0, 0] = 1;
                    //social
                    statValue[0, 1] = 0;
                    //love
                    statValue[0, 2] = 2;


                    statQuote[0] = RandomQuote("sobriety");

                    //button 2 text + stat schange
                    answerText[1].text =
                        "That's a great idea!";

                    //sobriety
                    statValue[1, 0] = -1;
                    //social
                    statValue[1, 1] = 1;
                    //love
                    statValue[1, 2] = 1;

                    statQuote[1] = RandomQuote("sobriety");

                    //button 3 text + stat schange
                    answerText[2].text =
                        "I'm not sure if " + gController.PlayerName("girl")+ " would like that or not." ;

                    //sobriety
                    statValue[2, 0] = 1;
                    //social
                    statValue[2, 1] = 0;
                    //love
                    statValue[2, 2] = -1;

                    statQuote[2] = RandomQuote("sobriety");

                    break;

                case 5:
                    //Dialogue text
                    dialogue.text =
                        "I bet you and " + gController.PlayerName("girl") +" would make a great drunk couple!";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Shut-up, dude.";
                    //sobriety
                    statValue[0, 0] = 0;
                    //social
                    statValue[0, 1] = -2;
                    //love
                    statValue[0, 2] = 1;


                    statQuote[0] = RandomQuote("sobriety");

                    //button 2 text + stat schange
                    answerText[1].text =
                        "I'm not going to pressure her into drinking.";

                    //sobriety
                    statValue[1, 0] = 1;
                    //social
                    statValue[1, 1] = 0;
                    //love
                    statValue[1, 2] = 2;

                    statQuote[1] = RandomQuote("sobriety");

                    //button 3 text + stat schange
                    answerText[2].text =
                        "But first we have to get drunk!";

                    //sobriety
                    statValue[2, 0] = -2;
                    //social
                    statValue[2, 1] = 2;
                    //love
                    statValue[2, 2] = -1;

                    statQuote[2] = RandomQuote("sobriety");

                    break;
                #endregion

                #region Social Scenarios

                case 6:
                    //Dialogue text
                    dialogue.text =
                        "Dude, go talk to " + gController.PlayerName("girl") + ", make sure she stays!";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "I don't want to pressure her.";
                    //sobriety
                    statValue[0, 0] = 0;
                    //social
                    statValue[0, 1] = -1;
                    //love
                    statValue[0, 2] = 2;


                    statQuote[0] = RandomQuote("social");

                    //button 2 text + stat schange
                    answerText[1].text =
                        "You're right, right after she's done with her friends.";

                    //sobriety
                    statValue[1, 0] = 0;
                    //social
                    statValue[1, 1] = 1;
                    //love
                    statValue[1, 2] = -1;

                    statQuote[1] = RandomQuote("social");

                    //button 3 text + stat schange
                    answerText[2].text =
                        "I'll do it later. You and I are hanging out right now!";

                    //sobriety
                    statValue[2, 0] = 0;
                    //social
                    statValue[2, 1] = 2;
                    //love
                    statValue[2, 2] = 0;

                    statQuote[2] = RandomQuote("social");

                    break;

                case 7:
                    //Dialogue text
                    dialogue.text =
                        "Joe went upstairs with some girl. She was drunk, but super hot!";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "We should go check on her!";
                    //sobriety
                    statValue[0, 0] = 2;
                    //social
                    statValue[0, 1] = -1;
                    //love
                    statValue[0, 2] = 0;


                    statQuote[0] = RandomQuote("sobriety");

                    //button 2 text + stat schange
                    answerText[1].text =
                        "Let's go Joe, what a stud!";

                    //sobriety
                    statValue[1, 0] = 0;
                    //social
                    statValue[1, 1] = 2;
                    //love
                    statValue[1, 2] = 0;

                    statQuote[1] = RandomQuote("social");

                    //button 3 text + stat schange
                    answerText[2].text =
                        "I hope she wasn't too drunk...";

                    //sobriety
                    statValue[2, 0] = 1;
                    //social
                    statValue[2, 1] = -1;
                    //love
                    statValue[2, 2] = 0;

                    statQuote[2] = RandomQuote("social");

                    break;

                case 8:
                    //Dialogue text
                    dialogue.text =
                        "Why havn't you gone upstairs with "+ gController.PlayerName("girl")+ " yet? She's been eyeing you all night!";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "I'm just waiting for the right time.";
                    //sobriety
                    statValue[0, 0] = 0;
                    //social
                    statValue[0, 1] = -1;
                    //love
                    statValue[0, 2] = 2;


                    statQuote[0] = RandomQuote("social");

                    //button 2 text + stat schange
                    answerText[1].text =
                        "I thought we were hanging out, bored of me already?";

                    //sobriety
                    statValue[1, 0] = 0;
                    //social
                    statValue[1, 1] = 1;
                    //love
                    statValue[1, 2] = 0;

                    statQuote[1] = RandomQuote("social");

                    //button 3 text + stat schange
                    answerText[2].text =
                        "I think I heard someone making out up there... I don't want to interrupt.";

                    //sobriety
                    statValue[2, 0] = 1;
                    //social
                    statValue[2, 1] = -1;
                    //love
                    statValue[2, 2] = 0;

                    statQuote[2] = RandomQuote("social");

                    break;

                case 9:
                    //Dialogue text
                    dialogue.text =
                        "Wow, that's the third couple I've seen enter your bathroom!";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Really? Why so many?";
                    //sobriety
                    statValue[0, 0] = 1;
                    //social
                    statValue[0, 1] = -1;
                    //love
                    statValue[0, 2] = 0;


                    statQuote[0] = RandomQuote("social");

                    //button 2 text + stat schange
                    answerText[1].text =
                        "Alcohol makes people do strange things.";

                    //sobriety
                    statValue[1, 0] = 1;
                    //social
                    statValue[1, 1] = -2;
                    //love
                    statValue[1, 2] = 0;

                    statQuote[1] = RandomQuote("social");

                    //button 3 text + stat schange
                    answerText[2].text =
                        "Don't they know what I do in there?";

                    //sobriety
                    statValue[2, 0] = 0;
                    //social
                    statValue[2, 1] = 2;
                    //love
                    statValue[2, 2] = 0;

                    statQuote[2] = RandomQuote("social");

                    break;

                case 10:
                    //Dialogue text
                    dialogue.text =
                        "Bro, I got condoms if you need.";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Bruh, I've been prepared for days!";
                    //sobriety
                    statValue[0, 0] = 0;
                    //social
                    statValue[0, 1] = 1;
                    //love
                    statValue[0, 2] = -1;


                    statQuote[0] = RandomQuote("social");

                    //button 2 text + stat schange
                    answerText[1].text =
                        "Ugh, I never use condoms.";

                    //sobriety
                    statValue[1, 0] = -2;
                    //social
                    statValue[1, 1] = -2;
                    //love
                    statValue[1, 2] = -2;

                    statQuote[1] = RandomQuote("social");

                    //button 3 text + stat schange
                    answerText[2].text =
                        "Thanks, but I'm not sure if anything will happen.";

                    //sobriety
                    statValue[2, 0] = 1;
                    //social
                    statValue[2, 1] = 1;
                    //love
                    statValue[2, 2] = -1;

                    statQuote[2] = RandomQuote("social");

                    break;
                #endregion
                #region Love Scenarios
                case 11:
                    //Dialogue text
                    dialogue.text =
                        gController.PlayerName("girl") + " is looking good tonight!";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Does she?";
                    //sobriety
                    statValue[0, 0] = 0;
                    //social
                    statValue[0, 1] = -1;
                    //love
                    statValue[0, 2] = -2;


                    statQuote[0] = RandomQuote("love");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "She always looks good.";

                    //sobriety
                    statValue[1, 0] = 0;
                    //social
                    statValue[1, 1] = 1;
                    //love
                    statValue[1, 2] = 2;

                    statQuote[1] = RandomQuote("love");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "She dressed really nice tonight.";

                    //sobriety
                    statValue[2, 0] = 0;
                    //social
                    statValue[2, 1] = 1;
                    //love
                    statValue[2, 2] = 1;

                    statQuote[2] = RandomQuote("love");

                    break;

                case 12:
                    //Dialogue text
                    dialogue.text =
                       "How are things going with " +gController.PlayerName("girl")+"?";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "I'm happier than ever!";
                    //sobriety
                    statValue[0, 0] = 0;
                    //social
                    statValue[0, 1] = 2;
                    //love
                    statValue[0, 2] = 2;


                    statQuote[0] = RandomQuote("love");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "I'm not sure where things are heading.";

                    //sobriety
                    statValue[1, 0] = 0;
                    //social
                    statValue[1, 1] = 0;
                    //love
                    statValue[1, 2] = -2;

                    statQuote[1] = RandomQuote("love");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "I asked her to stay after. I don't think she will.";

                    //sobriety
                    statValue[2, 0] = 0;
                    //social
                    statValue[2, 1] = -1;
                    //love
                    statValue[2, 2] = -1;

                    statQuote[2] = RandomQuote("love");

                    break;

                case 13:
                    //Dialogue text
                    dialogue.text =
                       "Are you and " +gController.PlayerName("girl")+ " exclusive? I have a girl to introduce to you.";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Well, we've only been on a few dates...";
                    //sobriety
                    statValue[0, 0] = 0;
                    //social
                    statValue[0, 1] = 1;
                    //love
                    statValue[0, 2] = -2;


                    statQuote[0] = RandomQuote("love");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "What? Of course we are!";

                    //sobriety
                    statValue[1, 0] = 1;
                    //social
                    statValue[1, 1] = 0;
                    //love
                    statValue[1, 2] = 2;

                    statQuote[1] = RandomQuote("love");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "I don't think "+ gController.PlayerName("girl")+ " would like it.";

                    //sobriety
                    statValue[2, 0] = 0;
                    //social
                    statValue[2, 1] = -1;
                    //love
                    statValue[2, 2] = -1;

                    statQuote[2] = RandomQuote("love");

                    break;

                case 14:
                    //Dialogue text
                    dialogue.text =
                        "I think Rony has the hots for you. Make a move or I will, she's super drunk right now.";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "She is pretty hot...";
                    //sobriety
                    statValue[0, 0] = -1;
                    //social
                    statValue[0, 1] = 1;
                    //love
                    statValue[0, 2] = -2;


                    statQuote[0] = RandomQuote("love");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "I'm already dating " + gController.PlayerName("girl") + " and you should wait for Rony to sober up.";

                    //sobriety
                    statValue[1, 0] = 1;
                    //social
                    statValue[1, 1] = 0;
                    //love
                    statValue[1, 2] = 2;

                    statQuote[1] = RandomQuote("love");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "Dude, that's disgusting. You can't take advantage of a drunk person!";

                    //sobriety
                    statValue[2, 0] = 1;
                    //social
                    statValue[2, 1] = -2;
                    //love
                    statValue[2, 2] = 1;

                    statQuote[2] = RandomQuote("love");

                    break;

                case 15:
                    //Dialogue text
                    dialogue.text =
                        "So when is your next date with " + gController.PlayerName("girl") + "?";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "I'm going to wait for her to ask me.";
                    //sobriety
                    statValue[0, 0] = 0;
                    //social
                    statValue[0, 1] = -1;
                    //love
                    statValue[0, 2] = -1;


                    statQuote[0] = RandomQuote("love");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "I'm not sure, I'm waiting to see how tonight goes.";

                    //sobriety
                    statValue[1, 0] = 0;
                    //social
                    statValue[1, 1] = 1;
                    //love
                    statValue[1, 2] = -2;

                    statQuote[1] = RandomQuote("love");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "I have something awesome planned for next weekend, she's going to love it!";

                    //sobriety
                    statValue[2, 0] = 0;
                    //social
                    statValue[2, 1] = 2;
                    //love
                    statValue[2, 2] = 2;

                    statQuote[2] = RandomQuote("love");

                    break;
                    #endregion

            }

            maleScenario.RemoveAt(scenario);
        }
        #endregion

    #region female scenarios
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


                    statQuote[0] = RandomQuote("love");

                    //button 2 text + stat schange
                    answerText[1].text =
                        "";

                    //sobriety
                    statValue[1, 0] = -1;
                    //social
                    statValue[1, 1] = -1;
                    //love
                    statValue[1, 2] = -1;

                    statQuote[1] = RandomQuote("love");

                    //button 3 text + stat schange
                    answerText[2].text =
                        "";

                    //sobriety
                    statValue[2, 0] = 2;
                    //social
                    statValue[2, 1] = -1;
                    //love
                    statValue[2, 2] = 1;

                    statQuote[2] = RandomQuote("love");

                    break;

                #region Mind Scenarios
                case 1:
                    //Dialogue text
                    dialogue.text =
                        "Let's fill up that red cup!";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Alright, but not too much. I havn't eaten yet.";
                    //sobriety
                    statValue[0, 0] = -1;
                    //social
                    statValue[0, 1] = 1;
                    //love
                    statValue[0, 2] = 0;


                    statQuote[0] = RandomQuote("sobriety");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "Fill it up!";

                    //sobriety
                    statValue[1, 0] = -2;
                    //social
                    statValue[1, 1] = 2;
                    //love
                    statValue[1, 2] = 0;

                    statQuote[1] = RandomQuote("sobriety");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "I think I'll just have a soda.";

                    //sobriety
                    statValue[2, 0] = 2;
                    //social
                    statValue[2, 1] = -1;
                    //love
                    statValue[2, 2] = 0;

                    statQuote[2] = RandomQuote("sobriety");

                    break;

                case 2:
                    //Dialogue text
                    dialogue.text =
                        "I'm going to mix some drinks. Want anything?";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Suprise me!";
                    //sobriety
                    statValue[0, 0] = -2;
                    //social
                    statValue[0, 1] = 2;
                    //love
                    statValue[0, 2] = 0;


                    statQuote[0] = RandomQuote("sobriety");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "Rum and cola, but easy on the rum.";

                    //sobriety
                    statValue[1, 0] = -1;
                    //social
                    statValue[1, 1] = 2;
                    //love
                    statValue[1, 2] = 0;

                    statQuote[1] = RandomQuote("sobriety");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "No thanks, I'm good."; 
                    //sobriety
                    statValue[2, 0] = 2;
                    //social
                    statValue[2, 1] = -1;
                    //love
                    statValue[2, 2] = 0;

                    statQuote[2] = RandomQuote("sobriety");

                    break;

                case 3:
                    //Dialogue text
                    dialogue.text =
                        "This week was intense, let's unwind!";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Ok, but I don't want to get wasted.";
                    //sobriety
                    statValue[0, 0] = -1;
                    //social
                    statValue[0, 1] = 1;
                    //love
                    statValue[0, 2] = 0;


                    statQuote[0] = RandomQuote("sobriety");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "Time for some serious drinking!";

                    //sobriety
                    statValue[1, 0] = -2;
                    //social
                    statValue[1, 1] = 2;
                    //love
                    statValue[1, 2] = 0;

                    statQuote[1] = RandomQuote("sobriety");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "Blegh, I think I've had enough to drink.";

                    //sobriety
                    statValue[2, 0] = 2;
                    //social
                    statValue[2, 1] = -1;
                    //love
                    statValue[2, 2] = 0;

                    statQuote[2] = RandomQuote("sobriety");

                    break;

                case 4:
                    //Dialogue text
                    dialogue.text =
                        "Go take shots with " + gController.PlayerName("boy")+"!";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Sounds like fun!";
                    //sobriety
                    statValue[0, 0] = -2;
                    //social
                    statValue[0, 1] = 1;
                    //love
                    statValue[0, 2] = 2;


                    statQuote[0] = RandomQuote("sobriety");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "Just one.";

                    //sobriety
                    statValue[1, 0] = -1;
                    //social
                    statValue[1, 1] = 2;
                    //love
                    statValue[1, 2] = 1;

                    statQuote[1] = RandomQuote("sobriety");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "I don't want to pressure him into drinking.";

                    //sobriety
                    statValue[2, 0] = 2;
                    //social
                    statValue[2, 1] = -2;
                    //love
                    statValue[2, 2] = 2;

                    statQuote[2] = RandomQuote("sobriety");

                    break;

                case 5:
                    //Dialogue text
                    dialogue.text =
                        "Let's play some drinking games with the boys!";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "You mean own the boys at some drinking games!";
                    //sobriety
                    statValue[0, 0] = -2;
                    //social
                    statValue[0, 1] = 2;
                    //love
                    statValue[0, 2] = 0;


                    statQuote[0] = RandomQuote("sobriety");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "I'll pass, I don't want to embarrass " +gController.PlayerName("boy")+".";

                    //sobriety
                    statValue[1, 0] = 2;
                    //social
                    statValue[1, 1] = -1;
                    //love
                    statValue[1, 2] = 1;

                    statQuote[1] = RandomQuote("sobriety");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "I'd rather not, "+gController.PlayerName("boy") + " might not want to drink.";

                    //sobriety
                    statValue[2, 0] = 1;
                    //social
                    statValue[2, 1] = -1;
                    //love
                    statValue[2, 2] = 2;

                    statQuote[2] = RandomQuote("sobriety");

                    break;
                #endregion
                #region Social Scenarios
                case 6:
                    //Dialogue text
                    dialogue.text =
                        "Truth or dare? Or just dare, I dare you to have sex tonight!";
                    //button 1 text + stat change
                    answerText[0].text =
                        "I dare you to shut-up!";
                    //sobriety
                    statValue[0, 0] = 0;
                    //social
                    statValue[0, 1] = -1;
                    //love
                    statValue[0, 2] = 1;


                    statQuote[0] = RandomQuote("social");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "Truth: I might, but not because of some dumb dare.";

                    //sobriety
                    statValue[1, 0] = 0;
                    //social
                    statValue[1, 1] = 1;
                    //love
                    statValue[1, 2] = 1;

                    statQuote[1] = RandomQuote("social");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "Like I don't feel pressured enough already?";

                    //sobriety
                    statValue[2, 0] = 0;
                    //social
                    statValue[2, 1] = -2;
                    //love
                    statValue[2, 2] = -1;

                    statQuote[2] = RandomQuote("social");

                    break;

                case 7:
                    //Dialogue text
                    dialogue.text =
                        "Truth or truth: Have you and "+ gController.PlayerName("boy")+" had sex yet?";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Not yet...";
                    //sobriety
                    statValue[0, 0] = 0;
                    //social
                    statValue[0, 1] = -2;
                    //love
                    statValue[0, 2] = -1;


                    statQuote[0] = RandomQuote("social");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "No, do you think he's going to lose interest?";

                    //sobriety
                    statValue[1, 0] = 0;
                    //social
                    statValue[1, 1] = 1;
                    //love
                    statValue[1, 2] = -1;

                    statQuote[1] = RandomQuote("social");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "No, we will when we are both ready.";

                    //sobriety
                    statValue[2, 0] = 0;
                    //social
                    statValue[2, 1] = 1;
                    //love
                    statValue[2, 2] = 2;

                    statQuote[2] = RandomQuote("social");

                    break;

                case 8:
                    //Dialogue text
                    dialogue.text =
                        "Are you and "+ gController.PlayerName("boy") +" just going to go upstairs already?";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "I'm not going to make the first move.";
                    //sobriety
                    statValue[0, 0] = 0;
                    //social
                    statValue[0, 1] = -1;
                    //love
                    statValue[0, 2] = -1;


                    statQuote[0] = RandomQuote("social");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "I'm hanging out with you guys!";

                    //sobriety
                    statValue[1, 0] = 0;
                    //social
                    statValue[1, 1] = 2;
                    //love
                    statValue[1, 2] = 0;

                    statQuote[1] = RandomQuote("social");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "I'm not sure if I want to.";

                    //sobriety
                    statValue[2, 0] = 0;
                    //social
                    statValue[2, 1] = 1;
                    //love
                    statValue[2, 2] = -1;

                    statQuote[2] = RandomQuote("social");

                    break;

                case 9:
                    //Dialogue text
                    dialogue.text =
                        "The bathroom has been constantly occupied by people making out.";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Wow, that's nasty!";
                    //sobriety
                    statValue[0, 0] = 0;
                    //social
                    statValue[0, 1] = -2;
                    //love
                    statValue[0, 2] = 0;


                    statQuote[0] = RandomQuote("social");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "I guess alcohol makes people horny.";

                    //sobriety
                    statValue[1, 0] = 1;
                    //social
                    statValue[1, 1] = 1;
                    //love
                    statValue[1, 2] = 0;

                    statQuote[1] = RandomQuote("social");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "This is a party, there has to be a better place and time to make out!";

                    //sobriety
                    statValue[2, 0] = 0;
                    //social
                    statValue[2, 1] = -1;
                    //love
                    statValue[2, 2] = 1;

                    statQuote[2] = RandomQuote("social");

                    break;

                case 10:
                    //Dialogue text
                    dialogue.text =
                        "Tessa went upstairs with some rando. She was pretty drunk, but the guy was hot!";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Should we go check on her?";
                    //sobriety
                    statValue[0, 0] = 1;
                    //social
                    statValue[0, 1] = 1;
                    //love
                    statValue[0, 2] = 0;


                    statQuote[0] = RandomQuote("social");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "Wow, go Tessa!";

                    //sobriety
                    statValue[1, 0] = -1;
                    //social
                    statValue[1, 1] = 1;
                    //love
                    statValue[1, 2] = 0;

                    statQuote[1] = RandomQuote("social");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "I hope she's not too drunk.";

                    //sobriety
                    statValue[2, 0] = 2;
                    //social
                    statValue[2, 1] = -1;
                    //love
                    statValue[2, 2] = 0;

                    statQuote[2] = RandomQuote("social");

                    break;
                #endregion
                #region Love Scenarios

                case 11:
                    //Dialogue text
                    dialogue.text =
                        "So what's up with you and " + gController.PlayerName("boy")+"?";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "I think it's been going well.";
                    //sobriety
                    statValue[0, 0] = 0;
                    //social
                    statValue[0, 1] = 1;
                    //love
                    statValue[0, 2] = 1;


                    statQuote[0] = RandomQuote("love");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "It's been going great!";

                    //sobriety
                    statValue[1, 0] = 0;
                    //social
                    statValue[1, 1] = 1;
                    //love
                    statValue[1, 2] = 2;

                    statQuote[1] = RandomQuote("love");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "He's nice and all, but I don't know...";

                    //sobriety
                    statValue[2, 0] = 0;
                    //social
                    statValue[2, 1] = -1;
                    //love
                    statValue[2, 2] = -2;

                    statQuote[2] = RandomQuote("love");

                    break;

                case 12:
                    //Dialogue text
                    dialogue.text =
                        "Did he ask you to stay the night? Do you want to?";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Yes, and I don't know.";
                    //sobriety
                    statValue[0, 0] = 0;
                    //social
                    statValue[0, 1] = 1;
                    //love
                    statValue[0, 2] = -1;


                    statQuote[0] = RandomQuote("love");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "Yes and yes, but only if he's sober.";

                    //sobriety
                    statValue[1, 0] = 1;
                    //social
                    statValue[1, 1] = 0;
                    //love
                    statValue[1, 2] = 2;

                    statQuote[1] = RandomQuote("love");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "I don't think I want to stay.";
                    //sobriety
                    statValue[2, 0] = 0;
                    //social
                    statValue[2, 1] = -1;
                    //love
                    statValue[2, 2] = -2;

                    statQuote[2] = RandomQuote("love");

                    break;

                case 13:
                    //Dialogue text
                    dialogue.text =
                        "I heard that Jenna has been flirting with " + gController.PlayerName("boy") +".";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "It's just a rumor. "+gController.PlayerName("boy")+ " wouldn't flirt with other girls.";
                    //sobriety
                    statValue[0, 0] = 0;
                    //social
                    statValue[0, 1] = -2;
                    //love
                    statValue[0, 2] = 2;


                    statQuote[0] = RandomQuote("love");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "He likes me way more than Jenna.";

                    //sobriety
                    statValue[1, 0] = 0;
                    //social
                    statValue[1, 1] = 1;
                    //love
                    statValue[1, 2] = 1;

                    statQuote[1] = RandomQuote("love");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "Really? Maybe I should ask him if anything is going on.";

                    //sobriety
                    statValue[2, 0] = 0;
                    //social
                    statValue[2, 1] = 1;
                    //love
                    statValue[2, 2] = -2;

                    statQuote[2] = RandomQuote("love");

                    break;

                case 14:
                    //Dialogue text
                    dialogue.text =
                        "Have you talked to your ex at all?";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "He texted me last week, but I ignored it.";
                    //sobriety
                    statValue[0, 0] = 0;
                    //social
                    statValue[0, 1] = 1;
                    //love
                    statValue[0, 2] = 2;


                    statQuote[0] = RandomQuote("love");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "We've texted a little.";

                    //sobriety
                    statValue[1, 0] = 0;
                    //social
                    statValue[1, 1] = 1;
                    //love
                    statValue[1, 2] = -1;

                    statQuote[1] = RandomQuote("love");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "We've had a few conversations, I might still have some feelings.";

                    //sobriety
                    statValue[2, 0] = 0;
                    //social
                    statValue[2, 1] = -1;
                    //love
                    statValue[2, 2] = -2;

                    statQuote[2] = RandomQuote("love");

                    break;

                case 15:
                    //Dialogue text
                    dialogue.text =
                        "You should share a drink and a kiss with "+ gController.PlayerName("boy")+".";
                    //button 1 text + stat schange
                    answerText[0].text =
                        "Perhaps a kiss, but I want to stop drinking.";
                    //sobriety
                    statValue[0, 0] = 2;
                    //social
                    statValue[0, 1] = 1;
                    //love
                    statValue[0, 2] = 2;


                    statQuote[0] = RandomQuote("love");

                    //button 2 text + stat schange
                    answerText[1].text =
                       "Eww, gross!";

                    //sobriety
                    statValue[1, 0] = 0;
                    //social
                    statValue[1, 1] = -1;
                    //love
                    statValue[1, 2] = -2;

                    statQuote[1] = RandomQuote("love");

                    //button 3 text + stat schange
                    answerText[2].text =
                       "Why not a few of each?";

                    //sobriety
                    statValue[2, 0] = -2;
                    //social
                    statValue[2, 1] = 2;
                    //love
                    statValue[2, 2] = 2;

                    statQuote[2] = RandomQuote("love");

                    break;

                    #endregion

            }

            femaleScenario.RemoveAt(scenario);
        }
    #endregion
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
        EnableStatUI(true,statQuote[1],statNumber);
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
    private void EnableStatUI(bool active, string quote, int[] statnumber)
    {
        if (active)
        {
            //boy
            if (!gController.CheckPlayer())
            {
                //statBg.sprite = statbgSprite[0];
                statButtonImage.sprite = statButtonSprite[0];
            }
            //girl
            else if (gController.CheckPlayer())
            {
                //statBg.sprite = statbgSprite[1];
                statButtonImage.sprite = statButtonSprite[1];
            }

            statText.text = quote; 
            statText.gameObject.SetActive(true);
            statBg.gameObject.SetActive(true);


            //change number text
            for (int i = 0; i < statnumber.Length; i++)
            {
                if (statnumber[i] > 0)
                    symbol[i] = "+";
                else if (statnumber[i] < 0)
                    symbol[i] = "-";
                else
                    symbol[i] = "";
            }

            StartCoroutine(UpdateStats(statnumber));
        }

        else if (!active)
        {
            statText.gameObject.SetActive(false);
            statBg.gameObject.SetActive(false);
            statButton.gameObject.SetActive(false);
        }
    }

    private IEnumerator UpdateStats(int[] statNumber)
    {
        
        if (!gController.CheckPlayer())
        {
            Color[] statColors = new Color[3];
            for (int i = 0; i < statColors.Length; i++)
                statColors[i] = boyIconValue[i];

            float elapsedTime = 0;
            float totalTime = 2;

            while (elapsedTime < totalTime)
            {
                for (int i = 0; i < changeText.Length; i++)
                {
                    int number = Mathf.RoundToInt(Mathf.Lerp(Mathf.Abs(statNumber[i]) * 12.5f, 0, elapsedTime / totalTime));
                    if (number == 0)
                        changeText[i].text = "";
                    else
                     changeText[i].text = symbol[i] + number;
                }

                valueText[0].text = Mathf.RoundToInt(Mathf.Lerp(boyStatValue[0], ((gController.CheckSobriety(gController.CheckPlayer())+4)*12.5f), elapsedTime / totalTime)).ToString();
                valueText[1].text = Mathf.RoundToInt(Mathf.Lerp(boyStatValue[1], ((gController.CheckSocial(gController.CheckPlayer()) + 4) * 12.5f), elapsedTime / totalTime)).ToString();
                valueText[2].text = Mathf.RoundToInt(Mathf.Lerp(boyStatValue[2], ((gController.CheckLove(gController.CheckPlayer()) + 4) * 12.5f), elapsedTime / totalTime)).ToString();

                statIcons[0].color = Color.Lerp(statColors[0], new Color(statColors[0].r, statColors[0].g, statColors[0].b, .502f + (gController.CheckSobriety(gController.CheckPlayer()) *.125f)), (elapsedTime / totalTime));
                statIcons[1].color = Color.Lerp(statColors[1], new Color(statColors[1].r, statColors[1].g, statColors[1].b, .502f + (gController.CheckSocial(gController.CheckPlayer()) * .125f)), (elapsedTime / totalTime));
                statIcons[2].color = Color.Lerp(statColors[2], new Color(statColors[2].r, statColors[2].g, statColors[2].b, .502f + (gController.CheckLove(gController.CheckPlayer()) * .125f)), (elapsedTime / totalTime));
                elapsedTime += Time.deltaTime;

                //Debug.Log(elapsedTime);
                yield return null; 
            }

            for (int i = 0; i < 3; i++)
            {
                boyIconValue[i] = new Color(statIcons[i].color.r, statIcons[i].color.g, statIcons[i].color.b, statIcons[i].color.a);
                boyStatValue[i] = int.Parse(valueText[i].text);
                //Debug.Log("SavingColor" + i);
            }

            gController.ChangeColor(boyIconValue);
            gController.ChangeStatTextValue(boyStatValue);
        }

        else if (gController.CheckPlayer())
        {
            Color[] statColors = new Color[3];
            for (int i = 0; i < statColors.Length; i++)
                statColors[i] = girlIconValue[i];

            float elapsedTime = 0;
            float totalTime = 2;

            while (elapsedTime < totalTime)
            {
                for (int i = 0; i < changeText.Length; i++)
                {
                    int number = Mathf.RoundToInt(Mathf.Lerp(Mathf.Abs(statNumber[i]) * 12.5f, 0, elapsedTime / totalTime));
                    if (number == 0)
                        changeText[i].text = "";
                    else
                        changeText[i].text = symbol[i] + number;
                }

                valueText[0].text = Mathf.RoundToInt(Mathf.Lerp(girlStatValue[0], ((gController.CheckSobriety(gController.CheckPlayer()) + 4) * 12.5f), elapsedTime / totalTime)).ToString();
                valueText[1].text = Mathf.RoundToInt(Mathf.Lerp(girlStatValue[1], ((gController.CheckSocial(gController.CheckPlayer()) + 4) * 12.5f), elapsedTime / totalTime)).ToString();
                valueText[2].text = Mathf.RoundToInt(Mathf.Lerp(girlStatValue[2], ((gController.CheckLove(gController.CheckPlayer()) + 4) * 12.5f), elapsedTime / totalTime)).ToString();

                statIcons[0].color = Color.Lerp(statColors[0], new Color(statColors[0].r, statColors[0].g, statColors[0].b, .502f + (gController.CheckSobriety(gController.CheckPlayer()) * .125f)), (elapsedTime / totalTime));
                statIcons[1].color = Color.Lerp(statColors[1], new Color(statColors[1].r, statColors[1].g, statColors[1].b, .502f + (gController.CheckSocial(gController.CheckPlayer()) * .125f)), (elapsedTime / totalTime));
                statIcons[2].color = Color.Lerp(statColors[2], new Color(statColors[2].r, statColors[2].g, statColors[2].b, .502f + (gController.CheckLove(gController.CheckPlayer()) * .125f)), (elapsedTime / totalTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            for (int i = 0; i < 3; i++)
            {
                girlIconValue[i] = new Color(statIcons[i].color.r, statIcons[i].color.g, statIcons[i].color.b, statIcons[i].color.a);
                girlStatValue[i] = int.Parse(valueText[i].text);   
                //Debug.Log("SavingColor" + i);
            }

            gController.ChangeColor(girlIconValue);
            gController.ChangeStatTextValue(girlStatValue);
        }

        statButton.gameObject.SetActive(true);
    }

    public void StatButton()
    {
        gController.ChangePlayer();

        if (counter <= 10)
            PassPhone();
        else
            SceneManager.LoadScene("End");

        statText.gameObject.SetActive(false);
        statBg.gameObject.SetActive(false);
        statButton.gameObject.SetActive(false);
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
