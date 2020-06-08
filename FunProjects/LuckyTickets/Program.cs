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
    Consider the following: 

    for numbers with 4 digits: abcd, split in half -> ab|cd, we will need to find for ab the pairs for which the following is true:
    a+b = c+d. a+b can have 10*10 values. Same for c+d. 
    
      0   1   2   3   4   5   6   7   8   9
    0 0   1   2   3   4   5   6   7   8   9
    1 1   2   3   4   5   6   7   8   9  10
    2 2   3   4   5   6   7   8   9  10  11
    3 3   4   5   6   7   8   9  10  11  12
    4 4   5   6   7   8   9  10  11  12  13
    5 5   6   7   8   9   10 11  12  13  14
    6
    7
    8
    9

    A diagonal pattern is emerging. 0 exists on the table only once, 1 twice, 2 3times, 3 4 times etc. I´m starting to smell combinatorics. I´m having flashbacks. Is this how it all ends?
     
    */
    class LuckyTickets
    {
        static void Main(string[] args)
        {
            //digits in numbers
            int n = 2;
            //amount of lucky numbers 
            int luckies = 0;

            //the loop keeps going until user wants to stop by entering '0' or nothing
            while (n != 0) {
                //get the amount of digits
                n = nGetter();  
                


            }
        }

        static int nGetter()
        {
            Console.WriteLine("How many digits do you want to your lucky numbers to have? ");
            int n = Convert.ToInt32(Console.ReadLine());
            return n;
        }
    }
}
