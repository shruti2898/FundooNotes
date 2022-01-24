using FundooModels;
namespace FundooRepository.CustomException
{
    public class ArgumentValidator
    {
        public static void Validate(RegisterModel model)
        {
            if (model == null)
            {
                throw new CustomException("Registeration form is null");
            }
            else if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                throw new CustomException("FirstName is null or empty string or whitespace");
            }
            else if (string.IsNullOrWhiteSpace(model.LastName))
            {
                throw new CustomException("LastName is null or empty string or whitespace");
            }
            else if (string.IsNullOrWhiteSpace(model.Email))
            {
                throw new CustomException("Email is null or empty string or whitespace");
            }
            else if (string.IsNullOrWhiteSpace(model.Password))
            {
                throw new CustomException("Password is null or empty string or whitespace");
            }
        }

        public static void Validate(UserCredentialsModel model)
        {
            if (model == null)
            {
                throw new CustomException($"{nameof(model)} is null");
            }
            else if (string.IsNullOrWhiteSpace(model.UserEmail) && string.IsNullOrWhiteSpace(model.UserPassword))
            {
                throw new CustomException($"Email and Password is null or empty or whitespaces");
            }
            else if (string.IsNullOrWhiteSpace(model.UserEmail))
            {
                throw new CustomException("Email is null or empty string or whitespace");
            }
            else if (string.IsNullOrWhiteSpace(model.UserPassword))
            {
                throw new CustomException("Password is null or empty string or whitespace");
            }
        }

        public static void Validate(string Email)
        {
        if (string.IsNullOrWhiteSpace(Email))
        {
            throw new CustomException("Email is null or empty string or whitespace");
        }
        }
    }
}
