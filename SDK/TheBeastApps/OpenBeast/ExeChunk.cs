using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VolmaxLauncherLibrary
{
    public class ExeChunk
    {
        public string productVersion
        {
            get;
            set;
        }

        public string NoOfChunks
        {
            get;
            set;
        }

        public string chunkName
        {
            get;
            set;
        }

        public byte[] chunkData
        {
            get;
            set;
        }

        public ExeChunk()
        {

        }

        public ExeChunk(string strProductVersion, string strNoOfChunks, string strChunkName, byte[] bytChunkData)
        {
            this.productVersion = strProductVersion;
            this.NoOfChunks = strNoOfChunks;
            this.chunkName = strChunkName;
            this.chunkData = bytChunkData;
        }
    }
}
