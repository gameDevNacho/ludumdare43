using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent (typeof(Rigidbody))]
public class BoxComponent : MonoBehaviour, IInteractable, IKvolume {

    public enum BoxSize { Small = 5, Medium = 10, Large = 20 };

    public BoxSize mySize;

    public Product myProduct;

    [SerializeField]
    private Transform parentTransform;
    [SerializeField]
    private Material normalMaterial;
    [SerializeField]
    private Material highlightedMaterial;
    [SerializeField]
    private MeshRenderer meshMaterial;

    private const float FORCETHROW = 20f;

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

        if(sucked)
        {
            rigid.AddForce(Vector3.forward * suckForce, ForceMode.Acceleration);
            rigid.AddTorque(Vector3.right * suckForce);
        }
    }

    public void Throw(HandlerComponent handler)
    {
        picked = false;
        handler.SetGrabbedObject(null);
        rigid.AddForce(handler.GetTransform().forward * FORCETHROW, ForceMode.Impulse);
        this.gameObject.layer = 11;
        PlayProductSound();
    }

    public void Interact(HandlerComponent handler)
    {
        picked = true;
        this.gameObject.layer = 9;
        this.transform.SetPositionAndRotation(handler.GetTransform().position, handler.GetTransform().rotation);
        handler.SetGrabbedObject(this);
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
        //aSource.clip = myProduct.shakeSound;
        //aSource.Play();
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
