using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode Key;

    public GameObject Hitfx;
    public GameObject GoodHitfx;
    public GameObject PerfectHitfx;

    static GameManager gameManager;
    AudioSource Hit;

    // Start is called before the first frame update
    void Start()
    {
        
        gameManager = FindObjectOfType<GameManager>();
        Hit = FindObjectOfType<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.startPlaying)
        {
            transform.position -= new Vector3(0, gameManager.Temp * Time.deltaTime, 0);
        }
        


        if (Input.GetKeyDown(Key) && canBePressed && !gameManager.gameOver)
        {
            Hit.Play();
            Destroy(gameObject);
            if (Mathf.Abs(transform.position.y) > 0.545f)
            {
                gameManager.Hit();
                Instantiate(Hitfx, transform.position, Hitfx.transform.rotation);
            }
            else if (Mathf.Abs(transform.position.y) > 0.2)
            {
                gameManager.GoodHit();
                Instantiate(GoodHitfx, transform.position, GoodHitfx.transform.rotation);
            }
            else
            {
                gameManager.PerfectHit();
                Instantiate(PerfectHitfx, transform.position, PerfectHitfx.transform.rotation);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Activator")
        {
            canBePressed = false;
        }
    }




}
