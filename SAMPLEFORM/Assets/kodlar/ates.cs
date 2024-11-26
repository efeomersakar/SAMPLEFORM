using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ates : MonoBehaviour
{
    Ozai ozai;
    Rigidbody2D fizik;
    void Start()
    {
        ozai = GameObject.FindGameObjectWithTag("Enemy_Ozai").GetComponent<Ozai>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(ozai.getYon()*1000);
        

    }

}
   
