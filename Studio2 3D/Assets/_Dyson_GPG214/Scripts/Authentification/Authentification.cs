using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using UnityEngine.UI;

namespace SAE.GPG214.Dyson.Authentification
{
    public class Authentification : MonoBehaviour
    {
        [SerializeField] private TMP_InputField userEmail;
        [SerializeField] private TMP_InputField userPassword;
        [SerializeField] private TMP_InputField userDisplayName;

        [SerializeField] private Button existingUserButton;
        [SerializeField] private Button newUserButton;
        [SerializeField] private Button logInButton;
        [SerializeField] private Button signOutButton;

        [SerializeField] private bool useDefaultCredentials;

        private string defaultEmail = "dysonmail@dyson.com";

        private string defaultPassword = "password";

        public bool isUserAuthenticated;

        private FirebaseAuth authentificationInstance;

        private FirebaseUser userProfile;
        

        private void NewUser()
        {

        }

        private void ExistingUser()
        {

        }

        private void SetUpButtons()
        {
            existingUserButton.onClick.RemoveAllListeners();
            existingUserButton.onClick.AddListener(ExistingUser);
            newUserButton.onClick.RemoveAllListeners();
            newUserButton.onClick.AddListener(NewUser);
        }
    }

}
