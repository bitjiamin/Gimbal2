using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using BGAPI;
using System.Runtime.InteropServices;

using HalconDotNet;


namespace Gimbal
{
    class BaumerSDK
    {
        private BGAPI.System pSystem;
        private BGAPI.Camera pCamera;
        private BGAPI.Image pImage;
        private BGAPIX_TypeRangeINT pExposure;
        private BGAPIX_TypeRangeFLOAT pGain;
        private BGAPI_FeatureState state;
        private BGAPIX_CameraInfo camdeviceInfo;
        private BGAPIX_TypeListINT pTriggerSourceList;
        private IntPtr imagebuffer;
        //public     BGAPI.BGAPI_NOTIFY_CALLBACK(AddressOf imageCallback) imgcallback;
        private int pResult;
        private Boolean bSaveImg;
        private BGAPIX_TypeListINT pTriggerActivacation;
        private BGAPI_FeatureState pTriggerState;
        private BGAPIX_TypeRangeINT pTriggerDelay;
        private Boolean bSwTrigger;
        private Boolean bFirstFrame;
        public Bitmap pBitmap;
        private Rectangle prcSource;
        private Rectangle prcPBox;
        private Byte[] pImgBits;
        private String pImgFileDir;

       public HObject ho_image;
       public bool j;

        public int imageCallback(object callBackOwner, ref BGAPI.Image image)
        {
          //  j = false;
            int res = BGAPI.Result.OK;
            IntPtr imagebuffer = new IntPtr();
          //  int pixelformat = 0;
            int w = 0;
            int h = 0;
            //byte[]  buffer;
            BaumerSDK actform = (BaumerSDK)callBackOwner;

            image.get(ref imagebuffer);
            image.getSize(ref w, ref h);

            
            HOperatorSet.GenImage1(out ho_image, "byte", w, h, imagebuffer);
            ((BaumerSDK)callBackOwner).pCamera.setImage(ref image);
            j = true;
            return res;
        }

