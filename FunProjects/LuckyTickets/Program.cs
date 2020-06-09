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
                n = nGetter();

                //form all the possible right sides, as integers
                int[] rightSides = RightSideFormer(n);

                //go through the numbers on the rightsides array and form their sums.
                //Then check how many different combinations form this sum. 
                //Add the amount of these combinations to the lucky tickets amount.
                
                for (int i = 0; i < rightSides.Length; i++) {
                    //first, the sums:
                    int sum = 0;
                    for (int j = 0; j < ArrayFormer(rightSides[i]).Length; j++) {
                        sum += ArrayFormer(rightSides[i])[j];                       
                    }

                    //Now, how many ways are there to form this sum from n/2 digits?
                    //luckies += CheckAmountOfCombinations(sum, n/2);
                    luckies += finalCount(n / 2, sum);
                }
                Console.WriteLine(luckies);
            }            
        }

        //How many ways are there to form this sum from m digits
        static int CheckAmountOfCombinations(int sum, int m) {

            return 0;
        }

        //try this:
        static int countRec(int n, int sum)
        {
            // Base case 
            if (n == 0)
                return sum == 0 ? 1 : 0;

            if (sum == 0)
                return 1;

            // Initialize answer 
            int ans = 0;
            // Traverse through every digit and count numbers beginning with it using recursion 
            for (int i = 0; i <= 9; i++)
                if (sum - i >= 0)
                    ans += countRec(n - 1, sum - i);
            return ans;
        }

        // This is mainly a wrapper over countRec. It explicitly handles leading digit and calls countRec() for remaining digits. 
        static int finalCount(int n, int sum)
        {

            // Initialize final answer 
            int ans = 0;

            // Traverse through every digit from 0 to 9 and count numbers beginning with it 
            for (int i = 0; i <= 9; i++)
                if (sum - i >= 0)
                    ans += countRec(n - 1, sum - i);
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

        static int[] RightSideFormer(int n) {
            int[] rightSide = new int[(int)Math.Pow(10.0, (double)(n / 2))];
            //form the first halves of the numbers.
            for (int i = 0; i < (int)Math.Pow(10.0, (double)(n / 2)); i++)
            {
                rightSide[i] = i;
            }
            return rightSide;
        }        

        static int nGetter()
        {
            Console.WriteLine("How many digits do you want your lucky numbers to have? ");
            int n = Convert.ToInt32(Console.ReadLine());
            return n;
        }
    }
}
