using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameController").Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }

#region Variables 
    //names
    private string boyName_ = "MissingBoyName";
    private string girlName_ = "MissingGirlName";
    //private string nameText;

    //stats
    private int boySobriety_ = 0;
    private int girlSobriety_ = 0;

    private int boySocial_ = 0;
    private int girlSocial_ = 0;

    private int boyLove_ = 0;
    private int girlLove_ = 0;

    //CurrentPlayer false = boy, true = girl
    [SerializeField]
    private bool currentPlayer = false;

    //Audio
    private AudioSource audi;

    //color of icons
    public Color[] boyIconValues = new Color[3];
    public Color[] girlIconValues = new Color[3]; 

    #endregion

    void Start()
    {

        audi = GetComponent<AudioSource>();
        audi.Play(); 
    }

    #region ChangeStats

    public void ChangeSobriety(int change)
    {
        if (!currentPlayer)
            boySobriety_ = Mathf.Clamp( boySobriety_+ change,-2,2);
        else if (currentPlayer)
            girlSobriety_ = Mathf.Clamp(girlSobriety_ + change, -2, 2);

        Debug.Log("boy_Sob:" + boySobriety_ + ", girl_sob:" + girlSobriety_);
    }

    public void ChangeSocial(int change)
    {
        if (!currentPlayer)
            boySocial_ = Mathf.Clamp(boySocial_ + change, -2, 2);
        else if (currentPlayer)
            girlSocial_ = Mathf.Clamp(girlSocial_ + change, -2, 2);

        Debug.Log("boy_Soc:" + boySocial_ + ", girl_soc:" + girlSocial_);
    }

    public void ChangeLove(int change)
    {
        if (!currentPlayer)
            boyLove_ = Mathf.Clamp(boyLove_ + change, -2, 2);
        else if (currentPlayer)
            girlLove_ = Mathf.Clamp(girlLove_ + change, -2, 2);

        Debug.Log("boy_Love:" + boyLove_ + ", girl_Love:" + girlLove_);


    }

    public void ChangeColor(Color[] newColor)
    {
        Debug.Log("ChangingColor");
        if (!currentPlayer)
            for (int i = 0; i < boyIconValues.Length; i++)
            {
                boyIconValues[i] = newColor[i];
                //Debug.Log("ColorChanged");
                //Debug.Log(boyIconValues[i]);
            }
        else if (currentPlayer)
            for (int i = 0; i < girlIconValues.Length; i++)
            {
                girlIconValues[i] = newColor[i];
                //Debug.Log("ColorChanged");
                //Debug.Log(girlIconValues[i]);
            }
    }

    #endregion

    #region CheckStats

    public int CheckSobriety(bool gender)
    {
        if (!gender)
            return boySobriety_;

        else if (gender)
            return girlSobriety_;

        else return 0;

    }

    public int CheckSocial(bool gender)
    {
        if (!gender)
            return boySocial_;

        else if (gender)
            return girlSocial_;

        else return 0;
    }

    public int CheckLove(bool gender)
    {
        if (!gender)
            return boyLove_;

        else if (gender)
            return girlLove_;

        else return 0;
    }

    #endregion

    #region Player
    public void ChangePlayer()
    {
        currentPlayer = !currentPlayer;
        Debug.Log(currentPlayer);
    }

    public bool CheckPlayer(){
        return currentPlayer; 
    }

    public string PlayerName(string gender)
    {
        if (gender == "boy")
            return boyName_;
        else if (gender == "girl")
            return girlName_;

        else return "MissingName"; 
    }

    public void ChangeName(string name)
    {
        if (!currentPlayer)
            boyName_ = name;
        else if (currentPlayer)
            girlName_ = name; 

        Debug.Log(girlName_ + " + " + boyName_);
    }

    public void randomPlayer()
    {
        int randomPlayer = Random.Range(0, 1);
        if (randomPlayer == 1)
            currentPlayer = true;
        else if (randomPlayer == 0)
            currentPlayer = false;
    }
    #endregion

    #region Audio

    public IEnumerator PlayAudio(AudioClip clip)
    {
        ////fade current audio
        //float fadeTime = 0;
        //float totalTime = 5;
        //while (fadeTime < totalTime)
        //{
        //    audi.volume = Mathf.Lerp(audi.volume, 0, fadeTime / totalTime);
        //    fadeTime += Time.deltaTime;
        //    yield return null;
        //}


        //audi.clip = clip;
        //audi.Play();
        ////fade in
        //fadeTime = 0;
        //totalTime = 5;
        //while (fadeTime < totalTime)
        //{
        //    audi.volume = Mathf.Lerp(audi.volume, 1, fadeTime / totalTime);
        //    fadeTime += Time.deltaTime;
        //    yield return null;
        //}

        float fadeOut = .1f;
        while (audi.volume > 0)
        {
            audi.volume = Mathf.MoveTowards(audi.volume, 0, fadeOut * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(2);

        audi.clip = clip;
        audi.Play();

        float fadeIn = .1f;
        while (audi.volume < 1)
        {
            audi.volume = Mathf.MoveTowards(audi.volume, 1, fadeIn * Time.deltaTime);
            yield return null;
        }

    }

    public IEnumerator FadeAudio(bool direction,float fadeTime)
    {
        float totalTime = fadeTime;
        float initialTime = 0;
        float initialVolume = audi.volume; 

        //true for fade in
        if (direction ==true)
        {
            while (initialTime < fadeTime)
            {
                audi.volume = Mathf.Lerp(initialVolume, 1, initialTime / totalTime);
                initialTime += Time.deltaTime; 
                yield return null; 
            }
        }

        //false for fade out
        else if (direction ==false)
        {
            while (initialTime < fadeTime)
            {
                audi.volume = Mathf.Lerp(initialVolume, 0, initialTime / totalTime);
                initialTime += Time.deltaTime;
                yield return null;
            }
        }
    }
    #endregion
}
