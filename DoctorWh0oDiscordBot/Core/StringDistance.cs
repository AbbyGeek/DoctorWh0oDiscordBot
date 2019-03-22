using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorWh0oDiscordBot.Core
{
    class StringDistance
    {
        public static int ComputeDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            //step 1 if s or t is an empty string, string distance equals the length of the other string.
            if (n==0)
            {
                return m;
            }

            if (m==0)
            {
                return n;
            }

            //step 2 ???
            for (int i = 0; i <= n; d[i, 0] = i++)
            { }
            for (int j = 0; j <= m; d[0,j] = j++)
            { }

            //step 3
            for (int i = 1; i <= n; i++)
            {
                //step 4
                for (int j = 1; j <= m; j++)
                {
                    //step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
    }
    
}
