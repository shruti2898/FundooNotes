using FundooModels;
namespace FundooRepository.CustomException
{
    public class ArgumentValidator
    {
        public static void Validate(RegisterModel model)
        {
            if (model == null)
            {
                throw new CustomArgumentException("Registeration form is null");
            }
            else if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                throw new CustomArgumentException("FirstName is null or empty string or whitespace");
            }
            else if (string.IsNullOrWhiteSpace(model.LastName))
            {
                throw new CustomArgumentException("LastName is null or empty string or whitespace");
            }
            else if (string.IsNullOrWhiteSpace(model.Email))
            {
                throw new CustomArgumentException("Email is null or empty string or whitespace");
            }
            else if (string.IsNullOrWhiteSpace(model.Password))
            {
                throw new CustomArgumentException("Password is null or empty string or whitespace");
            }
        }

        public static void Validate(UserCredentialsModel model)
        {
            if (model == null)
            {
                throw new CustomArgumentException($"{nameof(model)} is null");
            }
            else if (string.IsNullOrWhiteSpace(model.UserEmail) && string.IsNullOrWhiteSpace(model.UserPassword))
            {
                throw new CustomArgumentException($"Email and Password is null or empty or whitespaces");
            }
            else if (string.IsNullOrWhiteSpace(model.UserEmail))
            {
                throw new CustomArgumentException("Email is null or empty string or whitespace");
            }
            else if (string.IsNullOrWhiteSpace(model.UserPassword))
            {
                throw new CustomArgumentException("Password is null or empty string or whitespace");
            }
        }

        public static void Validate(string Email)
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                throw new CustomArgumentException("Email is null or empty string or whitespace");
            }
        }
    }
}
