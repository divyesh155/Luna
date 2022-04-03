using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private Healthbar healthbar;
    private void Start()
    {
        healthbar.SetSize(19f);
    }
}
