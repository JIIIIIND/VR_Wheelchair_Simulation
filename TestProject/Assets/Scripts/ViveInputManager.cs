﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveInputManager : MonoBehaviour {

	[SerializeField] private SteamVR_TrackedObject leftTrackedObject;
	[SerializeField] private SteamVR_TrackedObject rightTrackedObject;
	[SerializeField] private PlayerControl playerControl;

	private SteamVR_Controller.Device mDevice;

	[SerializeField] private bool isControllerGrip;

	void Start ()
	{}
	
	void Update () {
		if((int)rightTrackedObject.index != -1)
		{
			mDevice = SteamVR_Controller.Input((int)rightTrackedObject.index);

			//Debug.Log(mDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip));
			if (mDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
			{
				Debug.Log("Grip Down");
				isControllerGrip = true;
				playerControl.PositionInitiate();
				playerControl.SetRightInitPosition(rightTrackedObject.transform.localPosition);
			}
			if (mDevice.GetPress(SteamVR_Controller.ButtonMask.Grip))
			{
				isControllerGrip = true;
				playerControl.CalculateRightPoint(rightTrackedObject.transform.localPosition);
				Debug.Log("Griping");
			}
			if (mDevice.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
			{
				isControllerGrip = false;
				Debug.Log("Grip Up");
			}
		}

		if ((int)leftTrackedObject.index != -1)
		{
			mDevice = SteamVR_Controller.Input((int)leftTrackedObject.index);
			
			if (mDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
			{
				Debug.Log("Grip Down");
				isControllerGrip = true;
				playerControl.PositionInitiate();
				playerControl.SetLeftInitPosition(leftTrackedObject.transform.localPosition);
			}
			if (mDevice.GetPress(SteamVR_Controller.ButtonMask.Grip))
			{
				isControllerGrip = true;
				playerControl.CalculateLeftPoint(leftTrackedObject.transform.localPosition);
				Debug.Log("Griping");
			}
			if (mDevice.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
			{
				isControllerGrip = false;
				Debug.Log("Grip Up");
			}
		}
		if (isControllerGrip == true)
			playerControl.MakeMoveVector();
		else
		{
			if(playerControl.GetGripMovement() != null)
				playerControl.StopCoroutine(playerControl.GetGripMovement());
		}
			
		isControllerGrip = false;
	}

}
