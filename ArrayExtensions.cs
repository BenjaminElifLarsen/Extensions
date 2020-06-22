using System;

namespace Extensions
{
    /// <summary>
    /// Contains custom extensions for arrays.
    /// <list type="bulet">
    /// <item>
    /// <term>+Display(arrayToDisplay:T[],seperactor:string):void</term>
    /// <description>Displays all entires in arrayToDisplay and uses seperactor if signs need to be place between them.</description>
    /// </item>
    /// <item>
    /// <term>+Selection(arrayToExtractForm:T[], startLocation:uint, stopLocation:uint):T[]</term>
    /// <description>Returns array of T consisting of all entries from startLocation to and with stopLocation.</description>
    /// </item>
    /// <item>
    /// <term>+Concat(orginalArray:T[], arraysToAdd:params T[][]):T[]</term>
    /// <description>Concats multiple arrays of same type together to a single array and return it.</description>
    /// </item>
    /// </list>
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Displays all the content of an array. The variable <paramref name="seperactor"/> can be used to add a string between each value of <paramref name="arrayToDisplay"/>.
        /// </summary>
        /// <typeparam name="T">Can be any datatype of array.</typeparam>
        /// <param name="arrayToDisplay">The array to display.</param>
        /// <param name="seperactor">Seperate the values of <paramref name="arrayToDisplay"/>.</param>
        public static void Display<T>(this T[] arrayToDisplay, string seperactor = "")
        {
            try
            {
                foreach (T dis in arrayToDisplay)
                {
                    Console.Write($"{dis}{seperactor}");
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e + "\n");
            }

        }

        /// <summary>
        /// Returns array consisting of a specific part of <paramref name="arrayToExtractFrom"/>. 
        /// The first index value is <paramref name="startLocation"/> and the last index value is <paramref name="stopLocation"/>.
        /// The length of the new array is <c>1 + <paramref name="stopLocation"/> - <paramref name="startLocation"/></c>.
        /// E.g. if <c><paramref name="startLocation"/> = 0</c> and <c><paramref name="stopLocation"/> = 3</c>, the return array's length will be 4.
        /// If an exception is caught, the method will return null. 
        /// If <paramref name="startLocation"/> equal <paramref name="stopLocation"/> the method will return a null.
        /// </summary>
        /// <typeparam name="T">Data type of the array.</typeparam>
        /// <param name="arrayToExtractFrom">The array that should be trimmed.</param>
        /// <param name="startLocation">The start index.</param>
        /// <param name="stopLocation">The stop index.</param>
        /// <returns>Returns an array consisting of values from <paramref name="arrayToExtractFrom"/> going from <paramref name="startLocation"/> up to <paramref name="stopLocation"/>. 
        /// It will return null if an expection is caught. If <paramref name="startLocation"/> equal <paramref name="stopLocation"/> it will return a null array.</returns>
        public static T[] Selection<T>(this T[] arrayToExtractFrom, uint startLocation, uint stopLocation)
        {
            T[] trimmedArray;
            try
            {
                if (startLocation != stopLocation)
                {
                    trimmedArray = new T[1 + stopLocation - startLocation];
                    for (uint i = startLocation; i <= stopLocation; i++)
                    {
                        trimmedArray[i - startLocation] = arrayToExtractFrom[i];
                    }
                }
                else
                    trimmedArray = null;
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e + "\n");
                trimmedArray = null;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e + "\nRemember that stopLocation is also used.");
                trimmedArray = null;
            }
            return trimmedArray;
        }

        /// <summary>
        /// Concats mutliple arrays together.  
        /// </summary>
        /// <typeparam name="T">Can be any datatype of array.</typeparam>
        /// <param name="originalArray">The first array to concat.</param>
        /// <param name="arraysToAdd">The remaining arrays to concats.</param>
        /// <returns></returns>
        public static T[] Concat<T>(this T[] originalArray, params T[][] arraysToAdd)
        {
            T[] concatArray; 
            int newLength = originalArray.Length;
            foreach (T[] array in arraysToAdd)
            {
                newLength += array.Length;
            }
            concatArray = new T[newLength];
            int location = originalArray.Length;
            Array.Copy(originalArray, 0, concatArray, 0, originalArray.Length);
            foreach (T[] arrayToAdd in arraysToAdd)
            {
                Array.Copy(arrayToAdd, 0, concatArray, location, arrayToAdd.Length);
                location += arrayToAdd.Length;
            }
            return concatArray;
        }
    }

    /// <summary>
    /// Contain custom extensions for strings.
    /// <list type="bulet">
    /// <item>
    /// <term>+AddChar(str:string, chr:char):string</term>
    /// <description>Adds a char to the end of a string and returns the string.</description>
    /// </item>
    /// </list>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Adds a char to the end of a string.
        /// </summary>
        /// <param name="str">The string a char to be addded to.</param>
        /// <param name="chr">The char to be added.</param>
        /// <returns></returns>
        public static string AddChar(this string str, char chr)
        {
            return $"{str}{chr.ToString()}";
        }

        /// <summary>
        /// Work in progress
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUpperFirst(this string str)
        {//make a version that can find the first letter instead of just going for the first sign.
            string[] splitted = str.SplitAtLocation(0);
            string upperString = splitted[0].ToUpper() + splitted[1];
            return upperString;
        }

        /// <summary>
        /// Splits a string into two strings based upon <paramref name="posistion"/>. 
        /// All entires up to and with <paramref name="posistion"/> will become the first string.
        /// All other entires will become the second string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="posistion"></param>
        /// <returns></returns>
        public static string[] SplitAtLocation (this string str, uint posistion)
        { //ToString does not work the way you tought, make your own char[] to string function
            uint location = 0;
            //Console.WriteLine("String length " + str.Length);
            char[] firstpart = new char[posistion+1];
            char[] lastpart = new char[str.Length-posistion-1];
            //Console.WriteLine("Posistion" + posistion + " firstPart" + firstpart.Length + "lastpart" + lastpart.Length);
            foreach (char chr in str)
            {
                if (location <= posistion)
                    firstpart[location] = chr;
                else
                    lastpart[location-posistion-1] = chr;
                location++;
                //Console.WriteLine(location);
            }
            string[] splitted =
                {
                    new string(firstpart),
                    new string(lastpart)
                };
            //Console.WriteLine(splitted[0] + splitted[1]);
            //Console.WriteLine(new string(lastpart));
            return splitted;
        }
    }
}
