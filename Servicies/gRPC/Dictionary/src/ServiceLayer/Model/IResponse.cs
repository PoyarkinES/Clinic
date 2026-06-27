using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceLayer.Model
{
    public interface IResponse<T>
    {
        string Errors {  get; }
        bool Status {  get; }
        string Message {  get; }
        T Result {  get; }
    }
}
