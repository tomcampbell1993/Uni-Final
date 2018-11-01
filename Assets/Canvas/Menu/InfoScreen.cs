using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoScreen : MonoBehaviour {

    public Texture BF109texture;
    public Texture BF110texture;
    public Texture spitfireTexture;
    public Texture hurricaneTexture;
    public Texture heinkellTexture;
    public Texture junkersTexture;
    public Texture dornierTexture;

    public GameObject infoObject;
    public GameObject imageObject;

    public Button infoButton;
    public RawImage image;

    public int imageNumber;

	void Start () {

        imageNumber = 1;

        infoObject = GameObject.Find("InfoButton");
        imageObject = GameObject.Find("InfoImage");

        infoButton = infoObject.GetComponent<Button>();
        image = imageObject.GetComponent<RawImage>();

        imageObject.SetActive(false);

        SetButtons();

	}
	
	void Update () {

        ImageControl();

    }

    void SetButtons()
    {
        infoButton.onClick.AddListener(PressInfo);
    }

    public void PressInfo()
    {
        imageObject.SetActive(!imageObject.activeInHierarchy);
    }

    void ImageControl()
    {
        if (Input.GetKeyDown("n"))
        {
            imageNumber = imageNumber + 1;
        }

        if(imageNumber == 1)
        {
            image.texture = BF109texture;
        }
        if(imageNumber == 2)
        {
            image.texture = BF110texture;
        }
        if(imageNumber == 3)
        {
            image.texture = spitfireTexture;
        }
        if(imageNumber == 4)
        {
            image.texture = hurricaneTexture;
        }
        if(imageNumber == 5)
        {
            image.texture = heinkellTexture;
        }
        if(imageNumber == 6)
        {
            image.texture = junkersTexture;
        }
        if(imageNumber == 7)
        {
            image.texture = dornierTexture;
        }
        if(imageNumber > 7)
        {
            imageNumber = 1;
        }
    }
}
