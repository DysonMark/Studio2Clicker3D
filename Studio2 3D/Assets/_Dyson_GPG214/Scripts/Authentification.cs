using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class Authentification : MonoBehaviour
{
    [SerializeField] private TMP_InputField userEmail;
    [SerializeField] private TMP_InputField userPassword;
    [SerializeField] private TMP_InputField userDisplayName;

    [SerializeField] private Button existingUserButton;
    [SerializeField] private GameObject newUserButton;
    [SerializeField] private GameObject logInButton;
    [SerializeField] private GameObject signOutButton;

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
        //existingUserButton.on
        existingUserButton.GetComponent<Button>().onClick.RemoveAllListeners();
        existingUserButton.GetComponent<Button>().onClick.AddListener(ExistingUser);
        newUserButton.GetComponent<Button>().onClick.RemoveAllListeners();
        newUserButton.GetComponent<Button>().onClick.AddListener(NewUser);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
