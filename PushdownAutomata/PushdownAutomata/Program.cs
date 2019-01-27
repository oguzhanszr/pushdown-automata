using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushdownAutomata
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PDA> PdaList = new List<PDA>();
            int value = 0;

            while(true)
            {
                Console.WriteLine("0.Quit");
                Console.WriteLine("1.Define a PDA");
                Console.WriteLine("2.Show all PDAs");
                Console.WriteLine("3.Test PDA");
                Console.WriteLine("4.Delete PDA");
                Console.Write("Input ..: ");
                value = Convert.ToInt32(Console.ReadLine());
                
                if(value == 1)
                {
                    Console.Clear();
                    PDA pda = new PDA();
                    PdaList.Add(pda.DefinePDA());
                }
                else if(value == 2)
                {
                    Console.Clear();
                    Write(PdaList);
                }
                else if(value == 3)
                {
                    Console.Clear();
                    Write(PdaList);
                    int pdaId = -1;
                    Console.Write("Id ..: ");
                    pdaId = Convert.ToInt32(Console.ReadLine());

                    if (pdaId < 0 || !(PdaList.Count > pdaId))
                    {
                        Console.WriteLine("Not valid an input");
                        continue;
                    }

                    

                    Console.Clear();
                    if(PdaList[pdaId].Run())
                    {
                        Console.WriteLine("Accept");
                    }
                    else
                    {
                        Console.WriteLine("Reject");
                    }
                }
                else if (value == 4)
                {
                    Console.Clear();
                    Write(PdaList);
                    int pdaId = -1;
                    Console.Write("Id ..: ");
                    pdaId = Convert.ToInt32(Console.ReadLine());

                    if (pdaId < 0 || !(PdaList.Count > pdaId))
                    {
                        Console.WriteLine("Not valid an input");
                        continue;
                    }

                    PdaList.RemoveAt(pdaId);
                }
                else if (value == 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Not valid an input");
                }
            }

        }

        public static void Write(List<PDA> PdaList)
        {
            int counter = 0;
            foreach (var pda in PdaList)
            {
                Console.WriteLine("#Id :" + counter);
                Console.WriteLine("PDA Name : " + pda.Name);

                string alphabet = "";
                foreach (var str in pda.Alphabet)
                    alphabet += str + ",";
                alphabet = alphabet.Substring(0, alphabet.Length - 1);
                Console.WriteLine("Alphabet : { " + alphabet + " }");

                string stackAlphabet = "";
                foreach (var str in pda.StackAlphabet)
                    stackAlphabet += str + ",";
                stackAlphabet = stackAlphabet.Substring(0, stackAlphabet.Length - 1);
                Console.WriteLine("Stack Alphabet : { " + stackAlphabet + " }");

                string stateList = "";
                foreach (var state in pda.States)
                    stateList += state.StateName + ",";
                stateList = stateList.Substring(0,stateList.Length -1);
                Console.WriteLine("States : { " + stateList + " }");

                Console.WriteLine("State Inıt : { " + pda.StateInıt.StateName + " }");
                string finalState = pda.FinalState == null ? "" : pda.FinalState.StateName;
                Console.WriteLine("Final State : { " + finalState + " }");

                Console.WriteLine("Transition functions :");
                for(int i = 0; i < pda.TransitionFunctions.Count; i++)
                {
                    Console.WriteLine(i+1+": "+pda.TransitionFunctions[i].RuleString);
                }


                Console.WriteLine("===============================");
                counter++;
            }
        }
    }
}
