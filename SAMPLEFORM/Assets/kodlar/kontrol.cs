using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class kontrol : MonoBehaviour
{
    public Sprite[] Jbekleme;
    public Sprite[] Jziplama;
    public Sprite[] Jyurume;
    public Text canText;
    public Text bolumBittiText;
    public Image gameoverBackground;
    
    float gameoverSayac = 0;
    int can = 100;

    SpriteRenderer spriteRendere;
    Rigidbody2D Fizik;
  
    Vector3 vec;
    int JbeklemeSayac = 0;
    int JyurumeSayac = 0;

    float horizontal = 0;
    float JbeklemeAnim = 0;
    float JyurumeAnim = 0;
    float anaMenuyedonmezaman = 0;

    bool sadecebirkerezipla = true;
    
    Vector3 kamerailk;
    Vector3 kamerason;
    
    GameObject kam;
 
   

    void Start()
    {
        
        spriteRendere = GetComponent<SpriteRenderer>();
        Fizik = GetComponent<Rigidbody2D>();
       
        kam = GameObject.FindGameObjectWithTag("MainCamera");
        PlayerPrefs.SetInt("kacincilevel", SceneManager.GetActiveScene().buildIndex);
        kamerailk = kam.transform.position - transform.position;
        canText.text = "CAN   " + can;
        
        


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (sadecebirkerezipla)
            {
                Fizik.AddForce(new Vector2(0, 200));
                sadecebirkerezipla = false;
            }
        }
    }
    void FixedUpdate()
    {
        Jhareket();
        animasyon();
        if (can<=0)
        {
            
            canText.enabled = false;
            gameoverSayac += 0.03f;
            gameoverBackground.color = new Color(0, 0, 0, gameoverSayac);
            bolumBittiText.text = " KAYBETTIN! ";
            anaMenuyedonmezaman += Time.deltaTime;
            
            if (anaMenuyedonmezaman>1)
            {
                SceneManager.LoadScene("anaMenu");
            }
        }

    }
    void Jhareket()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vec = new Vector3(horizontal * 7, Fizik.linearVelocity.y, 0);
        Fizik.linearVelocity = vec;
    }
     void OnCollisionEnter2D(Collision2D col)
    {
        sadecebirkerezipla = true;
    }
     void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="ates")
        {
            can-=3;
            canText.text = "CAN   " + can;
        }

        if (col.gameObject.tag == "deniz")
        {
            can -= 100;
           


        }
        if (col.gameObject.tag == "Enemy_Ozai")
        {
            can -= 15;
            canText.text = "CAN   " + can;
            
        }
        if (col.gameObject.tag == "urr")
        {
            can-=15;
            canText.text = "CAN   " + can;
        }
        if (col.gameObject.tag == "sontabela")
        {

            
            canText.enabled = false;
            bolumBittiText.text = " KAZANDIN! ";
            SceneManager.LoadScene("lvl2");
            


        }
          if (col.gameObject.tag == "sontabela2")
        {

            
            canText.enabled = false;
            bolumBittiText.text = " KAZANDIN! ";
            SceneManager.LoadScene("anaMenu");
            


        }

    }
    private void LateUpdate()
    {
        kamera();
    }
    void kamera()
    {
        kamerason = kamerailk + transform.position;
        kam.transform.position = kamerason;
    }
    void animasyon()
    {
        if (sadecebirkerezipla)
        {
            if (horizontal == 0)
            {
                JbeklemeAnim += Time.deltaTime;
                if (JbeklemeAnim > 0.13f)
                {
                    spriteRendere.sprite = Jbekleme[JbeklemeSayac++];
                    if (JbeklemeSayac == Jbekleme.Length)
                    {
                        JbeklemeSayac = 0;
                    }
                    JbeklemeAnim = 0;

                }
            }
            else if (horizontal > 0)
            {

                JyurumeAnim += Time.deltaTime;
                if (JyurumeAnim > 0.15f)
                {
                    spriteRendere.sprite = Jyurume[JyurumeSayac++];
                    if (JyurumeSayac == Jyurume.Length)
                    {
                        JyurumeSayac = 0;
                    }
                    JyurumeAnim = 0;

                }
                transform.localScale = new Vector3(5, 5, 1);

            }
            else if (horizontal < 0)
            {

                JyurumeAnim += Time.deltaTime;
                if (JyurumeAnim > 0.15f)
                {
                    spriteRendere.sprite = Jyurume[JyurumeSayac++];
                    if (JyurumeSayac == Jyurume.Length)
                    {
                        JyurumeSayac = 0;
                    }
                    JyurumeAnim = 0;

                }
                transform.localScale = new Vector3(-5, 5, 1);

            }
          
        }
        else
        {
            if (Fizik.linearVelocity.y > 0)
            {
                spriteRendere.sprite = Jziplama[0];

            }
            else
            {
                spriteRendere.sprite = Jziplama[1];
            }
            if (horizontal > 0)
            {
                transform.localScale = new Vector3(5, 5, 1);
            }
            if (horizontal<0)
            {
                transform.localScale = new Vector3(-5, 5, 1);
            }


        }

    }

}
