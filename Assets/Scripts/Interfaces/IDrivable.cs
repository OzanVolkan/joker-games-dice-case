using System.Collections;
using UnityEngine;

namespace Interfaces
{
    public interface IDrivable
    {
        public void Move(Transform playerTrans, float time, Animator animator);
        public IEnumerator DriveCoroutine(Transform playerTrans, float time);
    }
}
