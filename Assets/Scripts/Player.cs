using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponrnt;
    private int superJumpsRemaining;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponrnt = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checked space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    // Fixed update (on every phisic update)
    private void FixedUpdate()
    {
        rigidbodyComponrnt.velocity = new Vector3(horizontalInput, rigidbodyComponrnt.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        // Checked space key
        if (jumpKeyWasPressed == true)
        {
            float jumpPower = 6.5f;
            if (superJumpsRemaining > 0)
            {
                jumpPower *= 1.5f;
                superJumpsRemaining--;
            }
            rigidbodyComponrnt.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }

}
