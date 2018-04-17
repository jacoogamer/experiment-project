namespace SignInSample {
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Google;
  using UnityEngine;
  using UnityEngine.UI;
    using System.Collections;

    public class GoogleSignin : MonoBehaviour {

    public Text statusText,GoogleUserID, GoogleIdToken, GoogleGivenName, GoogleEmail, GoogleDisplayName;
    public Image GoogleProfilePic;
    public string webClientId = "992185470650-720p9c2u1njg85kle92lb6r5ru07god1.apps.googleusercontent.com";
    public GameObject LogIn, LogOut;
     
    private GoogleSignInConfiguration configuration;
        
        // Defer the configuration creation until Awake so the web Client ID
        // Can be set via the property inspector in the Editor.
        void Awake() {
           
            if (PlayerPrefs.GetString("SignIn", "false") == "true")
            {
               InvokeRepeating("OnSignIn", 0.0f, 0.5f);
                
            }
            else
            {
                configuration = new GoogleSignInConfiguration
                {
                    WebClientId = webClientId,
                    RequestIdToken = true
                };
                LogIn.SetActive(true);
                LogOut.SetActive(false);

            }

            
        }
        public void SetSignInFalse()
        {
            PlayerPrefs.SetString("SignIn", "false");
        }
        private void OnDestroy()
        {
            OnSignOut();
        }

        public void OnSignIn() {

        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
            AddStatusText("Calling SignIn");
            
      GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
        OnAuthenticationFinished);
    }

    public void OnSignOut() {
      AddStatusText("Calling SignOut");
      GoogleSignIn.DefaultInstance.SignOut();
            LogIn.SetActive(true);
            LogOut.SetActive(false);
        }
        
    public void OnDisconnect() {
      AddStatusText("Calling Disconnect");
      GoogleSignIn.DefaultInstance.Disconnect();
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task) {
      if (task.IsFaulted) {
                LogIn.SetActive(true);
                LogOut.SetActive(false);

                using (IEnumerator<System.Exception> enumerator =
                task.Exception.InnerExceptions.GetEnumerator()) {
          if (enumerator.MoveNext()) {
            GoogleSignIn.SignInException error =
                    (GoogleSignIn.SignInException)enumerator.Current;
            AddStatusText("Got Error: " + error.Status + " " + error.Message);
          } else {
            AddStatusText("Got Unexpected Exception?!?" + task.Exception);
          }
        }
      }
      else if(task.IsCanceled)
            {
                LogIn.SetActive(true);
                LogOut.SetActive(false);
                statusText.text = "Came From Configuration null";
               
                AddStatusText("Canceled");
            } else
            {
                CancelInvoke("OnSignIn");
                PlayerPrefs.SetString("SignIn", "true");
                PlayerPrefs.Save();
                AddStatusText("Welcome: " + task.Result.DisplayName + "!");
                LogIn.SetActive(false);
                LogOut.SetActive(true);
                Uri uriAddress = task.Result.ImageUrl;
                GoogleUserID.text = "User Id " + task.Result.UserId;
                GoogleIdToken.text = "Id Token " + task.Result.IdToken;
                GoogleGivenName.text = "Given Name " + task.Result.GivenName;
                GoogleEmail.text = "Email " + task.Result.Email;
                //GoogleDisplayName.text = "Display Name " + task.Result.DisplayName;
                GoogleDisplayName.text = "Display Name " + task.Result.DisplayName;
                
                StartCoroutine(GoogleProfilePicStart("https://lh3.googleusercontent.com" + uriAddress.AbsolutePath + "?sz=512"));


            }
    }
        IEnumerator GoogleProfilePicStart(string url)
        {
            Texture2D tex;
            tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
            using (WWW www = new WWW(url))
            {
                yield return www;
                www.LoadImageIntoTexture(tex);
                GoogleProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 512, 512), new Vector2());
            }
        }
        public void OnSignInSilently() {
      GoogleSignIn.Configuration = configuration;
      GoogleSignIn.Configuration.UseGameSignIn = false;
      GoogleSignIn.Configuration.RequestIdToken = true;
      AddStatusText("Calling SignIn Silently");

      GoogleSignIn.DefaultInstance.SignInSilently()
            .ContinueWith(OnAuthenticationFinished);
    }
        

    public void OnGamesSignIn() {
      GoogleSignIn.Configuration = configuration;
      GoogleSignIn.Configuration.UseGameSignIn = true;
      GoogleSignIn.Configuration.RequestIdToken = false;
            
      AddStatusText("Calling Games SignIn");

      GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
        OnAuthenticationFinished);
    }

    private List<string> messages = new List<string>();
    void AddStatusText(string text) {
      if (messages.Count == 5) {
        messages.RemoveAt(0);
      }
      messages.Add(text);
      string txt = "";
      foreach (string s in messages) {
        txt += "\n" + s;
      }
      statusText.text = txt;
    }
  }
}
