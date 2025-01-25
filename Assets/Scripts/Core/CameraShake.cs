using UnityEngine;
using Cinemachine;

namespace SLC.Core
{
    public class CameraShake : MonoBehaviour
    {
        public static CameraShake Instance { get; private set; }
        private CinemachineVirtualCamera cam;
        private float shakeTimer;
        private float shakeTimerTotal;
        private float startingAmplitude;

        private void Awake()
        {
            Instance = this;
            cam = GetComponent<CinemachineVirtualCamera>();
        }

        public void ShakeCamera(float t_amplitude, float t_time)
        {
            CinemachineBasicMultiChannelPerlin t_perlin =
                cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            t_perlin.m_AmplitudeGain = t_amplitude;

            startingAmplitude = t_amplitude;
            shakeTimerTotal = t_time;
            shakeTimer = t_time;
        }

        private void Update()
        {
            if (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                if (shakeTimer <= 0f)
                {
                    CinemachineBasicMultiChannelPerlin t_perlin =
                        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                    t_perlin.m_AmplitudeGain = Mathf.Lerp(startingAmplitude, 0f, 1 - (shakeTimer / shakeTimerTotal));
                }
            }
        }
    }
}