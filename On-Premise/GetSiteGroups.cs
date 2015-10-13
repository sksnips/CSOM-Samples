//Dlls to include
//Microsoft.SharePoint.Client.dll
//Microsoft.SharePoint.Client.Runtime.dll
//The following example returns all the Groups available in Site using SharePoint's Managed Client Side Object Model.

using Microsoft.SharePoint.Client;  
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Net;  
using System.Text;  
  
namespace GetSiteGroups  
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
            //SiteGroups property should be passed in Load method to get the collection of groups    
            ctx.Load(web, w => w.Title, w => w.SiteGroups);  
            ctx.ExecuteQuery();  
  
            GroupCollection groups = web.SiteGroups;  
              
            Console.WriteLine("Groups associated to the site: " + web.Title);  
            Console.WriteLine("Groups Count: " + groups.Count.ToString());  
            foreach(Group grp in groups)  
            {  
                Console.WriteLine(grp.Title);  
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
