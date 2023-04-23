using UnityEngine;
using UnityEngine.AI;
using FuzzyLogic;
using System.Collections.Generic;

public class AIController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float acceleration = 5f;
    public float angularSpeed = 120f;
    public float stoppingDistance = 1f;

    private NavMeshAgent agent;
    private Transform playerTransform;
    private LinguisticVariable distance;
    private LinguisticVariable angle;
    private FuzzyRuleSet ruleSet;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Initialize fuzzy variables
        distance = new LinguisticVariable("distance");
        distance.AddFuzzySet(new LeftShoulderFuzzySet("near", 0f, 5f, 10f));
        distance.AddFuzzySet(new TriangleFuzzySet("medium", 5f, 10f, 15f));
        distance.AddFuzzySet(new RightShoulderFuzzySet("far", 10f, 15f, 20f));

        angle = new LinguisticVariable("angle");
        angle.AddFuzzySet(new LeftShoulderFuzzySet("left", 0f, 30f, 60f));
        angle.AddFuzzySet(new TriangleFuzzySet("center", 30f, 60f, 90f));
        angle.AddFuzzySet(new RightShoulderFuzzySet("right", 60f, 90f, 120f));

        // Initialize fuzzy rules
        ruleSet = new FuzzyRuleSet();

        FuzzyRule rule1 = new FuzzyRule();
        rule1.Antecedents.Add(distance.FuzzySets[0], 1f);
        rule1.Consequents.Add(angle.FuzzySets[2], 1f);
        ruleSet.AddRule(rule1);

        FuzzyRule rule2 = new FuzzyRule();
        rule2.Antecedents.Add(distance.FuzzySets[1], 1f);
        rule2.Consequents.Add(angle.FuzzySets[1], 1f);
        ruleSet.AddRule(rule2);

        FuzzyRule rule3 = new FuzzyRule();
        rule3.Antecedents.Add(distance.FuzzySets[2], 1f);
        rule3.Consequents.Add(angle.FuzzySets[0], 1f);
        ruleSet.AddRule(rule3);
    }

    private void Update()
    {
        // Fuzzy Logic
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        float angleToPlayer = Vector3.Angle(transform.forward, playerTransform.position - transform.position);

        Dictionary<LinguisticVariable, float> inputs = new Dictionary<LinguisticVariable, float>();
        inputs.Add(distance, dist);
        inputs.Add(angle, angleToPlayer);

        float desiredAngle = ruleSet.Infer(inputs, angle);
        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        targetRotation *= Quaternion.Euler(0f, desiredAngle, 0f);

        // Seek Player
        agent.speed = Mathf.MoveTowards(agent.speed, maxSpeed, acceleration * Time.deltaTime);
        agent.angularSpeed = angularSpeed;
        agent.stoppingDistance = stoppingDistance;
        agent.SetDestination(playerTransform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * Time.deltaTime);
    }
}
