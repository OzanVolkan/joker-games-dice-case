using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public Rigidbody rb;
    public Transform[] faceTransforms; // Zarın her bir yüzüne karşılık gelen transformlar
    public float throwForce = 5f;
    public float rollForce = 5f;
    public int biasedNumber = 6; // Hileli sayı (örneğin, zar 6 gelsin)
    private bool adjustingRotation = false;
    private bool hasRolled = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasRolled)
        {
            RollDice();
        }
    }

    void RollDice()
    {
        rb.useGravity = true;

        var randomVariance = Random.Range(-1f, 1f);
        rb.AddForce(transform.forward * (throwForce + randomVariance), ForceMode.VelocityChange);

        var randX = Random.Range(0f, 1f);
        var randY = Random.Range(0f, 1f);
        var randZ = Random.Range(0f, 1f);
        
        rb.AddTorque(new Vector3(randX, randY, randZ) * (rollForce + randomVariance), ForceMode.VelocityChange);
    }
}