using System;

namespace LuckyTickets
{
    /*
    A function that counts how many n-digit numbers
    have the same sum of the first half and second half of the digits(“lucky” numbers).
    The number n is even. For example, for n = 6, the numbers "001010", "112220", "000000" are lucky.
    */

    /*
    
    It's easy to see that bruteforcing this will NOT be efficient.

    When we know what the sum of the other half is, we can check in how many different ways this sum can be formed with the amount of digits on the other half.

    */


    class LuckyTickets
    {
        static void Main(string[] args)
        {
            //digits in numbers
            int n = 1;            

            //the loop keeps going until user wants to stop by entering '0'
            while (n != 0 ) {
                //amount of lucky numbers 
                int luckies = 0;

                //get the amount of digits
                n = NGetter();

                //form all the possible right sides, as integers
                int[] rightSides = IntToArray(n);

                //go through the numbers on the rightsides array and form their sums.
                //Then check how many different combinations form this sum. 
                //Add the amount of these combinations to the lucky tickets amount.
                
                for (int i = 0; i < rightSides.Length; i++) {
                    //first, the sums:
                    int sum = 0;
                    for (int j = 0; j < ArrayFormer(rightSides[i]).Length; j++) {
                        sum += ArrayFormer(rightSides[i])[j];                       
                    }
                    //check how many different ways there are to form this sum, with n/2 integers
                    luckies += SumCount(n / 2, sum);
                }

                //But in fact: The sums on the left side can be formed on the right side m times, and also exists on the left side m times,
                //since the groups containing the sides are identical!
                // 
                //Therefore, if we just know which sums can be formed by n/2 digits, then check how many different ways there are to form
                //these sums, and square them, then sum the squares, we get our wanted amount of Lucky Tickets.


                Console.WriteLine(luckies);
            }            
        }


        //how many ways to form a sum with n digits
        static int SumCount(int n, int sum)
        {
            // Base case 
            if (n == 0)
                return sum == 0 ? 1 : 0;

            if (sum == 0)
                return 1;

            // Initialize answer 
            int ans = 0;
            // Go through every digit and count numbers beginning with it using recursion 
            for (int i = 0; i <= 9; i++)
                if (sum - i >= 0)
                    ans += SumCount(n - 1, sum - i);
            return ans;
        }       

        //makes an array out of an integer. Index 0 has the most significant number etc.
        static int[] ArrayFormer(int givenInteger) {
            int arraySize = givenInteger.ToString().Length;
            int[] returnArray = new int[arraySize];
            for (int i = 0; i < arraySize; i++) {
                returnArray[arraySize - 1 - i] = givenInteger % 10;
                givenInteger /= 10;
            }
            return returnArray;
        }        

        //turns an integer n into an array of integers. 0 as leftmost digit is included
        //index 0 has the least significant number
        static int[] IntToArray(int n) {
            
            int[] nAsArray = new int[(int)Math.Pow(10.0, (double)(n / 2))];

            //form the first halves of the numbers.
            for (int i = 0; i < (int)Math.Pow(10.0, (double)(n / 2)); i++)
            {
                nAsArray[i] = i;
            }

            return nAsArray;
        }        

        //get the length of the numbers we are interested in
        static int NGetter()
        {
            Console.WriteLine("How many digits do you want your lucky numbers to have? ");
            int n = Convert.ToInt32(Console.ReadLine());
            return n;
        }
    }
}
