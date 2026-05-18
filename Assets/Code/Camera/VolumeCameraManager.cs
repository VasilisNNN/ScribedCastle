using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class VolumeCameraManager : MonoBehaviour
{

    private Camera MainCamera;
    public AudioClip[] RandomClip;
    private static int assetsPPU;
    private static PixelPerfectCamera pixelcamera;
    void Start()
    {
        MainCamera = InitializeObjects.PL.MainCamera;

        pixelcamera = MainCamera.GetComponent<PixelPerfectCamera>();

    }

    public void CameraDistanceSound(ref AudioSource AS, Transform trans, Camera camera)
    {
        if (pixelcamera == null) return;

        assetsPPU = pixelcamera.assetsPPU;

        if (IsOffCamera(camera, trans.position))
        {
            AS.volume = 0f;
            return;
        }

        if (assetsPPU < 70)
        {
            AS.volume = 0.001f;
            return;

        }

        if (assetsPPU > 80 && assetsPPU < 90)
            AS.volume = 0.05f;
        if (assetsPPU > 90 && assetsPPU < 100)
            AS.volume = 0.1f;
        if (assetsPPU > 100 && assetsPPU < 110)
            AS.volume = 0.3f;
        if (assetsPPU > 110)
            AS.volume = 1f;


    }


    bool IsOffCamera(Camera cam, Vector3 worldPos)
    {
        Vector3 viewportPos = cam.WorldToViewportPoint(worldPos);

        return viewportPos.x < 0f || viewportPos.x > 1f ||
               viewportPos.y < 0f || viewportPos.y > 1f ||
               viewportPos.z < 0f; // behind camera
    }

}
