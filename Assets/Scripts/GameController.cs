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

    #endregion

    void Start()
    {
        int randomPlayer = Random.Range(0, 1);
        if (randomPlayer == 1)
            currentPlayer = true;
        else if (randomPlayer == 0)
            currentPlayer = false; 
    }

    #region ChangeStats

    public void ChangeSobriety(int change)
    {
        if (!currentPlayer)
            boySobriety_ += change;
        else if (currentPlayer)
            girlSobriety_ += change;
    }

    public void ChangeSocial(int change)
    {
        if (!currentPlayer)
            boySocial_ += change;
        else if (currentPlayer)
            girlSocial_ += change;
    }

    public void ChangeLove(int change)
    {
        if (!currentPlayer)
            boyLove_ += change;
        else if (currentPlayer)
            girlLove_ += change; 
    }

    #endregion

    #region CheckStats

    public int CheckSobriety()
    {
        if (!currentPlayer)
            return boySobriety_;

        else if (currentPlayer)
            return girlSobriety_;

        else return 0;
    }

    public int CheckSocial()
    {
        if (!currentPlayer)
            return boySocial_;

        else if (currentPlayer)
            return girlSocial_;

        else return 0;
    }

    public int CheckLove()
    {
        if (!currentPlayer)
            return boyLove_;

        else if (currentPlayer)
            return girlLove_;

        else return 0;
    }



    #endregion

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

    public void ChangeName(string name, bool isMale)
    {
        if (isMale && boyName_ == "")
            boyName_ = name;
        else if (isMale && boyName_ != "")
            girlName_ = name;
        else if (!isMale && girlName_ == "")
            girlName_ = name;
        else if (!isMale && girlName_ != "")
            boyName_ = name;

        Debug.Log(girlName_ + " + " + boyName_);
    }


}
