using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerProject.ProblemCollection
{
    public class ProblemBase
    {
        public ProblemBase() { }

        public virtual string Description{get{return "";}}
        public virtual int ProblemNumber { get { return -1; } }
        
        public virtual string Solution1() { throw new SolutionNotImplementedException("not overloaded"); }
        public virtual string Solution2() { throw new SolutionNotImplementedException("not overloaded"); }
        public virtual string Solution3() { throw new SolutionNotImplementedException("not overloaded"); }
        public virtual string Solution4() { throw new SolutionNotImplementedException("not overloaded"); }
        public virtual string Solution5() { throw new SolutionNotImplementedException("not overloaded"); }
        public virtual string Solution6() { throw new SolutionNotImplementedException("not overloaded"); }
        public virtual string Solution7() { throw new SolutionNotImplementedException("not overloaded"); }
        public virtual string Solution8() { throw new SolutionNotImplementedException("not overloaded"); }
        public virtual string Solution9() { throw new SolutionNotImplementedException("not overloaded"); }
        public virtual string Solution10() { throw new SolutionNotImplementedException("not overloaded"); }
    }

    public class SolutionNotImplementedException : ApplicationException
    {
        public SolutionNotImplementedException(string message)
            : base(message)
        {
            
        }
    }
}
