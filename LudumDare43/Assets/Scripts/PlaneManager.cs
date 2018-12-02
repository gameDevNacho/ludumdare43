﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneManager : MonoBehaviour
{
    public static PlaneManager Instance { get; private set; }

    [System.Serializable]
    private struct RotationVelocity
    {
        public float weight;
        public float angularVelocity;
    }

    [SerializeField]
    private List<BoxComponent> boxes;
    [SerializeField]
    private Transform plane;
    [SerializeField]
    private Transform leftPoint;
    [SerializeField]
    private Transform centerPoint;
    [SerializeField]
    private Transform rightPoint;
    [SerializeField]
    private RotationVelocity maxRotationVelocity;
    [SerializeField]
    private float maxSecondsPerWeightProblem;
    [SerializeField]
    private float maxSecondsPerAngleProblem;
    [SerializeField]
    private float minimumTimePerWeightProblem;
    [SerializeField]
    private float minimumTimePerAngleProblem;
    [SerializeField]
    private float timeWeightProblem;
    [SerializeField]
    private float angleLost;
    [SerializeField]
    private ScreenEvents screenEvents;
    [SerializeField]
    private float angleAlarm;
    [SerializeField]
    private float timePerGame;

    private float timePassed;

    private float engineMalfunction;

    private bool weightProblem;

    private float totalWeight;
    private float weightBalance;

    private float timeForWeightProblem;
    private float timeForAngleProblem;

    private float timePassedSinceWeightProblem;
    private float timePassedSinceAngleProblem;

    private float timePassedInWeightProblem;

    private float weightExcess;

    private bool moreThanXAngle;

    private bool gameOver;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        timeForWeightProblem = Random.Range(minimumTimePerWeightProblem, maxSecondsPerWeightProblem);
        moreThanXAngle = false;
        timePassed = 0;

        gameOver = false;
    }

    public void CalculateWeight()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            totalWeight += (float)boxes[i].mySize;
        }

        engineMalfunction = 0;
    }

    private void Update()
    {
        if(!gameOver)
        {
            timePassed += Time.deltaTime;

            if (timePassed >= timePerGame)
            {
                List<Product> list = new List<Product>();

                for (int i = 0; i < boxes.Count; i++)
                {
                    if(boxes[i].myProduct)
                    {
                        list.Add(boxes[i].myProduct);
                    }

                    else
                    {
                        Debug.Log("Checkea los productos pisha que hay alguno con producto nulo");
                    }
                }

                Product_List_Manager.Instance.Initialize(list.ToArray());
                gameOver = true;
                return;
            }

            EvaluateWeights();

            if (!weightProblem)
            {
                timePassedSinceWeightProblem += Time.deltaTime;

                if (timePassedSinceWeightProblem >= timeForWeightProblem)
                {
                    timePassedSinceWeightProblem = 0;

                    StartWeightProblem();

                    weightProblem = true;

                }
            }

            timePassedSinceAngleProblem += Time.deltaTime;

            if (timePassedSinceAngleProblem >= timeForAngleProblem)
            {
                timePassedSinceAngleProblem = 0;

                StartAngleProblem();
            }

            RotatePlane();

            if (Vector3.Angle(Vector3.up, plane.transform.forward) >= angleLost)
            {
                SceneManager.LoadScene("Main");
            }

            if (Vector3.Angle(Vector3.up, plane.transform.forward) >= angleAlarm && !moreThanXAngle)
            {
                screenEvents.SetRotationAlarm(ScreenEvents.State.On);
                moreThanXAngle = true;
            }

            else if (Vector3.Angle(Vector3.up, plane.transform.forward) < angleAlarm && moreThanXAngle)
            {
                screenEvents.SetRotationAlarm(ScreenEvents.State.Solution);
                moreThanXAngle = false;
            }

            if (weightProblem)
            {
                timePassedInWeightProblem += Time.deltaTime;

                if (timePassedInWeightProblem >= timeWeightProblem)
                {
                    timePassedInWeightProblem = 0;
                    timePassedSinceWeightProblem = 0;
                    weightProblem = false;
                    SceneManager.LoadScene("Main");
                }
            }
        }
        
    }

    public void EvaluateWeights()
    {
        weightBalance = 0;

        for (int i = 0; i < boxes.Count; i++)
        {
            if(centerPoint.InverseTransformPoint(boxes[i].transform.position).x < centerPoint.localPosition.x)
            {
                float distanceCenterToLeft = Mathf.Abs(centerPoint.InverseTransformPoint(leftPoint.position).x);
                float distanceCenterToBox = Mathf.Abs(centerPoint.InverseTransformPoint(boxes[i].transform.position).x);

                weightBalance += ((float)boxes[i].mySize * (Mathf.Clamp(distanceCenterToBox / distanceCenterToLeft, 0, 1)));
            }

            else if(centerPoint.InverseTransformPoint(boxes[i].transform.position).x > centerPoint.localPosition.x)
            {
                float distanceCenterToRight = Mathf.Abs(centerPoint.InverseTransformPoint(rightPoint.position).x);
                float distanceCenterToBox = Mathf.Abs(centerPoint.InverseTransformPoint(boxes[i].transform.position).x);

                weightBalance += (-(float)boxes[i].mySize * (Mathf.Clamp(distanceCenterToBox / distanceCenterToRight, 0, 1)));
            }
        }
    }

    private void RotatePlane()
    {
        float balancedWeight = engineMalfunction + weightBalance;

        float angularSpeed = Mathf.Abs(maxRotationVelocity.angularVelocity * balancedWeight / maxRotationVelocity.weight);

        plane.Rotate(plane.forward * angularSpeed * -Mathf.Sign(balancedWeight) * Time.deltaTime);
    }

    public void BoxThrown(BoxComponent box)
    {
        boxes.Remove(box);
        weightExcess -= (float)box.mySize;

        Debug.Log(weightExcess);

        if(weightExcess <= 0 && weightProblem)
        {
            weightProblem = false;
            timeForWeightProblem = Random.Range(minimumTimePerWeightProblem, maxSecondsPerWeightProblem);

            screenEvents.SetKGAlarm(ScreenEvents.State.Solution);
        }
    }

    private void StartWeightProblem()
    {
        weightExcess = Random.Range(10f, 50f);
        screenEvents.SetKGAlarm(ScreenEvents.State.On);
    }

    private void StartAngleProblem()
    {
        int wing = Random.Range(0, 2);

        if(wing == 0)
        {
            engineMalfunction += -(Random.Range(5f, 15f));
        }

        else if(wing == 1)
        {
            engineMalfunction += Random.Range(5f, 15f);
        }

        timeForAngleProblem = Random.Range(minimumTimePerAngleProblem, maxSecondsPerAngleProblem);

        StartCoroutine(Shake(0.8f, 0.3f));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = Camera.main.transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.localPosition = originalPos;
    }
}
