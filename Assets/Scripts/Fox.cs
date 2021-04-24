using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    public float runSpeed = 150f;
    public float jumpSpeed = 150f;
    public float clampLeft;
    public float clampRight;
    private bool isAlive = true;

    //SFX
    [SerializeField] AudioClip jumpSound;
    [SerializeField] [Range(0, 1)] float jumpSoundVolume = 0.25f;

    //Cached component references
    Rigidbody2D myRigidBody;
    BoxCollider2D myBoxCollider;
    GameLevel gameLevel;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        gameLevel = FindObjectOfType<GameLevel>();
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

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("fox jump");
            myRigidBody.AddForce(transform.up * 20);
        }

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Debug.Log("fox jump");
            myRigidBody.AddForce(transform.up * 20);
        }
    }

    private void JumpAuto()
    {
        if (!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Path"))) { return; }

        Vector2 jumpVelocityToAdd = new Vector2(runSpeed, jumpSpeed);
        myRigidBody.velocity = jumpVelocityToAdd;

        AudioSource.PlayClipAtPoint(jumpSound, Camera.main.transform.position, jumpSoundVolume);
    }
}
