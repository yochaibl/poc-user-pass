using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace poc_user_pass
{
    class Program
    {

        private static bool checkValidity(String username, String password, String domain)
        {
            try {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain))
                {
                    // validate the credentials
                    return pc.ValidateCredentials(username, password);
                }
            }
            catch (PrincipalServerDownException) {
                return false;
            }
        }

        private static void printValidity(String username, String password, String domain)
        {
            bool valid = checkValidity(username, password, domain);
            Console.WriteLine(valid ? "Access granted" : "Access denied");
        }

        [System.Security.SecurityCritical(System.Security.SecurityCriticalScope.Everything)]
        static void Main(string[] args)
        {
            printValidity("yochai", "Password1!", "nosuchdomain"); // no such domain
            printValidity("nosuchuser", "Password1!", "tal"); // no such user
            printValidity("yochai", "wrong password", "tal"); // wrong password
            printValidity("yochai", "Password1!", "tal"); // valid
            Console.Read();
        }
    }
}
