using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
using Joint = Windows.Kinect.Joint;
using Windows.Kinect;
using System;

public class BodySourceView : MonoBehaviour
{
    public BodySourceManager mBodySourceManager;
    public GameObject JointObject;
    private Dictionary<ulong, GameObject> mBodies = new Dictionary<ulong, GameObject>();
    
    private List<JointType> _joints = new List<JointType> { JointType.HandLeft, JointType.HandRight, JointType.Head,JointType.AnkleLeft,JointType.AnkleRight}; //Add Hands
    public mJointColor jJointColorHandLeft;
    public mJointColor jJointColorHandRight;
    public mJointColor jJointColorAnkleLeft;
    public mJointColor jJointColorAnkleRight;
    public mJointColor jJointColorHead;


    void Update()
    {
        #region GetKinectData
        Body[] data = mBodySourceManager.GetData();
        if (data == null) return;

        List<ulong> trackedIDs = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null) continue;
            if (body.IsTracked) trackedIDs.Add(body.TrackingId);
        }
        #endregion

        #region DeleteKinectBodies
        List<ulong> knownIds = new List<ulong>(mBodies.Keys);
        foreach(ulong trackingID in knownIds)
        {
            if(!trackedIDs.Contains(trackingID))
            {

                //Destroy body object
                Destroy(mBodies[trackingID]);

                //Remove from List
                mBodies.Remove(trackingID);
            }
        }
        #endregion

        #region CreateKinectBodies
        foreach(var body in data)
        {
            //If no body, skip
            if (body == null) continue;
            if (body.IsTracked)
            {
                //If body isnt tracked, create one
                if(!mBodies.ContainsKey(body.TrackingId))
                {
                    mBodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }

                //Update positions
                UpdateBodyObjects(body, mBodies[body.TrackingId]);
            }
        }
        #endregion
    }
    private GameObject CreateBodyObject(ulong id)
    {
        //Create body parent
        GameObject body = new GameObject("Body:" + id);


        //Create joints
        foreach (JointType joint in _joints)
        {
            //TO DO CHANGE JointObject values via -> JointObject.GetComponent<PR_Joint>().jointType=;
            GameObject newJoint = null;
            switch(joint.ToString())
            {
                case "HandLeft":
                    JointObject.GetComponent<PR_Joint>().jointType = mJointType.HandLeft;
                    JointObject.GetComponent<PR_Joint>().jointColor = jJointColorHandLeft;
                    newJoint = Instantiate(JointObject);
                    break;
                case "HandRight":
                    JointObject.GetComponent<PR_Joint>().jointType = mJointType.HandRight;
                    JointObject.GetComponent<PR_Joint>().jointColor = jJointColorHandRight;
                    newJoint = Instantiate(JointObject);
                    break;
                case "Head":
                    JointObject.GetComponent<PR_Joint>().jointType = mJointType.Head;
                    JointObject.GetComponent<PR_Joint>().jointColor = jJointColorHead;
                    newJoint = Instantiate(JointObject);
                    break;
                case "AnkleRight":
                    JointObject.GetComponent<PR_Joint>().jointType = mJointType.AnkleRight;
                    JointObject.GetComponent<PR_Joint>().jointColor = jJointColorAnkleRight;
                    newJoint = Instantiate(JointObject);
                    break;
                case "AnkleLeft":
                    JointObject.GetComponent<PR_Joint>().jointType = mJointType.AnkleRight;
                    JointObject.GetComponent<PR_Joint>().jointColor = jJointColorAnkleLeft;
                    newJoint = Instantiate(JointObject);
                    break;

            }
            
            newJoint.name = joint.ToString();

            //Parent to body
            newJoint.transform.parent = body.transform;
        }
        return body;
    }
    private void UpdateBodyObjects(Body body, GameObject bodyobject)
    {
        foreach(JointType _joint in _joints)
        {
            //Get new target position
            Joint sourceJoint = body.Joints[_joint];
            Vector3 targetPosition = GetVector3FromJoints(sourceJoint);
            targetPosition.z = 0;

            // Get joint and set new position
            Transform jointObject = bodyobject.transform.Find(_joint.ToString());
            jointObject.position = targetPosition;
        }
    }
    private Vector3 GetVector3FromJoints(Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }

}