using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLC.Core
{
    public class Floater : MonoBehaviour
    {
        public AnimationCurve myCurve;

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(transform.position.x, myCurve.Evaluate(Time.time % myCurve.length), transform.position.z);
        }
    }
}
