using UnityEngine;
using UnityEngine.Timeline;


[RequireComponent(typeof(ChangeWeaponType))]
public class GunBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject launchPoint;

    [SerializeField] private float bulletSpeed = 15f;

    [Header("Audio")]
    [SerializeField] private AudioSource gunAudioSource;
    [SerializeField] private AudioClip dartGunPopSfx;
    [SerializeField] private AudioClip pistolGunShootSfx;

    [Header("VFX")]
    [SerializeField] private ParticleSystem pistolGunMuzzleFlashFX;
    [SerializeField] private GameObject bulletHolePrefab;

    private ChangeWeaponType changeWeapType;
    private void Awake()
    {
        changeWeapType = GetComponent<ChangeWeaponType>();
    }

    private ChangeWeaponType.GunType currentType;

    public void OnActivateTrigger()
    {
        currentType = changeWeapType.GetCurrentGunType();

        switch (currentType)
        {
            case ChangeWeaponType.GunType.ToyGun:
                ActivateToyGunTrigger();
                break;
            case ChangeWeaponType.GunType.PistolGun:
                ActivatePistolGunTrigger();
                break;
        }
    }

    private void ActivateToyGunTrigger()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, launchPoint.transform.position, gameObject.transform.rotation);
        Rigidbody bulletRb = bulletObj.GetComponent<Rigidbody>();

        bulletRb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);

        gunAudioSource.clip = dartGunPopSfx;
        gunAudioSource.Play();
    }

    private void ActivatePistolGunTrigger()
    {
        pistolGunMuzzleFlashFX.Play();

        gunAudioSource.clip = pistolGunShootSfx;
        gunAudioSource.Play();

        FirePistolRay();

        Debug.Log("FIring pistol gun");
    }


    private void FirePistolRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(launchPoint.transform.position, launchPoint.transform.forward, out hit,Mathf.Infinity))
        {
            Debug.Log("Hit: " + hit.collider.name);
            CreateBulletHole(hit);
        }
    }


    private float decalOffset = 0.01f;
    private void CreateBulletHole(RaycastHit objectHit)
    {
        GameObject bulletHole = Instantiate(
            bulletHolePrefab,
            objectHit.point + (objectHit.normal * decalOffset),
            Quaternion.LookRotation(objectHit.normal)
            );

        bulletHole.transform.SetParent(objectHit.transform);
    }

    private bool weaponGrabbed = false;

    public void OnWeaponGrab()
    {
        weaponGrabbed = true;
        Debug.Log("Weapon grabbed");
    }
    public void OnWeaponUnGrab()
    {
        weaponGrabbed = false;
        Debug.Log("Weapon UnGrabbed");
    }

    public bool IsWeaponGrabbed()
    {
        return weaponGrabbed;
    }
}
