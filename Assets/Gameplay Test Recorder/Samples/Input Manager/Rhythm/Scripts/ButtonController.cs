using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public KeyCode Key;
    public Sprite defaultImage;
    public Sprite PressedImage;
    public GameManager GM;
    private SpriteRenderer SR;


    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Key) && !GM.gameOver)
        {

            SR.sprite = PressedImage;

        }

        if (Input.GetKeyUp(Key) && !GM.gameOver)
        {
            SR.sprite = defaultImage;
        }

    }
}
  