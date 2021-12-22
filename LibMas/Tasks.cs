using System;
using System.Collections.Generic;
using System.Text;

namespace LibMas
{
    public class Tasks
    {

        public static void CreateArrayB(int[,] inputMatrix, out int[] arrayB, out bool j)
        {
            arrayB = new int[6];
            j = false;
            int zeroCounter = 0;
            for (int i = 0; i < 6; i++)
            {
                arrayB[i] = inputMatrix[4, i];
            }
            for (int i = 0; i < arrayB.Length; i++)
            {
                if (arrayB[i] == 0) zeroCounter++;
            }
            if (zeroCounter == arrayB.Length) j = true;
        }
    }
}


