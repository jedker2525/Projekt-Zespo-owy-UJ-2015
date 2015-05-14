using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CzatBot
{
    public class Słowo
    {
        public uint ID;
        public string słowo;
        public Słowo Ojciec;
        public string ojciecString;

        public List<Słowo> pokrewne;//lista słów odmienionych
        public List<Słowo> synonimy;//lista słów o identycznym znaczeniu ale innej pisowni
        public List<Słowo> następniki;//Lista słów następujących po tym słowie w rozmowach testowych;


        public bool ignorowana_część_mowy ;
        public bool przymiotnik; //adj
        public bool przymiotnik_poprzyimkowy; //adjp -(np. "niemiecku")
        public bool przysłówek; //adv -(np. "głupio")
        public bool przysłówek_forma_deprecjatywna; //depr
        public bool przysłówek_rzeczownik_odsłowny; //ger
        public bool spójnik; //conj
        public bool liczebnik; //num
        public bool imiesłów_przymiotnikowy_czynny; //pact
        public bool imiesłów_przysłówkowy_uprzedni; //pant
        public bool imiesłów_przysłówkowy_współczesny; //pcon
        public bool imiesłów_przymiotnikowy_bierny; //ppas
        public bool zaimek_nietrzecioosobowy; //ppron12
        public bool zaimek_trzecioosobowy; //ppron3 -
        public bool predykatyw; //* pred -  (np. "trzeba")
        public bool przyimek; //* prep 
        public bool zaimek_siebie; //* siebie -  "siebie"
        public bool rzeczownik; //* subst - 
        public bool czasownik; //* verb - 
        public bool skrót; //* brev
        public bool wykrzyknienie; //* interj - 
        public bool jednostka_obca; //* xxx - 
        public bool liczba_pojedyncza; //* sg
        public bool liczba_mnoga; //* pl
        public bool forma_nieregularna;//* irreg - (nierozpoznana dokładniej pod względem wartości atrybutów, np.subst:irreg
        public bool mianownik; //* nom - 
        public bool dopełniacz; //* gen -
        public bool biernik; //* acc - 
        public bool celownik; //* dat - 
        public bool narzędnik; //* inst -
        public bool miejscownik; //* loc - 
        public bool wołacz; //* voc - 
        public bool stopień_równy; //* pos - 
        public bool stopień_wyższy; //* comp - 
        public bool stopień_najwyższy; //* sup - 
        public bool rodzaje_męskie; //m
        public bool rodzaje_męskie1; //m1
        public bool rodzaje_męskie2; //m2
        public bool rodzaje_męskie3; //m3
        public bool rodzaje_nijakie; //n
        public bool rodzaje_nijakie1; //n1
        public bool rodzaje_nijakie2; //n2
        public bool rodzaj_żeński; //f
        public bool pierwsza_osoba; //* pri - 
        public bool druga_osoba; //* sec - 
        public bool trzecia_osoba; //* tri - 
        public bool forma_niezanegowana; //* aff - 
        public bool forma_zanegowana; //* neg - 
        public bool forma_zwrotna_czasownika; //* refl -  [nie występuje w znacznikach IPI]
        public bool czasownik_dokonany; //* perf - 
        public bool czasownik_niedokonany; //* imperf - 
        public bool czasownik_dokonany_niedokonany;//* imperf.perf - czasownik, który może występować zarówno jako dokonany, jak i jako niedokonany
        public bool forma_nieakcentowana_zaimka; //* nakc - 
        public bool forma_akcentowana_zaimka; //* akc - 
        public bool forma_poprzyimkowa; //* praep - 
        public bool forma_niepoprzyimkowa; //* npraep - 
        public bool rzeczownik_odsłowny; //* ger - 
        public bool forma_bezosobowa; //* imps -
        public bool tryb_rozkazujący; //* impt - 
        public bool bezokolicznik; //* inf - 
        public bool forma_nieprzeszła; //* fin - 
        public bool forma_przyszła_buć; //* bedzie -  "być"
        public bool forma_przeszła_czasownika; //* praet -  (pseudoimiesłów)
        public bool tryb_przypuszczający; //* pot -  [nie występuje w znacznikach IPI]
        public bool forma_niestandardowa; //* nstd -  np. archaiczna[nie występuje w znacznikach IPI]
        public bool skrót_z_kropką; //* pun - [za NKJP]
        public bool skrót_bez_kropki; //* npun - [za NKJP]

        public Słowo(string sl)//Załadowanie z pliku
        {

        }

        private string ConvertToString()
        {
            string ret = ID + ";" + słowo + ";" + Ojciec.ID +";" + ojciecString + ";" + ignorowana_część_mowy + ";" + przymiotnik + ";" + przymiotnik_poprzyimkowy + ";" + przysłówek +";" + przysłówek_forma_deprecjatywna + ";" + przysłówek_rzeczownik_odsłowny + ";" + spójnik + ";" + liczebnik + ";" + imiesłów_przymiotnikowy_czynny + ";" + imiesłów_przysłówkowy_uprzedni + ";" + imiesłów_przysłówkowy_współczesny + ";" + imiesłów_przymiotnikowy_bierny + ";" + zaimek_nietrzecioosobowy + ";" + zaimek_trzecioosobowy + ";" + predykatyw + ";" + przyimek + ";" + zaimek_siebie + ";" + rzeczownik + ";" + czasownik + ";" + skrót + ";" + wykrzyknienie + ";" + jednostka_obca + ";" + liczba_pojedyncza + ";" + liczba_mnoga + ";" + forma_nieregularna + ";" + mianownik + ";" + dopełniacz + ";" + biernik + ";" + celownik + ";" + narzędnik + ";" + miejscownik + ";" + wołacz + ";" + stopień_równy+";"+stopień_wyższy + ";" + stopień_najwyższy + ";" + rodzaje_męskie + ";" + rodzaje_męskie1 + ";" + rodzaje_męskie2 + ";" + rodzaje_męskie3 + ";" + rodzaje_nijakie + ";" + rodzaje_nijakie1 + ";" + rodzaje_nijakie2 + ";" + rodzaj_żeński + ";" + pierwsza_osoba + ";" + druga_osoba + ";" + trzecia_osoba + ";" + forma_niezanegowana + ";" + forma_zanegowana + ";" + forma_zwrotna_czasownika + ";" + czasownik_dokonany + ";" + czasownik_niedokonany + ";" + czasownik_dokonany_niedokonany + ";" + forma_nieakcentowana_zaimka + ";" + forma_akcentowana_zaimka + ";" + forma_poprzyimkowa + ";" + forma_niepoprzyimkowa + ";" + rzeczownik_odsłowny + ";" + forma_bezosobowa + ";" + tryb_rozkazujący + ";" + bezokolicznik + ";" + forma_nieprzeszła + ";" + forma_przyszła_buć + ";" + forma_przeszła_czasownika + ";" + tryb_przypuszczający + ";" + forma_niestandardowa + ";" + skrót_z_kropką + ";" + skrót_bez_kropki + ";";
            foreach (var slowo in pokrewne)
            {
                ret += slowo.ID + ':';
            }
            ret+= ";";
            foreach (var slowo in synonimy)
            {
                ret += slowo.ID + ':';
            }
            ret+= ";";
            foreach (var slowo in następniki)
            {
                ret += slowo.ID + ':';
            }
            ret+= ";";
            return ret;
        }

        public Słowo(uint id,string słowo,string słowo2,string parametry)
        {
            pokrewne = new List<Słowo>();
            synonimy = new List<Słowo>();
            następniki = new List<Słowo>();
            ID = id;
            this.słowo = słowo;
            ojciecString = słowo2;
            if (słowo == słowo2)
            {
                Ojciec = this;
            }
            else
            {
                Ojciec = null;
            }
            ZaładujParametryZesłownika(parametry);
        }

        private void ZaładujParametryZesłownika(string parametry)
        {
            string[] paramTab = parametry.Split(':');
            foreach (var VARIABLE1 in paramTab)
            {
                foreach (var VARIABLE2 in VARIABLE1.Split('.'))
                {
                    switch (VARIABLE2)
                    {
                        case "adj":
                            przyimek = true;
                            break;
                        case "adjp":
                            przymiotnik_poprzyimkowy = true;
                            break;
                        case "adv":
                            przysłówek = true;
                            break;
                        case "conj":
                            spójnik = true;
                            break;
                        case "ign":
                            ignorowana_część_mowy = true;
                            break;
                        case "num":
                            liczebnik = true;
                            break;
                        case "pact":
                            imiesłów_przymiotnikowy_czynny = true;
                            break;
                        case "pant":
                            imiesłów_przysłówkowy_uprzedni = true;
                            break;
                        case "pcon":
                            imiesłów_przysłówkowy_współczesny = true;
                            break;
                        case "ppas":
                            imiesłów_przymiotnikowy_bierny = true;
                            break;
                        case "ppron12":
                            zaimek_nietrzecioosobowy = true;
                            break;
                        case "ppron3":
                            zaimek_trzecioosobowy = true;
                            break;
                        case "pred":
                            predykatyw = true;
                            break;
                        case "prep":
                            przyimek = true;
                            break;
                        case "siebie":
                            zaimek_siebie = true;
                            break;
                        case "subst":
                            rzeczownik = true;
                            break;
                        case "verb":
                            czasownik = true;
                            break;
                        case "sg":
                            liczba_pojedyncza = true;
                            break;
                        case "pl":
                            liczba_mnoga = true;
                            break;
                        case "irreg":
                            forma_nieregularna = true;
                            break;
                        case "nom":
                            mianownik = true;
                            break;
                        case "gen":
                            dopełniacz = true;
                            break;
                        case "acc":
                            biernik = true;
                            break;
                        case "dat":
                            celownik = true;
                            break;
                        case "inst":
                            narzędnik = true;
                            break;
                        case "loc":
                            miejscownik = true;
                            break;
                        case "voc":
                            wołacz = true;
                            break;
                        case "pos":
                            stopień_równy = true;
                            break;
                        case "comp":
                            stopień_wyższy = true;
                            break;
                        case "sup":
                            stopień_najwyższy = true;
                            break;
                        case "m":
                            rodzaje_męskie = true;
                            break;
                        case "m1":
                            rodzaje_męskie1 = true;
                            break;
                        case "m2":
                            rodzaje_męskie2 = true;
                            break;
                        case "m3":
                            rodzaje_męskie3 = true;
                            break;
                        case "n":
                            rodzaje_nijakie = true;
                            break;
                        case "n1":
                            rodzaje_nijakie1 = true;
                            break;
                        case "n2":
                            rodzaje_nijakie2 = true;
                            break;
                        case "f":
                            rodzaj_żeński = true;
                            break;
                        case "pri":
                            pierwsza_osoba = true;
                            break;
                        case "sec":
                            druga_osoba = true;
                            break;
                        case "tri":
                            break;
                        case "depr":
                            przysłówek_forma_deprecjatywna = true;
                            break;
                        case "aff":
                            forma_niezanegowana = true;
                            break;
                        case "neg":
                            forma_zanegowana = true;
                            break;
                        case "refl":
                            forma_zwrotna_czasownika = true;
                            break;
                        case "perf":
                            czasownik_dokonany = true;
                            break;
                        case "imperf":
                            czasownik_niedokonany = true;
                            break;
                        case "nakc":
                            forma_nieakcentowana_zaimka = true;
                            break;
                        case "akc":
                            forma_nieakcentowana_zaimka = true;
                            break;
                        case "praep":
                            forma_poprzyimkowa = true;
                            break;
                        case "npraep":
                            forma_niepoprzyimkowa = true;
                            break;
                        case "ger":
                            przysłówek_rzeczownik_odsłowny = true;
                            break;
                        case "imps":
                            forma_bezosobowa = true;
                            break;
                        case "impt":
                            tryb_rozkazujący = true;
                            break;
                        case "inf":
                            bezokolicznik = true;
                            break;
                        case "fin":
                            forma_nieprzeszła = true;
                            break;
                        case "bedzie":
                            forma_przyszła_buć = true;
                            break;
                        case "praet":
                            forma_przeszła_czasownika = true;
                            break;
                        case "pot":
                            tryb_przypuszczający = true;
                            break;
                        case "qub": //nie ma takiego
                            break;
                        case "pltant": //nie ma takiego
                            break;
                        case "ter": //nie ma takiego
                            break;
                        case "p1": //nie ma takiego
                            break;
                        case "p2": //nie ma takiego
                            break;
                        case "p3": //nie ma takiego
                            break;
                        case "rec": //nie ma takiego
                            break;
                        case "congr": //nie ma takiego
                            break;
                        case "nstd": //nie ma takiego
                            break;
                        case "winien": //nie ma takiego
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void SetParent(Słowo parent)
        {
            Ojciec = parent;
            parent.pokrewne.Add(this);
        }

        public override string ToString()
        {
            return ConvertToString() + "\n";
        }
    }

    public enum ZdanieTyp
    {
        oznajmujące_twierdzące,
        oznajmujące_przeczące,
        pytające,
        rozkazujące,
        wykrzyknikowe
    }

    public class Zdanie
    {
        public ZdanieTyp zdanieTyp;
        public List<Słowo> SłowaZdania;

        private static char[] forbidden = new char[] { ',', '.', '/', ':', ';', '\'', '[', ']', '\\', '?', '>', '<', '{', '}', '(', ')', '-', '=', '+', '_', '~', '!', '@', '#', '$', '%', '^', '&', '*', '`', '\t', '\n', '\r' };

        public Zdanie(string zdanie)
        {
            string zdanieKopia = zdanie;
            List<Słowo> zdanieListaSłów = new List<Słowo>();
            List<string> slowa=tokenize(zdanie);
            foreach (var VARIABLE in slowa)
            {
                if (Program.ListaWszystkichSłów.Any(x => x.słowo == VARIABLE))
                {
                    zdanieListaSłów.Add(Program.ListaWszystkichSłów.First(x => x.słowo == VARIABLE));
                }
                else
                {
                    Console.WriteLine("Nie znaleziono słowa w słowniku. : " + VARIABLE);
                }
            }
            if (zdanie.EndsWith("?"))
            {
                zdanieTyp = ZdanieTyp.pytające;
            }
            else
            {
                if (zdanie.EndsWith("!"))
                {
                    zdanieTyp = ZdanieTyp.wykrzyknikowe;
                }
                else
                {
                    //określenie czy zdanie jest oznajmujące
                    zdanieTyp=ZdanieTyp.oznajmujące_przeczące;
                    zdanieTyp=ZdanieTyp.oznajmujące_twierdzące;
                    zdanieTyp=ZdanieTyp.rozkazujące;
                }
            }
            //czy kończy się pytajnikiem? , kropką? czy wykrzyknikiem?
            if(true)
            {
                //czy słowo jest nazwą własną
                //jeśli tak to wikipediia
                //jeśli nie to początek zdania
            }
            //wyznacz podmiot 
                //zakładamy że pierwszy rzeczownik jest podmioptem jeśli zdanie nie jest pytaniem

        }
        public static List<string> tokenize(string zdanie)
        {
            char[] zd = zdanie.ToCharArray();
            List<string> ret = new List<string>();
            for (int i = 0; i < zd.Length; i++)
            {
                if (forbidden.Contains(zd[i]))
                {
                    zd[i] = ' ';
                }
            }

            zdanie = new string(zd);
            string[] zdTab = zdanie.Split(new char[] {' '});
            foreach (var VARIABLE in zdTab)
            {
                if(VARIABLE!="")
                    ret.Add(VARIABLE);
            }
            return ret;
        }
    }


    public class Program
    {
        
        public static List<Słowo> ListaWszystkichSłów=new List<Słowo>();
        private static uint LicznikID=0;
        private static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\andrzej\Desktop\CzatBot\morfologik.txt");
            for (int i = 0; i < lines.Length;i++)
            {
                string[] stab = lines[i].Split('\t');
                string[] paramTab = stab[2].Split('+');
                foreach (var VARIABLE in paramTab)
                {
                    ListaWszystkichSłów.Add(new Słowo(LicznikID++,stab[0],stab[1], VARIABLE));
                }
            }
            for (int i=0;i<ListaWszystkichSłów.Count;i++)
            {
                if (ListaWszystkichSłów.Any(x => x.słowo.CompareTo(ListaWszystkichSłów[i].ojciecString) == 0))
                {
                    Słowo sł = ListaWszystkichSłów.First(x => x.słowo.CompareTo(ListaWszystkichSłów[i].ojciecString) == 0);
                    ListaWszystkichSłów[i].SetParent(sł);
                }
                else
                {
                    Słowo sł = new Słowo(LicznikID++, ListaWszystkichSłów[i].ojciecString,ListaWszystkichSłów[i].ojciecString, "");
                    ListaWszystkichSłów.Add(sł);
                }
            }
            while (true)
            {

            }

            for (int i = 0; i < ListaWszystkichSłów.Count; i++)
                System.IO.File.WriteAllText(@"C:\Users\andrzej\Desktop\CzatBot\CzatBotDatabase.txt", ListaWszystkichSłów[i].ToString());
            Console.WriteLine("Koniec");
            Console.ReadKey();
        }
    }
}
