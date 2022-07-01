using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    #region Singleton
    static ItemsManager _instance;

    public static ItemsManager Instance => _instance;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public List<Item> AvailableBuffs;
    public List<Item> AvailableDebuffs;

    [Range(0f, 100f)]
    public float BuffChance;

    [Range(0f, 100f)]
    public float DebuffChance;


}
