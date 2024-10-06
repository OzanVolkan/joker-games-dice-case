using System.Collections;
using UnityEngine;

namespace Interfaces
{
    public interface IDrivable
    {
        public void Move(Transform playerTrans, float time, Animator animator, int currentIndex,
            int blockCount);

        public IEnumerator DriveCoroutine(Transform playerTrans, float time, int currentIndex,
            int blockCount);
    }
}