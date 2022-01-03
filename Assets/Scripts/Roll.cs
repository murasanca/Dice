// murasanca

using UnityEngine;

namespace murasanca
{
    public class Roll : MonoBehaviour
    {
        private Touch touch;
        private Vector3 vector3;

        private void Update()
        {
            if (Input.touchCount is not 0 && Time.timeScale is 1)
            {
                touch = Input.GetTouch(0);
                if
                (
                    ! // Panel
                    (
                        touch.position.x < 416 + Screen.width / 2 &&
                        touch.position.x > Screen.width / 2 - 416 &&
                        touch.position is { y: < 320, y: > 64 }
                    )
                    &&
                    (
                        ! // Menu Button
                        (
                            touch.position is { x: < 192, x: > 64 } &&
                            touch.position.y < Screen.height - 64 &&
                            touch.position.y > Screen.height - 192
                        )
                        &&
                        ! // Pause & Play Button
                        (
                            touch.position.x < Screen.width - 64 &&
                            touch.position.x > Screen.width - 192 &&
                            touch.position.y < Screen.height - 64 &&
                            touch.position.y > Screen.height - 192
                        )
                        &&
                        (
                            !Monetization.IsInitialized
                            ||
                            IAP.HasReceipt(0)
                        )

                        ||

                        ! // Menu Button
                        (
                            touch.position is { x: < 192, x: > 64 } &&
                            touch.position.y < Screen.height - 154 &&
                            touch.position.y > Screen.height - 282
                        )
                        &&
                        ! // Pause & Play Button
                        (
                            touch.position.x < Screen.width - 64 &&
                            touch.position.x > Screen.width - 192 &&
                            touch.position.y < Screen.height - 154 &&
                            touch.position.y > Screen.height - 282
                        )
                    )
                )
                {
                    if (touch.phase is TouchPhase.Began or TouchPhase.Moved or TouchPhase.Stationary)
                    {
                        if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out RaycastHit hit))
                            vector3 = hit.point;

                        for (int i = 0; i < Three.dices.Count; i++)
                        {
                            Three.dices[i].GetComponent<Rigidbody>().velocity = 32 * Three.dices[i].transform.forward;
                            Three.dices[i].transform.LookAt(vector3);
                        }
                    }
                    if (touch.phase is TouchPhase.Canceled or TouchPhase.Ended)
                        for (int i = 0; i < Three.dices.Count; i++)
                            Three.dices[i].transform.rotation = Random.rotation;
                }
            }
        }
    }
}

// murasanca