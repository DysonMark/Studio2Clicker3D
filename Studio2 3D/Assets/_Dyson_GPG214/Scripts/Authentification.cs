using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using Firebase;
using Firebase.Auth;
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
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void NewUser()
    {
        
    }

    private void ExistingUser()
    {
        
    }

    private void SetUpButtons()
    {
        existingUserButton.clicked += NewUser;
        existingUserButton.clicked -= NewUser;
        existingUserButton.clicked += ExistingUser;
        existingUserButton.clicked -= ExistingUser;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
