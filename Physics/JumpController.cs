using UnityEngine;
using FuzzyLogic;
using System.Collections.Generic;

public class JumpController : MonoBehaviour
{
    public float jumpForce = 10f;
    public LinguisticVariable distanceToGround;
    public LinguisticVariable jumpHeight;
    public FuzzyRuleSet ruleSet;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Step 1: Fuzzification
        float distance = DistanceToGround();
        Dictionary<LinguisticVariable, float> inputs = new Dictionary<LinguisticVariable, float>();
        inputs[distanceToGround] = distance;

        // Step 2: Rule evaluation
        float jumpHeightValue = ruleSet.Infer(inputs, jumpHeight);

        // Step 3: Defuzzification
        float jumpHeightClamped = Mathf.Clamp(jumpHeightValue, 0f, 1f);
        float jumpForceValue = jumpHeightClamped * jumpForce;

        // Step 4: Apply force to jump
        if (IsGrounded() && jumpForceValue > 0f)
        {
            rb.AddForce(Vector3.up * jumpForceValue, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        float distance = DistanceToGround();
        return distance <= 0.1f;
    }

    private float DistanceToGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            return hit.distance;
        }
        return float.MaxValue;
    }
}
