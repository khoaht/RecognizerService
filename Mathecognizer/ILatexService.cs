using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace MathRecognizer
{
    [ServiceContract]
    public interface ILatexService
    {

        [OperationContract]
        string GetSingleLatex(int[] points);


        [OperationContract]
        string TestLatex();

    }


}