// CSOM Package (16.1.3912.1204) 
// Gets and sets the current Time Zone settings from the given SharePoint site
// Get access to source site  
using (var ctx = new ClientContext("https://spknowledge.sharepoint.com"))  
{  
    //Provide count and pwd for connecting to the source  
    var passWord = new SecureString();  
    foreach (char c in "<mypassword>".ToCharArray()) passWord.AppendChar(c);  
    ctx.Credentials = new SharePointOnlineCredentials("<office 365 mail id>", passWord);  
  
    // Actual code for operations  
    Web web = ctx.Web;  
    RegionalSettings regSettings = web.RegionalSettings;  
    ctx.Load(web);  
    ctx.Load(regSettings); //To get regional settings properties  
    Microsoft.SharePoint.Client.TimeZone currentTimeZone = regSettings.TimeZone;  
    ctx.Load(currentTimeZone);  //To get the TimeZone propeties for the current web region settings  
    ctx.ExecuteQuery();  
      
    //Get the current site TimeZone  
    Console.WriteLine(string.Format("Connected to site with title of {0}", web.Title));      
    Console.WriteLine("Current TimeZone Settings: " + currentTimeZone.Id.ToString() +" - "+ currentTimeZone.Description);  
  
    //Update the TimeZone setting to (UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi. TimeZone Id is 23  
    TimeZoneCollection globalTimeZones = RegionalSettings.GetGlobalTimeZones(ctx);  
    ctx.Load(globalTimeZones);  
    ctx.ExecuteQuery();  
  
    Microsoft.SharePoint.Client.TimeZone newTimeZone = globalTimeZones.GetById(23);  
    regSettings.TimeZone = newTimeZone;  
    regSettings.Update();  //Update New settings to the web  
    ctx.ExecuteQuery();  
  
    Console.WriteLine("New TimeZone settings are updated.");      
    Console.ReadLine();  
}  
