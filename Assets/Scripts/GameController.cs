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
    private string boyName_ = "";
    private string girlName_ = "";
    //private string nameText;

    //stats
    private int boySobriety_;
    private int girlSobriety_;

    private int boySocial_;
    private int girlSocial_;

    private int boyLove_;
    private int girlLove_;

    //CurrentPlayer false = boy, true = girl
    [SerializeField]
    private bool currentPlayer = false;

    #endregion

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
