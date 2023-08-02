// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// // public class testVr : MonoBehaviour
// // {
// //     // Start is called before the first frame update
// //     void Start()
// //     {
        
// //     }

// //     // Update is called once per frame
// //     void Update()
// //     {
        
// //     }
// // }


// // using UnityEngine.VR;
// using UnityEngine.XR;

 

// public class testVr : MonoBehaviour {

 

//     private bool Once=true;

//     public Transform TrCamera;

//     // Use this for initialization

//     void Start () {

//         InputTracking.Recenter ();

//     }

//     // Update is called once per frame

//     void Update () {
//         if(Once)
//         {
//             if(null == TrCamera)
//             {
//                 return;
//             }
//             if(VRDevice.isPresent)
//             {
//                 transform.eulerAngles = new Vector3 (transform.eulerAngles.x-TrCamera.localEulerAngles.x,transform.eulerAngles.y-TrCamera.localEulerAngles.y,transform.eulerAngles.z-TrCamera.localEulerAngles.z);

//                 Once = false;
//             }
//         }
//     }
// }