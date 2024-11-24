using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInteractions : MonoBehaviour
{
    [SerializeField] private GameObject canvasCamera;
    //[SerializeField] private GameObject purpleSword;
    //[SerializeField] private GameObject redSword;
    [SerializeField] private GameObject originalSword;
    [SerializeField] private Material purpleMaterial;
    [SerializeField] private Material redMaterial;
    public TextMeshProUGUI coinsText;

    int coins = 1000;
    private int click = 0;
    private int otherClick = 0;
    private int newCoins;

    public int maximum;

    public int current;

    public Image mask;
    //[SerializeField] private Material originalMaterial;
    void Start()
    {
     //   coinsText = GetComponent<TextMeshProUGUI>();
        StringToInt();
        //coinsText.text = "Coins : 1000";
        //  purpleSword = GameObject.Find("PurpleSword");
        // redSword = GameObject.Find("RedSword");
        //originalSword = GameObject.Find("OriginalSword");

    }

    // Update is called once per frame
    void Update()
    {
        EnableCanvasCamera();
    }

    void StringToInt()
    {
        coinsText.text = "1000";
        int.TryParse(coinsText.text, out coins);
        //coinsText.text = string.Format("{1000}", coins);
    }
    private void EnableCanvasCamera()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            canvasCamera.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvasCamera.SetActive(false);
        }
    }

    public void PurpleSword()
    {
        click += 1;
        originalSword.GetComponent<Renderer>().material = purpleMaterial;
        if (click == 1)
            coins -= 200;
        else
        {
         Debug.Log("Sword already equipped");   
        }
        coinsText.text = coins.ToString();
        
        Debug.Log("value of coins after purple buy: " + coins);
        //originalMaterial = purpleMaterial;
        // purpleSword.transform.position = originalSword.transform.position;
        //Debug.Log("Purple sword");
    }

    public void RedSword()
    {
        otherClick += 1;
        originalSword.GetComponent<Renderer>().material = redMaterial;
        if (otherClick == 1)
            coins -= 200;
        else
        {
            Debug.Log("Sword already equipped");
        }
        coinsText.text = coins.ToString();
        //originalMaterial = redMaterial;
    }

    public void FillGauge()
    {
        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = 1;
        Debug.Log("Gauge");
    }   
}
