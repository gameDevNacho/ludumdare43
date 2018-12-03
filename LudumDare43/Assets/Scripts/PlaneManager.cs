using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

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
    [SerializeField]
    private Animator fadeOut;
    [SerializeField]
    private LightSwitch lightSwitch;
    [SerializeField]
    private AudioClip planeCrash;
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private GameObject[] possibleScenes;
    [SerializeField]
    private GameObject albaran;

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

    public bool mainMenu;

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
        audioMixer.TransitionToSnapshots(new AudioMixerSnapshot[1] { audioMixer.FindSnapshot("Snapshot") }, new float[1] { 1f }, 0f);

        if(!mainMenu){
            int sceneIndex = Random.Range(0, possibleScenes.Length);

            GameObject sceneSelected = Instantiate(possibleScenes[sceneIndex], plane);
            sceneSelected.transform.localPosition = Vector3.zero;

            BoxComponent[] boxesComponents = sceneSelected.GetComponentsInChildren<BoxComponent>();

            boxes.Clear();

            for (int i = 0; i < boxesComponents.Length; i++)
            {
                boxes.Add(boxesComponents[i]);
                boxesComponents[i].parentTransform = plane;
            }
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Main");
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

                albaran.SetActive(true);
                Product_List_Manager.Instance.Initialize(list.ToArray());
                audioMixer.TransitionToSnapshots(new AudioMixerSnapshot[1] { audioMixer.FindSnapshot("MuteEverythingButMusic") }, new float[1] { 1f }, 0f);
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

                    lightSwitch.SwitchOnLights();
                }
            }

            timePassedSinceAngleProblem += Time.deltaTime;

            if (timePassedSinceAngleProblem >= timeForAngleProblem)
            {
                timePassedSinceAngleProblem = 0;

                StartAngleProblem();

                lightSwitch.SwitchOnLights();
            }

            RotatePlane();

            if (Vector3.Angle(Vector3.up, plane.transform.forward) >= angleLost)
            {
                fadeOut.SetTrigger("FadeOut");
                Invoke("ResetGame", 5f);
                audioSource.clip = planeCrash;
                audioSource.Play();
                audioMixer.TransitionToSnapshots(new AudioMixerSnapshot[1] { audioMixer.FindSnapshot("MuteEverything") }, new float[1] { 1f }, 0f);
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
                    fadeOut.SetTrigger("FadeOut");
                    Invoke("ResetGame", 5f);
                    audioSource.clip = planeCrash;
                    audioSource.Play();
                    audioMixer.TransitionToSnapshots(new AudioMixerSnapshot[1] { audioMixer.FindSnapshot("MuteEverything") }, new float[1] { 1f }, 0f);
                }
            }

            if(!weightProblem && !moreThanXAngle)
            {
                lightSwitch.SwitchOffLights();
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

        if(weightExcess <= 0 && weightProblem)
        {
            weightProblem = false;
            timeForWeightProblem = Random.Range(minimumTimePerWeightProblem, maxSecondsPerWeightProblem);
            timePassedInWeightProblem = 0;
            screenEvents.SetKGAlarm(ScreenEvents.State.Solution);
        }
    }

    private void StartWeightProblem()
    {
        weightExcess = Random.Range(5f, 20f);
        screenEvents.SetKGAlarm(ScreenEvents.State.On);
    }

    private void StartAngleProblem()
    {
        int wing = Random.Range(0, 2);

        if(wing == 0)
        {
            engineMalfunction += -(Random.Range(5f, 15f));
            GameObject.Find("EngineRight").GetComponent<AudioSource>().Play();
        }

        else if(wing == 1)
        {
            engineMalfunction += Random.Range(5f, 15f);
            GameObject.Find("EngineLeft").GetComponent<AudioSource>().Play();
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
