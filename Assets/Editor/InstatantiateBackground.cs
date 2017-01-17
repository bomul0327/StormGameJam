using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InstatantiateBackground : Editor {

	[MenuItem("Menu/InstantiateTile")]
	static public void InstantiateTile(){
		var activeObject = Selection.activeGameObject;
		var activeObjectSpriteRenderer = activeObject.GetComponent<SpriteRenderer>();
		float size = activeObjectSpriteRenderer.bounds.size.x;

		for(int i = 0; i < 9; ++i){
			for(int j = 0; j < 16; ++j){
				Instantiate(activeObject, new Vector3(j * size, i * size, 0), Quaternion.identity);
			}
		}
	}

	[MenuItem("Menu/GettingSizeWithSpriteRenderer")]
	static public void GettingSizeWithSpriteRenderer(){
		var activeObject = Selection.activeGameObject;
		Debug.Log(activeObject.name + "'s size is " + activeObject.GetComponent<SpriteRenderer>().bounds.size);
	}

	[MenuItem("Menu/InstantiateTile1LineHor")]
	static public void InstantiateTile1LineHor(){
		var activeObject = Selection.activeGameObject;
		var activeObjectSpriteRenderer = activeObject.GetComponent<SpriteRenderer>();
		float size = activeObjectSpriteRenderer.bounds.size.x;

		for(int i = 0; i < 16; ++i){
			Instantiate(activeObject, new Vector3(i * size, 0, 0), Quaternion.identity);
		}
	}

	[MenuItem("Menu/InstantiateTile1LineVer")]
	static public void InstantiateTile1LineVer(){
		var activeObject = Selection.activeGameObject;
		var activeObjectSpriteRenderer = activeObject.GetComponent<SpriteRenderer>();
		float size = activeObjectSpriteRenderer.bounds.size.x;

		for(int i = 0; i < 9; ++i){
			Instantiate(activeObject, new Vector3(0, i * size, 0), Quaternion.identity);
		}
	}
}
