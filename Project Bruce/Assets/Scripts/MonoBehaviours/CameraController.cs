using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bruce;
public class CameraController : MonoBehaviour
{
    [SerializeField] public Camera mainCamera;
    [SerializeField] public WorldController worldController;
    Transform swivel, stick;
    public float zoom = 1f;
    public float stickMinZoom, stickMaxZoom;
    public float swivelMinZoom, swivelMaxZoom;
    public float moveSpeedMinZoom, moveSpeedMaxZoom;

    void Start()
    {
        mainCamera = Camera.main;
        swivel = transform.GetChild(0);
        stick = swivel.GetChild(0);
        //CenterCameraOnPlot(worldController.world.ActiveCountries.First().Territory.First());
    }

    void Update()
    {
        float zoomDelta = UnityEngine.Input.GetAxis("Mouse ScrollWheel");
        if (zoomDelta != 0f)
        {
            AdjustZoom(zoomDelta);
        }

        float xDelta = UnityEngine.Input.GetAxis("Horizontal");
        float zDelta = UnityEngine.Input.GetAxis("Vertical");

        if (xDelta != 0f || zDelta != 0f)
        {
            AdjustPosition(xDelta, zDelta);
        }
    }

    void AdjustZoom(float delta)
    {
        zoom = Mathf.Clamp01(zoom + delta);
        float distance = Mathf.Lerp(stickMinZoom, stickMaxZoom, zoom);
        stick.localPosition = new Vector3(0f,0f, distance);

        float angle = Mathf.Lerp(swivelMinZoom, swivelMaxZoom, zoom);
        swivel.localRotation = Quaternion.Euler(angle, 0f, 0f);
    }

    void AdjustPosition(float xDelta, float zDelta)
    {
        Vector3 direction = new Vector3(xDelta, 0f, zDelta).normalized;
        float damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta));

        float distance = Mathf.Lerp(moveSpeedMinZoom, moveSpeedMaxZoom, zoom) * damping * Time.deltaTime;
        Vector3 position = transform.localPosition;

        position += direction * distance;
        transform.localPosition = position;
    }

    public void CenterCameraOnPlot(Hex hex)
    {
        this.transform.position = hex.Position;
    }
}
