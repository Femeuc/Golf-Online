using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.Femeuc.GolfOnline
{
    public class CameraLogic : MonoBehaviourPun
    {
        private Camera cam;
        private Transform targetOfCamera;
        private Vector3 previousMousePosition;
        public float cameraHeightRelativeToTheTarget = 1.5f;
        [Tooltip("The distance must be a negative number!")]
        public float cameraDistanceOfTheTarget = -4;
        private bool wasPreviousMouseClickOnUI;

        void Start()
        {
            if (!photonView.IsMine)
            {
                this.enabled = false;
                return;
            }
            cam = Camera.main;
            targetOfCamera = this.gameObject.transform;
        }

        void LateUpdate()
        {
            cam.transform.position = targetOfCamera.position;
            if (!EventSystem.current.IsPointerOverGameObject() && !isTouchOverUI()) // Checks if mouse pointer isn't over UI elment.
            {
                if (Input.GetMouseButtonDown(0))
                {
                    previousMousePosition = cam.ScreenToViewportPoint(Input.mousePosition); // this line is important for it to feel better.
                    wasPreviousMouseClickOnUI = false;
                }
                if (Input.GetMouseButton(0) && !wasPreviousMouseClickOnUI) // GetMouseButton(int button) Returns whether the given mouse button is held down.
                {
                    Vector3 rotationDirection = previousMousePosition - cam.ScreenToViewportPoint(Input.mousePosition);

                    cam.transform.Rotate(new Vector3(1, 0, 0), rotationDirection.y * 180);
                    cam.transform.Rotate(new Vector3(0, 1, 0), -rotationDirection.x * 180, Space.World);

                    previousMousePosition = cam.ScreenToViewportPoint(Input.mousePosition);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    wasPreviousMouseClickOnUI = true;
                }
            }
            // Translate(Vector3 translation); Moves the transform in the direction and distance of translation.
            cam.transform.Translate(0, cameraHeightRelativeToTheTarget, cameraDistanceOfTheTarget);
        }

        private bool isTouchOverUI()
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                return EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId);
            }
            return false;
        }
    }
}