using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Hand hand;
    public string name;
    public TextMesh textmesh;

    // TODO

    public void Start()
    {
        textmesh.text = name;
    }
}
