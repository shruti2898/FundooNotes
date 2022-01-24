using System;

namespace FundooRepository.CustomException
{
    public class CustomException : Exception
    {   
        public CustomException(string message) : base(message)
        {

        }
    }
}
