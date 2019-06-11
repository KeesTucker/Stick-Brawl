using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror
{
    public class NetworkErrorHandler : MonoBehaviour
    {
        public virtual void ErrorSend(string arg0)
        {
            Debug.Log(arg0);
        }
    }
}
