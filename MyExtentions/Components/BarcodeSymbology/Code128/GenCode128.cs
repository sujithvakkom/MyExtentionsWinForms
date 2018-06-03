using System;
using System.Collections.Generic;
using System.Text;

namespace MyExtentions.Components.BarcodeSymbology.Code128
{
    public enum CodeSet
    {
        CodeA,
        CodeB,
        CodeC   // not supported
    }

    /// <summary>
    /// Represent the set of code values to be output into barcode form
    /// </summary>
    public class Code128Content
    {
        private int[] mCodeList;

        /// <summary>
        /// Create content based on a string of ASCII data
        /// </summary>
        /// <param name="AsciiData">the string that should be represented</param>
        public Code128Content(string AsciiData)
        {
            mCodeList = StringToCode128(AsciiData);
        }

        /// <summary>
        /// Provides the Code128 code values representing the object's string
        /// </summary>
        public int[] Codes
        {
            get
            {
                return mCodeList;
            }
        }

        /// <summary>
        /// Transform the string into integers representing the Code128 codes
        /// necessary to represent it
        /// </summary>
        /// <param name="AsciiData">String to be encoded</param>
        /// <returns>Code128 representation</returns>
        private int[] StringToCode128(string AsciiData)
        {
            byte[] asciiBytes = null;
            System.Collections.ArrayList codes = new System.Collections.ArrayList();
            // turn the string into ascii byte data

            Int64 temp;
            if (Int64.TryParse(AsciiData, out temp))
            {
                codes.Add(105);
                codes.AddRange(CreateAsciiForDigit(AsciiData));
            }
            else
            {
                asciiBytes = Encoding.ASCII.GetBytes(AsciiData);

                // decide which codeset to start with
                Code128Code.CodeSetAllowed csa1 = asciiBytes.Length > 0 ? Code128Code.CodesetAllowedForChar(asciiBytes[0]) : Code128Code.CodeSetAllowed.CodeA;
                Code128Code.CodeSetAllowed csa2 = asciiBytes.Length > 0 ? Code128Code.CodesetAllowedForChar(asciiBytes[1]) : Code128Code.CodeSetAllowed.CodeA;
                CodeSet currcs = GetBestStartSet(csa1, csa2);

                // set up the beginning of the barcode
                codes = new System.Collections.ArrayList(asciiBytes.Length + 3); // assume no codeset changes, account for start, checksum, and stop
                codes.Add(Code128Code.StartCodeForCodeSet(currcs));
                //codes.Add(104);

                // add the codes for each character in the string
                for (int i = 0; i < asciiBytes.Length; i++)
                {
                    int thischar = asciiBytes[i];
                    int nextchar = asciiBytes.Length > (i + 1) ? asciiBytes[i + 1] : -1;

                    codes.AddRange(Code128Code.CodesForChar(thischar, nextchar, ref currcs, ref i));
                }
            }

            // calculate the check digit
            int checksum = (int)(codes[0]);
            for (int i = 1; i < codes.Count; i++)
            {
                checksum += i * (int)(codes[i]);
            }
            codes.Add(checksum % 103);

            codes.Add(Code128Code.StopCode());

            int[] result = codes.ToArray(typeof(int)) as int[];
            return result;
        }

        private int FindCheckSum(System.Collections.ArrayList codes)
        {
            int checksum = (int)(codes[0]);
            for (int i = 1; i < codes.Count; i++)
            {
                checksum += i * (int)(codes[i]);
            }
            return checksum;
        }

        private int[] CreateAsciiForDigit(string AsciiData)
        {
            Int16 val1, val2;
            //if (AsciiData.Length % 2 != 2) AsciiData = "0" + AsciiData;
            System.Collections.ArrayList outData = new System.Collections.ArrayList();
            char[] StringArray = AsciiData.ToCharArray();
            for (int i = 0; i < StringArray.Length; i++)
            {
                Int16.TryParse(StringArray[i++].ToString(), out val1);
                try
                {
                    Int16.TryParse(StringArray[i].ToString(), out val2);
                }
                catch (IndexOutOfRangeException)
                {
                    val2 = 0;
                    CodeSet temp = CodeSet.CodeB;//Code128Code.CodesetForChar(val1);
                    val2 = (short)Code128Code.CodeValueForChar(val1, CodeSet.CodeC);
                    outData.Add((int)Code128Code.ShiftCodeForCodeSet(temp));
                    outData.Add((int)val2); ;
                    return outData.ToArray(typeof(int)) as int[];
                }
                val1 *= 10;
                val1 += val2;
                outData.Add((int)val1);
            }
            return outData.ToArray(typeof(int)) as int[];
        }

