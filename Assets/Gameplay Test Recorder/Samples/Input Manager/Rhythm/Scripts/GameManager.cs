using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public readonly int scoreHit = 50;
    public readonly int scoreGoodHit = 100;
    public readonly int scorePerfectHit = 300;
    public int Combo;

    public bool startPlaying;
    public bool gameOver;
    public bool startGame = false;

    [Range(10f, 100f)]
    public float Temp;

    public NoteSpawner theNS;

    public RythmScore score;

    public GameObject Menu;
    public GameObject PressSpace;
    

    public Text BPMdisplay;
    public Slider BPMSlider;

    public Text Tempodisplay;
    public Slider TempoSlider;

    public GameObject GameOver;
    public GameObject[] Notes; 


    void start()
    {
        theNS.BPM = BPMSlider.value;
        Temp = TempoSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying && Input.GetKeyDown(KeyCode.Space) && !gameOver && startGame)
        {
            PressSpace.SetActive(false);
            startPlaying = true;
        }

        if (gameOver)
        {
            GameOver.SetActive(true);
        }
        BPMdisplay.text = "BPM: "+BPMSlider.value.ToString();
        Tempodisplay.text = "Tempo: " + TempoSlider.value.ToString();
        Temp += .001f;
    }   

    public void Hit()
    {
        Combo++;
        score.ScoreTotal += scoreHit * Combo;
        
    }
    public void GoodHit()
    {
        Combo++;
        score.ScoreTotal += scoreGoodHit * Combo;
    }
    public void PerfectHit()
    {
        Combo++;
        score.ScoreTotal += scorePerfectHit * Combo;
    }


    public void MainMenu()
    {
        Menu.SetActive(true);
        BPMSlider.gameObject.SetActive(true);
        TempoSlider.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        theNS.BPM = BPMSlider.value;
        Temp = TempoSlider.value;
        theNS.SpawnRate = 60 / theNS.BPM;
        theNS.InvokeRepeating("Spawn", 2f, theNS.SpawnRate);
        BPMSlider.gameObject.SetActive(false);
        TempoSlider.gameObject.SetActive(false);
        Menu.SetActive(false);
        PressSpace.SetActive(true);
        startGame = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Stop()
    {
        //Clear
        Notes = getObjects();
              
        theNS.CancelInvoke();
        
        
        for (int j = 0; j < Notes.Length; j++)
        {
            Destroy(Notes[j]);
        }
        Combo = 0;
        score.ScoreTotal = 0;
        startGame = false;
        GameOver.SetActive(false);
        gameOver = false;
        MainMenu();
    }

    GameObject[] getObjects()
    {
        NoteObject[] scripts = FindObjectsOfType<NoteObject>();
        GameObject[] objects = new GameObject[scripts.Length];
        for (int i = 0; i < objects.Length; i++)
            objects[i] = scripts[i].gameObject;
        return objects;
    }
}
