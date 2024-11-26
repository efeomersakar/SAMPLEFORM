using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class kameraKontrol : MonoBehaviour
{
    GameObject level1, level2;
    GameObject leveller;
    void Start()
    {
        level1 = GameObject.Find("LEVEL1");
        level2 = GameObject.Find("LEVEL2");
        
        level1.SetActive(false);
        level2.SetActive(false);

        leveller = GameObject.Find("leveller");
        

        for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)
        {
            leveller.transform.GetChild(i).GetComponent<Button>().interactable = true;

        }

    }
    public void butonSec(int gelenButon)
    {
        if (gelenButon==1)
        {
            SceneManager.LoadScene(1);
        }
        if (gelenButon == 2)
        {
            level1.SetActive(true);
            level2.SetActive(true);
        }
        if (gelenButon == 3)
        {

            Application.Quit();
        }
        if (gelenButon == 4)
        {

            SceneManager.LoadScene(1);
        }
        if (gelenButon == 5)
        {

            SceneManager.LoadScene(2);
        }
    }

}
