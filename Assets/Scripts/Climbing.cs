using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    //public Rigidbody rb;
    public CharacterController controller;
    public Movement pm;
    public LayerMask whatIsWall;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    private float climbTimer;
    private Vector3 climbMove;
    private bool climbing;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    //public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    private void Update()
    {
        climbMove = new Vector3(0, climbSpeed);
        WallCheck();
        StateMachine();

        if (climbing) ClimbingMovement();
    }

    private void StateMachine()
    {
        // State 1 - Climbing
        //if(wallFront && Input.GetKey(KeyCode.D) && wallLookAngle < maxWallLookAngle)
        if (wallFront && Input.GetKey(KeyCode.D))
            {
            if (!climbing && climbTimer > 0) StartClimbing();

            //timer
            if (climbTimer > 0) climbTimer -= Time.deltaTime;
            if (climbTimer < 0) StopClimbing();
        }
        /*else if (wallFront && Input.GetKey(KeyCode.A) && wallLookAngle < maxWallLookAngle)
        {
            if (!climbing && climbTimer > 0) StartClimbing();

            //timer
            if (climbTimer > 0) climbTimer -= Time.deltaTime;
            if (climbTimer < 0) StopClimbing();
        }*/

        //State 3 - None
        else
        {
            if (climbing) StopClimbing();
        }

    }

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);
        //wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);

        //Debug.Log(wallLookAngle);

        if (pm.isGrounded)
        {
            climbTimer = maxClimbTime;
        }
    }

    private void StartClimbing()
    {
        climbing = true;
    }

    private void ClimbingMovement()
    {
        //rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
        controller.Move(climbMove);
        Debug.Log(wallLookAngle);
    }

    private void StopClimbing()
    {
        climbing = false;
    }
}
