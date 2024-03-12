using System.Collections.Generic;
using UnityEngine;

namespace TD
{
    // Simple script that will hold references to service-like 
    // scriptable objects required in scene to keep them loaded
    internal class SOLoadHelper: MonoBehaviour
    {
        [SerializeField] private List<ScriptableObject> _services;
    }
}
