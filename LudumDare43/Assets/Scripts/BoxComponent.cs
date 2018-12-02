using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent (typeof(Rigidbody))]
public class BoxComponent : MonoBehaviour, IInteractable {

    public enum Type { Small = 5, Medium = 10, Large = 20 };

    public Type myType;

    public Product myProduct;

    public AudioClip audio;

    [SerializeField]
    private Transform parentTransform;
    [SerializeField]
    private Material normalMaterial;
    [SerializeField]
    private Material highlightedMaterial;
    [SerializeField]
    private MeshRenderer mesh;

    Rigidbody rigid;

    float speed = 10.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        mesh = GetComponentInChildren<MeshRenderer>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        rigid.AddForce(parentTransform.right * 10f * -parentTransform.right.y, ForceMode.Force);
        if(rigid.angularVelocity.magnitude > 4 && !this.GetComponent<AudioSource>().isPlaying)
        {
            this.GetComponent<AudioSource>().clip = audio;
            this.GetComponent<AudioSource>().Play();
        }

        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }

    public void Throw(HandlerComponent handler)
    {
        handler.SetGrabbedObject(null);
        rigid.AddForce(handler.GetTransform().forward * 10, ForceMode.Impulse);
        this.gameObject.layer = 0;
    }

    public void Interact(HandlerComponent handler)
    {
        this.gameObject.layer = 9;
        this.transform.SetPositionAndRotation(handler.GetTransform().position, handler.GetTransform().rotation);
        handler.SetGrabbedObject(this);
    }

    public void Release(HandlerComponent handler)
    {
        this.gameObject.layer = 0;
        handler.SetGrabbedObject(null);
    }

    public void ChangeMaterial(bool outline)
    {
        if(outline)
        {
            mesh.material = highlightedMaterial;
        }

        else
        {
            mesh.material = normalMaterial;
        }
    }
}
