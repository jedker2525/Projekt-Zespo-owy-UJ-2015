using System;
using System.Collections.Generic;
using System.Linq;

namespace CzatBot
{
    public class WiązanieSłów
    {
        public int IlośćWiązań;
        public Słowo slowo;

        public WiązanieSłów(Słowo sl)
        {
            slowo = sl;
            IlośćWiązań = 1;
        }
    }

    public class Słowo
    {
        public static Random rand=new Random();
        public int Id;//ID uniwersalne dla całego programu
        public string word;//słowo
        public double kontekstowość;//określenie czy dane słowo jest kontekstowe
        public int counterZdania;//ilość wystąpień tego słowa we wszystkich ciągach słów
        public List<WiązanieSłów> następnicyList;//lista słów następujących po tym słowie słów
        //public List<WiązanieSłów> poprzednicyList;//lista słów poprzedzających to słowo



        public Słowo(int id,string sl,double kontekstowosc)
        {
            Id = id;
            word = sl;
            kontekstowość = kontekstowosc;
            następnicyList =new List<WiązanieSłów>();
            //poprzednicyList = new List<WiązanieSłów>();
        }

        public Słowo RandomNextSłowo()
        {
            int IlośćOdwołań = 0;
            foreach (var VARIABLE in następnicyList)
            {
                IlośćOdwołań += VARIABLE.IlośćWiązań;
            }
            int losowy=rand.Next(0, IlośćOdwołań);
            IlośćOdwołań = 0;
            foreach (var VARIABLE in następnicyList)
            {
                IlośćOdwołań += VARIABLE.IlośćWiązań;
                if (IlośćOdwołań >= losowy)
                    return VARIABLE.slowo;
            }
            return następnicyList.ElementAt(rand.Next(0, następnicyList.Count-1)).slowo;
        }

        //public void AddPoprzednik(Słowo sl)
        //{
        //    if (poprzednicyList.Any(x => x.slowo == sl))
        //    {
        //        poprzednicyList.First(x => x.slowo == sl).IlośćWiązań++;
        //    }
        //    else
        //    {
        //        poprzednicyList.Add(new WiązanieSłów(sl));
        //    }
        //}
        public void AddNastępnik(Słowo sl)
        {
            if (następnicyList.Any(x => x.slowo == sl))
            {
                następnicyList.First(x => x.slowo == sl).IlośćWiązań++;
            }
            else
            {
                następnicyList.Add(new WiązanieSłów(sl));
            }
        }

    }

    public class WiązanieZdań
    {
        public int IlośćWiązań;
        public Zdanie zdanie;

        public WiązanieZdań(Zdanie zd)
        {
            IlośćWiązań = 1;
            zdanie = zd;
        }
    }

    public class Zdanie
    {
        public static Random rand = new Random();
        public int ID;//uniwersalne dla całego programu
        public Słowo kontekst//wskaźnik na słowo najbardziej kontekstowe ze słów ze zdania
        {
            get
            {
                if (ListaSłów == null)
                    return null;
                Słowo max = ListaSłów.First();
                foreach (var VARIABLE in ListaSłów)
                {
                    if (max.kontekstowość <= VARIABLE.kontekstowość)
                    {
                        max = VARIABLE;
                    }

                }
                return max;
            }
        }
        public List<Słowo> ListaSłów;//Lista słów w zdaniu
        public List<WiązanieZdań> nextZdanie;//Lista zdań następujących po tym zdaniu
        //public List<WiązanieZdań> prevZdanie;//Lista zdań poprzedzających to zdanie

        public Zdanie(int id,List<Słowo> listSł)
        {
            ID = id;
            ListaSłów = listSł;
            nextZdanie=new List<WiązanieZdań>();
        }

        public Zdanie RandomNextZdanie()
        {
            //TODO puki co nie bawimy się z kontekstowość tylko w random na zdaniach
            int IlośćOdwołań = 0;
            foreach (var VARIABLE in nextZdanie)
            {
                IlośćOdwołań += VARIABLE.IlośćWiązań;
            }
            int losowy = rand.Next(0, IlośćOdwołań);
            IlośćOdwołań = 0;
            foreach (var VARIABLE in nextZdanie)
            {
                IlośćOdwołań += VARIABLE.IlośćWiązań;
                if (IlośćOdwołań >= losowy)
                    return VARIABLE.zdanie;
            }
            return nextZdanie.ElementAt(rand.Next(0, nextZdanie.Count)).zdanie;
        }
        public void AddNastępnik(Zdanie zdanie)
        {
            if (nextZdanie.Any(x => x.zdanie == zdanie))
            {
                nextZdanie.First(x => x.zdanie == zdanie).IlośćWiązań++;
            }
            else
            {
                nextZdanie.Add(new WiązanieZdań(zdanie));
            }
        }

        public void WypiszNaEkran()
        {
            for (int i = 1; i < ListaSłów.Count-1; i++)
            {
                Console.Write(ListaSłów[i].word+" ");
            }
            Console.WriteLine();
        }
    }


    //Start
        //powitanie
            //odpowiedz na powitanie
        //zdania
            //pytanie
            //odpowiedz
                //odpowiedz
                //potwierdzenie
                //zaprzeczenie
            //twierdzenie
                //wikipedia
        //pożegnanie
    //Stop
    public class Program
    {
        public static int licznikIdSłów = 0;
        public static int licznikIdZdań = 0;
        public static Słowo słowoPustePoczątkowe = new Słowo(licznikIdSłów++, "", 0);
        public static Słowo słowoPusteKońcowe = new Słowo(licznikIdSłów++, "", 0);
        public static List<Słowo> ListaWszystkichSłów=new List<Słowo>();
        public static Zdanie zdaniePustePoczątkowe = new Zdanie(licznikIdZdań++, new List<Słowo>());
        public static Zdanie zdaniePusteKońcowe = new Zdanie(licznikIdZdań++, new List<Słowo>());
        public static List<Zdanie> ListaWszystkichZdań=new List<Zdanie>();

        

