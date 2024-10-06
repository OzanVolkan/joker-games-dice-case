using System.Collections;
using UnityEngine;

namespace Interfaces
{
    public interface IJumpable
    {
        public void Move(Transform playerTrans, float height, float time, Animator animator, int currentIndex,
            int blockCount);
        public IEnumerator JumpCoroutine(Transform playerTrans, float height, float time, int currentIndex,
            int blockCount);

    }
}
