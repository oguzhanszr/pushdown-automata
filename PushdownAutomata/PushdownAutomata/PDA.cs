using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PushdownAutomata
{
    class PDA
    {
        public string Name { get; set; }

        public List<string> Alphabet { get; set; }

        public List<string> StackAlphabet { get; set; }

        public List<State> States { get; set; }

        public Stack<string> Stack { get; set; }

        public string StackInıt { get; set; }

        public State StateInıt { get; set; }

        public State FinalState { get; set; }

        public List<TransitionFunction> TransitionFunctions { get; set; }

        public PDA()
        {
            this.Alphabet = new List<string>();
            this.StackAlphabet = new List<string>();
            this.States = new List<State>();
            this.Stack = new Stack<string>();
            this.TransitionFunctions = new List<TransitionFunction>();
        }

        public PDA DefinePDA()
        {
            //Console.Clear();
            Console.Write("PDA Name..:");
            this.Name = Console.ReadLine();

            Console.Write("Alphabet(Input Format = a,b,..,x) ..:");
            this.Alphabet = Console.ReadLine().Split(',').ToList();

            Console.Write("Stack Alphabet(Input Format = a,b,..,x) ..:");
            this.StackAlphabet = Console.ReadLine().Split(',').ToList();

            Console.Write("Stack Inıt Symbol ..:");
            string initSymbol = Console.ReadLine();
            if(StackAlphabet.Any(x => x == initSymbol) != true)
            {
                throw new Exception("Inıt symbol must be in the stack alphabet");
            }
            this.StackInıt = initSymbol;

            /*States ,init state and final state input*/
            Console.Write("States(Input Format = a,b,..,x) ..:");
            List<string> states = Console.ReadLine().Split(',').ToList();
            //this.States = new List<State>();
            foreach(var state in states)
            {
                this.States.Add(new State(state));
            }

            Console.Write("State Inıt ..:");
            string stateInıt = Console.ReadLine();
            if(this.States.Any(x => x.StateName == stateInıt) != true)
            {
                throw new Exception("Inıt state must be in the state list");
            }
            this.StateInıt = this.States.FirstOrDefault(x => x.StateName == stateInıt);

            Console.Write("Final State (Nullable) ..:");
            string finalState = Console.ReadLine();
            if (finalState == "")
            {
                this.FinalState = null;
            }
            else
            {
                if (this.States.Any(x => x.StateName == finalState))
                    this.FinalState = this.States.FirstOrDefault(x => x.StateName == finalState);
                else
                    throw new Exception("Final state must be in the state list");
            }

            /*Transition functions input*/
            Console.WriteLine("Transition Functions(Input Format = (state,symbol,stack)=>(newState,newStack) )");
            Console.WriteLine("Null symbol and pop symbol == £ ");
            Console.WriteLine("Stack stay(don't change the stack) symbol == #");

            //Regex
            //string myRegexFormat = @"\([A-z][0-9]*?,.,[A-z][0-9]*?\)=>\([A-z][0-9]*?,[A-z][0-9]*?\)";v1
            //string myRegexFormat = @"\([A-z][0-9]*?,.,[A-z][0-9]*?\)=>\([A-z][0-9]*?,[A-z][0-9]*?\)|\([A-z][0-9]*?,£\)";v2
            string myRegexFormat = @"\([A-z][0-9]*?,.,[A-z][0-9]*?\)=>\([A-z][0-9]*?,[A-z][0-9]*?\)|\([A-z][0-9]*?,[£#]\)";
            Regex regex = new Regex(myRegexFormat);

            while (true)
            {
                Console.Write("Input(0.Quit) ..:");
                string input = Console.ReadLine();

                if (input == "0")
                    break;

                if(regex.Match(input).Success)
                {
                    string[] inputList = input.Split('=');
                    string[] inputValues1 = inputList[0].Replace("(", "").Replace(")", "").Split(',');
                    string[] inputValues2 = inputList[1].Replace(">","").Replace("(","").Replace(")","").Split(',');

                    string state = inputValues1[0];
                    string symbol = inputValues1[1];
                    string stackSymbol = inputValues1[2];

                    string transitionState = inputValues2[0];
                    string transitionStackSymbol = inputValues2[1];

                    if (!this.States.Any(x => x.StateName == state) || !this.States.Any(x => x.StateName == transitionState))
                        throw new Exception("State must be in the state list");

                    if (!this.Alphabet.Any(x => x == symbol))
                    {
                        if (stackSymbol == "£" || transitionStackSymbol == "£" || transitionStackSymbol == "#")
                            ;//ignored £ and # symbol
                        else
                            throw new Exception("This symbol not supported");

                    }

                    if (!this.StackAlphabet.Any(x => x == stackSymbol) || !this.StackAlphabet.Any(x => x == transitionStackSymbol))
                    {
                        if (stackSymbol == "£" || transitionStackSymbol == "£" || transitionStackSymbol == "#")
                            ;//ignored £ and # symbol
                        else
                            throw new Exception("Stack symbol must be in the stack alphabet");

                    }

                    if (this.TransitionFunctions.Any(x => x.RuleString == input))
                        throw new Exception("This rule already exist.");

                    this.TransitionFunctions.Add(new TransitionFunction(this.Name, state, symbol, stackSymbol, transitionState, transitionStackSymbol, input));

                    Console.WriteLine("Added..." +input);
                }
                else
                {
                    Console.WriteLine("Invalid format. Ex:(q0,a,z0)=>(q1,a)");
                }
            }

            return this;
            
        }

        public bool Run()
        {
            Ready();

            string input = "";
            Console.Write("String ..: ");
            input = Console.ReadLine();
            input += "£";
            State currentState = this.StateInıt;
            Stack<string> log = new Stack<string>();

            for (int i = 0; i < input.Length; i++)
            {
                List<TransitionFunction> filter = this.TransitionFunctions.FindAll(x => x.State == currentState.StateName);

                filter = filter.FindAll(x => input[i].Equals(x.Symbol.ToArray()[0]) && x.StackSymbol == this.Stack.Peek());
                if (filter.Count != 1)
                    return false;

                
                currentState = this.States.FirstOrDefault(x => x.StateName == filter.First().TransitionState);
                if(filter.First().TransitionStackSymbol == "£")
                {
                    this.Stack.Pop();
                }
                else if(filter.First().TransitionStackSymbol == "#")
                {
                    //Stack did not change
                }
                else
                {
                    this.Stack.Push(filter.First().TransitionStackSymbol);
                }



                Stack<string> tempStack = new Stack<string>(this.Stack);
                string stack = "";
                if (tempStack.Count > 0)
                {
                    while(tempStack.Count != 0)
                    {
                        stack += tempStack.Pop();
                    }
                }
                else
                {
                    stack = "£";
                }
                    

                log.Push("#" + i + "curr:" + currentState.StateName + "," + stack + " | mov:" + filter.First().RuleString);

                Console.WriteLine(log.Pop());
                

            }
            if (this.FinalState != null)
            {
                if (currentState == this.FinalState)
                    return true;
            }
            else
            {
                if (this.Stack.Count == 0)
                    return true;
            }

            return false;
        }

        private void Ready()
        {
            this.Stack.Clear();
            this.Stack.Push(this.StackInıt);
        }
    }
}
