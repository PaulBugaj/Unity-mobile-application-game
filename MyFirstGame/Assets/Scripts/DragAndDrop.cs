using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    bool moveAllowed;
    Collider2D col;
    public GameObject selectionEffect;
    public GameObject deathEffect;
    GameObject[] gameObjects;

    private GameMaster gm;
    private deployAsteroids da;

    private AudioSource source;
    //public GameObject selectionEffect;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        col = GetComponent<Collider2D>();
    }

    void DestroyAllObjects()
     {
          gameObjects = GameObject.FindGameObjectsWithTag ("Meteor");

         for(var i = 0 ; i < gameObjects.Length ; i ++)
         {
             Destroy(gameObjects[i]);
         }
     }

    // Update is called once per frame
void Update()
{
     if (gameObject.tag == "Meteor"){
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            //first touch on screen
            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                if (col == touchedCollider)
                {
                    Instantiate(selectionEffect,transform.position, Quaternion.identity);
                    source.Play();
                    moveAllowed = true;
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (moveAllowed)
                {
                    transform.position = new Vector2 (touchPosition.x, touchPosition.y);
                }
            }

            //Take finger off screen
            if (touch.phase == TouchPhase.Ended)
            {
                moveAllowed = false;
            }
        }
     }
}

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Planet")
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                gm.GameOver();
                Destroy(gameObject);
                DestroyAllObjects();
                Destroy(collision.gameObject);
            }
        }
}
