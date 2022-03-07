// Murat Sancak

using UnityEngine;

public class Roll:MonoBehaviour
{
    private Touch t; // t: Touch.

    private Vector3 v3; // v3: Vector3.

    // Murat Sancak

    private void Update()
    {
        if(Input.touchCount is not 0&&Time.timeScale is not 0)
        {
            t=Input.GetTouch(0);
            if
            (
                ! // Lower Panel
                (
                    t.position is { y:<384, y:>0 }
                )
                &&
                (
                    ! // Upper Panel
                    (
                        t.position.y<Screen.height&&
                        t.position.y>Screen.height-256
                    )
                    &&
                    (
                        !Monetization.IBL
                        ||
                        IAP.HR(0)
                    )

                    ||

                    ! // Upper Panel
                    (
                        t.position.y<Screen.height&&
                        t.position.y>Screen.height-346
                    )
                )
            )
            {
                if(t.phase is TouchPhase.Began or TouchPhase.Moved or TouchPhase.Stationary)
                {
                    if(Physics.Raycast(Camera.main.ScreenPointToRay(t.position),out RaycastHit hit))
                        v3=hit.point;

                    for(int i = 0;i<Play.ds.Count;i++)
                    {
                        Play.ds[i].GetComponent<Rigidbody>().velocity=32*Play.ds[i].transform.forward;
                        Play.ds[i].transform.LookAt(v3);
                    }
                }
                if(t.phase is TouchPhase.Canceled or TouchPhase.Ended)
                    for(int i = 0;i<Play.ds.Count;i++)
                        Play.ds[i].transform.rotation=Random.rotation;
            }
        }
    }
}

// Murat Sancak