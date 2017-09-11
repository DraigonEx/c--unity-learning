//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CameraMotion : MonoBehaviour {

//	// Use this for initialization
//	void Start () {
//        oldPosition = this.transform.position;
//	}

//    Vector3 oldPosition;
	
//	// Update is called once per frame
//	void Update () {

//        // Code to click and drag camera
//        // WASD, Zoom in and out

//        CheckIfCameraMoved();
//	}

//    public void PanToHex ( Hex hex )
//    {
//        //Move Camera to Hex
//    }

//    void CheckIfCameraMoved()
//    {
//        if(oldPosition != this.transform.position)
//        {
//            // Something moved the camera
//            oldPosition = this.transform.position;

//            //// Below is for moving hexes relative to the camera
//            //
//            //HexComponent[] hexes = GameObject.FindObjectsOfType<HexComponent>();

//            //foreach(HexComponent hex in hexes)
//            //{
//            //    hex.UpdatePosition();
//            //}
//        }
//    }
//}
