using UnityEngine;

namespace ItemRelated.Camera
{
    public class CameraController : MonoBehaviour
    {
        private float _tilt;
        void Update()
        {
            if (Pause.Active)
                return;
            float mouseRotation = Input.GetAxis("Mouse Y");
            _tilt = Mathf.Clamp(_tilt - mouseRotation, -30f, 50f);
            transform.localRotation = Quaternion.Euler(Vector3.right * _tilt);
        }
    }
}
