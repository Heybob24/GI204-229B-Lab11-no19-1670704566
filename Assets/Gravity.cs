using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f;

    public static List<Gravity> otherObjectlist;

    [SerializeField] bool planet = false;
    [SerializeField] int orbitspeed = 1000;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (otherObjectlist == null)
        {
            otherObjectlist = new List<Gravity>();
        }
        otherObjectlist.Add(this);

        if (!planet)
        { rb.AddForce(Vector3.left * orbitspeed); }
    }

    private void FixedUpdate()
    {
        foreach (Gravity obj in otherObjectlist)
        {
            if (obj != null)
            {
                Attract(obj);
            }
        }
        void Attract(Gravity other)
        {
            Rigidbody otherRB = other.rb;

            Vector3 direction = rb.position - otherRB.position;

            float distance = direction.magnitude;

            if (distance == 0f)
            {
                return;
            }
            float forceMagnitude = G * (rb.mass * otherRB.mass) / Mathf.Pow(distance, 2);
            Vector3 gravityforce = forceMagnitude * direction.normalized;
            otherRB.AddForce(gravityforce);


        }
    }
}
