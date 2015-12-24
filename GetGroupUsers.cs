using System; 
using System.Linq;  
using System.Net;  
using System.Text;  
using System.Collections.Generic;  
using Microsoft.SharePoint.Client;  
  
namespace spknowledge.csomsamples.GetGroupUsers  
{  
    class Program  
    {  
        static void Main(string[] args)  
        {  
            //Get Site Url fro user    
            Console.Write("Enter Site URL: ");  
            string strURL = Console.ReadLine();  
  
            //Get Username from user in the format of (Domain/Login ID)    
            Console.Write("Enter UserName (domain/userid): ");  
            string strUserName = Console.ReadLine();  
  
            Console.Write("Enter your password: ");  
            string pass = getPassword();  
            Console.WriteLine();  
            RETRY_GROUP:  
            Console.Write("Enter Group Name: ");  
            string groupName = Console.ReadLine();  
            Console.WriteLine();  
  
            try  
            {  
                ClientContext ctx = new ClientContext(strURL);  
                ctx.Credentials = new NetworkCredential(strUserName, pass);  
                Web web = ctx.Web;  
  
                Group group = web.SiteGroups.GetByName(groupName);  
                  
                //Parameters to receive response from the server                      
                ctx.Load(web, w => w.Title);  
                ctx.Load(group, grp => grp.Title, grp => grp.Users, grp => grp.Owner);  
                ctx.ExecuteQuery();  
  
                Console.WriteLine("Groups Name: " + group.Title);  
                Console.WriteLine("Users Count: " + group.Users.Count);  
                Console.WriteLine("Group Owner: " + group.Owner.Title);  
                Console.WriteLine("Users:");  
                foreach(User usr in group.Users)  
                {  
                    Console.WriteLine(usr.Title);  
                }  
            }  
            catch (Exception ex)  
            {  
                Console.WriteLine("Error: " + ex.Message);  
                Console.Write("Do you want to continue (y / n):");  
                ConsoleKeyInfo boolContinue = Console.ReadKey();  
                Console.WriteLine();  
                if (boolContinue.KeyChar == 'y')  
                    goto RETRY_GROUP;  
                else  
                    Console.Write("Enter to Exit.");  
            }  
  
            Console.Read();              
        }  
  
        private static string getPassword()  
        {  
            ConsoleKeyInfo key;  
            string pass = "";  
            do  
            {  
                key = Console.ReadKey(true);  
                // Backspace Should Not Work    
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)  
                {  
                    pass += key.KeyChar;  
                    Console.Write("*");  
                }  
                else  
                {  
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)  
                    {  
                        pass = pass.Substring(0, (pass.Length - 1));  
                        Console.Write("\b \b");  
                    }  
                }  
            }  
            // Stops Receving Keys Once Enter is Pressed    
            while (key.Key != ConsoleKey.Enter);  
            return pass;  
        }  
    }  
}  
