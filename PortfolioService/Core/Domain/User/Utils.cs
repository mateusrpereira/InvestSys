namespace Domain.User
{
    public static class Utils
    {
        public static bool ValidateEmail(string email)
        {
            //Implementar validação de e-mail...
            if (email == "b@b.com") return false;
            
            return true;
        }
    }
}
