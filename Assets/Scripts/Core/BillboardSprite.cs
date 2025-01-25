using UnityEngine;

namespace SLC.Core
{
    public class BillboardSprite : MonoBehaviour
    {
        [SerializeField] private Transform m_cameraTransform;
        private Transform m_localTransform;

        void Start()
        {
            m_localTransform = transform;
            m_cameraTransform = Camera.main.transform;
        }

        void LateUpdate()
        {
            Vector3 t_lookPosition = m_cameraTransform.position - m_localTransform.position;
            t_lookPosition.y = 0f;
            transform.rotation = Quaternion.LookRotation(t_lookPosition);
        }
    }
}