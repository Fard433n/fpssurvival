using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem muzzleflesh;
    public GameObject hitparticle;
    public float impactforce = 30f;
    public float firerate = 15f;

    public int maxAmmo = 20;
    private int currentAmmo = -1;
    public float reloadTime = 1f;

    private bool isReloading = false;

    private float nextTimeFire = 0f;


    public Animator animator;
    private void Start()
    {
      //  if (currentAmmo == -1)
            currentAmmo = maxAmmo;
    }
    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    private void Update()
    {
        if (isReloading)
            return;
        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if(Input.GetButton("Fire1") && Time.time >= nextTimeFire )
        {
            nextTimeFire = Time.time + 1f/firerate;
            Shoot();
        }
    }
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Relooding...");
        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(- .25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
    void Shoot()
    {
        muzzleflesh.Play();

        currentAmmo--;

        RaycastHit hit;
        if(  Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
          Debug.Log(hit.transform.name);


            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactforce);
            }
            GameObject impactgo = Instantiate(hitparticle, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactgo, 2f);
        }
    }
}
