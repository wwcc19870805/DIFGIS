using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DF2DTool.Interface
{
    public interface ICadWrite
    {
        

        string OutputFileName { set; }

        DataSet CurrentDs { set; }

        double MapScale { set; }

        bool IfoutputZ { set; }

        //		IEnvelope PEnv          {set;}

        void Process();
    }
}
