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
    private Button continueButton;

    private GameController gController;
    private bool isMale = false;
    private string playerName;
    private bool nameEntered = false;
    private int players = 0;

    [SerializeField]
    private Text[] genderedText;
    [SerializeField]
    private Color boyColor;

    [SerializeField]
    private Sprite boySprite;
    [SerializeField]
    private Image genderedImage;
    [SerializeField]
    private Sprite boyButton;
    [SerializeField]
    private Sprite boyButtonPressed;

    private SpriteState boySpriteState; 

	// Use this for initialization
	void Start ()
    {
        continueButton.onClick.AddListener(SubmitName);
        input.onEndEdit.AddListener(NewName);
        gController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        boySpriteState.pressedSprite = boyButtonPressed; 
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
            gController.ChangeName(playerName, isMale);
            players += 1;
            nameEntered = false;
            input.text = "";

            if (players <= 1)
            {
                isMale = true;
                for (int i = 0; i < genderedText.Length; i++)
                    genderedText[i].color = boyColor;
                genderedImage.sprite = boySprite;
                continueButton.spriteState = boySpriteState;
                continueButton.gameObject.GetComponent<Image>().sprite = boyButton; 
            }

            else if (players > 1)
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.buildIndex+1);
            }
        }
    }	
}
