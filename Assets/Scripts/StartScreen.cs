using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField]
    private Image bg;
    [SerializeField]
    private Text text;

    private bool interactable = false;
    [SerializeField]
    private GameController gController; 

    void Start()
    {
        StartCoroutine(FadeIn());
        text.gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0) && interactable == true)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex + 1);
        }
	}

    private IEnumerator FadeIn()
    {
        float initialTime = 0;
        float totalTime = 3;
        Color startColor = bg.color;
        StartCoroutine(gController.FadeAudio(true, totalTime));
        while (initialTime < totalTime)
        {
            bg.color = Color.Lerp(startColor, new Color(bg.color.r, bg.color.g, bg.color.b, 1), initialTime / totalTime);
            initialTime += Time.deltaTime;
            yield return null;
        }
        text.gameObject.SetActive(true);
        interactable = true; 
    }
}
