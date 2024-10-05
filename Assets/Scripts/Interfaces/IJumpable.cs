using System.Collections;
using UnityEngine;

namespace Interfaces
{
    public interface IJumpable
    {
        public void Move(Vector3 target, float height, float time);
        public IEnumerator JumpCoroutine(Vector3 target, float height, float time);

    }
}