        public static Zdanie SzukajKajtuśSzukaj(List<Słowo> zdanko)
        {
            //zdanko.Count
            List<Zdanie>[] TablicaList=new List<Zdanie>[zdanko.Count];//tablica po słowach z listami w których są zdania zawierające dane słowo
            for(int i=1;i<zdanko.Count-1;i++)
            {
                TablicaList[i]=new List<Zdanie>();
                foreach (var zdanie in ListaWszystkichZdań)
                {
                    if (zdanie.ListaSłów.Contains(zdanko[i]))
                    {
                        TablicaList[i].Add(zdanie);
                    }
                }
            }
            TablicaList[0] = new List<Zdanie>();
            TablicaList[TablicaList.Length-1] = new List<Zdanie>();
            //Znajdz najczęściej powtażające się zdanie
            int[] tablica=new int[ListaWszystkichZdań.Count];
            //for(int i=0;i<tablica.Length;i++)//c# zeruje pamięć
            //{
            //    tablica[i] = 0;
            //}
            foreach (var ListaZdan in TablicaList)
            {
                foreach (var zdanie in ListaZdan)
                {
                    tablica[zdanie.ID]++;
                }
            }
            int max = 0, index = 0;
            for (int i = 0; i < tablica.Length; i++)
            {
                if (max < tablica[i])
                {
                    max = tablica[i];
                    index = i;
                }
            }
            double r= (double)max / (double)(zdanko.Count - 2);
            if (max == 0||r<0.75)
                return null;
            return ListaWszystkichZdań.First(x => x.ID == index);
        }

        private static void Main(string[] args)
        {
            ListaWszystkichSłów.Add(słowoPustePoczątkowe);
            ListaWszystkichSłów.Add(słowoPusteKońcowe);
            ListaWszystkichZdań.Add(zdaniePustePoczątkowe);
            ListaWszystkichZdań.Add(zdaniePusteKońcowe);

            słowoPustePoczątkowe.następnicyList.Add(new WiązanieSłów(słowoPusteKońcowe));
            //słowoPusteKońcowe.poprzednicyList.Add(new WiązanieSłów(słowoPustePoczątkowe));

            zdaniePustePoczątkowe.nextZdanie.Add(new WiązanieZdań(zdaniePusteKońcowe));
            //zdaniePusteKońcowe.prevZdanie.Add(new WiązanieZdań(zdaniePustePoczątkowe));

            zdaniePustePoczątkowe.ListaSłów=new List<Słowo>();
            zdaniePustePoczątkowe.ListaSłów.Add(słowoPustePoczątkowe);
            zdaniePustePoczątkowe.ListaSłów.Add(słowoPusteKońcowe);

            zdaniePusteKońcowe.ListaSłów=new List<Słowo>();
            zdaniePusteKońcowe.ListaSłów.Add(słowoPustePoczątkowe);
            zdaniePusteKońcowe.ListaSłów.Add(słowoPusteKońcowe);


            int stan = 0;
            Zdanie OstatnioNapisane = zdaniePustePoczątkowe;
            while (true)
            {
                if (stan == 0)
                {
                    string[] sTab=Console.ReadLine().Split(new char[] { ' ' });
                    //słowa
                    Słowo[] slowa=new Słowo[sTab.Length+2];

                    slowa[0] = słowoPustePoczątkowe;
                    slowa[sTab.Length + 1] = słowoPusteKońcowe;

                    for (int i = 0; i < sTab.Length; i++)
                    {
                        if (ListaWszystkichSłów.Any(x => x.word == sTab[i]))
                        {
                            slowa[i+1]= ListaWszystkichSłów.First(x => x.word == sTab[i]);
                        }
                        else
                        {
                            slowa[i+1]=new Słowo(licznikIdSłów,sTab[i],1/sTab.Length);
                            ListaWszystkichSłów.Add(slowa[i+1]);
                        }
                        slowa[i].AddNastępnik(slowa[i+1]);
                    }
                    slowa[sTab.Length].AddNastępnik(slowa[sTab.Length + 1]);

                    //zdania
                    Zdanie właśnieNapisane= SzukajKajtuśSzukaj(slowa.ToList());
                    if(właśnieNapisane == null)
                    {
                        właśnieNapisane = new Zdanie(licznikIdZdań++, slowa.ToList());
                        właśnieNapisane.AddNastępnik(zdaniePusteKońcowe);
                        ListaWszystkichZdań.Add(właśnieNapisane);
                    }
                    OstatnioNapisane.AddNastępnik(właśnieNapisane);
                    OstatnioNapisane = właśnieNapisane;
                    stan = 1;
                }
                else
                {
                    Słowo sl = słowoPustePoczątkowe;
                    Random rand=new Random();
                    sl = sl.następnicyList.ElementAt(rand.Next(1, sl.następnicyList.Count)).slowo;
                    Console.Write("Słowami: ");
                    while (sl.Id!=1)
                    {
                        Console.Write(sl.word+ " ");
                        sl = sl.RandomNextSłowo();
                    }
                    Console.WriteLine();
                    Console.Write("Zdaniami: ");
                    OstatnioNapisane.RandomNextZdanie().WypiszNaEkran();
                    stan = 0;
                }
            }
        }
    }
}
