using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//player object script

public class PickUpManagerRedux : MonoBehaviour
{
    //Player
    public PickableItem currentPickup;
    
    //UI
    public Slider pickupTimeBar;
    public Image currentPickupImage;
    public TextMeshProUGUI usesCounterText;
    public GameObject useButton;
    
    public event Action<PickableItem> OnDiscardPickup;

    void Start()
    {
        pickupTimeBar.gameObject.SetActive(false);
        useButton.SetActive(false);
        PickUpTimer.TimerEnded += DeletePickup;
    }

    private void Update()
    {
        if (PickUpTimer.isRunning)
        {
            pickupTimeBar.value = 1 - PickUpTimer.currentTime / PickUpTimer.time;
        }
    }

    //picks up things

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Pickup"))
        {
            var otherPickup = other.GetComponent<PickupRedux>();
            if (otherPickup == null)
            {
                Debug.Log("No pickup script assigned to pickup object");
                return;
            }

            if (otherPickup is PickableItem)
            {
                if (TryEquipPickup(otherPickup as PickableItem))
                {
                    OnPickup(otherPickup);
                    otherPickup.OnPickup();
                    otherPickup.GetComponent<MeshRenderer>().enabled = false;
                    otherPickup.GetComponent<Collider2D>().enabled = false;
                }
            }
            else //if pickup is not pickable then it must be used on pickup
            {
                OnPickup(otherPickup);
                UseOnPickup(otherPickup);
            }
        }
    }

    void OnPickup(PickupRedux pickup)
    {
        SoundManager.instance.Play(pickup.onPickUpClip);
    }

    void UseOnPickup(PickupRedux pickup)
    {
        pickup.OnPickup();
        Destroy(pickup.gameObject);
    }

    //equips pickup
    private bool TryEquipPickup(PickableItem pickableItem)
    {
        if (currentPickup != null)
        {
            //Механика пополнения пикапов без дискарда
            //
            //if (currentPickup.name == pickableItem.name)
            //{
            //    if (currentPickup is LastingItem)
            //    {
            //        if ((currentPickup as LastingItem).timeOfUsage - PickUpTimer.currentTime < (pickableItem as LastingItem).timeOfUsage - 0.01f)
            //        {
            //            // доработать логику, так как текущий пикап по сути удаляется и берется новый, 
            //            // если он был активирован, он деактивируется, хотя по сути действие должно удлинняться
            //            // причем без остановки.
            //            DeletePickup();
            //            currentPickup = pickableItem;
            //            ToggleButton(true);
            //            return true;
            //        }
            //    }
            //    else if (currentPickup is ReusableItem)// можно через "или" внести в предыдущее условие
            //    {
            //        if ((currentPickup as ReusableItem).usesRemaining < (pickableItem as ReusableItem).usesRemaining)
            //        {
            //            DeletePickup();
            //            currentPickup = pickableItem;
            //            ToggleButton(true);
            //            return true;
            //        }
            //    }
            //}
            return false;
        }
        else
        {
            currentPickup = pickableItem;
            ToggleButton(true);
            return true;
        }
    }

    public void ActivatePickup()
    {
        if (currentPickup == null)
            return;

        currentPickup.Activate();
        
        // возможно нужно разнести это по функциям Activate
        if (!(currentPickup is LastingItem))
        {
            if (currentPickup is ReusableItem)
            {
                if ((currentPickup as ReusableItem).usesRemaining > 1)
                {
                    (currentPickup as ReusableItem).usesRemaining--;
                    UpdateCounterText((currentPickup as ReusableItem).usesRemaining);
                }
                else
                    DeletePickup();
            }
            else
                DeletePickup();
        }
        else
        {
            if (PickUpTimer.isUsed)
            {
                if (PickUpTimer.isRunning)
                    PickUpTimer.PauseTimer();
                else
                    PickUpTimer.ResumeTimer();
            }
            else
            {
                PickUpTimer.SetTimer((currentPickup as LastingItem).timeOfUsage);
            }
        }
    }

    public void DeletePickup()
    {
        if (currentPickup is LastingItem)
        {
            PickUpTimer.StopTimer();
            if ((currentPickup as LastingItem).state)
            {
                currentPickup.Activate();
            }
        }

        if (currentPickup!= null)
            Destroy(currentPickup.gameObject);
        
        ToggleButton(false);
        currentPickup = null;
    }

    public void DiscardPickup()
    {
        if (currentPickup != null)
        {
            OnDiscardPickup(currentPickup);
            DeletePickup();
        }
    }

    // UI

    void ToggleButton(bool state)
    {
        if (useButton != null)
            useButton.SetActive(state);

        if (state)
            currentPickupImage.sprite = currentPickup.spriteUI;

        if (currentPickup is LastingItem)
        {
            pickupTimeBar.gameObject.SetActive(state);
            pickupTimeBar.value = 1;
        }
        else
        {
            if (currentPickup is ReusableItem)
            {
                usesCounterText.gameObject.SetActive(state);
                UpdateCounterText((currentPickup as ReusableItem).usesRemaining);
            }
            pickupTimeBar.gameObject.SetActive(false);
        }
    }

    void UpdateImage(Sprite sprite)
    {
        currentPickupImage.sprite = sprite;
    }

    void UpdateCounterText(int value)
    {
        usesCounterText.text = value.ToString();
    }

    private void OnDestroy()
    {
        DeletePickup();
    }
}
