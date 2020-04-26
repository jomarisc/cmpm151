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
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", "ready");
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
        if (collision.gameObject.name == "floor1")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("red"))
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("yellow"))
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("green"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
