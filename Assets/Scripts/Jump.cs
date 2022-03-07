// Murat Sancak

using UnityEngine;

public class Jump:MonoBehaviour
{
    private Rigidbody r; // r: Rigidbody.

    private Vector3 v3; // v3: Vector3.

    private readonly int f = 128; // f: Force.

    // Murat Sancak

    private void Update()
    {
        if(Input.accelerationEventCount is not 0&&Time.timeScale is not 0)
        {
            v3=Input.acceleration;
            if(2<v3.magnitude)
            {
                Handheld.Vibrate();

                if(v3.x is not 0)
                    for(int i = 0;i<Play.ds.Count;i++)
                    {
                        Play.ds[i].transform.rotation=Random.rotation;
                        r=Play.ds[i].GetComponent<Rigidbody>();
                        r.AddForce(f*v3.x*Vector3.back);
                        r.AddTorque(f*new Vector3(Random.value,Random.value,Random.value));
                    }
                if(v3.y is not 0)
                    for(int i = 0;i<Play.ds.Count;i++)
                    {
                        Play.ds[i].transform.rotation=Random.rotation;
                        r=Play.ds[i].GetComponent<Rigidbody>();
                        r.AddForce(f*v3.y*Vector3.right);
                        r.AddTorque(f*new Vector3(Random.value,Random.value,Random.value));

                    }
                if(v3.z is not 0)
                    for(int i = 0;i<Play.ds.Count;i++)
                    {
                        Play.ds[i].transform.rotation=Random.rotation;
                        r=Play.ds[i].GetComponent<Rigidbody>();
                        r.AddForce(f*v3.z*Vector3.up);
                        r.AddTorque(f*new Vector3(Random.value,Random.value,Random.value));
                    }
            }
        }
    }
}

// Murat Sancak