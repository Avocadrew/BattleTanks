using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponSystem : MonoBehaviour
{
    public Camera AimCamera;
    public GameObject shell;
    public Transform FirePosition;
    public Transform PlayerCamera;
    Transform AimPosition;
    public Transform canonBase;
    public Transform AimMode;
    public Transform ThirdPersonMode;
    public AudioSource tankSource;
    public ParticleSystem shellFire;

    public AudioClip FireAudio;
    bool isLoaded;
    bool infiniteMode = false;
    public int ammoCount;
    int maxAmmo = 6;
    public RawImage[] AmmoUI;
    //AmmoBoard
    public 
    // Start is called before the first frame update
    void Start()
    {
        AimPosition = ThirdPersonMode;
        isLoaded = true;
        Global.Ammo = 3;
        maxAmmo = 6;
        if(ammoCount == 1000)
        {
            infiniteMode = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (infiniteMode == true && Global.Ammo <= 2)
        {
            Global.Ammo += 1;
        }

        if (AimCamera.gameObject.activeSelf)
        {
            AimPosition = AimMode;
        }
        else
        {
            AimPosition = ThirdPersonMode;
        }
        if (Input.GetMouseButtonDown(0) && isLoaded && Global.Ammo > 0)//fire
        {
            Global.Ammo--;
            GameObject newShell = Instantiate(shell, FirePosition.position, canonBase.rotation);
            newShell.GetComponent<ShellExplode>().isEnemyFire = false;
            newShell.GetComponent<Rigidbody>().velocity = (AimPosition.position - canonBase.position).normalized * 30;
            isLoaded = false;
            FireParticleEmmision();
            Invoke("LoadUpShell", 1.5f);
        }
        for (int i = 0; i < maxAmmo; i++)
        {
            if (i < Global.Ammo)
            {
                AmmoUI[i].enabled = true;
            }
            else
            {
                AmmoUI[i].enabled = false;
            }
        }
        if(Global.Ammo > maxAmmo)
        {
            Global.Ammo = maxAmmo;
        }

    }
    void LoadUpShell()
    {
        isLoaded = true;
    }
    void FireParticleEmmision()
    {
        ParticleSystem fireParticle = Instantiate(shellFire, FirePosition.position, FirePosition.rotation);
        Destroy(fireParticle.gameObject, 2f);
        tankSource.PlayOneShot(FireAudio);
    }
}
