using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haraket : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight; //serializeField sayesinde public olmayanlarý kullanabilriz 
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backLeft;

  

    public float hizlanma = 500f; //public olduðundan oyun kýsmýndan deðiþtirilebilir 
    public float frenGuc = 300f;
    public float maxDonmeAci = 15f;


    private float mevcutHizlama = 0f;
    private float mevcutFrenGuc = 0f;
    private float mevcutDonmeAci = 0f;

    private float hedefHorizontal = 0f; // Hedef deðeri
    private float hedefVertical = 0f;   // Hedef deðeri

    private float Horizontal = 0f; // Mevcut deðer
    private float Vertical = 0f;   // Mevcut deðer

    public float gecisHizi = 5f; // Geçiþ hýzý

    private void FixedUpdate()
    {
        Horizontal = Mathf.Lerp(Horizontal, hedefHorizontal, Time.deltaTime * gecisHizi); //Horizontal ve Vertical deðerlerini yumuþak geçiþ
        Vertical = Mathf.Lerp(Vertical, hedefVertical, Time.deltaTime * gecisHizi);


        //mevcutHizlama = hizlanma * Input.GetAxis("Vertical"); //vertical ile w ve s tuþuna basdýðýmýzda w = 1 s = - 1 deðeri alýr  bu sayede iler ve geri yönde hýzlanma yapabiliriz 
        mevcutHizlama = hizlanma * Vertical; //button kontrol

        //if (Input.GetKey(KeyCode.Space)) //space tuþu ile fren kullanýmý 
        //{
        //    mevcutFrenGuc = frenGuc;
        //}
        //else
        //    mevcutFrenGuc = 0f;

        frontRight.motorTorque = mevcutHizlama;
        frontLeft.motorTorque = mevcutHizlama;


        frontRight.brakeTorque = mevcutFrenGuc;
        frontLeft.brakeTorque = mevcutFrenGuc;
        backRight.brakeTorque = mevcutFrenGuc;
        backLeft.brakeTorque = mevcutFrenGuc;








        

        //------DÖNME-------
        //mevcutDonmeAci = maxDonmeAci * Input.GetAxis("Horizontal"); //a ve s tuslarýný kullanýrken 
        mevcutDonmeAci = maxDonmeAci * Horizontal;//button kontrol 

        frontRight.steerAngle = mevcutDonmeAci; //direksiyon açýsýný sað ön tekerde mevcut açý deðerine eþitle 
        frontLeft.steerAngle = mevcutDonmeAci;  //direksiyon açýsýný sol ön tekerde mevcut açý deðerine eþitle 

        UpdateWheel(frontRight, frontRigthTransform);
        UpdateWheel(backRight, backRigthTransform);
        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(backLeft, backLeftTransform);
    }
    void UpdateWheel(WheelCollider col , Transform trans) 
    {

        //Get
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);//fonksiyona konulan colliderin pozisyonu ve rotasyonunu buradakilere göre aktarma yapar

        //Set
        trans.position = position;
        trans.rotation = rotation;

    }
    public void Sol()
    {
        hedefHorizontal = -1f; 
    }
    public void Sag()
    {
        hedefHorizontal = 1f;
    }
    public void ileri()
    {
        hedefVertical = 1f;
    }
    public void Geri()
    {
        hedefVertical = -1f;
    }
    public void DurHorizontal()
    {
        hedefHorizontal = 0f; 
    }
    public void DurVertical()
    {
        hedefVertical = 0f;
    }
    public void fren()
    {
        mevcutFrenGuc = frenGuc;
    }
    public void durFren()
    {
        mevcutFrenGuc = 0f;
    }

    

}
