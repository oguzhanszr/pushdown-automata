using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushdownAutomata
{
    class State
    {
        public string StateName { get; set; }

        public List<string> Rules { get; set; }

        public State(string stateName)
        {
            this.StateName = stateName;
        }
    }
}
