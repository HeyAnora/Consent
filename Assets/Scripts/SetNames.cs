using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetNames : MonoBehaviour
{

    private InputField input;
    private GameController gController;
    [SerializeField]
    private bool isMale;

    public bool entered = false;

	// Use this for initialization
	void Start ()
    {
        input = GetComponent<InputField>();
        input.onEndEdit.AddListener(SubmitName);
        gController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

    public void SubmitName(string name)
    {
        gController.ChangeName(name,isMale);
        entered = true; 
    }	
}