        /// <summary>
        /// Determines the best starting code set based on the the first two 
        /// characters of the string to be encoded
        /// </summary>
        /// <param name="csa1">First character of input string</param>
        /// <param name="csa2">Second character of input string</param>
        /// <returns>The codeset determined to be best to start with</returns>
        private CodeSet GetBestStartSet(Code128Code.CodeSetAllowed csa1, Code128Code.CodeSetAllowed csa2)
        {
            int vote = 0;
            vote += this.GetCodeSetWeight(csa1);
            vote += this.GetCodeSetWeight(csa2);
            switch (vote)
            {
                case 8:
                    return CodeSet.CodeC;
                case 2:
                case 5:
                    return CodeSet.CodeA;
                default:
                    return CodeSet.CodeB;
            }
        }

        private int GetCodeSetWeight(Code128Code.CodeSetAllowed code)
        {
            switch (code)
            {
                case Code128Code.CodeSetAllowed.CodeA:
                    return 1;
                case Code128Code.CodeSetAllowed.CodeB:
                    return 2;
                case Code128Code.CodeSetAllowed.CodeC:
                    return 4;
                default:
                    return 2;
            }
        }
    }

    /// <summary>
    /// Static tools for determining codes for individual characters in the content
    /// </summary>
    public static class Code128Code
    {
        #region Constants

        private const int cCodeSetBLB = 32;
        private const int cCodeSetBUB = 127;
        private const int cCodeSetALB1 = 48;
        private const int cCodeSetAUB1 = 57;
        private const int cCodeSetALB2 = 65;
        private const int cCodeSetAUB2 = 90;
        private const int cCodeSetCLB = 47;
        private const int cCodeSetCUB = 57;

        private const int cSHIFT = 98;
        private const int cCODEA = 101;
        private const int cCODEB = 100;
        private const int cCODEC = 99;

        private const int cSTARTA = 103;
        private const int cSTARTB = 104;
        private const int cSTARTC = 105;
        private const int cSTOP = 106;

        #endregion

        /// <summary>
        /// Get the Code128 code value(s) to represent an ASCII character, with 
        /// optional look-ahead for length optimization
        /// </summary>
        /// <param name="CharAscii">The ASCII value of the character to translate</param>
        /// <param name="LookAheadAscii">The next character in sequence (or -1 if none)</param>
        /// <param name="CurrCodeSet">The current codeset, that the returned codes need to follow;
        /// if the returned codes change that, then this value will be changed to reflect it</param>
        /// <returns>An array of integers representing the codes that need to be output to produce the 
        /// given character</returns>
        public static int[] CodesForChar(int CharAscii, int LookAheadAscii, ref CodeSet CurrCodeSet, ref int position)
        {
            int[] result;
            int shifter = -1;

            if (!CharCompatibleWithCodeset(CharAscii, CurrCodeSet))
            {
                // if we have a lookahead character AND if the next character is ALSO not compatible
                if ((LookAheadAscii != -1) && !CharCompatibleWithCodeset(LookAheadAscii, CurrCodeSet))
                {
                    // we need to switch code sets
                    switch (CurrCodeSet)
                    {
                        case CodeSet.CodeA:
                            shifter = cCODEB;
                            CurrCodeSet = CodeSet.CodeB;
                            break;
                        case CodeSet.CodeB:
                            shifter = cCODEA;
                            CurrCodeSet = CodeSet.CodeA;
                            break;
                        //Need to completed
                        case CodeSet.CodeC:
                            break;
                    }
                }
                else
                {
                    // no need to switch code sets, a temporary SHIFT will suffice
                    shifter = cSHIFT;
                }
            }

            if (shifter != -1)
            {
                result = new int[2];
                result[0] = shifter;
                result[1] = CodeValueForChar(CharAscii);
            }
            else
            {
                result = new int[1];
                result[0] = CodeValueForChar(CharAscii);
            }

            return result;
        }

