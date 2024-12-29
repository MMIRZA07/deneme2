using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haraket : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight; //serializeField sayesinde public olmayanlar� kullanabilriz 
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backLeft;

    [SerializeField] Transform frontRigthTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform backRigthTransform;
    [SerializeField] Transform backLeftTransform;

    public float hizlanma = 500f; //public oldu�undan oyun k�sm�ndan de�i�tirilebilir 
    public float frenGuc = 300f;
    public float maxDonmeAci = 15f;


    private float mevcutHizlama = 0f;
    private float mevcutFrenGuc = 0f;
    private float mevcutDonmeAci = 0f;

    private float hedefHorizontal = 0f; // Hedef de�eri
    private float hedefVertical = 0f;   // Hedef de�eri

    private float Horizontal = 0f; // Mevcut de�er
    private float Vertical = 0f;   // Mevcut de�er

    public float gecisHizi = 5f; // Ge�i� h�z�

    private void FixedUpdate()
    {
        Horizontal = Mathf.Lerp(Horizontal, hedefHorizontal, Time.deltaTime * gecisHizi); //Horizontal ve Vertical de�erlerini yumu�ak ge�i�
        Vertical = Mathf.Lerp(Vertical, hedefVertical, Time.deltaTime * gecisHizi);


        //mevcutHizlama = hizlanma * Input.GetAxis("Vertical"); //vertical ile w ve s tu�una basd���m�zda w = 1 s = - 1 de�eri al�r  bu sayede iler ve geri y�nde h�zlanma yapabiliriz 
        mevcutHizlama = hizlanma * Vertical; //button kontrol

        //if (Input.GetKey(KeyCode.Space)) //space tu�u ile fren kullan�m� 
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

        //------D�NME-------
        //mevcutDonmeAci = maxDonmeAci * Input.GetAxis("Horizontal"); //a ve s tuslar�n� kullan�rken 
        mevcutDonmeAci = maxDonmeAci * Horizontal;//button kontrol 

        frontRight.steerAngle = mevcutDonmeAci; //direksiyon a��s�n� sa� �n tekerde mevcut a�� de�erine e�itle 
        frontLeft.steerAngle = mevcutDonmeAci;  //direksiyon a��s�n� sol �n tekerde mevcut a�� de�erine e�itle 

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
        col.GetWorldPose(out position, out rotation);//fonksiyona konulan colliderin pozisyonu ve rotasyonunu buradakilere g�re aktarma yapar

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
