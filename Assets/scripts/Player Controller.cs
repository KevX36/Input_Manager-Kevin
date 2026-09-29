using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    
    //player parts
    private Rigidbody rb;
    private Collider col;
    

    //movement

    [SerializeField] private Vector2 MoveDirection;
    public float speed = 5;
    private float baseSpeed;
    [SerializeField] private Vector3 move;
    //jump
    [SerializeField] Vector3 PlayerFall = new Vector3(0, 0, 0);
    
    
    public float JumpHighet = 7;
    public float baseJumpHighet;
    private bool Jumping = false;
    [SerializeField] private bool isGrounded = true;


    

    



 



    //makes sure no movement happens at start without imput
    private bool doNothingOnStart = true;




    void Awake()
    {
        //I stole this from an old project so some things might be odd, I removed the unneeded parts
        

        
        
        
        baseSpeed = speed;
        col = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();
        
        MoveDirection = rb.position;
        baseJumpHighet = JumpHighet;
    }
    public void OnJump(InputValue input)
    {
        
        Debug.Log("started jump");
        if (input.isPressed)
        {
            if (isGrounded)
            {
                Jumping = true;
            }
            


        }



    }
    
    public void OnMove(InputValue value)
    {
        Debug.Log("moving");
        MoveDirection = value.Get<Vector2>();

    }
    public bool cheakIfGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, col.bounds.extents.y + 0.1f);


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("collectable"))
        {
            Destroy(other.gameObject);
        }
    }
    void Update()
    {
        if (running && speed < baseSpeed*2)
        {
            speed += Time.deltaTime*5;
        }
        else if(!running && speed > baseSpeed)
        {
            speed -= Time.deltaTime*5;
        }
        isGrounded = cheakIfGrounded();
        
        if (PlayerFall.y > 0 && !Jumping)
        {

            PlayerFall.y += Physics.gravity.y * Time.deltaTime;

        }
        if (Jumping)
        {

            PlayerFall.y = JumpHighet;
            

            Debug.Log("Jumped");
            Jumping = false;
        }

        


        move = new Vector3(MoveDirection.x, PlayerFall.y, MoveDirection.y);
        move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * move;
        if (doNothingOnStart)
        {
            Debug.Log("stopped movement");
            MoveDirection.y = 0;
            move.z = 0;
            MoveDirection.x = 0;
            doNothingOnStart = false;
        }
        rb.MovePosition(rb.position + move * speed * Time.deltaTime);

        
        
    }
    public bool running;
    public void OnRun(InputValue value)
    {
        Debug.Log("running");
        if (value.isPressed)
        {
            running = true;
        }
        else
        {
            running = false;
        }
    }
}
