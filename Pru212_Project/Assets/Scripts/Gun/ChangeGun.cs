using UnityEngine;

public class ChangeGun : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int totalGuns = 3;
    public int currentGunIndex;
    public GameObject[] guns;
    public GameObject gunHolder;
    public GameObject currentGun;
    [SerializeField]
    public GameObject player;
    void Start()
    {
        totalGuns = gunHolder.transform.childCount;
        guns = new GameObject[totalGuns];
        for (int i = 0; i < totalGuns; i++)
        {
            guns[i] = gunHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
        }

        
        if (player.name.Trim().Equals("PinkMan(Clone)"))
        {
            guns[0].SetActive(true);
            currentGun = guns[0];
        }else if(player.name.Trim().Equals("NinjaFrog(Clone)"))
        {
            guns[1].SetActive(true);
            currentGun = guns[1];
        }
        else
        {
            guns[2].SetActive(true);
            currentGun = guns[2];
        }
       
        //currentGunIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButtonDown(1))
        //{
        //    if(currentGunIndex < totalGuns - 1)
        //    {
        //        guns[currentGunIndex].SetActive(false);
        //        currentGunIndex++;
        //        guns[currentGunIndex].SetActive(true);
        //        currentGun = guns[currentGunIndex];
        //    }
        //    else
        //    {
        //        guns[currentGunIndex].SetActive(false);
        //        currentGunIndex = 0;
        //        guns[currentGunIndex].SetActive(true);
        //        currentGun = guns[currentGunIndex];
        //    }

        //}
        
    }
}
