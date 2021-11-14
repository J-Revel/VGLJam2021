using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float bounceForce = 10;
    private List<ContactPoint2D> contacts = new List<ContactPoint2D>();
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        contacts.Add(collision.contacts[0]);
    }

    void Update()
    {
        foreach(ContactPoint2D contact in contacts)
        {
            Vector2 force = -contact.normal * bounceForce;
            contact.rigidbody?.AddForce(force, ForceMode2D.Impulse);
        }
        contacts.Clear();
    }
}
