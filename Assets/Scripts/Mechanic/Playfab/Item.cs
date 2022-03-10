using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP.Backend
{
    public class Item : Singleton<Item>
    {
        public string Name;
        public int Cost;
        public int GainPerSecond;
    }
}

