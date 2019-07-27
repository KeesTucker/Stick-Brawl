using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UpdateUI : MonoBehaviour {

    public RefrenceKeeperAI refrenceKeeper;

    public Transform itemsParent;

    public SlotUI[] inventorySlots;

    public GameObject canvas;

    public GameObject fNote;

    public GameObject deadMessage;
    public GameObject deadPanel;
    public GameObject deadMessageServer;
    public GameObject deadMessageClient;
    public GameObject multiWonServer;
    public GameObject won;
    public GameObject clientWon;
    public GameObject clientMultiWon;
    public GameObject[] HUDSlots;

    void Start()
    {
        inventorySlots = itemsParent.GetComponentsInChildren<SlotUI>();
    }

    public void UpdateSlotsUI()
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < refrenceKeeper.weaponInventory.Count)
            {
                inventorySlots[i].AddItem(refrenceKeeper.weaponInventory[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
            
        }
        
    }

    public void OpenClose()
    {
        if(canvas.activeSelf == false)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }

    public void HighlightSlot(int activeSlot)
    {
        if(refrenceKeeper.weaponInventory.Count > 0)
        {
            activeSlot = Mathf.Clamp(activeSlot, 0, refrenceKeeper.weaponInventory.Count - 1);
            for (int i = 0; i < 4; i++)
            {
                gameObject.transform.GetChild(0).GetChild(i).GetChild(1).gameObject.SetActive(false);
            }
            gameObject.transform.GetChild(0).GetChild(activeSlot).GetChild(1).gameObject.SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                HUDSlots[i].SetActive(false);
            }
            HUDSlots[activeSlot].SetActive(true);
        }
    }
    public void HighlightSlotOnPickup(int activeSlot)
    {
        activeSlot = Mathf.Clamp(activeSlot, 0, refrenceKeeper.weaponInventory.Count);
        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(0).GetChild(i).GetChild(1).gameObject.SetActive(false);
        }
        gameObject.transform.GetChild(0).GetChild(activeSlot).GetChild(1).gameObject.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            HUDSlots[i].SetActive(false);
        }
        HUDSlots[activeSlot].SetActive(true);
    }

    public void BackToHome()
    {
        if (GameObject.Find("LocalPlayer").GetComponent<Reconnect>().isServer)
        {
            SyncData.backToHome = true;
            SyncData.nextLevel = false;
            SyncData.retryLevel = false;
            SyncData.isCampaignLevel = false;
            LevelUpdate();
        }
        else
        {
            SyncData.backToHome = true;
            SyncData.nextLevel = false;
            SyncData.retryLevel = false;
            SyncData.isCampaignLevel = false;
            NetworkManager.singleton.StopClient();
        }
    }

    public void NextCampaignLevel()
    {
        SyncData.backToHome = false;
        SyncData.nextLevel = true;
        SyncData.reconnectLevel = SyncData.chunkID;
        SyncData.retryLevel = false;
        SyncData.isCampaignLevel = true;
        LevelUpdate();
    }

    public void NextMultiplayerLevel()
    {
        SyncData.backToHome = false;
        SyncData.nextLevel = true;
        SyncData.retryLevel = false;
        SyncData.isCampaignLevel = false;
        LevelUpdate();
    }

    public void RetryCampaignLevel()
    {
        SyncData.backToHome = false;
        SyncData.nextLevel = false;
        SyncData.reconnectLevel = SyncData.chunkID;
        SyncData.retryLevel = true;
        SyncData.isCampaignLevel = true;
        LevelUpdate();
    }

    public void LevelUpdate()
    {
        if (GameObject.Find("LocalPlayer"))
        {
            GameObject.Find("LocalPlayer").GetComponent<Reconnect>().CmdLevelUpdate();
        }
        if (FindObjectOfType<PlayGames>())
        {
            FindObjectOfType<PlayGames>().Game();
        }
    }
}
