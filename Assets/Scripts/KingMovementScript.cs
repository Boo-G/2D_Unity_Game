using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMovementScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator animate;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    
    private bool doubleJump;

    

    // Start is called before the first frame update
    void Start()
    {
        // Get regerences from game object
        body = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Variables
        horizontalInput = Input.GetAxis("Horizontal");

 

        // Set animate parameters
        animate.SetBool("run",horizontalInput != 0);
        animate.SetBool("grounded",isGrounded());


        // Wall Jump Logic
        if (wallJumpCooldown > 0.3f){

            body.velocity = new Vector2(horizontalInput*speed, body.velocity.y);

        

            if(onWall() && !isGrounded()){
                body.gravityScale = 0;
                body.velocity = new Vector2(0,0);
            }
            else{
                body.gravityScale = 3;
            }

            if(Input.GetKey(KeyCode.Space)){
                Jump();
            }

        }
        else{
            wallJumpCooldown += Time.deltaTime;
        }

        // flip the player sprite when moving to the left or right
        if(horizontalInput > 0.01){
            transform.localScale = new Vector3(4,4,1);
        }
        else if(horizontalInput < -0.01){
            transform.localScale = new Vector3(-4,4,1);
        }

        
    }

    // Jump function
    private void Jump(){

        // regular jump
        if(isGrounded()){
            doubleJump = true;
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            animate.SetTrigger("jump");
            wallJumpCooldown = 0;
        }

        // double jump 
        else if(doubleJump){
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            animate.SetTrigger("jump");
            doubleJump = false;
        }

        // wall jump
        else if(onWall() && !isGrounded()){
            //idk if i like this if, adds push off of wall when not holding a direction
            if(horizontalInput == 0){
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*10,0) ;
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);


            }
            else{
                // create a force opposite of where the player is facing on the wall and push them up
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*3,6) ;

            }
            wallJumpCooldown = 0;
    
        }


    }


    // create a box that checks 0.1 units under the player to see if there is a groundLayer
    // if something is under the player, raycastHit.collider != null evaluates to True, else False
    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    
    private bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack(){
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
