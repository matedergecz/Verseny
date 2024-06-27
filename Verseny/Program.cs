using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Verseny
{

    class Versenyzo
    {
        //mezők
        private int rajtSzam;
        private string nev;
        private string szak;
        private int pontSzam;


        //konstruktor
        public Versenyzo(int rajtSzam, string nev, string szak)
        {
            this.rajtSzam = rajtSzam;
            this.nev = nev;
            this.szak = szak;
        }

        //metódusok
        public void PontotKap(int pont)
        {
            pontSzam += pont;
        }

        public override string ToString()
        {
            return rajtSzam + "\t" + nev + "\t" + szak + "\t" + pontSzam + "pont";
        }

        //tulajdonságok
        public int RajtSzam
        {
            get { return rajtSzam; }
        }
        public string Nev
        {
            get
            { return nev; }
        }
        public string Szak
        {
            get { return szak; }
        }
        public int PontSzam
        {
            get { return pontSzam; }
        }

        internal class Program
        {
            class VezerloOsztaly
            {
                public void Start()
                {
                    AdatBevitel();

                    Kiiratas("\nRésztvevők:\n");
                    Verseny();
                    Kiiratas("\nEredmények:\n");

                    Eredmenyek();
                    Keresesek();
                }


                private void AdatBevitel()
                {
                    Versenyzo versenyzo;
                    string nev, szak;
                    int sorszam = 1;

                    StreamReader olvasoCsatorna = new StreamReader("C:/txt/versenyzok.txt");

                    while (!olvasoCsatorna.EndOfStream)
                    {
                        nev = olvasoCsatorna.ReadLine();
                        szak = olvasoCsatorna.ReadLine();

                        //az adatok alapján létrehozzuk a versenyző példáynt
                        versenyzo = new Versenyzo(sorszam, nev, szak);
                        //itt adjuk hozzá a listához az aktuális versenyző példányt
                        versenyzok.Add(versenyzo);
                        sorszam++;

                    }
                    olvasoCsatorna.Close();

                }
                private void Kiiratas(string cim)
                {
                    Console.WriteLine(cim);
                    foreach (Versenyzo enekes in versenyzok)
                    {
                        Console.WriteLine(enekes);
                    }



                }
                private int zsuriletszam = 5;
                private int pontHatar = 10;

                private void Verseny()
                {
                    Random rand = new Random();
                    int pont;
                    foreach (Versenyzo versenyzo in versenyzok)
                    {
                        //ponthoz a zsűri:
                        for (int i = 1; i <= zsuriletszam; i++)
                        {
                            pont = rand.Next(pontHatar);
                            versenyzo.PontotKap(pont);
                        }
                    }
                }

                private void Eredmenyek()
                {
                    Nyertes();
                    Sorrend();

                }
                private void Nyertes()
                {
                    //kezdőérték beállítása
                    int max = versenyzok[0].pontSzam;

                    //a legnagyobb érték megáálapítása
                    foreach (Versenyzo enekes in versenyzok)
                    {
                        if (enekes.pontSzam > max)
                        {
                            max = enekes.pontSzam;
                        }
                    }
                    //a legjobbak kiíratása
                    Console.WriteLine("\nA legjobb(ak)\n");
                    foreach (Versenyzo enekes in versenyzok)
                    {
                        if (enekes.pontSzam == max)
                        {
                            Console.WriteLine(enekes);
                        }
                    }
                }

                private void Sorrend()
                {
                    //rendezés
                    Versenyzo temp;
                    for (int i = 0; i < versenyzok.Count; i++)
                    {
                        for (int j = i + 1; j < versenyzok.Count; j++)
                        {
                            if (versenyzok[i].PontSzam < versenyzok[j].PontSzam)
                            {
                                temp = versenyzok[i];
                                versenyzok[i] = versenyzok[j];
                                versenyzok[j] = temp;
                            }
                        }
                    }
                    Kiiratas("\nEredménytábla\n");
                }

                private void Keresesek()
                {
                    Console.WriteLine("\nAdott szakhoz tartozó énekesek keresése\n");
                    Console.WriteLine("\nKeres valakit? (i/n)\n");
                    char valasz;
                    while (!char.TryParse(Console.ReadLine(), out valasz))
                    {
                        Console.WriteLine("Egy karakter írjon. ");
                    }

                    string szak;
                    bool vanIlyen;

                    while (valasz == 'i' || valasz == 'I')
                    {
                        Console.Write("Szak: ");
                        szak = Console.ReadLine();
                        vanIlyen = false;


                        foreach (Versenyzo enekes in versenyzok)
                        {
                            if (enekes.Szak == szak)
                            {
                                Console.WriteLine(enekes);
                                vanIlyen = true;
                            }
                        }

                        if (!vanIlyen)
                        {
                            Console.WriteLine("Erről a szakról senki sem indult.");

                        }
                        Console.Write("\nKeres még valakit (i/n)");
                        valasz = char.Parse(Console.ReadLine());
                    }
                }



                private List<Versenyzo> versenyzok = new List<Versenyzo>();
                static void Main(string[] args)
                {
                    VezerloOsztaly vezerles = new VezerloOsztaly();
                    vezerles.Start();
                   


                }


            }
        }
    }
}
