using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

[RequireComponent(typeof (GunBehaviour))]
public class ChangeWeaponType : MonoBehaviour
{
    [Header("Input")]
    [SerializeField]
    [Tooltip("Input data that will be used to Change weapon type")]
    private XRInputButtonReader m_changeWeaponInput = new XRInputButtonReader("Switch Weapon");



    private GunBehaviour gunBehav;

    private void Awake()
    {
        gunBehav = GetComponent<GunBehaviour>();
    }

    public XRInputButtonReader jumpInput
    {
        get => m_changeWeaponInput;
        set => XRInputReaderUtility.SetInputProperty(ref m_changeWeaponInput, value, this);
    }

    private void OnEnable()
    {
        m_changeWeaponInput.EnableDirectActionIfModeUsed();
    }

    private void OnDisable()
    {
        m_changeWeaponInput.DisableDirectActionIfModeUsed();
    }

    // Update is called once per frame
    void Update()
    {
        CheckChangeInput();
    }

    [Header("Material")]
    [SerializeField] private MeshRenderer gripMeshRenderer;
    [SerializeField] private MeshRenderer muzzleMeshRenderer;

    [SerializeField] private Color pistolColor;
    [SerializeField] private Color toyGunColor;
    
    public enum GunType { ToyGun, PistolGun }

    public GunType CurrentActiveGunType { get; private set; }

    private void Start()
    {
        CurrentActiveGunType = GunType.ToyGun;
    }

    public GunType GetCurrentGunType()
    {
        return CurrentActiveGunType;
    }

    private void CheckChangeInput()
    {
        if (m_changeWeaponInput.ReadWasPerformedThisFrame() && gunBehav != null && gunBehav.IsWeaponGrabbed())
        {
            switch (CurrentActiveGunType)
            {
                case GunType.ToyGun:
                    ChangeToPistolGun();
                    break;
                case GunType.PistolGun:
                    ChangeToToyGun();
                    break;
            }
        }
    }

    private void ChangeToPistolGun()
    {
        gripMeshRenderer.GetComponent<MeshRenderer>().material.color = pistolColor;
        muzzleMeshRenderer.GetComponent<MeshRenderer>().material.color = pistolColor;

        CurrentActiveGunType = GunType.PistolGun;

        Debug.Log("Changin weapon to pistol");
    }
    
    private void ChangeToToyGun()
    {
        gripMeshRenderer.GetComponent<MeshRenderer>().material.color = toyGunColor;
        muzzleMeshRenderer.GetComponent<MeshRenderer>().material.color = toyGunColor;

        CurrentActiveGunType = GunType.ToyGun;

        Debug.Log("Changin weapon to toy gun");
    }
}