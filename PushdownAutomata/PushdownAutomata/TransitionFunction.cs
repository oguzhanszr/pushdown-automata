using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushdownAutomata
{
    class TransitionFunction
    {
        public string PdaName { get; set; }

        public string State { get; set; }

        public string Symbol { get; set; }

        public string StackSymbol { get; set; }

        public string TransitionState { get; set; }
        
        public string TransitionStackSymbol { get; set; }

        public string RuleString { get; set; }

        public TransitionFunction(string pdaName, string state, string symbol, string stackSymbol, string transitionState, string transitionStackSymbol, string ruleString)
        {
            this.PdaName = pdaName;
            this.State = state;
            this.Symbol = symbol;
            this.StackSymbol = stackSymbol;
            this.TransitionState = transitionState;
            this.TransitionStackSymbol = transitionStackSymbol;
            this.RuleString = ruleString;
        }
    }
}
