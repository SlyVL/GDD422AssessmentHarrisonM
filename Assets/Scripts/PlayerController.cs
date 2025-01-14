using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{ //Creating borders on the map so the player cannot move outside it
    float xMin = -130;
    float xMax = 130f;
    float yMin = -400f;
    float yMax = 400f;
    //the player movement speed which can be changed on the inspector 
    [SerializeField] float speed = 10f;
   
   

    private void Start()
    {
        
    }

   

    private void Update()
    {
        //Allows me to put lines of code else where in a different private void
        Moving();
        
    }

    private void Moving()
    {
        //Retrieving the player input for horizontal movement and moving the sprite by speed which was set prior to this and Time.deltaTime
        var xInput = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        var newXPos = Mathf.Clamp(transform.position.x + xInput, xMin, xMax);

        //local vars for vertical movement
        var yInput = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        var newYPos = Mathf.Clamp(transform.position.y + yInput, yMin, yMax);
        //updating the position of the player
        transform.position = new Vector2(newXPos, newYPos);
    }

   

   
}