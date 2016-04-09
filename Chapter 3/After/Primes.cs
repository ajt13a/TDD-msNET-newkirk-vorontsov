using System;
using System.Collections;

public class Primes
{
    /// <summary>
    ///  This method will generate the prime numbers 
    ///  that exist between 0 up to an including the 
    ///  maximum number passed in. 
    /// </summary>
    /// <param name="maxValue">The largest prime</param>
    /// <returns>An ArrayList of prime numbers</returns>
    public static ArrayList Generate(int maxValue)
    {
        if(maxValue < 2) return new ArrayList();

        Primes primes = new Primes(maxValue);
        return primes.Sieve();
    }

    private bool[] isPrime; 

    private Primes(int maxValue)
    {   
        isPrime = new bool[maxValue+1];

        for(int i = 0; i < isPrime.Length; i++)
            isPrime[i] = true;
    }

    private ArrayList Sieve()
    {
        ArrayList result = new ArrayList();

        for(int i = 2; i < isPrime.Length; i++)
        {
            if(isPrime[i])
            {
                result.Add(i);
                RemoveMultiples(i, isPrime);
            }
        }

        return result;
    }

    private void RemoveMultiples(int prime, bool[] isPrime)
    {
        for(int j = 2 * prime; j < isPrime.Length; j += prime)
            isPrime[j] = false;
    }
}

