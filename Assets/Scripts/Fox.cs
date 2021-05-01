using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    public float runSpeed = 5f;
    public float jumpSpeed = 5f;
    public float clampLeft;
    public float clampRight;
    private bool isAlive = true;
    //private bool isPlayerJumped = false;
    private float touchBeginTime;
    private float touchEndTime;
    private float touchTime;

    private float jumpBeginTime;
    private float jumpPrevTime;

    //SFX
    [SerializeField] AudioClip jumpSound;
    [SerializeField] [Range(0, 1)] float jumpSoundVolume = 0.25f;

    [SerializeField] float jumpBonus = 10f; 

    //Cached component references
    Rigidbody2D myRigidBody;
    BoxCollider2D myBoxCollider;
    GameLevel gameLevel;
    PointText pointText;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        gameLevel = FindObjectOfType<GameLevel>();

        pointText = FindObjectOfType<PointText>();
    }

    // Update is called once per frame
    void Update()
    {
        IsTouched();

        if (isAlive)
        { 
            Jump();
            JumpAuto();
        }
    }

    private void IsTouched()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Obstacle")))
        {
            isAlive = false;
            gameLevel.GameOver();
        }
    }

    private void Jump()
    {
        //if (!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Path"))) { return; }

        if (Input.touchCount > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                touchBeginTime = Time.time;
                //pointText.SetPhase(Input.touches[0].phase.ToString() + " - " + touchBeginTime);
                
            }
            else if(Input.touches[0].phase == TouchPhase.Ended)
            {
                touchEndTime = Time.time;
                touchTime = touchEndTime - touchBeginTime;

                //0.04-0.5
                if (touchTime > 0.5f)
                    touchTime = 0.5f;

                //pointText.SetPhase("touch time: " + touchTime.ToString());


                //isPlayerJumped = true;
            } 
        }

        if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("fox jump");
            touchEndTime = Time.time;
            touchTime = 0.5f;
        }


    }

    private void JumpAuto()
    {
        if (!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Path"))) { return; }

        if (touchEndTime < jumpPrevTime)
            touchTime = 0;

        jumpPrevTime = jumpBeginTime;
        jumpBeginTime = Time.time;

        

        float jumpTempSpeed = jumpSpeed + (jumpBonus * touchTime);

        //Debug.Log("t: " + touchTime + "j: " + jumpTempSpeed);
        //Debug.Log("tend: " + touchEndTime + "jprev: " + jumpPrevTime + "jbegin: " + jumpBeginTime);

        pointText.SetPhase("t: " + touchTime + "j: " + jumpTempSpeed);

        Vector2 jumpVelocityToAdd = new Vector2(runSpeed, jumpTempSpeed);
        myRigidBody.velocity = jumpVelocityToAdd;  

        AudioSource.PlayClipAtPoint(jumpSound, Camera.main.transform.position, jumpSoundVolume);

        //touchTime = 0;
    }
}