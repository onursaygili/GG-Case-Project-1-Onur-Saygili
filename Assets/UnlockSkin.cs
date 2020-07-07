using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UnlockSkin : MonoBehaviour
{
    
    public Button unlockbutton;
    public Sprite[] SkinSprites;
    public GameObject[] skins;
    public List<int> holder;
    int SkinNum,FrameNum=0;
    bool checker = false;
    public Color FramecolorGreen;
    public Color FramecolorGray;
    int ManuelFrameHolder = -1;

    void Start()
    {
        FramecolorGreen = skins[0].transform.GetComponent<Image>().color;
        FramecolorGray = skins[1].transform.GetComponent<Image>().color;
        skins[0].transform.GetComponent<Image>().color = FramecolorGray;
    }
    public void Unlocker()
    {
        if(ManuelFrameHolder != -1)
        {
            skins[ManuelFrameHolder].transform.GetComponent<Image>().color = FramecolorGray;
        }
        unlockbutton.GetComponent<Button>().interactable = false; // to prevent doubleclick to unlock button
        checker = false;
        skins[SkinNum].transform.GetComponent<Image>().color = FramecolorGray;
        SelectSkinNum();                                        // call function to select random number of skin to unlock
        while (checker.Equals(true))
        {
            checker = false;                                    //check if randomly selected number unlocked before
            SelectSkinNum();
        }
        holder.Add(SkinNum);                         
        StartCoroutine(GreenFrame());                           // call to green frame movement sequence   
     }


    void revealUnlocked()
    {
        
        skins[SkinNum].transform.GetChild(1).GetComponent<Image>().sprite = SkinSprites[SkinNum];
        skins[SkinNum].GetComponent<Button>().interactable = true;
        unlockbutton.GetComponent<Button>().interactable = true;

        if (holder.Count == 9)
        {
            unlockbutton.GetComponent<Button>().interactable = false;
            unlockbutton.transform.GetChild(0).GetComponent<Text>().text = "All Unlocked";
        }
    }

    void SelectSkinNum()
    {
        SkinNum = 99;
        SkinNum = Random.Range(0,9);
        if (holder.Contains(SkinNum))                   // Select random number to open a skin
        {
            checker = true;
        }
        
        
    }

    IEnumerator GreenFrame()
    {
        if (holder.Count <= 7) {
            for (int i = 0; i < 9; i++)
            {
                skins[FrameNum].transform.GetComponent<Image>().color = FramecolorGray;
                skins[FrameNum].GetComponent<Button>().interactable = false;
                FrameNum = Random.Range(0, 9);
                while (holder.Contains(FrameNum))
                {
                    FrameNum = Random.Range(0, 9);
                }
                skins[FrameNum].GetComponent<Image>().color = FramecolorGreen;                  //Green frame randomly travel between skins
                skins[FrameNum].GetComponent<Button>().interactable = true;

                yield return new WaitForSeconds(0.2f);
            }
            skins[FrameNum].GetComponent<Button>().interactable = false;
        }
            
        skins[FrameNum].transform.GetComponent<Image>().color = FramecolorGray;
        skins[SkinNum].transform.GetComponent<Image>().color = FramecolorGreen;
        skins[SkinNum].GetComponent<Button>().interactable = true;
        yield return new WaitForSeconds(0.4f);
        revealUnlocked();
        
    }

    public void MakeItGreen()                                       // on click of any skins greenframe switch to the selected one
    {   
        if(holder.Count >= 2)
        {
            foreach (int j in holder)
            {
                skins[j].transform.GetComponent<Image>().color = FramecolorGray;
            }
            string butname = EventSystem.current.currentSelectedGameObject.name;
            char a = butname[4];
            int b = (int)a - 49;
            ManuelFrameHolder = b;
            skins[b].transform.GetComponent<Image>().color = FramecolorGreen;
        }
        
    }
}
