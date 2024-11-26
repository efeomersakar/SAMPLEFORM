using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Ozai : MonoBehaviour
{
    public int res;
    GameObject[] rota;
    Vector3 aralik;
    bool aralikbirkereal = true;
    bool ilerigeri = true;
    int araliksayac = 0;
    int hiz = 5;
    GameObject J;
    RaycastHit2D ray;
    public Sprite yavasani;
    public Sprite hizliani;
    public GameObject ates;
    float ateszaman = 0;
    SpriteRenderer spriteRenderer;
    public LayerMask layermask; 
    void Start()
    {
        rota = new GameObject[transform.childCount];
        J = GameObject.FindGameObjectWithTag("J");
        spriteRenderer = GetComponent<SpriteRenderer>();
        for (int i = 0; i < rota.Length; i++)
        {
            rota[i] = transform.GetChild(0).gameObject;
            rota[i].transform.SetParent(transform.parent);

        }
    }

    void FixedUpdate()
    {
        JgorulduMu();
        if (ray.collider.tag=="J")
        {
            hiz = 6;
            spriteRenderer.sprite = hizliani;
            atesEt();

        }
        else
        {
            hiz = 3;
            spriteRenderer.sprite = yavasani;
        }
        git();
    }

    void atesEt()
    {
        ateszaman += Time.deltaTime;
        if (ateszaman>Random.Range(0.2f,1))
        {
            Instantiate(ates, transform.position, Quaternion.identity);
            ateszaman = 0;
        }
    }

    void JgorulduMu()
    {
        Vector3 rayY = J.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayY, 1000, layermask);
        Debug.DrawLine(transform.position, ray.point, Color.blue);

    }

    void git()
    {
        if (aralikbirkereal)
        {
            aralik = (rota[araliksayac].transform.position - transform.position).normalized;
            aralikbirkereal = false;
        }
        float mesafe = Vector3.Distance(transform.position, rota[araliksayac].transform.position);
        transform.position += aralik * Time.deltaTime * hiz;
        if (mesafe < 0.5f)
        {
            aralikbirkereal = true;
            if (araliksayac == rota.Length - 1)
            {
                ilerigeri = false;
            }
            else if (araliksayac == 0)
            {
                ilerigeri = true;
            }
            if (ilerigeri)
            {
                araliksayac++;
                transform.localScale = new Vector3(4, 4, 1);
            }
            else
            {
                araliksayac--;
                transform.localScale = new Vector3(-4, 4, 1);
            }

        }
    }
    public Vector2 getYon()
    {
        return (J.transform.position - transform.position).normalized;
    }
  
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);

        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(Ozai))]
[System.Serializable]

class ozaiEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Ozai script = (Ozai)target;
        if (GUILayout.Button("EKLE", GUILayout.MinWidth(50), GUILayout.Width(50)))
        {
            GameObject yeniObje = new GameObject();
            yeniObje.transform.parent = script.transform;
            yeniObje.transform.position = script.transform.position;
            yeniObje.name = script.transform.childCount.ToString();
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layermask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("hizliani"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("yavasani"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ates"));
        

        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}
#endif

