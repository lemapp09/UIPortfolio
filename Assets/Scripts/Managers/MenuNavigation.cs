using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuNavigation : MonoBehaviour
{
   [SerializeField] private GameObject _defaultSelectedObjet;

   private void Start()
   {
      EventSystem.current.SetSelectedGameObject(_defaultSelectedObjet);
   }

   private void Update()
   {
      if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame &&
          EventSystem.current.currentSelectedGameObject == null)
      {
         EventSystem.current.SetSelectedGameObject(_defaultSelectedObjet);
      }
   }
}
