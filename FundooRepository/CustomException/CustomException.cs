using System;

namespace FundooRepository.CustomException
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {

        }
    }
    public class CustomArgumentException : CustomException
    {
        public CustomArgumentException(string message) : base(message)
        {

        }
    }
    public class CustomExistingDataException : CustomException
    {
        public CustomExistingDataException(string message) : base(message)
        {

        }
    }
    public class CustomNotFoundException : CustomException
    {
        public CustomNotFoundException(string message) : base(message)
        {

        }
    }
    public class CustomUnauthorizedException : CustomException
    {
        public CustomUnauthorizedException(string message) : base(message)
        {

        }
    }
}
