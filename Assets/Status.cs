using SLC.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SLC
{
    public class Status : MonoBehaviour
    {
        public TextMeshProUGUI text;

        public Health hp;

        // Update is called once per frame
        void Update()
        {
            text.text = hp.CurrentHealth + " / " + hp.maximumHealth;
        }
    }
}
