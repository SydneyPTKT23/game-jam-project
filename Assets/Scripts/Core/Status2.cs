using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SLC
{
    public class Status2 : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public int victimCount = 0;

        private void Update()
        {
            text.text = victimCount.ToString();
        }

        public void Adder()
        {
            victimCount++;
        }
    }
}
