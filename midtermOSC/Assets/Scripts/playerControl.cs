using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;

public class playerControl : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public float jump;
    public bool isGrounded = true;

    Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Instantiate the OSC Handler
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("pd","/unity/trigger", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        rb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("wall"))
        {
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/bumpWall", 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("collectable"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
