﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Proyecto26;
using FullSerializer;

public class NewCharacter : MonoBehaviour
{
    public static fsSerializer serializer = new fsSerializer();
    public TMP_InputField nameInput;
    public NewCharStats statsRef;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void New()
    {
        PlayerData.Instance.tempChar.id = PlayerData.Instance.user.localId;
        UIManager.Instance.LoadScreen(7);
    }

    public void AddName()
    {
        if (nameInput.text != "")
        {
            PlayerData.Instance.tempChar.name = nameInput.text;
            UIManager.Instance.LoadScreen(8);
        }
        else
        {
            UIManager.Instance.Warning("No name entered");
        }
    }



    public void AddStats()
    {
        if (statsRef.points == 0)
        {
            PlayerData.Instance.tempChar.word = statsRef.stats[0];
            PlayerData.Instance.tempChar.wit = statsRef.stats[1];
            PlayerData.Instance.tempChar.will = statsRef.stats[2];
            PlayerData.Instance.tempChar.want = statsRef.stats[3];

            UIManager.Instance.LoadScreen(9);
        }
        else
        {
            UIManager.Instance.Warning("Make sure to use all your points!");
        }
    }

    public void Create()
    {
        if (ActiveCampaign.Instance.activeCampaign.characters == null)
            ActiveCampaign.Instance.activeCampaign.characters = new List<Character>();

        PlayerData.Instance.tempChar.level = 1;

        ActiveCampaign.Instance.activeCampaign.characters.Add(PlayerData.Instance.tempChar);

        RestClient.Put(AccountManager.Instance.uri + "/campaigns/" + ActiveCampaign.Instance.activeCampaign.key + ".json?auth=" + AccountManager.Instance.idToken, ActiveCampaign.Instance.activeCampaign)
        .Then(response =>
        {
            Debug.Log(response.Text);
            UIManager.Instance.LoadScreen(5);
        })
        .Catch(error =>
        {
            Debug.Log(error);
        });
    }
}
