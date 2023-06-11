using System;
using System.Collections.Generic;
using System.Text;
using NITGEN.SDK.NBioBSP;
using NBioBSPCOMLib;
using Microsoft.VisualBasic;

namespace EmatWinFormsNetFramework13032.Digitais.COM
{
    public class Nitgen 
    {
        public NBioBSP objNBioBSP;
        public IDevice objDevice;
        public IExtraction objExtraction;
        public IMatching objMatching;
        public IIndexSearch objIndexSearch;

        public IFPData objFPData;
        public byte[] biFIR;
        public int iIdDedo;

        private const int NBioAPI_DEVICE_ID_AUTO_DETECT = 255;
        private const int NBioAPI_FIR_PURPOSE_VERIFY = 1;

        public long iContDedo;

        public Nitgen()
        {
            objNBioBSP = new NBioBSP();
            objDevice = (NBioBSPCOMLib.IDevice)objNBioBSP.Device;
            objExtraction = (NBioBSPCOMLib.IExtraction)objNBioBSP.Extraction;
            objMatching = (NBioBSPCOMLib.IMatching)objNBioBSP.Matching;
            objIndexSearch = (NBioBSPCOMLib.IIndexSearch)objNBioBSP.IndexSearch;
            objFPData = (NBioBSPCOMLib.IFPData)objNBioBSP.FPData;

        }
    }
}
