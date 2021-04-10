using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    public float runSpeed = 150f;
    public float jumpSpeed = 150f;
    public float clampLeft;
    public float clampRight;

    //Cached component references
    Rigidbody2D myRigidBody;
    BoxCollider2D myBoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
    }

    private void Run()
    {
        //if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < clampRight)
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector2 playerVelocity = new Vector2(runSpeed * Time.deltaTime, myRigidBody.velocity.y);
            //Debug.Log("update fox right fox" + playerVelocity.x);
            myRigidBody.velocity = playerVelocity;

            //transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        //else if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > clampLeft)
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Debug.Log("update fox left fox");
            Vector2 playerVelocity = new Vector2(-runSpeed * Time.deltaTime, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;
            //transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
    }

    private void Jump()
    {
        if (!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Path"))) { return; }

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("fox jump");
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed * Time.deltaTime);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }
}
