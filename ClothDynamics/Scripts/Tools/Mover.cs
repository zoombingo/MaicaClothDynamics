using UnityEngine;

namespace ClothDynamics
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _force = 1;

        void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.LeftShift))
                this.transform.position += Vector3.forward * _force * Time.deltaTime;
            if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftShift))
                this.transform.position -= Vector3.forward * _force * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift))
                this.transform.localRotation *= Quaternion.Euler(0, _force * Time.deltaTime * 100, 0);
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift))
                this.transform.localRotation *= Quaternion.Euler(0, -_force * Time.deltaTime * 100, 0);

            if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftShift))
                this.transform.position += Vector3.right * _force * Time.deltaTime;
            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftShift))
                this.transform.position += Vector3.up * _force * Time.deltaTime;

            if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftShift))
                this.transform.position -= Vector3.right * _force * Time.deltaTime;
            if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftShift))
                this.transform.position -= Vector3.up * _force * Time.deltaTime;
        }
    }
}