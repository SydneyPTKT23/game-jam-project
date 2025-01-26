using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLC.Core
{
    public class Floater : MonoBehaviour
    {
        public AnimationCurve myCurve;
        public GameObject prefab;

        public AudioClip clip;
        public AudioSource source;

        public Status2 joo;

        private void Start()
        {
            joo = FindObjectOfType<Status2>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(transform.position.x, myCurve.Evaluate(Time.time % myCurve.length), transform.position.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject t_particle = Instantiate(prefab, transform.position, Quaternion.identity);
            Destroy(t_particle, 2f);

            source.gameObject.transform.parent = null;
            source.PlayOneShot(clip);
            Destroy(source, 2f);

            joo.Adder();
            Destroy(gameObject);
        }
    }
}