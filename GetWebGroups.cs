using System;  
using System.Linq;  
using System.Net;  
using System.Text;  
using System.Collections.Generic;  
using Microsoft.SharePoint.Client; 
  
namespace spknowledge.csomsamples.GetWebGroups  
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
  
            ClientContext ctx = new ClientContext(strURL);  
            ctx.Credentials = new NetworkCredential(strUserName, pass);  
            Web web = ctx.Web;  
            //Parameters to receive response from the server    
            //RoleAssignments property should be passed in Load method to get the collection of Groups assigned to the web    
            ctx.Load(web, w => w.Title);  
            RoleAssignmentCollection roleAssignments= web.RoleAssignments;  
            //RoleAssignment.Member property returns the group associated to the web  
            //RoleAssignement.RoleDefinitionBindings property returns the permissions associated to the group for the web  
            ctx.Load(roleAssignments, roleAssignement => roleAssignement.Include(r => r.Member, r => r.RoleDefinitionBindings));  
            ctx.ExecuteQuery();              
  
            Console.WriteLine("Groups has permission to the Web: " + web.Title);  
            Console.WriteLine("Groups Count: " + roleAssignments.Count.ToString());  
            Console.WriteLine("Group with Permissions as follows:");  
            foreach (RoleAssignment grp in roleAssignments)  
            {  
                string strGroup = "";                  
                strGroup += grp.Member.Title +" : ";  
                  
                foreach (RoleDefinition rd in grp.RoleDefinitionBindings)  
                {                  
                    strGroup += rd.Name+ " ";                   
                }  
                Console.WriteLine(strGroup);  
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
