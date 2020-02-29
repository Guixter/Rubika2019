using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Hand hand;
    public string name;
    public int chips;
    public TextMesh textmesh;

    public void Start()
    {
        textmesh.text = name;
    }

    public int Bid()
    {
        // TODO
        return 0;
    }
}
