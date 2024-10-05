using System.Collections;
using UnityEngine;

namespace Interfaces
{
    public interface IDrivable
    {
        public void Move(Vector3 target, float time);
        public IEnumerator DriveCoroutine(Vector3 target, float time);
    }
}
