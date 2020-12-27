
using UnityEngine;

public class weaponholder : MonoBehaviour
{
    public int selectedweapon = 0;
    // Start is called before the first frame update
    void Start()
    {
        Selectweapon();
    }

    // Update is called once per frame
    void Update()
    {

       int previousSelectedWeapon = selectedweapon;

       if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedweapon >= transform.childCount - 1)
                selectedweapon = 0;
            else
                selectedweapon++;
                 
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedweapon <= 0)
               selectedweapon = transform.childCount - 1;

            else
                selectedweapon--;

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedweapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedweapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedweapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedweapon = 3;
        }

        if (previousSelectedWeapon != selectedweapon)
        {
            Selectweapon();
        }

    }
    void Selectweapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedweapon)
             weapon.gameObject.SetActive(true);
             else
               weapon.gameObject.SetActive(false);
             i++;
        }
    }
}
