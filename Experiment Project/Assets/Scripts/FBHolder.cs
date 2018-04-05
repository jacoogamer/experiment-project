using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;
using UnityEngine.UI;

public class FBHolder : MonoBehaviour
{
    public GameObject FBLoginObj, FBLoggedOutObj,FBPublicProfileData, FBProfilePic;
    private void Awake()
    {
        if(!FB.IsInitialized)
            FB.Init(SetInitializer, OnHideUnity);
        else
        {

            MenuIsLoggedIn(FB.IsLoggedIn);
        }
    }

    private void SetInitializer()
    {
       
        if (FB.IsLoggedIn)
        {
            Debug.Log("FB IsLoggedIn");
        }
        else
        {
            Debug.Log("NOT FB IsLoggedIn");
        }
        MenuIsLoggedIn(FB.IsLoggedIn);
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    IEnumerator CheckForSuccussfulLogout()
    {
        if (FB.IsLoggedIn)
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine("CheckForSuccussfulLogout");
            
        }
        else
        {
            if (!FB.IsInitialized)
                FB.Init(SetInitializer, OnHideUnity);
            else
            {

                MenuIsLoggedIn(FB.IsLoggedIn);
            }
        }
    }

    public void FBLogOut()
    {
        if (FB.IsLoggedIn)
        {
            FB.LogOut();
            StartCoroutine("CheckForSuccussfulLogout");
        }
    }

    public void FBLogin()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        FB.LogInWithReadPermissions(permissions, AuthCallBack);
    }

    private void AuthCallBack(IResult result)
    {
       if(result.Error != null)
        {
            Debug.Log(result.Error);
        }else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("FB IsLoggedIn");
            }
            else
            {
                Debug.Log("NOT FB IsLoggedIn");
            }
            MenuIsLoggedIn(FB.IsLoggedIn);
        }
    }

    void MenuIsLoggedIn(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            FBLoginObj.SetActive(true);
            FBLoggedOutObj.SetActive(false);

            FB.API("/me?fields=first_name,last_name,id,name,link,gender,locale,picture,timezone,updated_time,verified", HttpMethod.GET, ShowPublicProfile);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, ShowProfilePic);
        }
        else
        {
            FBLoginObj.SetActive(false);
            FBLoggedOutObj.SetActive(true);
            FBProfilePic.SetActive(false);
        }
    }

    private void ShowPublicProfile(IResult result)
    {
        Text PublicProfileData = FBPublicProfileData.GetComponent<Text>();
        
        
        if (result.Error == null)
        {
            PublicProfileData.text =
                "First Name " + result.ResultDictionary["first_name"] + "\n" + 
                "Last Name " + result.ResultDictionary["last_name"] + "\n" +
            "ID " + result.ResultDictionary["id"] + "\n" +
            "Name " + result.ResultDictionary["name"] + "\n" +
            "Link " + result.ResultDictionary["link"] + "\n" +
            "Gender " + result.ResultDictionary["gender"] + "\n" +
            "Locale " + result.ResultDictionary["locale"] + "\n" +
            "Timezone " + result.ResultDictionary["timezone"] + "\n" +
            "Updated_time " + result.ResultDictionary["updated_time"] + "\n" +
            "Verified " + result.ResultDictionary["verified"];

        } else
        {
            Debug.Log(result.Error);
        }
    }

   

    void ShowProfilePic(IGraphResult result)
    {

        if (result.Texture != null)
        {
            FBProfilePic.SetActive(true);
            Image ProfilePic = FBProfilePic.GetComponent<Image>();

            ProfilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());

        }

    }

    IEnumerator WaitingToGetProfileName()
    {
        while (!FB.IsLoggedIn)
        {
            yield return null;
        }

        MenuIsLoggedIn(FB.IsLoggedIn);
    }

   
}
