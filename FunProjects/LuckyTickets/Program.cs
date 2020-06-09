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
    
    
    All numbers that can be shown as a sum of two one digit integers:
    0 = 0 + 0
    1 = 0 + 1 || 1 + 0
    2 = 0 + 2 || 1 + 1 || 0 + 2
    3 = 0 + 3 || 1 + 2 || 2 + 1 || 3 + 0
    4 = 0 + 4 || 1 + 3 || 2 + 2 || 3 + 1 || 4 + 0
    5 = 0 + 5 || 1 + 4 || 2 + 3 || 3 + 2 || 4 + 1 || 5 + 0
    ...
    9 = 0 + 9 || 1 + 8 || 2 + 7 || 3 + 6 || 4 + 5 || 5 + 4 || 6 + 3 || 7 + 2 || 8 + 1 || 9 + 0
    ...
    18 = 9 + 9
    
    HMMPF

    A n-digit numbers is = a*10**0 + b*10**1 + c*10**2 + ... + x*10**(n-1)

    What we are interested in is finding how many pairs exist, where a1 + b1 + ... + x1 = a2 + b2 + ... + x2









    */
    class LuckyTickets
    {
        static void Main(string[] args)
        {
            //digits in numbers
            int n = 1;
            //amount of lucky numbers 
            int luckies = 0;

            //the loop keeps going until user wants to stop by entering '0'
            while (n != 0) {

                //get the amount of digits
                n = nGetter();

                //break the number in to two halves
                //(int num1, int num2) = NumBreaker(n);

                //form all the possible right sides
                int[] rightSide = RightSideFormer(n);

                //go through all the sums formed by the rightside, and 


                Console.WriteLine();

            }
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

        static (int num1, int num2) NumBreaker(int n) {
            //Array formed from the number. It is known that the n is even.
            //Array needs to fit all the numbers a for which 0 =< a < 10**(n/2)
           




                int num1 = 7;
            int num2 = 4;
            return (num1, num2);

            
           
            
        }

        static int nGetter()
        {
            Console.WriteLine("How many digits do you want to your lucky numbers to have? ");
            int n = Convert.ToInt32(Console.ReadLine());
            return n;
        }
    }
}
