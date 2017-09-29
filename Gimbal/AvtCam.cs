using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AVT.VmbAPINET;
using HalconDotNet;
using System.Threading;

namespace Gimbal
{
    public class AvtCam
    {
        Vimba sys = new Vimba();
        CameraCollection cameras = null;
        private Camera camera;
        private bool isCaptureFinished = false;
        private HObject sourceImage = new HObject();
        
        //private API.IniHelper IniHelper = new API.IniHelper();
        private bool isOpen = false;
        private double exposureTimeAbs = 500 * 1000;//500000;
     //   private double gain;
        public delegate void ErrorHandle();
       // public event ErrorHandle OnErrorHandle;

        private bool isOneHardwareTrigger = true;
        public bool IsOneHardwareTrigger
        {
            get { return isOneHardwareTrigger; }
            set { isOneHardwareTrigger = value; }
        }
        public AvtCam()
        {
            sys.Startup();
            //cameras = sys.Cameras;
        }
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

        public void StartContinuousImageAcquisition(int frameCount)
        {
            camera.StartContinuousImageAcquisition(frameCount);
        }

        public bool OneShotNew(out HObject image, int timeOut)
        {
            image = null;
            HOperatorSet.GenEmptyObj(out image);
            image.Dispose();
            try
            {
                isCaptureFinished = false;
                camera.StartContinuousImageAcquisition(1);
                while (!isCaptureFinished)
                {
                    Thread.Sleep(10);
                    Application.DoEvents();
                }
                HOperatorSet.CopyImage(sourceImage, out image);
                sourceImage.Dispose();
                camera.StopContinuousImageAcquisition();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }

        private void camera_OnFrameReceived(Frame frame)
        {
            //if (isOneHardwareTrigger)
            //{
            sourceImage = null;
            HOperatorSet.GenEmptyObj(out sourceImage);
            sourceImage.Dispose();
            GCHandle hObject = GCHandle.Alloc(frame.Buffer, GCHandleType.Pinned);
            IntPtr pObject = hObject.AddrOfPinnedObject();
            HOperatorSet.GenImage1(out sourceImage, "uint2", 3296, 2472, pObject);
            if (hObject.IsAllocated)
                hObject.Free();
            camera.QueueFrame(frame);
            isCaptureFinished = true;
            isOneHardwareTrigger = false;
            //}
            //else
            //{
            //    camera.QueueFrame(frame);
            //}
        }

        public bool Open(double exposureTimeAbs)
        {
            isOpen = false;

            cameras = sys.Cameras;
            try
            {
                camera = cameras[0];
                //camera.Open(VmbAccessModeType.VmbAccessModeFull);
                camera.Features["PixelFormat"].EnumValue = "Mono14";
                camera.Features["ExposureTimeAbs"].FloatValue = exposureTimeAbs;

                //camera.Features["TriggerDelayAbs"].FloatValue = 1000;
                camera.Features["TriggerActivation"].EnumIntValue = 0;
                camera.Features["TriggerMode"].EnumIntValue = 1;
                camera.Features["TriggerSource"].EnumIntValue = 2;
                camera.Features["SyncInSelector"].EnumIntValue = 1;
                camera.Features["SyncInGlitchFilter"].IntValue = 1000;
                camera.Features["SensorDigitizationTaps"].EnumIntValue = (int)SensorDigitizationTapsEnum.One;
                camera.OnFrameReceived += camera_OnFrameReceived;
                camera.StartContinuousImageAcquisition(3);
                isOpen = true;
                isCaptureFinished = true;
            }
            catch
            {
                isOpen = false;
                MessageBox.Show("Can not find the camera!", "Error", MessageBoxButtons.OK);
            }
            return isOpen;
        }
        public bool Open_TriggerPositionZero(double exposureTimeAbs)
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
                camera.Features["SyncInGlitchFilter"].IntValue = 1000;
                camera.Features["SyncInSelector"].EnumIntValue = 1;
                
                camera.Features["SensorDigitizationTaps"].EnumIntValue = (int)SensorDigitizationTapsEnum.One;
                camera.OnFrameReceived += camera_OnFrameReceived;
                camera.StartContinuousImageAcquisition(3);
                isOpen = true;
            }
            catch
            {
                isOpen = false;
                MessageBox.Show("Can not find the camera!", "Error", MessageBoxButtons.OK);
            }
            return isOpen;
        }

        public bool OneShotTrigger(out HObject image, int timeOut)
        {
            int _timeOut = 0;
            image = null;
            HOperatorSet.GenEmptyObj(out image);
            image.Dispose();
            try
            {
                isCaptureFinished = false;
                while (!isCaptureFinished && timeOut>_timeOut)
                {
                    Thread.Sleep(10);
                    _timeOut += 10;
                    Application.DoEvents();
                }
                HOperatorSet.CopyImage(sourceImage, out image);
                sourceImage.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }
     
        public void Close()
        {
            if (isOpen)
            {
                this.camera.Close();
                
            }
            sys.Shutdown();
        }

        public bool OneShot(ref HObject image)
        {
            if (isOpen)
            {
                try
                {
                    Frame frame = null;
                    camera.AcquireSingleImage(ref frame, 6000);

                    GCHandle hObject = GCHandle.Alloc(frame.Buffer, GCHandleType.Pinned);
                    IntPtr pObject = hObject.AddrOfPinnedObject();
                    HOperatorSet.GenImage1(out image, "uint2", frame.Width, frame.Height, pObject);
                    if (hObject.IsAllocated)
                        hObject.Free();
                    return true;
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, "Camera Capture");
                    return false;
                }
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
