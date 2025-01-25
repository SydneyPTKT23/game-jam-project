using UnityEngine;

namespace SLC.Core
{
    public class BillboardSprite : MonoBehaviour
    {
        [SerializeField] private Transform m_cameraTransform;
        private Transform m_localTransform;
        public bool alignNotLook = true;

        void Start()
        {
            m_localTransform = transform;
            m_cameraTransform = Camera.main.transform;
        }

        void LateUpdate()
        {
            if (alignNotLook)
            {
                m_localTransform.forward = m_cameraTransform.forward;
            }
            else
            {
                m_localTransform.LookAt(m_cameraTransform, Vector3.up);
            }        
        }
    }
}