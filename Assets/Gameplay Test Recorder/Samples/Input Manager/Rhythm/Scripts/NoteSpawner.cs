using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
   
    public float BPM = 120f;
    public float SpawnRate;

    public RythmScore score;
    public GameManager gameManger;

    public GameObject[] Arrow = new GameObject[4];
    
    int[] Numbers = new int[18] { 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 0, 1, 2, 3, 4, 5 };
    Vector2[] ArrowIndex = new Vector2[6] {
        new Vector2(0, 1),
        new Vector2(0, 2),
        new Vector2(0, 3),
        new Vector2(3, 1),
        new Vector2(3, 2),
        new Vector2(2, 1) };
    Vector2[] Position = new Vector2[4] { 
        new Vector2(-3.7f, 0), 
        new Vector2(-1.2f, 0), 
        new Vector2( 1.2f, 0), 
        new Vector2( 3.7f, 0) };

    
    void Spawn()
    {
        
        if (!gameManger.gameOver && gameManger.startPlaying)
        {
            var r = Random.Range(0, 18);

            if (r <= 12)
            {

                Instantiate(Arrow[Numbers[r]], (Vector2)transform.position + Position[Numbers[r]], Arrow[Numbers[r]].transform.rotation);
            }
            else
            {
            
                int X = (int)ArrowIndex[Numbers[r]].x;
                int Y = (int)ArrowIndex[Numbers[r]].y;

                Instantiate(Arrow[X], (Vector2)transform.position + Position[X], Arrow[X].transform.rotation);
                Instantiate(Arrow[Y], (Vector2)transform.position + Position[Y], Arrow[Y].transform.rotation);
            }                        
        }
        
    }

}
