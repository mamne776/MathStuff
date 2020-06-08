using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

/*
A number may not be a palindrome, but its descendant can be. A number's direct child is created by summing 
each pair of adjacent digits to create the digits of the next number.

For instance, 123312 is not a palindrome, but its next child 363 is, where: 3 = 1 + 2; 6 = 3 + 3; 3 = 1 + 2.

In this example, children are formed starting from the least signifigant digit.

Create a function that returns true if the number itself is a palindrome or any of its descendants down to 2 digits (a 1-digit number is trivially a palindrome).

For extra fun, the number is entered as a string.
*/

namespace PalindromeDescendant
{
    class Program
    {
        static void Main(string[] args)
        {            
            //variables
            //the number we are intrested in, AS A STRING!
            string num = "";

            //the main loop
            //the program keeps running until "0" is entered
            while (num != "0" )
            {
                //start by asking for a number
                num = AskForNumber();
                //if given "0" or nothing exit the loop
                if (num == "0" || num == "") break;

                //create an array of integers from the number 
                int[] numAsInt = ChangeNumToIntArray(num);

                //then check if the number is a Palindrome, and inform the user accordingly
                Console.Write("Your number: ");
                for (int i = 0; i < numAsInt.Length; i++)
                {
                    Console.Write(numAsInt[numAsInt.Length - 1 - i].ToString());
                }
                Console.WriteLine();
                if (CheckIfPalindrome(numAsInt)) Console.WriteLine("Your number is a Palindrome!");
                else Console.WriteLine("Your number is not a palindrome :(");

                //next form the numbers child
                int[] numC = FormChild(numAsInt);

                //check if the child is a palindrome
                Console.Write("Your numbers child: ");
                for (int i = 0; i < numC.Length; i++)
                {
                    Console.Write(numC[numC.Length - 1 - i].ToString());
                }
                Console.WriteLine();
                if (CheckIfPalindrome(numC)) Console.WriteLine("Your Numbers child is a palindrome!");
                else Console.WriteLine("Your Numbers child is not a palindrome :(");

                //form the grandchild of the number
                int[] numGC = FormChild(numC);

                Console.Write("Your numbers grandchild: ");
                for (int i = 0; i < numGC.Length; i++)
                {
                    Console.Write(numGC[numGC.Length - 1 - i].ToString());
                }
                Console.WriteLine();
                if (CheckIfPalindrome(numGC)) Console.WriteLine("Your Numbers grandchild is a palindrome!");
                else Console.WriteLine("Your Numbers grandchild is not a palindrome :(");
            }            
        }

        static string AskForNumber() {
            bool stillAsking = true;
            string given = "";

            while (stillAsking)
            {
                Console.WriteLine("Enter your number!\n" +
                    "Number must have an even amount of digits.\nEnd with '0'");
                given = Console.ReadLine();
                if (given == "0") return "0";

                //check if the input is an integer
                bool givenIsNumber = true;
                foreach (char c in given) {
                    int i = (int)c;
                    if ((i > 57) || (i < 48)) givenIsNumber = false;
                }

                //also checks for odd amount of digits
                if ((given.Length % 2 == 1) || (!givenIsNumber))
                {
                    stillAsking = true;
                }                
                else stillAsking = false;
            }
            return given;
        }

        static int[] FormChild(int[] parentArray) {
           
            int l = parentArray.Length;
            
            //cases where given number has odd number of digits
            //replaces the parents "missing" first digit with 0
            if (parentArray.Length % 2 == 1) {
                int[] tempArray = new int[l + 1];
                for (int i = 0; i < l; i++) {
                    tempArray[i] = parentArray[i];
                }
                tempArray[l] = 0;
                parentArray = tempArray;
                l += 1;                
            } 
            
            // Child Array
            int[] cArray = new int[l/2];
            //form values for the child
            for (int i = 0; i < l/2; i++) {
                cArray[i] = parentArray[2*i] + parentArray[2*i + 1];
                //cases where the child digit is greater than 9
                if (cArray[i] > 9) {
                    string s = cArray[i].ToString();
                    int[] littleArray = new int[1];
                    littleArray = ChangeNumToIntArray(s);
                    cArray[i] = FormChild(littleArray)[0];
                }

            }            
            return cArray;
        }

        static bool CheckIfPalindrome(int[] theArray) {
            //turn the array of integers around
            int[] turnedArray = new int[theArray.Length];
            for (int i = 0; i < theArray.Length; i++) {
                turnedArray[i] = theArray[theArray.Length - 1 - i];
            }

            //check if it is palindrome
            bool isPalindrome = true;

            for (int i = 0; i < theArray.Length; i++) {
                if (theArray[i] != turnedArray[i]) isPalindrome = false;
            }            

            if (isPalindrome) return true;
            return false;
        }

        static int[] ChangeNumToIntArray(string s) {
            //first we parse the number to an integer, and get the size of the array we need to create
            int numAsInt = int.Parse(s);
            int arraySize = s.Length;

            //enter the numbers to the array
            int[] intArray = new int[arraySize];

            for (int i = 0; i < arraySize; i++) {
                intArray[i] = numAsInt % 10;
                numAsInt = (int)(numAsInt / 10);
            }

            return intArray;
        }
    }
}
