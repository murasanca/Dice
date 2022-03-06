// Murat Sancak

using UnityEngine;

public class Dice:MonoBehaviour
{
    [HideInInspector]
    public int s = 1; // s: Surface.

    private GameObject[] ls; // ls: Lights.

    private int f; // f: For.

    private Transform[] ss; // ss: Surfaces.

    // Murat Sancak

    private void Awake()
    {
        ls=new GameObject[transform.GetChild(0).childCount];
        ss=new Transform[transform.GetChild(1).childCount];

        for(f=0;f<ls.Length;f++)
        {
            ls[f]=transform.GetChild(0).GetChild(f).gameObject;
            ss[f]=transform.GetChild(1).GetChild(f).GetComponent<Transform>();
        }
    }

    private void Update()
    {
        for(f=0;f<ls.Length;f++)
        {
            if(ss[f].position.y>ss[s-1].position.y)
                s=f+1;

            ls[f].SetActive(false);
        }
        ls[s-1].SetActive(true);
    }
}

// Murat Sancak