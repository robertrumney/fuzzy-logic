using UnityEngine;
using System.Collections.Generic;

using FuzzyLogic;

public class JumpController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpTime = 0.5f;
    public float jumpSpeed = 5f;
    public float gravity = 20f;
    public Collider groundCollider;

    private Rigidbody rb;
    private bool grounded = false;
    private bool jumping = false;
    private float jumpStartTime;
    private LinguisticVariable distance;
    private LinguisticVariable velocity;
    private LinguisticVariable jump;
    private FuzzyRuleSet ruleSet;
    private FuzzyInferenceSystem fis;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Create the linguistic variables
        distance = new LinguisticVariable("Distance");
        velocity = new LinguisticVariable("Velocity");
        jump = new LinguisticVariable("Jump");

        // Create the fuzzy sets for each linguistic variable
        FuzzySet near = new TriangularFuzzySet("Near", 0, 0, 5);
        FuzzySet far = new TriangularFuzzySet("Far", 3, 7, 10);
        FuzzySet slow = new TriangularFuzzySet("Slow", 0, 0, 5);
        FuzzySet fast = new TriangularFuzzySet("Fast", 3, 7, 10);
        FuzzySet low = new TriangularFuzzySet("Low", 0, 0, 0.5f);
        FuzzySet high = new TriangularFuzzySet("High", 0.5f, 1, 1);

        // Add the fuzzy sets to their corresponding linguistic variables
        distance.AddFuzzySet(near);
        distance.AddFuzzySet(far);
        velocity.AddFuzzySet(slow);
        velocity.AddFuzzySet(fast);
        jump.AddFuzzySet(low);
        jump.AddFuzzySet(high);

        // Create the fuzzy rules
        FuzzyRule rule1 = new FuzzyRule();
        rule1.Antecedents.Add(distance, near);
        rule1.Antecedents.Add(velocity, slow);
        rule1.Consequents.Add(jump, high);

        FuzzyRule rule2 = new FuzzyRule();
        rule2.Antecedents.Add(distance, far);
        rule2.Antecedents.Add(velocity, fast);
        rule2.Consequents.Add(jump, low);

        // Add the fuzzy rules to a rule set
        ruleSet = new FuzzyRuleSet();
        ruleSet.AddRule(rule1);
        ruleSet.AddRule(rule2);

        // Create the fuzzy inference system
        fis = new FuzzyInferenceSystem();
    }

    public float groundedThreshold = 0.1f;

    void Update()
    {
        // Get input for left/right movement
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0f, 0f) * moveSpeed;

        // Check if the player is on the ground
        RaycastHit hitInfo;
        grounded = Physics.Raycast(transform.position, -transform.up, out hitInfo, groundCollider.bounds.extents.y + groundedThreshold);
        if (grounded)
        {
            // Reset the jumping variables
            jumping = false;
            jumpStartTime = Time.time;
        }
        else
        {
            grounded = false;
        }

        // Get input for jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumping = true;
            jumpStartTime = Time.time;
        }

        // Fuzzify input values
        Dictionary<LinguisticVariable, float> inputs = new Dictionary<LinguisticVariable, float>();
        inputs.Add(distance, hitInfo.distance);
        inputs.Add(velocity, rb.velocity.magnitude);

        // Infer output value
        float jumpMembership = fis.Infer(inputs, jump, ruleSet);

        // Apply jump force
        if (jumping && Time.time - jumpStartTime < jumpTime)
        {
            float jumpForce = jumpMembership * jumpSpeed;

            print(jumpMembership);

            if (!float.IsNaN(jumpSpeed))
            {
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Acceleration);
            }
        }

        // Apply movement force
        rb.AddForce(movement, ForceMode.VelocityChange);

        // Apply gravity
        if (!grounded)
        {
            rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
    }
}
