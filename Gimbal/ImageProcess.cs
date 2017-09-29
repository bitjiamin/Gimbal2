using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using HalconDotNet;

namespace Gimbal
{
    class ImageProcess
    {
        BaumerSDK BM = new BaumerSDK();
        HObject ho_Image;
        string basepath = System.IO.Directory.GetCurrentDirectory();
        public double getmax(HObject img)
        {
            // Local iconic variables 

            HObject ho_ROI_0, ho_ImageReduced;
            HObject ho_Region;

            // Local control variables 

            HTuple hv_Pointer = null, hv_Type = null, hv_Width = null;
            HTuple hv_Height = null, hv_Rows = null, hv_Columns = null;
            HTuple hv_Length = null, hv_Grayval = null;
            HTuple hv_Value = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ROI_0);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            ho_ROI_0.Dispose();
            HOperatorSet.GenRectangle1(out ho_ROI_0, 246.084, 179.5, 2163.27, 3091.5);
            ho_ImageReduced.Dispose();
            try
            {
                HOperatorSet.ReduceDomain(img, ho_ROI_0, out ho_ImageReduced);
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 4000, 16383);

                HOperatorSet.GetImagePointer1(ho_ImageReduced, out hv_Pointer, out hv_Type, out hv_Width, out hv_Height);
                HOperatorSet.GetRegionPoints(ho_Region, out hv_Rows, out hv_Columns);
                HOperatorSet.TupleLength(hv_Rows, out hv_Length);
                HOperatorSet.GetGrayval(ho_ImageReduced, hv_Rows, hv_Columns, out hv_Grayval);
                insertion_sort(ref hv_Grayval, hv_Length);
                hv_Grayval = hv_Grayval.TupleSelectRange(hv_Length - 100, hv_Length - 1);
                hv_Value = hv_Grayval.TupleMean();
                ho_ROI_0.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                return hv_Value;
            }
            catch (Exception ex)
            {
                return 0;
            }
          
        }

        public void insertion_sort(ref HTuple arr, int len)
        {
            int i, j;
            int temp;
            for (i = 1; i < len; i++)
            {
                temp = arr[i]; //與已排序的數逐一比較，大於temp時，該數向後移
                j = i - 1;  // 如果将赋值放到下一行的for循环内, 会导致在第10行出现j未声明的错误
                for (; j >= 0 && arr[j] > temp; j--)
                {
                    //j循环到-1时，由于[[短路求值]]，不会运算array[-1]
                    arr[j + 1] = arr[j];
                }
                arr[j + 1] = temp; //被排序数放到正确的位置
            }
        }
        public void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem, HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
            HTuple hv_Row1Part = null, hv_Column1Part = null, hv_Row2Part = null;
            HTuple hv_Column2Part = null, hv_RowWin = null, hv_ColumnWin = null;
            HTuple hv_WidthWin = null, hv_HeightWin = null, hv_MaxAscent = null;
            HTuple hv_MaxDescent = null, hv_MaxWidth = null, hv_MaxHeight = null;
            HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRow = new HTuple();
            HTuple hv_FactorColumn = new HTuple(), hv_UseShadow = null;
            HTuple hv_ShadowColor = null, hv_Exception = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
            HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = new HTuple();
            HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
            HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
            HTuple hv_CurrentColor = new HTuple();
            HTuple hv_Box_COPY_INP_TMP = hv_Box.Clone();
            HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
            HTuple hv_String_COPY_INP_TMP = hv_String.Clone();
            //
            //Prepare window
            HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
            HOperatorSet.GetPart(hv_WindowHandle, out hv_Row1Part, out hv_Column1Part, out hv_Row2Part,
                out hv_Column2Part);
            HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowWin, out hv_ColumnWin,
                out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            //
            //default settings
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
            {
                hv_Color_COPY_INP_TMP = "";
            }
            //
            hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
            //
            //Estimate extentions of text depending on font size.
            HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent,
                out hv_MaxWidth, out hv_MaxHeight);
            if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
            {
                hv_R1 = hv_Row_COPY_INP_TMP.Clone();
                hv_C1 = hv_Column_COPY_INP_TMP.Clone();
            }
            else
            {
                //Transform image to window coordinates
                hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
                hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
                hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
                hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
            }
            //
            //Display text box depending on text size
            hv_UseShadow = 1;
            hv_ShadowColor = "gray";
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleEqual("true"))) != 0)
            {
                if (hv_Box_COPY_INP_TMP == null)
                    hv_Box_COPY_INP_TMP = new HTuple();
                hv_Box_COPY_INP_TMP[0] = "#fce9d4";
                hv_ShadowColor = "#f28d26";
            }
            if ((int)(new HTuple((new HTuple(hv_Box_COPY_INP_TMP.TupleLength())).TupleGreater(
                1))) != 0)
            {
                if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual("true"))) != 0)
                {
                    //Use default ShadowColor set above
                }
                else if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual(
                    "false"))) != 0)
                {
                    hv_UseShadow = 0;
                }
                else
                {
                    hv_ShadowColor = hv_Box_COPY_INP_TMP[1];
                    //Valid color?
                    try
                    {
                        HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                            1));
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        hv_Exception = "Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)";
                        throw new HalconException(hv_Exception);
                    }
                }
            }
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleNotEqual("false"))) != 0)
            {
                //Valid color?
                try
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    hv_Exception = "Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)";
                    throw new HalconException(hv_Exception);
                }
                //Calculate box extents
                hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
                hv_Width = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    HOperatorSet.GetStringExtents(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                        hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                    hv_Width = hv_Width.TupleConcat(hv_W);
                }
                hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    ));
                hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                hv_R2 = hv_R1 + hv_FrameHeight;
                hv_C2 = hv_C1 + hv_FrameWidth;
                //Display rectangles
                HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
                HOperatorSet.SetDraw(hv_WindowHandle, "fill");
                //Set shadow color
                HOperatorSet.SetColor(hv_WindowHandle, hv_ShadowColor);
                if ((int)(hv_UseShadow) != 0)
                {
                    HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1 + 1, hv_C1 + 1, hv_R2 + 1, hv_C2 + 1);
                }
                //Set box color
                HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
            }
            //Write text.
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                    )));
                if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                    "auto")))) != 0)
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
                }
                else
                {
                    HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                }
                hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
                HOperatorSet.SetTposition(hv_WindowHandle, hv_Row_COPY_INP_TMP, hv_C1);
                HOperatorSet.WriteString(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                    hv_Index));
            }
            //Reset changed window settings
            HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
            HOperatorSet.SetPart(hv_WindowHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part,
                hv_Column2Part);

            return;
        }

        public string action_Normal(HTuple hv_window)
        {
            HTuple hv_WidthWin, hv_HeightWin, hv_Number_1 = null;
            HObject ROI1, ho_ImageReduced_1, ho_Region_1, ho_RegionFillUp1, ho_RegionOpening_1, ho_SelectedRegions_1, ho_ConnectedRegions_1;
            //读取图像
            BM.SwTrigger();
            Thread.Sleep(100);
            int k = 0;
            while (true)
            {
                if (k >= 500)
                    break;
                else if (BM.j)
                {
                    ho_Image = BM.ho_image;
                    break;
                }
                k++;
                Thread.Sleep(10);
            }
            //write image
            HOperatorSet.WriteImage(ho_Image, "png", -1, basepath + @"\image\product");
            string time;
            time = DateTime.Now.ToString("yyyyMMddhhmmss");
            HOperatorSet.WriteImage(ho_Image, "png", -1, basepath + @"\image\" + time);
            //read image
            HOperatorSet.ReadImage(out ho_Image, basepath + @"\image\product");
            //Display image
            HOperatorSet.GetImageSize(ho_Image, out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_window, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            HOperatorSet.DispObj(ho_Image, hv_window);
            //读取ROI文件
            HOperatorSet.ReadRegion(out ROI1, basepath + @"\ROI_Normal_Pass.reg");
              //blob分析Pass
            HOperatorSet.ReduceDomain(ho_Image, ROI1, out ho_ImageReduced_1);
            HOperatorSet.Threshold(ho_ImageReduced_1, out ho_Region_1, 50, 255);
            HOperatorSet.FillUp(ho_Region_1, out ho_RegionFillUp1);
            HOperatorSet.OpeningCircle(ho_RegionFillUp1, out ho_RegionOpening_1, 35);
            HOperatorSet.SelectShape(ho_RegionOpening_1, out ho_SelectedRegions_1, "area", "and", 3000, 10000);
            HOperatorSet.Connection(ho_SelectedRegions_1, out ho_ConnectedRegions_1);
            HOperatorSet.CountObj(ho_ConnectedRegions_1, out hv_Number_1);
            //分析并返回结果
                if (hv_Number_1 == 1)
                {
                    disp_message(hv_window, "pass", hv_window, 100, hv_WidthWin - 500, "red", "false");
                    return "1";
                }
                else
                {
                    disp_message(hv_window, "fail", hv_window, 100, hv_WidthWin - 500, "red", "false");
                    return "-1";
                }            
        }

        public string action_Diffuser(HTuple hv_window)
        {
            HTuple hv_WidthWin, hv_HeightWin, hv_Number_1 = null;
            HObject ROI1, ho_ImageReduced_1, ho_Region_1, ho_RegionFillUp1, ho_RegionOpening_1, ho_SelectedRegions_1, ho_ConnectedRegions_1;
            //读取图像
            BM.SwTrigger();
            Thread.Sleep(100);
            int k = 0;
            while (true)
            {
                if (k >= 500)
                    break;
                else if (BM.j)
                {
                    ho_Image = BM.ho_image;
                    break;
                }
                k++;
                Thread.Sleep(10);
            }
            //write image
            HOperatorSet.WriteImage(ho_Image, "png", -1, basepath + @"\image\product");
            string time;
            time = DateTime.Now.ToString("yyyyMMddhhmmss");
            HOperatorSet.WriteImage(ho_Image, "png", -1, basepath + @"\image\" + time);
            //read image
            HOperatorSet.ReadImage(out ho_Image, basepath + @"\image\product");
            //Display image
            HOperatorSet.GetImageSize(ho_Image, out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_window, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            HOperatorSet.DispObj(ho_Image, hv_window);
            //读取ROI文件
            HOperatorSet.ReadRegion(out ROI1, basepath + @"\ROI_Diffuser_Pass.reg");
            //blob分析Pass
            HOperatorSet.ReduceDomain(ho_Image, ROI1, out ho_ImageReduced_1);
            HOperatorSet.Threshold(ho_ImageReduced_1, out ho_Region_1, 50, 255);
            HOperatorSet.FillUp(ho_Region_1, out ho_RegionFillUp1);
            HOperatorSet.OpeningCircle(ho_RegionFillUp1, out ho_RegionOpening_1, 35);
            HOperatorSet.SelectShape(ho_RegionOpening_1, out ho_SelectedRegions_1, "area", "and", 3000, 10000);
            HOperatorSet.Connection(ho_SelectedRegions_1, out ho_ConnectedRegions_1);
            HOperatorSet.CountObj(ho_ConnectedRegions_1, out hv_Number_1);
            //分析并返回结果
            if (hv_Number_1 == 1)
            {
                disp_message(hv_window, "pass", hv_window, 100, hv_WidthWin - 500, "red", "false");
                return "1";
            }
            else
            {
                disp_message(hv_window, "fail", hv_window, 100, hv_WidthWin - 500, "red", "false");
                return "-1";
            }        
        }

        public void gen_roi_pass(HTuple hv_Window)
        {
            HTuple hv_row1, hv_column1, hv_row2, hv_column2, hv_WidthWin, hv_HeightWin;
            HObject ROI;
            //读取并显示产品
            HOperatorSet.ReadImage(out ho_Image, basepath + @"\product");
            HOperatorSet.GetImageSize(ho_Image, out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_Window, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            HOperatorSet.DispObj(ho_Image, hv_Window);
            //选择ROI
            disp_message(hv_Window, "Please Select ROI!", "window", 10, 10, "red", "true");
            HOperatorSet.DrawRectangle1(hv_Window, out hv_row1, out hv_column1, out hv_row2, out hv_column2);
            HOperatorSet.GenRectangle1(out ROI, hv_row1, hv_column1, hv_row2, hv_column2);

            HOperatorSet.WriteRegion(ROI, basepath + @"\ROI_Normal_Pass.reg");
        }

        public void gen_roi_fail(HTuple hv_Window)
        {
            HTuple hv_row1, hv_column1, hv_row2, hv_column2, hv_WidthWin, hv_HeightWin;
            HObject ROI;
            //读取并显示产品
            HOperatorSet.ReadImage(out ho_Image, basepath + @"\product");
            HOperatorSet.GetImageSize(ho_Image, out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_Window, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            HOperatorSet.DispObj(ho_Image, hv_Window);
            //选择ROI
            disp_message(hv_Window, "Please Select ROI!", "window", 10, 10, "red", "true");
            HOperatorSet.DrawRectangle1(hv_Window, out hv_row1, out hv_column1, out hv_row2, out hv_column2);
            HOperatorSet.GenRectangle1(out ROI, hv_row1, hv_column1, hv_row2, hv_column2);

            HOperatorSet.WriteRegion(ROI, basepath + @"\ROI_Normal_Fail.reg");

        }

        public void gen_roi_empty(HTuple hv_Window)
        {
            HTuple hv_row1, hv_column1, hv_row2, hv_column2, hv_WidthWin, hv_HeightWin;
            HObject ROI;
            //读取并显示产品
            HOperatorSet.ReadImage(out ho_Image, basepath + @"\product");
            HOperatorSet.GetImageSize(ho_Image, out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_Window, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            HOperatorSet.DispObj(ho_Image, hv_Window);
            //选择ROI
            disp_message(hv_Window, "Please Select ROI!", "window", 10, 10, "red", "true");
            HOperatorSet.DrawRectangle1(hv_Window, out hv_row1, out hv_column1, out hv_row2, out hv_column2);
            HOperatorSet.GenRectangle1(out ROI, hv_row1, hv_column1, hv_row2, hv_column2);
            HOperatorSet.WriteRegion(ROI, basepath + @"\ROI_Empty.reg");
            disp_message(hv_Window, "Save ROI Success!", "window", 10, 10, "red", "true");
        }

        public void action_calculate_auto_correlation(HObject ho_Image, HTuple hv_Subsampling, out HTuple hv_Sharpness)
        {
            // Local iconic variables 

            HObject ho_ImageZoomed, ho_ImageFFT, ho_ImageCorrelation;
            HObject ho_ImageFFTInv;

            // Local control variables 

            HTuple hv_Scale = null, hv_Width = null, hv_Height = null;
            HTuple hv_SumCorrelation = null, hv_Mean = null, hv_Deviation = null;
            HTuple hv_Bluriness = null;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImageZoomed);
            HOperatorSet.GenEmptyObj(out ho_ImageFFT);
            HOperatorSet.GenEmptyObj(out ho_ImageCorrelation);
            HOperatorSet.GenEmptyObj(out ho_ImageFFTInv);

            try
            {
                hv_Scale = 1.0 / hv_Subsampling;
                ho_ImageZoomed.Dispose();
                HOperatorSet.ZoomImageFactor(ho_Image, out ho_ImageZoomed, hv_Scale, hv_Scale,
                    "constant");
                HOperatorSet.GetImageSize(ho_ImageZoomed, out hv_Width, out hv_Height);
                ho_ImageFFT.Dispose();
                HOperatorSet.RftGeneric(ho_ImageZoomed, out ho_ImageFFT, "to_freq", "none",
                    "complex", hv_Width);
                ho_ImageCorrelation.Dispose();
                HOperatorSet.CorrelationFft(ho_ImageFFT, ho_ImageFFT, out ho_ImageCorrelation
                    );
                ho_ImageFFTInv.Dispose();
                HOperatorSet.RftGeneric(ho_ImageCorrelation, out ho_ImageFFTInv, "from_freq",
                    "n", "real", hv_Width);
                HOperatorSet.GetGrayval(ho_ImageFFTInv, ((((new HTuple(0)).TupleConcat(1)).TupleConcat(
                    hv_Height - 1))).TupleConcat(hv_Height - 2), ((((new HTuple(1)).TupleConcat(
                    0)).TupleConcat(hv_Width - 2))).TupleConcat(hv_Width - 1), out hv_SumCorrelation);
                HOperatorSet.Intensity(ho_ImageZoomed, ho_ImageZoomed, out hv_Mean, out hv_Deviation);
                hv_Bluriness = ((hv_SumCorrelation / (hv_Width * hv_Height)) - (hv_Mean * hv_Mean)) / (hv_Deviation * hv_Deviation);
                hv_Sharpness = (1000.0 - ((hv_Bluriness.TupleMin()) * 1000.0)) / 40.0;
                ho_ImageZoomed.Dispose();
                ho_ImageFFT.Dispose();
                ho_ImageCorrelation.Dispose();
                ho_ImageFFTInv.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_ImageZoomed.Dispose();
                ho_ImageFFT.Dispose();
                ho_ImageCorrelation.Dispose();
                ho_ImageFFTInv.Dispose();

                throw HDevExpDefaultException;
            }
        }
    }
}
