
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public bool player2;
    public bool isDead;
    public bool lastAlive;
    public bool debug;

    //Hunger
    [SerializeField] float maxHunger = 100;
    [SerializeField] float startHunger = 30;
    [SerializeField] float decayRateHunger = 0.5f;
    [SerializeField] Slider hungerBarShared, hungerBarSplit;
    public float currentHunger;

    //Courage
    [SerializeField] float maxCourage = 100;
    private float currentCourage;

    private void Start()
    {
        currentHunger = startHunger;
        currentCourage = maxCourage;
    }

    private void Update()
    {
        if(!debug)
            currentHunger -= decayRateHunger * Time.deltaTime;
        if (!isDead && currentHunger <= 0)
        {
            currentHunger = 0;
            Die();
        }
        hungerBarShared.value = currentHunger / 100;
        hungerBarSplit.value = currentHunger / 100;
    }

    public void AddHunger(int amount)
    {
        currentHunger += amount;
        if (currentHunger > maxHunger)
            currentHunger = maxHunger;
    }
    public void MinusHunger(int amount)
    {
        currentHunger -= amount;
        if (currentHunger < 0)
            currentHunger = 0;
    }

    public void AddCourage(int amount)
    {
        currentCourage += amount;
        if (currentCourage > maxCourage)
            currentCourage = maxCourage;
    }
    public void MinusCourage(int amount)
    {
        currentCourage -= amount;
        if (currentCourage > maxCourage)
            currentCourage = maxCourage;
    }

    public void Die()
    {
        isDead = true;
        Debug.Log(name + " has died");
        PlayerManager.instance.PlayerHasDied(player2);
        GetComponent<PlayerInventory>().DropItem();
        GetComponent<PlayerController2>().enabled = false;
        GetComponent<CharacterController>().enabled = false;

        GameManager.instance.gd_statistics.numDeaths_total++;
        if (player2)
            GameManager.instance.gd_statistics.numDeaths_p2++;
        else
            GameManager.instance.gd_statistics.numDeaths_p1++;
    }

}
