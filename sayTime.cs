using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class sayTime : MonoBehaviour
{

    public Flowchart flowchart;

    // Update is called once per frame
    void Update()
    {
        flowchart.SetStringVariable("level3time", PlayerPrefs.GetString("level3time"));
        flowchart.SetIntegerVariable("time", PlayerPrefs.GetInt("level3mins"));
    }
}
