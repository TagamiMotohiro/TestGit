using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Target : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject DestroyEffect;
    [SerializeField]
    int HP;
    [SerializeField]
    int hit_Score=100;
    [SerializeField]
    int deferted_Score=1000;
    protected MainGamePUNmaneger maneger;
    bool isTargeted;
    bool sceneUnload = false;
    Behaviour halo;
    // Start is called before the first frame update
    protected void Start()
    {
        halo = (Behaviour)gameObject.GetComponent("Halo");
        maneger = GameObject.Find("PUN2Script").GetComponent<MainGamePUNmaneger>();

    }
	public override void OnJoinedRoom()
    {
         
	}
    // Update is called once per frame
    private void OnDisable()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
         {
                PhotonNetwork.Destroy(this.gameObject);
         }
    }
    void LateUpdate()
    {
        if (this.HP <= 0)
        {
            this.gameObject.SetActive(false);
            
        }
        if (isTargeted)
        {
            if (halo == null) { return; }
            halo.enabled = true;
        }
        else
        {
            halo.enabled = false;
        }
        isTargeted = false;
    }

    public void Targeting()
    {
        isTargeted = true;
    }
	private void OnApplicationQuit()
	{
        sceneUnload = true;
	}
	private void OnDestroy()
	{
        if (sceneUnload) { return; }
        Instantiate(DestroyEffect,transform.position,Quaternion.identity);
	}
	public void OnCollisionEnter(Collision collision)
	{   
        PhotonView Collision_photonView = collision.gameObject.GetComponent<PhotonView>();
        //���������I�u�W�F�N�g��PhotonView���擾
        if (Collision_photonView == null) { return; }//PhtonView�������Ȃ��I�u�W�F�N�g�ɓ��������ꍇ�������Ȃ�
        this.HP--;//���g�̑ϋv�l�����炷
        if (Collision_photonView.IsMine) {//
            if (collision.gameObject.tag == "Player")
            {
                this.HP = 0;
                maneger.PlusScore(-deferted_Score);
                return;
            }
            maneger.PlusScore(hit_Score);
            if (this.HP <= 0)
            {
                maneger.PlusScore(deferted_Score);
                return;
            }
        }
        
	}
}
