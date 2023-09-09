using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    public bool smash;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            smash = true;
        if (Input.GetMouseButtonUp(0))
            smash = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && smash == true)
        {
            //smash = true;
            rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
        }

        if (rb.velocity.y > 5)
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!smash)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
        else
        {
            if(collision.gameObject.tag == "enemy")
            {
                Destroy(collision.transform.parent.gameObject);
            }
            if(collision.gameObject.tag == "plane")
            {
                Debug.Log("Over");
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionStay(Collision target)
    {
        if(!smash || target.gameObject.tag == "Finish")
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
            smash = false;
            Debug.Log("Win");
            //LevelSpawner.Instance.Init();
        }
    }
}
