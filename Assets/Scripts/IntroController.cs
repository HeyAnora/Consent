using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class IntroController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] panels;

    [SerializeField]
    private float[] waitTime;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Button continueButton;

    [SerializeField]
    private Text text;

    [SerializeField]
    private AudioClip clip; 

    private GameController gController; 

    void Start()
    {
        gController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        continueButton.onClick.AddListener(Continue);
        text.text =
            gController.PlayerName("boy") + " and " + gController.PlayerName("girl") + 
                " have been dating for a couple weeks. So far they've been in a healthy, loving relationship.\n\n" +

            gController.PlayerName("boy") +" is hosting a party tonight, and " + gController.PlayerName("girl") + " is on the way.\n\n" +
           
            "They are both hoping for a fun night of drinks and games with their friends. Perhaps afterwords, if everything goes right, they will take their realtionship to the next level."
            ;
        StartCoroutine(gController.PlayAudio(clip));
    }
	
    public void Continue()
    {
        StartCoroutine(ChangePanel());
        image.color = Color.white;
        text.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }

    private IEnumerator ChangePanel()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            image.sprite = panels[i];
            yield return new WaitForSeconds(waitTime[i]);
        }

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1); 
    }
}
