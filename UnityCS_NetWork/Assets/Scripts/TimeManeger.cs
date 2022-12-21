using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
public class TimeManeger : MonoBehaviourPunCallbacks
{
    bool start = false;
    [SerializeField]
    TMPro.TextMeshProUGUI TimeText;
    int startTime;
    int startTimeSec=0;
    int timeMin = 3;
    int timesec;
    int latetimesec;

    // Start is called before the first frame update
    void Start()
    {
       
    }
	public override void OnJoinedRoom()
	{
	   startTime = PhotonNetwork.ServerTimestamp;
        start = true;
	}
	// Update is called once per frame
	void Update()
    {
        if (!start) { return; }
        int time =unchecked(PhotonNetwork.ServerTimestamp-startTime);
        timesec = 59-(time/1000)%60;
        
        if (timesec==59&&latetimesec==0)
        {
            timeMin -= 1;
            Debug.Log("timeMin����" + "timesec=" + timesec.ToString()) ;
        }
        TimeText.text = timeMin.ToString("D2")+":"+timesec.ToString("D2");
        latetimesec = timesec;
    }
}
