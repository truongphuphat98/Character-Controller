using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [Header("Camera Swap")]
    public GameObject thirdPersonCamera;
    public GameObject firstPersonCamera;
    public int CameraMode;
    [SerializeField] private KeyCode cameraKey;
    [SerializeField] private bool tpCamera;
    [SerializeField] private bool fpCamera;


    // Update is called once per frame
    void Update()
    {
        CameraSwap();
    }

    void CameraSwap()
    {
        if (Input.GetKeyDown(cameraKey))
        {
            if(CameraMode == 1) //Cycling through the modes
            {
                CameraMode = 0;
            }
            else
            {
                CameraMode += 1;
            }
            StartCoroutine(CameraChange());
        }
    }

    IEnumerator CameraChange()
    {
        yield return new WaitForSeconds(0.01f);
        if(CameraMode == 0)
        {
            thirdPersonCamera.SetActive(true);
            tpCamera = true;
            firstPersonCamera.SetActive(false);
            fpCamera = false;
        }

        if(CameraMode == 1)
        {
            firstPersonCamera.SetActive(true);
            fpCamera = true;
            thirdPersonCamera.SetActive(false);
            tpCamera = false;
        }
    }
}