        public void Initialize()
        {
            bSaveImg = false;
            bFirstFrame = true;
            int res;
            pSystem = new BGAPI.System();
            int system_count;
            int sys;
            state = new BGAPI.BGAPI_FeatureState();
            int camera_count;
            pCamera = new BGAPI.Camera();
            int cam;
            camdeviceInfo = new BGAPI.BGAPIX_CameraInfo();
            pTriggerSourceList = new BGAPI.BGAPIX_TypeListINT();
            pTriggerActivacation = new BGAPI.BGAPIX_TypeListINT();
            pExposure = new BGAPIX_TypeRangeINT();
            pGain = new BGAPIX_TypeRangeFLOAT();
            imagebuffer = new IntPtr();
            pTriggerState = new BGAPI_FeatureState();
            pTriggerDelay = new BGAPIX_TypeRangeINT();
            bSwTrigger = false;
     //       prcPBox = new Rectangle(0, 0, pictureBoxA.Width, pictureBoxA.Height);
            pExposure.current = 800000;
            pResult = BGAPI.Result.FAIL;
            res = BGAPI.Result.FAIL;
            system_count = 0;
            sys = 0;
            camera_count = 0;
            cam = 0;
            pImgFileDir = "E:\\";

            res = BGAPI.EntryPoint.countSystems(ref system_count);
       //     if (res != BGAPI.Result.OK)
      //          MessageBox.Show("BGAPI.EntryPoint.CountSystems failed");


            // create system.
            res = BGAPI.EntryPoint.createSystem(sys, ref pSystem);
     //       if (res != BGAPI.Result.OK)
     //           MessageBox.Show("BGAPI.EntryPoint.createSystems failed");

            // open system
            res = pSystem.open();
     //       if (res != BGAPI.Result.OK)
     //           MessageBox.Show("System open failed!");

            res = pSystem.countCameras(ref camera_count);
     //       if (res != BGAPI.Result.OK)
    //            MessageBox.Show("System count cameras failed!");

            // create camera
            res = pSystem.createCamera(cam, ref pCamera);
     //       if (res != BGAPI.Result.OK)
    //            MessageBox.Show("System create camera failed!");

            // get camera device information
            res = pCamera.getDeviceInformation(ref state, ref camdeviceInfo);
    //        if (res != BGAPI.Result.OK)
    //            MessageBox.Show("Camera get Device Information failed!");

    //        String camType;
    //        camType = "Form1--" + camdeviceInfo.modelName;
    //        this.Text = camType;
            res = pCamera.open();
    //        if (res != BGAPI.Result.OK)
    //            MessageBox.Show("Camera open failed!");

            res = BGAPI.EntryPoint.createImage(ref pImage);
    //        if (res != BGAPI.Result.OK)
   //             MessageBox.Show("Create Image failed!");

            res = pCamera.setImage(ref pImage);
    //        if (res != BGAPI.Result.OK)
   //             MessageBox.Show("Camera set Image failed!");

            res = pCamera.registerNotifyCallback(this, imageCallback);
   //         if (res != BGAPI.Result.OK)
   //             MessageBox.Show("Camera register Notify Callback failed!");

            /* 
             res = pCamera.getExposure(ref state, ref pExposure);
              if (res != BGAPI.Result.OK)
                  MessageBox.Show("Camera getExposure failed!");

              trackBarExposure.SetRange(pExposure.minimum, pExposure.maximum);
              trackBarExposure.Value = pExposure.current;
              textBoxExposure.Text = pExposure.current.ToString();
              labelMinExposue.Text = pExposure.minimum.ToString();
              labelMaxEposure.Text = pExposure.maximum.ToString();

              res = pCamera.getGain(ref state, ref pGain);
              if (res != BGAPI.Result.OK)
                  MessageBox.Show("Camera getGain failed!");

              trackBarGain.SetRange((int)pGain.minimum * 100, (int)pGain.maximum * 100);
              trackBarGain.Value = (int)pGain.current * 100;
              textBoxGain.Text = pGain.current.ToString();
              labelMinGain.Text = pGain.minimum.ToString();
              labelMaxGain.Text = pGain.maximum.ToString();

              res = pCamera.getTriggerSource(ref state, ref pTriggerSourceList);
              if (res != BGAPI.Result.OK)
                  MessageBox.Show("Camera get Trigger source failed!");

              int  i;
              int cTrgsource = pTriggerSourceList.current;
              for (i = 0; i < pTriggerSourceList.length; i++)
              {
                  BGAPI_TriggerSource Trg = (BGAPI.BGAPI_TriggerSource)pTriggerSourceList.array[i];
                  BGAPI_TriggerSource curTrgs = (BGAPI.BGAPI_TriggerSource)pTriggerSourceList.array[cTrgsource];
                  switch (Trg)
                  {
                      case BGAPI.BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_ALL:
                          comboBoxTrgSource.Items.Add("BGAPI_TRIGGERSOURCE_ALL");
                          if (curTrgs == BGAPI.BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_ALL)
                              comboBoxTrgSource.Text = "BGAPI_TRIGGERSOURCE_ALL";
                          break;
                      case BGAPI.BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_OFF:
                          comboBoxTrgSource.Items.Add("BGAPI_TRIGGERSOURCE_OFF");
                          if (curTrgs == BGAPI.BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_OFF)
                              comboBoxTrgSource.Text = "BGAPI_TRIGGERSOURCE_OFF";
                          break;
                      case BGAPI.BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_SOFTWARE:
                          comboBoxTrgSource.Items.Add("BGAPI_TRIGGERSOURCE_SOFTWARE");
                          if (curTrgs == BGAPI.BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_SOFTWARE)
                              comboBoxTrgSource.Text = "BGAPI_TRIGGERSOURCE_SOFTWARE";
                          break;
                      case BGAPI.BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_HARDWARE1:
                          comboBoxTrgSource.Items.Add("BGAPI_TRIGGERSOURCE_HARDWARE1");
                          if (curTrgs == BGAPI.BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_HARDWARE1)
                              comboBoxTrgSource.Text = "BGAPI_TRIGGERSOURCE_HARDWARE1";
                          break;
                      case BGAPI.BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_HARDWARE2:
                          comboBoxTrgSource.Items.Add("BGAPI_TRIGGERSOURCE_HARDWARE2");
                          if (curTrgs == BGAPI.BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_HARDWARE2)
                              comboBoxTrgSource.Text = "BGAPI_TRIGGERSOURCE_HARDWARE2";
                          break;
                      case BGAPI.BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_HARDWARE3:
                          comboBoxTrgSource.Items.Add("BGAPI_TRIGGERSOURCE_HARDWARE3");
                          if (curTrgs == BGAPI.BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_HARDWARE3)
                              comboBoxTrgSource.Text = "BGAPI_TRIGGERSOURCE_HARDWARE3";
                          break;
                      case BGAPI.BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_HARDWARE4:
                          comboBoxTrgSource.Items.Add("BGAPI_TRIGGERSOURCE_HARDWARE4");
                          if (curTrgs == BGAPI.BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_HARDWARE4)
                              comboBoxTrgSource.Text = "BGAPI_TRIGGERSOURCE_HARDWARE4";
                          break;
                      case BGAPI.BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_COMMANDTRIGGER:
                          comboBoxTrgSource.Items.Add("BGAPI_TRIGGERSOURCE_COMMANDTRIGGER");
                          if (curTrgs == BGAPI.BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_COMMANDTRIGGER)
                              comboBoxTrgSource.Text = "BGAPI_TRIGGERSOURCE_COMMANDTRIGGER";
                          break;
                      default:
                          break;
                  }
              }
              res = pCamera.getTrigger(ref pTriggerState);
             if (res != BGAPI.Result.OK)
                  MessageBox.Show("Camera get Trigger failed!");
             if(pTriggerState.bIsEnabled)
             {
                 checkBoxTrgEnabled.CheckState = CheckState.Checked;
                 pCamera.setTrigger(true);
             }
              else
             {
                 checkBoxTrgEnabled.CheckState = CheckState.Unchecked;
                 pCamera.setTrigger(false);
             }


             res = pCamera.getTriggerActivation(ref state, ref pTriggerActivacation);
             if (res != BGAPI.Result.OK)
                 MessageBox.Show("Camera get Trigger Activation failed!");
             int cts = pTriggerActivacation.current;
             for (int s = 0; s < pTriggerActivacation.length; s++)
             {
                 BGAPI_Activation Trgav = (BGAPI.BGAPI_Activation)pTriggerActivacation.array[s];
                 BGAPI_Activation curTrgav = (BGAPI.BGAPI_Activation)pTriggerActivacation.array[cts];
                 switch (Trgav)
                 {
                     case BGAPI.BGAPI_Activation.BGAPI_ACTIVATION_STATEHIGH:
                         comboBoxTrgActivation.Items.Add("BGAPI_ACTIVATION_STATEHIGH");
                         if (curTrgav == BGAPI.BGAPI_Activation.BGAPI_ACTIVATION_STATEHIGH)
                             comboBoxTrgActivation.Text = "BGAPI_ACTIVATION_STATEHIGH";
                         break;
                     case BGAPI.BGAPI_Activation.BGAPI_ACTIVATION_STATELOW:
                         comboBoxTrgActivation.Items.Add("BGAPI_ACTIVATION_STATELOW");
                         if (curTrgav == BGAPI.BGAPI_Activation.BGAPI_ACTIVATION_STATELOW)
                             comboBoxTrgActivation.Text = "BGAPI_ACTIVATION_STATELOW";
                         break;
                     case BGAPI.BGAPI_Activation.BGAPI_ACTIVATION_RISINGEDGE:
                         comboBoxTrgActivation.Items.Add("BGAPI_ACTIVATION_RISINGEDGE");
                         if (curTrgav == BGAPI.BGAPI_Activation.BGAPI_ACTIVATION_RISINGEDGE)
                             comboBoxTrgActivation.Text = "BGAPI_ACTIVATION_RISINGEDGE";
                         break;
                     case BGAPI.BGAPI_Activation.BGAPI_ACTIVATION_FALLINGEDGE:
                         comboBoxTrgActivation.Items.Add("BGAPI_ACTIVATION_FALLINGEDGE");
                         if (curTrgav == BGAPI.BGAPI_Activation.BGAPI_ACTIVATION_FALLINGEDGE)
                             comboBoxTrgActivation.Text = "BGAPI_ACTIVATION_FALLINGEDGE";
                         break;
                     case BGAPI.BGAPI_Activation.BGAPI_ACTIVATION_ANYEDGE:
                         comboBoxTrgActivation.Items.Add("BGAPI_ACTIVATION_ANYEDGE");
                         if (curTrgav == BGAPI.BGAPI_Activation.BGAPI_ACTIVATION_ANYEDGE)
                             comboBoxTrgActivation.Text = "BGAPI_ACTIVATION_ANYEDGE";
                         break;
                     default:
                         break;
                 }
             }



             res = pCamera.getTriggerDelay(ref pTriggerState, ref pTriggerDelay);
             if (res != BGAPI.Result.OK)
                 MessageBox.Show("Camera get Trigger Delay failed!");

             numericUpDownDelay.Minimum = pTriggerDelay.minimum;
             numericUpDownDelay.Maximum = pTriggerDelay.maximum;
             numericUpDownDelay.Value = pTriggerDelay.current;
             */
        }

        public void Play()
        {
            pResult = pCamera.setTrigger(true);
            pCamera.setTriggerSource(BGAPI_TriggerSource.BGAPI_TRIGGERSOURCE_SOFTWARE);
            pResult = pCamera.setStart(true);
        }

        public void Pause()
        {
            pResult = pCamera.setStart(false);
        }

        public void SwTrigger()
        {
          //  pResult = pCamera.getTriggerSource(ref state, ref pTriggerSourceList);
          //  int ctrs = pTriggerSourceList.current;
          //  BGAPI_TriggerSource ctx = (BGAPI_TriggerSource)pTriggerSourceList.array[ctrs];
            j = false;
            pResult = pCamera.doTrigger();
        }

        public void Exit()
        {
            pResult = pSystem.releaseImage(ref pImage);
            pResult = pSystem.releaseCamera(ref pCamera);
            pResult = pSystem.release();
        }


    }
}
