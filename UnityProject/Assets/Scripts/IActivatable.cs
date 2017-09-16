using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GO
{
    public interface IActivatable
    {
        void OnActivate();
        void OnDeactivate();
    }
}