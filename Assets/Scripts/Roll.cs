// murasanca

using UnityEngine;

namespace murasanca
{
    public class Roll : MonoBehaviour
    {
        private Touch touch;

        private Vector3 vector3;

        // murasanca

        private void Update()
        {
            if (Input.touchCount is not 0 && Time.timeScale is 1)
            {
                touch = Input.GetTouch(0);
                if
                (
                    ! // Lower Panel
                    (
                        touch.position is { y: < 432, y: > 0 }
                    )
                    &&
                    (
                        ! // Upper Panel
                        (
                            touch.position.y < Screen.height &&
                            touch.position.y > Screen.height - 256
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
                            touch.position.y < Screen.height &&
                            touch.position.y > Screen.height - 346
                        )
                    )
                )
                {
                    if (touch.phase is TouchPhase.Began or TouchPhase.Moved or TouchPhase.Stationary)
                    {
                        if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out RaycastHit hit))
                            vector3 = hit.point;

                        for (int i = 0; i < Play.ds.Count; i++)
                        {
                            Play.ds[i].GetComponent<Rigidbody>().velocity = 32 * Play.ds[i].transform.forward;
                            Play.ds[i].transform.LookAt(vector3);
                        }
                    }
                    if (touch.phase is TouchPhase.Canceled or TouchPhase.Ended)
                        for (int i = 0; i < Play.ds.Count; i++)
                            Play.ds[i].transform.rotation = Random.rotation;
                }
            }
        }
    }
}

// murasanca