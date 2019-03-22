using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorWh0oDiscordBot.Core.Commands
{
    class TypoResponse
    {
        public static string TypoRespond(Dictionary<string, string> SearchDictionary, string enteredName, string queryType )
        {
            List<string> possibleNames = new List<string>();

            foreach (KeyValuePair<string, string> entry in SearchDictionary)
            {
                int distance = StringDistance.ComputeDistance(enteredName, entry.Key);
                if (distance <= 3)
                {
                    possibleNames.Add(entry.Key);
                }
            }
            string response = $"{enteredName} is not a {queryType}.";
            if (possibleNames.Count > 0)
            {
                response += "\n Did you mean ";
                foreach (string x in possibleNames)
                {
                    response += ($"\n {x}?");
                }

            }
            return response;
        }

        
    }
}
