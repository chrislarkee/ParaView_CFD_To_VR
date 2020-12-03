using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonClick : MonoBehaviour, IPointerEnterHandler {

	public void OnPointerEnter(PointerEventData eventData){
		Debug.Log ("Please Work");
	}

}
