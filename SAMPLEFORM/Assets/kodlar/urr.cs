using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class urr : MonoBehaviour
{
    public int res;
    GameObject []rota;
    Vector3 aralik;
    bool aralikbirkereal = true;
    bool ilerigeri = true;
    int araliksayac = 0;
    void Start()
    {
        rota = new GameObject[transform.childCount];
        for (int i = 0; i < rota.Length; i++)
        {
            rota[i] = transform.GetChild(0).gameObject;
            rota[i].transform.SetParent(transform.parent);
            
        }
    }

    void FixedUpdate()
    {
       
        git();
    }

    void git()
    {
        if (aralikbirkereal)
        {
            aralik = (rota[araliksayac].transform.position - transform.position).normalized;
            aralikbirkereal = false;
        }
        float mesafe = Vector3.Distance(transform.position, rota[araliksayac].transform.position);
        transform.position += aralik * Time.deltaTime * 8;
        if (mesafe<0.5f)
        {
            aralikbirkereal = true;
            if (araliksayac == rota.Length-1)
            {
                ilerigeri = false;
            }
            else if(araliksayac == 0)
            {
                ilerigeri = true;
            }
            if (ilerigeri)
            {
                araliksayac++;
            }
            else
            {
                araliksayac--;
            }

        }
    }

#if UNITY_EDITOR
     void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);

        }
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(urr))]
[System.Serializable]

class urrEditor : Editor
{
    public override void OnInspectorGUI()
    {
        urr script = (urr)target;
        if (GUILayout.Button("EKLE", GUILayout.MinWidth(50), GUILayout.Width(50)))
        {
            GameObject yeniObje = new GameObject();
            yeniObje.transform.parent = script.transform;
            yeniObje.transform.position = script.transform.position;
            yeniObje.name = script.transform.childCount.ToString();
        }
    }
}
#endif
