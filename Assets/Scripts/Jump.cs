﻿// murasanca

using UnityEngine;

namespace murasanca
{
    public class Jump : MonoBehaviour
    {
        private Rigidbody r;
        private Vector3 vector3;
        private readonly int force = 128;

        private void Update()
        {
            vector3 = Input.acceleration;
            if (2 < vector3.magnitude)
            {
                Handheld.Vibrate();

                if (vector3.x is not 0)
                    for (int i = 0; i < Three.dices.Count; i++)
                    {
                        Three.dices[i].transform.rotation = Random.rotation;
                        r = Three.dices[i].GetComponent<Rigidbody>();
                        r.AddForce(force * vector3.x * Vector3.back);
                        r.AddTorque(force * new Vector3(Random.value, Random.value, Random.value));
                    }
                if (vector3.y is not 0)
                    for (int i = 0; i < Three.dices.Count; i++)
                    {
                        Three.dices[i].transform.rotation = Random.rotation;
                        r = Three.dices[i].GetComponent<Rigidbody>();
                        r.AddForce(force * vector3.y * Vector3.right);
                        r.AddTorque(force * new Vector3(Random.value, Random.value, Random.value));

                    }
                if (vector3.z is not 0)
                    for (int i = 0; i < Three.dices.Count; i++)
                    {
                        Three.dices[i].transform.rotation = Random.rotation;
                        r = Three.dices[i].GetComponent<Rigidbody>();
                        r.AddForce(force * vector3.z * Vector3.up);
                        r.AddTorque(force * new Vector3(Random.value, Random.value, Random.value));
                    }
            }
        }
    }
}

// murasanca