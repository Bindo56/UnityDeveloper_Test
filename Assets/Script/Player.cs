using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator playerAnim;
    [SerializeField] Animator holoAnim;
    [SerializeField] Rigidbody playerRigid;
    [SerializeField] float walkingSpeed;
    [SerializeField] float backWalkingSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Transform playerTrans;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckRadius;
    [SerializeField] HoloGravityManipulations gravityManipulations;

    private bool isGrounded;
    private bool falling;

    private static readonly int Walking = Animator.StringToHash("Running");
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int BackWalk = Animator.StringToHash("BackWalk");
  

    void FixedUpdate()
    {
        HandleMovement();
        CheckGroundStatus();
       // Debug.Log("falling" +falling);
        Debug.Log("isground" + isGrounded);
    }

    void Update()
    {
        HandleRotation();
        HandleJump();
        HandleFalling();
    }

    void HandleMovement()
    {
        Vector3 movement = Vector3.zero;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        /*if (upwall)
        {
            movement = new Vector3(0, 0,50 * vertical);
           
           
        }*/

         if (Input.GetKey(KeyCode.W))
         {
            /*if (upwall)
            {

               movement = playerTrans.forward * walkingSpeed * Time.deltaTime;
               //  movement = new Vector3(25 * horizontal, 0,25 * vertical);
                Debug.Log("upMove");

               playerRigid.velocity = movement;
            }*/

            /*if (rightwall)
            {

                 movement = new Vector3(5 * horizontal, 0, 5 * vertical);
                Debug.Log("righwalk");
                playerRigid.velocity = movement;
                return;

            }
            else if (leftwall)
            {
                movement = new Vector3(5 * horizontal, 0, 5 * vertical);
                Debug.Log("leftwalk");
                playerRigid.velocity = movement;
                return;
            }*/

                movement = playerTrans.forward * walkingSpeed * Time.deltaTime;
                playerRigid.velocity = movement;
             SetAnimationState(Walking, true);
             

            

           
         }
         else if (Input.GetKey(KeyCode.S))
         {
             movement = -playerTrans.forward * backWalkingSpeed * Time.deltaTime;
           //  playerRigid.velocity = movement;
             SetAnimationState(BackWalk, true);
         }
        else
        {
           // playerRigid.velocity = Vector3.zero;
            SetAnimationState(Walking, false);
            SetAnimationState(BackWalk, false);
        }
         playerRigid.velocity = movement;

        /*Vector3 adjustedMove = Quaternion.Euler(gravityManipulations.gravityDirection) * movement;
        playerRigid.velocity = new Vector3(adjustedMove.x, playerRigid.velocity.y, adjustedMove.z);*/
        // Vector3 adjustedMove = Quaternion.Euler(gravityManipulations.gravityDirection) * movement;
        //  playerTrans.position += adjustedMove;

        // Vector3 adjustMovement = gravityManipulations.characterRigidbody.transform.TransformDirection(movement);
        //  playerRigid.velocity = adjustedMove;


    }

    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            playerTrans.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerTrans.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
           
            playerRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
           
           
        }
    }
    [SerializeField] float gravityStrenth;
    void HandleFalling()
    {
        if ( !isGrounded)
        {
            playerAnim.SetBool("Jump", true);

            Vector3 customGravity = new Vector3(0, gravityStrenth, 0);
            playerRigid.AddForce( -customGravity * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
       if (isGrounded)
        {
            playerAnim.SetBool("Jump", false); 
           
        }
    }
    bool upwall;
    bool leftwall;
    bool rightwall;
    [SerializeField] Transform playerPrefab;
    Vector3 upLocation;
    void CheckGroundStatus()
    {

       RaycastHit hit;
          isGrounded = Physics.Raycast(groundCheck.position, Vector3.down,out hit,groundCheckRadius, groundLayer);
        if (gravityManipulations.down)
        {
          isGrounded = Physics.Raycast(groundCheck.position, Vector3.down,out hit,groundCheckRadius, groundLayer);

        }else if(gravityManipulations.up)
        {
            isGrounded = Physics.Raycast(groundCheck.position, Vector3.up, out hit, groundCheckRadius, groundLayer);
            if (hit.collider.TryGetComponent(out UpWall upWall))
            {
                Debug.Log("upWall");
                upLocation = hit.collider.transform.position;            
                upwall = true;
            }
        }
        else if (gravityManipulations.right)
        {
            isGrounded = Physics.Raycast(groundCheck.position, new Vector3(0, 0, -1), out hit, groundCheckRadius, groundLayer);
            if (hit.collider.TryGetComponent(out rightWall rightWall))
            {
                Debug.Log("rightWall");
                upLocation = hit.collider.transform.position;
                rightwall = true;
            }
        }
        else if (gravityManipulations.left)
        {
            isGrounded = Physics.Raycast(groundCheck.position, new Vector3(-1, 0, 0), out hit, groundCheckRadius, groundLayer);
            if (hit.collider.TryGetComponent(out LeftWall leftWall))    
            {
                Debug.Log("leftWall");
                upLocation = hit.collider.transform.position;
                leftwall = true;
            }
        }



       

        
    }

    void SetAnimationState(int animationHash, bool state)
    {
        if (state)
        {
            playerAnim.SetBool(animationHash, true);
            holoAnim.SetBool(animationHash, true);
            if (animationHash == Walking || animationHash == BackWalk)
                playerAnim.SetBool(Idle, false);
            holoAnim.SetBool(Idle,false);
        }
        else
        {
            playerAnim.SetBool(animationHash, false);
            holoAnim.SetBool(animationHash, false);
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                playerAnim.SetBool(Idle, true);
                holoAnim.SetBool(Idle, true);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (gravityManipulations.up)
        {
          Vector3 endPosition = groundCheck.position + Vector3.up * groundCheckRadius;
          Gizmos.DrawLine(groundCheck.position,endPosition);
        }
       else if (gravityManipulations.down)
        {
            Vector3 endPosition = groundCheck.position + Vector3.down * groundCheckRadius;
            Gizmos.DrawLine(groundCheck.position, endPosition);
        }
        else if (gravityManipulations.left)
        {
            Vector3 endPosition = groundCheck.position + new Vector3(1,0,0) * groundCheckRadius;
            Gizmos.DrawLine(groundCheck.position, endPosition);
        }
        else if (gravityManipulations.right)
        {
            Vector3 endPosition = groundCheck.position + new Vector3(0, 0, -1) * groundCheckRadius;
            Gizmos.DrawLine(groundCheck.position, endPosition);
        }

    }
}




