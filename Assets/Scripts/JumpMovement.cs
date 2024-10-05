using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class JumpMovement : MonoBehaviour
{
    public Transform targetPosition; // Gitmek istediğin hedef
    public float jumpHeight = 2f; // Zıplama yüksekliği
    public float duration = 1f; // Zıplama süresi
    private bool _isJumping;
    private bool _isMoving;
    public Animator playerAnimator;

    private void Update()
    {
        // Başlangıçta bir yere zıplatmak için fonksiyonu çağırabilirsiniz.
        if (Input.GetKeyDown(KeyCode.A))
        {
            Jump();
            print("girdi");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!_isMoving)
            {
                StartCoroutine(MoveToTarget(targetPosition.position, duration));
            }
        }
    }

    public void Jump()
    {
        if (!_isJumping)
        {
            StartCoroutine(JumpToTarget(targetPosition.position, jumpHeight, duration));
        }
    }

    //expoout
    //expoin
    //expoinout
    //backin

    private IEnumerator JumpToTarget(Vector3 target, float height, float time)
    {
        playerAnimator.SetTrigger("Forward");

        _isJumping = true;
        float elapsedTime = 0;
        Vector3 startPosition = transform.position;

        while (elapsedTime < time)
        {
            // İlerleme oranı (0 - 1 arası bir değer alır)
            float t = elapsedTime / time;

            // Easing kullanarak smooth bir geçiş yapıyoruz
            float easeT = EaseInOutQuad(t);

            // Yatay hareket
            Vector3 currentPosition = Vector3.Lerp(startPosition, target + Vector3.forward * 8.15f, easeT);

            // Zıplama hareketi: Parabolik bir hareket oluşturmak için sinüs eğrisi kullanıyoruz
            float verticalOffset = Mathf.Sin(easeT * Mathf.PI) * height;
            currentPosition.y += verticalOffset;

            // Yeni pozisyonu uygula
            transform.position = currentPosition;

            elapsedTime += Time.deltaTime;
            yield return null; // Bir sonraki frame'e geç
        }

        // Hedefe tam olarak ulaştıktan sonra pozisyonu düzelt
        transform.position = target + Vector3.forward * 8.15f;
        _isJumping = false;
    }

    // Zamanla yumuşak geçiş sağlamak için EaseInOutQuad fonksiyonu
    private float EaseInOutQuad(float t)
    {
        if (t < 0.5f) return 2 * t * t;
        return -1 + (4 - 2 * t) * t;
    }

    private IEnumerator MoveToTarget(Vector3 target, float time)
    {
        playerAnimator.SetTrigger("Forward");

        _isMoving = true;
        float elapsedTime = 0;
        Vector3 startPosition = transform.position;

        while (elapsedTime < time)
        {
            // İlerleme oranı (0 - 1 arası bir değer alır)
            float t = elapsedTime / time;

            // BackIn easing kullanarak smooth bir geçiş yapıyoruz
            float easeT = EaseExpoInOut(t);

            // Yatay hareket (A noktasından B noktasına)
            Vector3 currentPosition = Vector3.Lerp(startPosition, target + Vector3.forward * 8.15f, easeT);

            // Yeni pozisyonu uygula
            transform.position = currentPosition;

            elapsedTime += Time.deltaTime;
            yield return null; // Bir sonraki frame'e geç
        }

        // Hedef pozisyona tam olarak ulaştıktan sonra konumu düzelt
        transform.position = target + Vector3.forward * 8.15f;
        _isMoving = false;
    }

    // Easing fonksiyonu: BackIn
    private float EaseBackIn(float t)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;

        return c3 * t * t * t - c1 * t * t;
    }
    
    private float EaseExpoInOut(float t)
    {
        if (t == 0) return 0f;
        if (t == 1) return 1f;

        if (t < 0.5f)
        {
            return Mathf.Pow(2, 20 * t - 10) / 2;
        }
        else
        {
            return (2 - Mathf.Pow(2, -20 * t + 10)) / 2;
        }
    }
}