        /// <summary>
        /// Tells us which codesets a given character value is allowed in
        /// </summary>
        /// <param name="CharAscii">ASCII value of character to look at</param>
        /// <returns>Which codeset(s) can be used to represent this character</returns>
        public static CodeSetAllowed CodesetAllowedForChar(int CharAscii)
        {
            /*
             * (47,45) CodeC
             * */
            if (CharAscii >= cCodeSetCLB && CharAscii <= cCodeSetCUB) return CodeSetAllowed.CodeC;
            else if (CharAscii >= cCodeSetALB2 && CharAscii <= cCodeSetAUB2) return CodeSetAllowed.CodeA;
            else return CodeSetAllowed.CodeB;

            if (CharAscii >= 32 && CharAscii <= 95)
            {
                return CodeSetAllowed.CodeAorB;
            }
            else
            {
                return (CharAscii < 32) ? CodeSetAllowed.CodeA : CodeSetAllowed.CodeB;
            }
        }

        /// <summary>
        /// Determine if a character can be represented in a given codeset
        /// </summary>
        /// <param name="CharAscii">character to check for</param>
        /// <param name="currcs">codeset context to test</param>
        /// <returns>true if the codeset contains a representation for the ASCII character</returns>
        public static bool CharCompatibleWithCodeset(int CharAscii, CodeSet currcs)
        {
            //GetBestStartSet
            CodeSet csa = CodesetForChar(CharAscii);

            return (csa == currcs);
            /*
            return csa == CodeSetAllowed.CodeB
                     || (csa == CodeSetAllowed.CodeA && currcs == CodeSet.CodeA)
                     || (csa == CodeSetAllowed.CodeB && currcs == CodeSet.CodeB);
             * */
        }

        public static CodeSet CodesetForChar(int CharAscii)
        {
            if (CharAscii >= cCodeSetCLB && CharAscii <= cCodeSetCUB) return CodeSet.CodeC;
            else if (CharAscii >= cCodeSetALB2 && CharAscii <= cCodeSetAUB2) return CodeSet.CodeA;
            else return CodeSet.CodeB;
        }

        /// <summary>
        /// Gets the integer code128 code value for a character (assuming the appropriate code set)
        /// </summary>
        /// <param name="CharAscii">character to convert</param>
        /// <returns>code128 symbol value for the character</returns>
        public static int CodeValueForChar(int CharAscii)
        {
            return (CharAscii >= 32) ? CharAscii - 32 : CharAscii + 64;
        }
        public static int CodeValueForChar(int CharAscii, CodeSet codeSet)
        {
            if (codeSet == CodeSet.CodeC)
                return CharAscii + 16;
            return 47;
        }

        /// <summary>
        /// Return the appropriate START code depending on the codeset we want to be in
        /// </summary>
        /// <param name="cs">The codeset you want to start in</param>
        /// <returns>The code128 code to start a barcode in that codeset</returns>
        public static int StartCodeForCodeSet(CodeSet cs)
        {
            switch (cs)
            {
                case CodeSet.CodeA:
                    return cSTARTA;
                case CodeSet.CodeB:
                    return cSTARTA;
                case CodeSet.CodeC:
                    return cSTARTA;
                default:
                    return cSTARTA;
            }
        }
        public static int ShiftCodeForCodeSet(CodeSet cs)
        {
            switch (cs)
            {
                case CodeSet.CodeA:
                    return cCODEA;
                case CodeSet.CodeB:
                    return cCODEB;
                case CodeSet.CodeC:
                    return cCODEC;
                default:
                    return cSTARTA;
            }
        }

        /// <summary>
        /// Return the Code128 stop code
        /// </summary>
        /// <returns>the stop code</returns>
        public static int StopCode()
        {
            return cSTOP;
        }

        /// <summary>
        /// Indicates which code sets can represent a character -- CodeA, CodeB, or either
        /// </summary>
        public enum CodeSetAllowed
        {
            CodeA,
            CodeB,
            CodeAorB,
            CodeC
        }

    }
}
