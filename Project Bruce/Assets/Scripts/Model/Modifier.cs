using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class Modifier
    {
        public Action OnTriggered;
        public Action OnRemoved;


        public virtual void RegisterOnTriggered(Action callback)
        {
            OnTriggered += callback;
        }
        public virtual void RegisterOnRemoved(Action callback)
        {
            OnRemoved += callback;
        }
    }

   
}