using System;
using System.Collections.Generic;

namespace uloha3_kral
{
    public class Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    class Program
    {
        static void print_sachovnice(int[,] sachovnica)
        {
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    Console.Write(sachovnica[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            int pocet_prekazok = Convert.ToInt32(Console.ReadLine());
            List<int[]> suradnice = new List<int[]>();

            while (suradnice.Count != pocet_prekazok)  // len(suradnice) 
            {
                int[] pole = new int[2];
                string[] prekazky = Console.ReadLine().Split();
                int x_prekazky = Convert.ToInt32(prekazky[0]);
                int y_prekazky = Convert.ToInt32(prekazky[1]);
                pole[0] = x_prekazky;  //x súradnica prekážok
                pole[1] = y_prekazky;  //y súradnica prekážok
                suradnice.Add(pole);
            }
            string[] start = Console.ReadLine().Split();
            string[] ciel = Console.ReadLine().Split();

            int x_start = Convert.ToInt32(start[0]);
            int y_start = Convert.ToInt32(start[1]);
            int x_ciel = Convert.ToInt32(ciel[0]);
            int y_ciel = Convert.ToInt32(ciel[1]);

            if(x_ciel == x_start && y_ciel == y_start)
            {
                Console.WriteLine(0);
                return;
            }

            int[,] sachovnica = new int[9, 9];
            int[] x_tah = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] y_tah = { 0, -1, 1, 1, -1, 1, 0, -1 };
            bool existuje = true;

            foreach (int[] i in suradnice)
            {
                sachovnica[i[0], i[1]] = -1;
            }
            sachovnica[x_start, y_start] = -1;

            Queue<Position> fronta = new Queue<Position>();
            fronta.Enqueue(new Position(x_start, y_start));

            while (sachovnica[x_ciel, y_ciel] == 0)
            {
                if (fronta.Count == 0)
                {
                    existuje = false;
                    break;
                }
                else
                {
                    Position top = fronta.Dequeue();  //i1, i2 = fronta.pop(0)
                    int i1 = top.x;
                    int i2 = top.y;
                    int krok = sachovnica[i1,i2] + 1;
                    krok = (krok == 0) ? 1 : krok;   //   krok = 1 if krok == 0 else krok

                    for (int i = 0; i < x_tah.Length; ++i)
                    {
                        int j1 = i1 + x_tah[i];
                        int j2 = i2 + y_tah[i];

                        if (j1 >= 1 && j1 <= 8 && j2 >= 1 && j2 <= 8)  //aby som nevyšla zo šachovnce
                        {
                            if (sachovnica[j1, j2] == 0)
                            {
                                sachovnica[j1, j2] = krok;
                                fronta.Enqueue(new Position(j1, j2));
                            }
                        }
                    }
                }
            }
            if (existuje is false)
            {
                Console.WriteLine(-1);
            }
            else
            {
                Console.WriteLine(sachovnica[x_ciel,y_ciel]);
            }
        }
    }
}
