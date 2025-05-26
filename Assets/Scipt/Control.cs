using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control
{

    [SerializeField] Movement _movement;

    public Control(Movement movement)
    {

        _movement = movement;


    }

    public void ArtificialUpdate()
    {

       var h = Input.GetAxis("Horizontal");
       var v = Input.GetAxis("Vertical");

        _movement.Move(h, v);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            _movement.Jump();

        }


    }




}
