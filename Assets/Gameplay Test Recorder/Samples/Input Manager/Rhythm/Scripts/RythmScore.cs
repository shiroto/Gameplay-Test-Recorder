using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RythmScore : MonoBehaviour
{
    public Text score;
    public int ScoreTotal;



    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + ScoreTotal;
    }
}
