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



    //Scenarios
    [SerializeField]
    private List<int> maleScenario = new List<int>();
    [SerializeField]
    private List<int> femaleScenario = new List<int>();
    private int scenario;

    //UI
    [SerializeField]
    private Button[] answerButton;
    [SerializeField]
    private Text dialogue;

    //Stat Values
    private int[] statValue = new int[3];
    private string changeStat;

    private GameController gController;

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

        answerButton[0].onClick.AddListener(SubmitAnswer_1);
        answerButton[1].onClick.AddListener(SubmitAnswer_2);
        answerButton[2].onClick.AddListener(SubmitAnswer_3);

        SetScenario(); 
    }

    //creates the scenario
    public void SetScenario()
    {
        //male scenarios
        if (!gController.CheckPlayer())
        {
            scenario = Random.Range(0, maleScenario.Count);
            maleScenario.RemoveAt(scenario);

            switch (scenario+1)
            {
                default:
                    //Dialogue text
                    dialogue.text =
                        "";
                    //button 1 text + stat schange
                    answerButton[0].GetComponentInChildren<Text>().text =
                        "";
                    statValue[0] = 0;

                    //button 2 text + stat schange
                    answerButton[1].GetComponentInChildren<Text>().text =
                        "";

                    statValue[1] = 0;
                    //button 3 text + stat schange
                    answerButton[2].GetComponentInChildren<Text>().text =
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
                    answerButton[0].GetComponentInChildren<Text>().text =
                        "Bring it on";
                    statValue[0] = 0; 

                    //button 2 text + stat schange
                    answerButton[1].GetComponentInChildren<Text>().text =
                        "Ok but just one";

                    statValue[1] = 0;
                    //button 3 text + stat schange
                    answerButton[2].GetComponentInChildren<Text>().text =
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
                    answerButton[0].GetComponentInChildren<Text>().text =
                        "No thanks, I'm driving";
                    statValue[0] = 0;

                    //button 2 text + stat schange
                    answerButton[1].GetComponentInChildren<Text>().text =
                        "Sure, thanks";

                    statValue[1] = 0;
                    //button 3 text + stat schange
                    answerButton[2].GetComponentInChildren<Text>().text =
                        "Ok, but I shouldn't drink too much, I'm driving";

                    statValue[2] = 0;

                    //Stat Being Changed
                    changeStat = "ChangeSobriety";
                    break;
            }
        }

        //female scenarios
        else if (gController.CheckPlayer())
        {
            switch (femaleScenario[scenario])
            {
                default:
                    //insert code
                    break;

                case 1:
                    //insert code
                    break;
            }
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
}
