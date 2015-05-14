using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CzatBot;

namespace CzatBot
{
    class wordNode
    {
        private static long nodePtr=0;
        private static Random rnd = new Random();
        private static char[] forbidden = new char[]{',','.','/',':',';','\'','[',']','\\','?','>','<','{','}','(',')','-','=','+','_','~','!','@','#','$','%','^','&','*','`','\t','\n','\r'};

        public List<wordNode> nodeList;
        private long counter = 0;
        private long id;
        public string word;
        public wordNode(string _word)
        {
            id = nodePtr++;
 	        word = _word;
            nodeList=new List<wordNode>();
        }
        public wordNode chooseNext()
        {
            if (nodeList.Count == 0)
            {
                return null;
            }
            return nodeList[rnd.Next(0,nodeList.Count)];
        } // end chooseNext();
        public static List<string> tokenize(string zdanie)
        {
            char[] zd = zdanie.ToCharArray();
            List<string> ret = new List<string>();
            ret.Add("$");
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
            ret.Add("#");
            return ret;
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
        static List<wordNode> allNodes;
        byte state;
        static void Main(string[] args)
        {
            allNodes=new List<wordNode>();
            allNodes.Add(new wordNode("$"));
            int stan = 0;//określa 
            while (true)
            {
                if (stan == 0)//czeka na tekst
                {
                    string input = Console.ReadLine();
                    List<string> tab = wordNode.tokenize(input);
                    wordNode slowoPoprzedzające=null;
                    foreach (var VARIABLE in tab)
                    {
                        try
                        {
                            wordNode slowo=allNodes.First(x => x.word == VARIABLE);
                            if (slowoPoprzedzające == null)
                            {
                                slowoPoprzedzające = slowo;
                            }
                            else
                            {
                                slowoPoprzedzające.nodeList.Add(slowo);
                                slowoPoprzedzające = slowo;
                            }
                        }
                        catch (Exception)
                        {
                            
                            wordNode wdnd = new wordNode(VARIABLE);
                            slowoPoprzedzające.nodeList.Add(wdnd);
                            allNodes.Add(wdnd);
                            slowoPoprzedzające = wdnd;
                        }
                    }
                    stan = 1;
                }
                else//odpowiada
                {
                    wordNode slowo = allNodes[0];
                    while (slowo != null)
                    {
                        slowo = slowo.chooseNext();
                        if (slowo.word == "#")
                        {
                            break;
                        }
                        Console.Write(slowo.word+" ");
                    }
                    Console.WriteLine();
                    stan = 0;
                }
            }
        }
    }
}
