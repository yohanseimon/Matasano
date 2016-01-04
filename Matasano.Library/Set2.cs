using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Matasano.Utilities;

namespace Matasano.Library
{
    public class Set2
    {
        EcbUtilities _ecbUtilities;
        StringUtilities _stringUtilities;
        HexadecimalUtilities _hexadecimalUtilities;

        public Set2()
        {
            _ecbUtilities = new EcbUtilities();
            _stringUtilities = new StringUtilities();
            _hexadecimalUtilities = new HexadecimalUtilities();
        }

        /*
         * Implement PKCS#7 padding
         * A block cipher transforms a fixed-sized block (usually 8 or 16 bytes) of plaintext into ciphertext. But we almost never want to transform a single block; we encrypt irregularly-sized messages.
         * 
         * One way we account for irregularly-sized messages is by padding, creating a plaintext that is an even multiple of the blocksize. The most popular padding scheme is called PKCS#7.
         * 
         * So: pad any block to a specific block length, by appending the number of bytes of padding to the end of the block. For instance,
         * "YELLOW SUBMARINE" padded to 20 bytes would be:"YELLOW SUBMARINE\x04\x04\x04\x04"
         * 
         */
        public string PKCS7Padding(string blockString, int blockSize)
        {
            int padSize = blockSize - (blockString.Length % blockSize);
            string padString = _hexadecimalUtilities.ByteToHexadecimalString((byte)padSize);

            for (int i = 0; i < padSize; i++)
                blockString = String.Concat(blockString, padString);

            return blockString;
        }
    }
}