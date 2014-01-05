using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MinimizeEnvironmentVariables
{
    class MinimizePath
    {
        static void Main(string[] args)
        {
            const string environmentVariableName = "path";
            String oldPathVal = Environment.GetEnvironmentVariable(environmentVariableName, EnvironmentVariableTarget.Machine);
            String newVal = RemoveUnusedAndDuplicateValues(oldPathVal, ';');
            Environment.SetEnvironmentVariable(environmentVariableName, newVal, EnvironmentVariableTarget.Machine);

            int removedCharacters = oldPathVal.Length - newVal.Length ;
            Console.WriteLine("Path environment variable changed.\n" +
                              "Removed "+ removedCharacters+" characters." );
           }
     
        /// <summary>
        /// Remove duplicates and removes paths that doesn't exist on the system
        /// </summary>
        /// <param name="list"></param>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        public static String RemoveUnusedAndDuplicateValues(String list, char delimeter)
        {
            var newList = new List<String>();
            foreach (var elem in list.Split(delimeter)) // Remove duplicates
            {
                var value = elem.ToLower();
                if (!newList.Contains(value))
                    newList.Add(value);
            }
            
            return newList.Where(Directory.Exists).Aggregate((first, last) => first + delimeter + last); //Return only existing directories
            
       }
    }
}

