using System.Xml.Serialization;
using PP.Library.Models;

namespace PP.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var clientList = new List<Client>();
            var projectList = new List<Project>();

            while (true)
            {

                Console.WriteLine("Client Mode or Project Mode? (C or P) (X to exit): ");
                var choice = Console.ReadLine() ?? "X";

                if (choice.Equals("x", StringComparison.InvariantCultureIgnoreCase)) { break; }
                else if (choice.Equals("c", StringComparison.InvariantCultureIgnoreCase))
                {
                    ClientMode(clientList);
                
                }
                else if (choice.Equals("p", StringComparison.InvariantCultureIgnoreCase))
                {
                    ProjectMode(projectList, clientList);
                }
                else
                {
                    Console.WriteLine("Please choose a valid option...");
                }
            }
        }

        static void ClientMode(List<Client> clientList)
        {
            while (true)
            {
                Console.WriteLine("\nCreate a Client (C)");
                Console.WriteLine("Read Client List (R)");
                Console.WriteLine("Update a Client (U)");
                Console.WriteLine("Delete a Client (D)");
                Console.WriteLine("Exit Client Mode (X)");

                var choice = Console.ReadLine() ?? "X";

                if (choice.Equals("x", StringComparison.InvariantCultureIgnoreCase)) { break; }
                else if (choice.Equals("c", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Id: ");
                    var id = int.Parse(Console.ReadLine() ?? "0");

                    Console.WriteLine("Open Date (Month/Day/Year): ");
                    var openDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Today.ToString());
                    

                    Console.WriteLine("Close Date (Month/Day/Year): ");
                    var closeDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Today.ToString());

                    Console.WriteLine("Is the Client Active? ((T)rue or (F)alse): ");
                    var tOrF = Console.ReadLine() ?? "F";
                    Boolean active;
                    if (tOrF.Equals("T", StringComparison.InvariantCultureIgnoreCase))
                    {
                        active = true;
                    }
                    else
                    {
                        active = false;
                    }

                    Console.WriteLine("Client Name: ");
                    var name = Console.ReadLine() ?? "NULL";

                    Console.WriteLine("Client Notes: ");
                    var notes = Console.ReadLine() ?? "NULL";

                    clientList.Add(
                        new Client
                        {
                            Id = id,
                            OpenDate = openDate,
                            CloseDate = closeDate,
                            isActive = active,
                            Name = name,
                            Notes = notes
                        }
                    );

                }
                else if (choice.Equals("r", StringComparison.InvariantCultureIgnoreCase))
                {
                    clientList.ForEach(Console.WriteLine);
                }
                else if (choice.Equals("u", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Which Client should be updated? (ID): ");
                    var clientID = int.Parse(Console.ReadLine() ?? "0");

                    var clientToUpdate = clientList.FirstOrDefault(i => i.Id == clientID);

                    if (clientToUpdate != null) 
                    {
                        Console.WriteLine("New ID: ");
                        clientToUpdate.Id = int.Parse(Console.ReadLine() ?? "0");

                        Console.WriteLine("New Open Date (Month/Day/Year): ");
                        clientToUpdate.OpenDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Today.ToString());

                        Console.WriteLine("New Close Date (Month/Day/Year): ");
                        clientToUpdate.CloseDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Today.ToString());

                        Console.WriteLine("Is the Client Active? ((T)rue or (F)alse): ");
                        var tOrF = Console.ReadLine() ?? "F";
                        if (tOrF.Equals("T", StringComparison.CurrentCultureIgnoreCase))
                        {
                            clientToUpdate.isActive = true;
                        }
                        else
                        {
                            clientToUpdate.isActive = false;
                        }

                        Console.WriteLine("New Client Name: ");
                        clientToUpdate.Name = Console.ReadLine() ?? "NULL";

                        Console.WriteLine("New Client Notes: ");
                        clientToUpdate.Notes = Console.ReadLine() ?? "NULL";
                    }
                }
                else if (choice.Equals("d", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Which Client should be deleted? (ID): ");
                    var clientID = int.Parse(Console.ReadLine() ?? "0");

                    var clientToDelete = clientList.FirstOrDefault(i =>  i.Id == clientID);

                    if (clientToDelete != null) { clientList.Remove(clientToDelete); }
                
                }
                else
                { Console.WriteLine("Please select a valid option..."); }
         
            }
        }

        static void ProjectMode(List<Project> projectList, List<Client> clientList)
        {
            while (true)
            {
                Console.WriteLine("\nCreate a Project (C)");
                Console.WriteLine("Read Project List (R)");
                Console.WriteLine("Update a Project (U)");
                Console.WriteLine("Delete a Project (D)");
                Console.WriteLine("Exit Project Mode (X)");

                var choice = Console.ReadLine() ?? "X";

                if (choice.Equals("x", StringComparison.InvariantCultureIgnoreCase)) { break; }
                else if (choice.Equals("c", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Id: ");
                    var id = int.Parse(Console.ReadLine() ?? "0");

                    Console.WriteLine("Open Date (Month/Day/Year): ");
                    var openDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Today.ToString());


                    Console.WriteLine("Close Date (Month/Day/Year): ");
                    var closeDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Today.ToString());

                    Console.WriteLine("Is the Project Active? ((T)rue or (F)alse): ");
                    var tOrF = Console.ReadLine() ?? "F";
                    Boolean active;
                    if (tOrF.Equals("T", StringComparison.CurrentCultureIgnoreCase))
                    {
                        active = true;
                    }
                    else
                    {
                        active = false;
                    }

                    Console.WriteLine("Short Name: ");
                    var shortName = Console.ReadLine() ?? "NULL";

                    Console.WriteLine("Long Name: ");
                    var LongName = Console.ReadLine() ?? "NULL";

                    Console.WriteLine("Would you like to link this Project with a Client? (T or F)");

                    var clientID = 0;
                    var updateClient = Console.ReadLine() ?? "F";
                    if (updateClient.Equals("T", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Console.WriteLine("Client ID: ");
                        var clientIDtoFind = int.Parse(Console.ReadLine() ?? "0");
                        Boolean found = false;
                        foreach (var client in clientList)
                        {
                            if (client.Id == clientIDtoFind)
                            {
                                Console.WriteLine($"{client.Id}");
                                clientID = client.Id;
                                found = true;
                            }
                                
                        }
                        if (!found)
                            Console.WriteLine("Client Not found");
                        
                    }

                    projectList.Add(
                        new Project
                        {
                            Id = id,
                            OpenDate = openDate,
                            CloseDate = closeDate,
                            isActive = active,
                            ShortName = shortName,
                            LongName = LongName,
                            ClientID = clientID
                        }
                    );
                }
                else if (choice.Equals("r", StringComparison.InvariantCultureIgnoreCase))
                {
                    projectList.ForEach(Console.WriteLine);
                }
                else if (choice.Equals("u", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Which Project should be deleted? (ID): ");
                    var projectID = int.Parse(Console.ReadLine() ?? "0");

                    var projectToUpdate = projectList.FirstOrDefault(i => i.Id == projectID);

                    if (projectToUpdate != null) 
                    {
                        Console.WriteLine("New ID: ");
                        projectToUpdate.Id = int.Parse(Console.ReadLine() ?? "0");

                        Console.WriteLine("New Open Date (Month/Day/Year): ");
                        projectToUpdate.OpenDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Today.ToString());

                        Console.WriteLine("New Close Date (Month/Day/Year): ");
                        projectToUpdate.CloseDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Today.ToString());

                        Console.WriteLine("Is the Project Active? ((T)rue or (F)alse): ");
                        var tOrF = Console.ReadLine() ?? "F";
                        if (tOrF.Equals("T", StringComparison.CurrentCultureIgnoreCase))
                        {
                            projectToUpdate.isActive = true;
                        }
                        else
                        {
                            projectToUpdate.isActive = false;
                        }

                        Console.WriteLine("New Project Name: ");
                        projectToUpdate.ShortName = Console.ReadLine() ?? "NULL";

                        Console.WriteLine("New Project Notes: ");
                        projectToUpdate.LongName = Console.ReadLine() ?? "NULL";

                        Console.WriteLine("Would you like to link to a new Client? (T or F)");
                        var updateClient = Console.ReadLine() ?? "F";
                        if (updateClient.Equals("T", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Console.WriteLine("Client ID: ");
                            var clientIDtoFind = int.Parse(Console.ReadLine() ?? "0");
                            Boolean found = false;
                            foreach (var client in clientList)
                            {
                                if (client.Id == clientIDtoFind)
                                    projectToUpdate.ClientID = client.Id;
                                    found = true;
                            }
                            if (!found)
                                Console.WriteLine("Client Not found");
                        }

                    }
                }
                else if (choice.Equals("d", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Which Project should be deleted? (ID): ");
                    var projectID = int.Parse(Console.ReadLine() ?? "0");

                    var projectToDelete = projectList.FirstOrDefault(i => i.Id == projectID);

                    if (projectToDelete != null) { projectList.Remove(projectToDelete); }
                }
                else
                { Console.WriteLine("Please select a valid option..."); }
            }

        }
    }
}