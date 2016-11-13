using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AVT.VmbAPINET;
using HalconDotNet;

namespace Gimbal
{
    public class AvtCam
    {
        
        private Camera camera;
        //private API.IniHelper IniHelper = new API.IniHelper();
        private bool isOpen = false;
        private double exposureTimeAbs=500000;
     //   private double gain;
        public delegate void ErrorHandle();
       // public event ErrorHandle OnErrorHandle;
        public void LoadCameraSettings(string path)
        {
            this.camera.LoadCameraSettings(path);
        }
        public void SaveCameraSettings(string path)
        {
            this.camera.SaveCameraSettings(path);
        }

        public enum SensorDigitizationTapsEnum
        {
            One = 0,
            Two = 1,
            Four = 3
        }
        public SensorDigitizationTapsEnum SensorDigitizationTaps
        {
            get { return (SensorDigitizationTapsEnum)SensorDigitizationTapsFeature.EnumIntValue; }
            set { SensorDigitizationTapsFeature.EnumIntValue = (int)value; }
        }
        public AVT.VmbAPINET.Feature SensorDigitizationTapsFeature
        {
            get
            {
                if (m_SensorDigitizationTapsFeature == null)
                    m_SensorDigitizationTapsFeature = camera.Features["SensorDigitizationTaps"];
                return m_SensorDigitizationTapsFeature;
            }
        }
        private AVT.VmbAPINET.Feature m_SensorDigitizationTapsFeature = null;

        public bool Open(double exposureTimeAbs)
        {
            isOpen = false;
            Vimba sys = new Vimba();
            CameraCollection cameras = null;

            sys.Startup();
            cameras = sys.Cameras;
            try
            {
                camera = cameras[0];
                camera.Open(VmbAccessModeType.VmbAccessModeFull);
                camera.Features["PixelFormat"].EnumValue = "Mono14";
                camera.Features["ExposureTimeAbs"].FloatValue = exposureTimeAbs;
                camera.Features["TriggerActivation"].EnumIntValue = 0;
                camera.Features["TriggerMode"].EnumIntValue = 1;
                camera.Features["TriggerSource"].EnumIntValue = 2;
                camera.Features["SyncInGlitchFilter"].IntValue = 500;
                camera.Features ["SyncInSelector"].EnumIntValue = 1;
                
                //camera.OnFrameReceived+=camera_OnFrameReceived;

                //camera.StartContinuousImageAcquisition(1);
                camera.Features["SensorDigitizationTaps"].EnumIntValue = (int)SensorDigitizationTapsEnum.One;
                isOpen = true;
            }
            catch
            {
                isOpen = false;
                MessageBox.Show("Can not find the camera!", "Error", MessageBoxButtons.OK);
            }
            return isOpen;
        }
        public bool Open_NoTrigger()
        {
            isOpen = false;
            Vimba sys = new Vimba();
            CameraCollection cameras = null;

            sys.Startup();
            cameras = sys.Cameras;
            try
            {
                camera = cameras[0];
                camera.Open(VmbAccessModeType.VmbAccessModeFull);
                camera.Features["PixelFormat"].EnumValue = "Mono14";
                camera.Features["ExposureTimeAbs"].FloatValue = exposureTimeAbs;
                camera.Features["TriggerActivation"].EnumIntValue = 0;
                camera.Features["TriggerMode"].EnumIntValue = 0;
                camera.Features["TriggerSource"].EnumIntValue = 2;
                camera.Features["SyncInGlitchFilter"].IntValue = 500;
                camera.Features["SyncInSelector"].EnumIntValue = 1;

                //camera.OnFrameReceived+=camera_OnFrameReceived;

                //camera.StartContinuousImageAcquisition(1);
                camera.Features["SensorDigitizationTaps"].EnumIntValue = (int)SensorDigitizationTapsEnum.One;
                isOpen = true;
            }
            catch
            {
                isOpen = false;
                MessageBox.Show("Can not find the camera!", "Error", MessageBoxButtons.OK);
            }
            return isOpen;
        }
        void camera_OnFrameReceived(Frame frame)

        {
            HObject image;
            GCHandle hObject = GCHandle.Alloc(frame.Buffer, GCHandleType.Pinned);
            IntPtr pObject = hObject.AddrOfPinnedObject();
            HOperatorSet.GenImage1(out image, "uint2", frame.Width, frame.Height, pObject);
            if (hObject.IsAllocated)
                hObject.Free();
            //camera.OnFrameReceived += camera_OnFrameReceived;
            //camera.StopContinuousImageAcquisition();
            //camera.StartContinuousImageAcquisition(1);
            Close();
        }
        public void Close()
        {
            if (isOpen)
                this.camera.Close();
        }

        public bool OneShot(ref HObject image)
        {
            if (isOpen)
            {
                Frame frame = null;
                camera.AcquireSingleImage(ref frame, 6000);
               // camera.AcquireSingleImage(ref frame, 6000);
               // camera.AcquireSingleImage(ref frame, 6000);
                GCHandle hObject = GCHandle.Alloc(frame.Buffer, GCHandleType.Pinned);
                IntPtr pObject = hObject.AddrOfPinnedObject();
                HOperatorSet.GenImage1(out image, "uint2", frame.Width, frame.Height, pObject);
                if (hObject.IsAllocated)
                    hObject.Free();
                return true;
            }
            else
                return false;
        }

        public ushort[,] OneShot2(string filename)
        {
            if (isOpen)
            {
                Frame frame = null;
             //   camera.Features["ExposureTimeAbs"].FloatValue = exposureTimeAbs;
                camera.AcquireSingleImage(ref frame, 2000);
                ProcessTif tif = new ProcessTif();
                byte[][] data = new byte[tif.height][];
                ushort[,] rawdata = new ushort[tif.height, tif.width];
                ushort[] temp = new ushort[tif.width];
                for (int i = 0; i < tif.height; i++)
                {
                    data[i] = new byte[tif.width * 2];
                    for (int j = 0; j < tif.width * 2; j++)
                    {
                        data[i][j] = frame.Buffer[tif.width * 2 * i + j];
                    }
                    Buffer.BlockCopy(data[i], 0, temp, 0, data[i].Length);
                    for (int k = 0; k < tif.width; k++)
                    {
                        rawdata[i, k] = temp[k];
                    }
                }
                tif.ByteToTiff(data, filename);
                return rawdata;
            }
            else
                return null;
        }

        public void GetCameraList(Vimba m_Vimba, ref List<string> cameraIdList)
        {
            foreach (Camera _Camera in m_Vimba.Cameras)
            {
                cameraIdList.Add(_Camera.Id);
            }
        }

        public string GetCameraID()
        {
            string camerraid;
            Vimba sys = new Vimba();
            CameraCollection cameras = null;

            sys.Startup();
            cameras = sys.Cameras;

            try
            {
                camerraid = cameras[0].Id;
                return camerraid;
            }

            catch (Exception ve)
            {
                camerraid = ve.Message;
                return "Invalid";
            }  
        }

        public double ExposureTimeAbs
        {
            get { return exposureTimeAbs; }
            set { exposureTimeAbs = value; }
        }
        public AVT.VmbAPINET.Feature ExposureTimeAbsFeature
        {
            get
            {
                if (m_ExposureTimeAbsFeature == null)
                    m_ExposureTimeAbsFeature = this.camera.Features["ExposureTimeAbs"];
                return m_ExposureTimeAbsFeature;
            }
        }
        private AVT.VmbAPINET.Feature m_ExposureTimeAbsFeature = null;
        public bool IsOpen
        {
            get { return isOpen; }
        }
    }
}
