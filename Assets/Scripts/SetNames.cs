using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetNames : MonoBehaviour
{
    [SerializeField]
    private InputField input;

    [SerializeField]
    private Dropdown sexSelect;

    [SerializeField]
    private Button continueButton;

    [SerializeField]
    private GameObject MainInputs;

    [SerializeField]
    private GameObject passText;

    private GameController gController;
    private bool isMale;
    private string playerName;
    private bool nameEntered = false;
    private int players = 0; 


	// Use this for initialization
	void Start ()
    {
        continueButton.onClick.AddListener(SubmitName);
        input.onEndEdit.AddListener(NewName);
        gController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

    private void NewName(string name)
    {
        if (name != "")
        {
            playerName = name;
            nameEntered = true;
        }
    }

    public void SubmitName()
    {
        if (nameEntered)
        {
            if (sexSelect.value == 0)
                isMale = true;
            else if (sexSelect.value == 1)
                isMale = false;

            gController.ChangeName(playerName, isMale);
            players += 1;
            nameEntered = false;
            input.text = "";

            if (players <= 1)
            {
                MainInputs.SetActive(false);
                passText.SetActive(true);
            }

            else if (players > 1)
            {
                SceneManager.LoadScene("Main");
            }
        }

        else if (!nameEntered && MainInputs.activeSelf == false)
        {
            MainInputs.SetActive(true);
            passText.SetActive(false);
        } 
    }	
}
