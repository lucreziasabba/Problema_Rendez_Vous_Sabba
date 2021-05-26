using System;
using System.Threading;


namespace Problema_Rendez_Vous_Sabba
{
    class Program
    {
        static int[] V = new int[1000];
        static int[] W = new int[1000];
        static int minimo = int.MaxValue;
        static double media = 0;

        static SemaphoreSlim s1 = new SemaphoreSlim(1);
        static SemaphoreSlim s2 = new SemaphoreSlim(1);

        static Random r = new Random();
        static void Main(string[] args)
        { 
            Thread t1 = new Thread(() => Metodo1());
            t1.Start();
            Thread t2 = new Thread(() => Metodo2());
            t2.Start();

            while (t1.IsAlive) { }
            while (t2.IsAlive) { }

            Console.WriteLine($"minimo: {minimo}");
            Console.WriteLine($"media: {media}");

            Console.ReadLine();
        }
        static void Metodo1()
        {
            for (int i = 0; i < V.Length; i++)
            {
                V[i] = r.Next(0, 1000);
                if (V[i] < minimo)
                    minimo = V[i];
            }
            s2.Release();
            s1.Wait();
            for (int i = 0; i < W.Length; i++)
            {
                if (W[i] < minimo)
                    minimo = W[i];
            }
        }

        static void Metodo2()
        {
            for (int i = 0; i < W.Length; i++)
            {
                W[i] = r.Next(0, 1000);
                media += W[i];
            }
            s1.Release();

            s2.Wait();
            for (int i = 0; i < V.Length; i++)
                media += V[i];
            media = media / 2000;
        }
    }
}
