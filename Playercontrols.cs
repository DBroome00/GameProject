using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontrols : MonoBehaviour
{
    private Rigidbody2D pc;
    private Animator anim;
    private enum State {idle, running, jumping, falling}
    private State state = State.idle;private Collider2D coll;
    [SerializeField]private LayerMask ground;

    bool CanDoublejump;
    private void Start()
    {
        pc = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    public void Update()
    {



        float HDirection = Input.GetAxis("Horizontal");

        if (HDirection < 0)
        {
            pc.velocity = new Vector2(-7, pc.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (HDirection > 0)
        {
            pc.velocity = new Vector2(7, pc.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        else 
        {
            
        }


        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers())
        {
            pc.velocity = new Vector2(pc.velocity.x, 13);
            state = State.jumping;
            CanDoublejump = true;
        }
        else if (Input.GetButtonDown("Jump") && (CanDoublejump))
        {
            pc.velocity = new Vector2(pc.velocity.x, 13);
            state = State.jumping;
            CanDoublejump = false;
        }

        VelocityState();
        anim.SetInteger("state", (int)state);
       
    }

    private void VelocityState()

    {
        if (state == State.jumping)
        {
            if (pc.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }

        else if (Mathf.Abs(pc.velocity.x) > 2f)
        {
            //moving
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
        
    }
}
