using System.Collections;
using UnityEngine;

namespace Interfaces
{
    public interface IJumpable
    {
        public void Move(Transform playerTrans, float height, float time, Animator animator);
        public IEnumerator JumpCoroutine(Transform playerTrans, float height, float time);

    }
}
