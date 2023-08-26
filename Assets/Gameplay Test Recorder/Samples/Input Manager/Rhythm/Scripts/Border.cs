using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Border : MonoBehaviour
{
    public GameManager GM;
    public Text Life_text;
    public int Lifes = 5;
    public GameObject Miss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 InstantiatePosition = collision.gameObject.transform.position;

        Destroy(collision.gameObject);
        
        Instantiate(Miss, InstantiatePosition , Miss.transform.rotation);

        Lifes--;
        Life_text.text = "Lifes: " + Lifes;

        GM.Combo = 0;

        if (Lifes <= 0){
            GM.startPlaying = false;
            GM.gameOver = true;
            Lifes = 5;
        }
        
        
    }


}
