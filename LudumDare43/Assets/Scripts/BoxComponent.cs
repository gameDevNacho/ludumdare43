using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent (typeof(Rigidbody))]
public class BoxComponent : MonoBehaviour, IInteractable, IKvolume {

    public enum BoxSize { Small = 3, Medium = 6, Large = 12 };

    public BoxSize mySize;

    public Product myProduct;

    [SerializeField]
    public Transform parentTransform;
    [SerializeField]
    private Material normalMaterial;
    [SerializeField]
    private Material highlightedMaterial;
    [SerializeField]
    private MeshRenderer meshMaterial;

    private const float FORCETHROW = 5f;

    private bool picked = false;
    private bool sucked = false;
    private AudioSource aSource;

    private static float suckForce = 10.0f;

    Rigidbody rigid;

    float speed = 10.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        meshMaterial = GetComponentInChildren<MeshRenderer>();
        aSource = GetComponent<AudioSource>();
    }

    public void InitializeBox(BoxProductSelector.Str_BoxProduct boxProduct)
    {
        myProduct = boxProduct.product;
        this.GetComponentInChildren<MeshFilter>().mesh = boxProduct.boxMesh;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!picked && parentTransform != null)
            rigid.AddForce(parentTransform.right * 10f * -parentTransform.right.y, ForceMode.Force);

        if(sucked && !picked)
        {
            rigid.AddForce(Vector3.forward * suckForce, ForceMode.Acceleration);
            rigid.AddTorque(Vector3.right * suckForce);
        }
    }

    public void Throw(HandlerComponent handler)
    {
        picked = false;
        handler.SetGrabbedObject(null);
        float force;
        switch (mySize)
        {
            case BoxSize.Small:
                force = 3;
                break;
            case BoxSize.Medium:
                force = 1.7f;
                break;
            case BoxSize.Large:
                force = 1.3f;
                break;
            default:
                force = 1.00f;
                break;
        }
        rigid.AddForce(handler.GetTransform().forward * FORCETHROW * force, ForceMode.VelocityChange);
        this.gameObject.layer = 11;
        PlayProductSound();
    }

    public void Interact(HandlerComponent handler)
    {
        picked = true;
        this.gameObject.layer = 9;
        this.transform.position = handler.GetTransform().position;
        handler.SetGrabbedObject(this);
        PlayProductSound();
    }

    public void Release(HandlerComponent handler)
    {
        picked = false;
        this.gameObject.layer = 11;
        handler.SetGrabbedObject(null);
    }

    public void ChangeMaterial(bool outline)
    {
        if(outline)
        {
            meshMaterial.material = highlightedMaterial;
        }

        else
        {
            meshMaterial.material = normalMaterial;
        }
    }

    public void PlayProductSound()
    {
        if(myProduct != null)
        {
            aSource.clip = myProduct.shakeSound;
            aSource.Play();
        }       
    }

    public void OnKillVolumeEnter()
    {
        Destroy(gameObject);
    }

    public void AddSuccionAceleration()
    {
        if (!picked)
        {
            sucked = true;
            PlaneManager.Instance.BoxThrown(this);
        }       
    }
}
