using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
namespace VPMidTask2
{
    class DataHandler
    {
        static void Main(string[] args)
        {
            int choice;
            string id="";
            string name="";
            string date="";
            string status="";
            bool sold=false;
            bool available=false;
            InputHandler inputHandler = new InputHandler();
            do
            {
                Console.Clear();
                Console.WriteLine("\t\tProducts Menu\n");
                Console.WriteLine("==>>\t.1\tADD PRODUCT\n");
                Console.WriteLine("==>>\t.2\tSHOW PRODUCT\n");
                Console.WriteLine("==>>\t.3\tMODIFY PRODUCT\n");
                Console.WriteLine("==>>\t.4\tSEARCH PRODUCT\n");
                Console.WriteLine("==>>\t.5\tDELETE PRODUCT\n");
                Console.WriteLine("==>>\t.6\tEXIT\n");
                Console.Write("Your choice: ");
                choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    addProduct();
                }
                else if (choice == 2)
                {
                    showProduct();
                }
                else if (choice == 3)
                {
                    modifyProduct();
                }
                else if (choice == 4)
                {
                    searchProduct();
                }
                else if (choice == 5)
                {
                    deleteProduct();
                }
                else if (choice == 6)
                {
                    Environment.Exit(0);
                }
            } while (true);
           
            void addProduct()
            {
                List<bool> allValuesFalg;//Will have 4 true falgs to show all values were entered properly
                bool MainLoopflag = false;//To keep the add menu in loop
                bool InnerLoopflag = false;//To make sure user enters the correct choice
                do
                {
                    allValuesFalg = new List<bool>();
                    Console.Clear();
                    Console.WriteLine("\t\tADD PRODUCT\n");
                    Console.Write("==>>\t.1\tEnter Product ID: ");
                    id = Console.ReadLine();
                    if (inputHandler.IDValidation(id))//Checking if enetered ID matches the Regular Expression
                    {
                        allValuesFalg.Add(true);//Adding true for correct ID input
                        nameloop://Goto loop to keep asking for the name in case of wrong input
                        Console.Write("\n");
                        Console.Write("==>>\t.2\tEnter Product Name: ");
                        name = Console.ReadLine();
                        if(inputHandler.NameValidation(name))//Checking if the entered name matches the Regular Expression
                        {
                            allValuesFalg.Add(true);//Adding true for correct name input
                            dateloop://Goto loop to keep asking for the date in case of wrong input
                            Console.Write("\n");
                            Console.Write("==>>\t.3\tEnter Date of Entrance : ");
                            date = Console.ReadLine();
                            if (inputHandler.DateValidation(date))//Checking if the entered date matches the Regular Expression
                            {
                                allValuesFalg.Add(true);// Adding true for correct date input
                                statusloop://Goto loop to keep asking for the product status in case of wrong input
                                Console.Write("\n");
                                Console.Write("==>>\t.4\tEnter Sold status 1 for avaiable and 0 for sold : ");
                                status = Console.ReadLine();
                                if(inputHandler.StatusValidation(status))//Checking if the entered status matches the Regular Expression
                                {
                                    allValuesFalg.Add(true);//Adding true for correct status input
                                    if (status=="1")
                                    {
                                        sold = false;
                                        available = true;

                                    }
                                    else
                                    {
                                        available = false;
                                        sold = true;
                                    }
                                }
                                else
                                {
                                    Console.Write("\n==>>\t.Error\t Product Status can be either 1 or 0 \n");
                                    goto statusloop;

                                }
                            }
                            else
                            {
                                Console.Write("\n==>>\t.Error\t Date can be entered in this format 01 May 2020 \n");
                                goto dateloop;
                            }

                        }
                        else
                        {
                            Console.Write("\n==>>\t.Error\tName can only have alphanumeric characters \n");
                            goto nameloop;
                        }
                    }
                    else
                    {
                        Console.Write("\n==>>\t.Error\tID can only have numeric characters \n");
                    }
                    Console.Write("\n==>>\t.Choice\tPress 1 to add record again : Press 0 to exit: ");
                    // Checking if the user wants to add another record
                    string again=Console.ReadLine();
                    do
                    {
                        if (inputHandler.StatusValidation(again))//Checking if the user's choice input matches the Regular Expression
                        {
                            InnerLoopflag = true;
                            if(again=="1")
                            {
                                MainLoopflag = true;
                            }
                            else
                            {
                                MainLoopflag = false;
                            }
                        }
                    } while (InnerLoopflag == false);//Keep the user in loop unless he chooses the right option
                } while (MainLoopflag);
                //Checking if list has 4 falgs
                if(allValuesFalg.Count==4)
                {
                    inputHandler.insertRecord(id, name, date, sold, available);//Inserting data in database
                }
            }

            void showProduct()
            {
                Console.Clear();
                Console.WriteLine("\t\tALL Products\n");
                Console.Write("Product ID");
                Console.Write("\tProduct Name");
                Console.Write("\tEntry Date");
                Console.Write("\tProduct Sold");
                Console.WriteLine("\tProduct Available");
                // Intializing a datatable with all the records in the database after running the query
                DataTable AllProducts = inputHandler.showRecords();
                // Looping through the datatable
                foreach (DataRow row in AllProducts.Rows)
                {
                    foreach (DataColumn col in AllProducts.Columns)
                    {
                        Console.Write(row[col].ToString() + "\t\t");
                    }
                    Console.WriteLine("\n");
                }
                //Giving user the choice to exit
                Console.WriteLine("==>>\t.1\tEXIT\n");
                Console.Write("Your choice: ");
                Console.ReadLine();
            }

            void searchProduct()
            {
                string searchChoice;
                string value;    
                SqlDataReader reader=null;// Rader will be used to traverse through the query output
                bool MainLoopflag = false;//To keep the search menu in loop
                bool InnerLoopflag = false;//To make sure user enters the correct choice
                do
                {
                    Console.Clear();
                    Console.WriteLine("\t\tSearch Menu\n");
                    Console.WriteLine("==>>\t.1\tProduct ID\n");
                    Console.WriteLine("==>>\t.2\tProduct Name\n");
                    Console.WriteLine("==>>\t.3\tEntry Date\n");
                    Console.WriteLine("==>>\t.4\tProduct Sold\n");
                    Console.WriteLine("==>>\t.5\tProduct Available\n");
                    Console.WriteLine("==>>\t.6\tEXIT\n");
                    Console.Write("Your choice: ");
                    searchChoice = (Console.ReadLine());
                    Console.Write("\n");
                    if (inputHandler.ChoiceValidation(searchChoice))//Checking if choice matches Regular Expression
                    {
                        if (searchChoice != "6")//If the user didn't choose exit option
                        {
                        loop://Goto loop for keep asking for the proper value user wants to search
                            Console.Write("Enter value: ");
                            value = (Console.ReadLine());
                            if (searchChoice == "1")
                            {
                                if (inputHandler.IDValidation(value))//Checking if enetered ID matches the Regular Expression
                                {
                                    //Reader has the search result after exeuting the query to search from ID
                                    reader = inputHandler.Search(value, int.Parse(searchChoice));
                                }
                                else
                                {
                                    Console.Write("\n==>>\t.Error\tID can only have numeric characters \n");
                                    goto loop;
                                }
                            }
                            if (searchChoice == "2")
                            {
                                if (inputHandler.NameValidation(value))//Checking if enetered name matches the Regular Expression
                                {
                                    //Reader has the search result after exeuting the query to search from name
                                    reader = inputHandler.Search(value, int.Parse(searchChoice));
                                }
                                else
                                {
                                    Console.Write("\n==>>\t.Error\tName can only have alphanumeric characters \n");
                                    goto loop;
                                }
                            }
                            if (searchChoice == "3")
                            {
                                if (inputHandler.DateValidation(value))//Checking if enetered date matches the Regular Expression
                                {
                                    //Reader has the search result after exeuting the query to search from date
                                    reader = inputHandler.Search(value, int.Parse(searchChoice));
                                }
                                else
                                {
                                    Console.Write("\n==>>\t.Error\t Date can be entered in this format 01 May 2020 \n");
                                    goto loop;
                                }
                            }
                            if (searchChoice == "4" || searchChoice == "5")
                            {
                                if (inputHandler.StatusValidation(value))//Checking if enetered status matches the Regular Expression
                                {
                                    //Reader has the search result after exeuting the query to search from status
                                    reader = inputHandler.Search(value, int.Parse(searchChoice));
                                }
                                else
                                {
                                    Console.Write("\n==>>\t.Error\t Product Status can be either 1 or 0 \n");
                                    goto loop;
                                }
                            }
                            //If the search result has rows
                            if (reader.HasRows)
                            {
                                Console.Clear();
                                Console.WriteLine("\t\tSearched Products\n");
                                Console.Write("Product ID");
                                Console.Write("\tProduct Name");
                                Console.Write("\tEntry Date");
                                Console.Write("\tProduct Sold");
                                Console.WriteLine("\tProduct Available");
                                // Traversing reader
                                while (reader.Read())
                                {
                                    Console.Write(reader[0] + "\t\t");
                                    Console.Write(reader[1] + "\t\t");
                                    Console.Write(reader[2] + "\t\t");
                                    Console.Write(reader[3] + "\t\t");
                                    Console.Write(reader[4] + "\t\t");

                                }

                            }
                            else
                            {
                                Console.WriteLine("No record found.");
                            }
                            reader.Close();
                        }
                    }
                    Console.Write("\n\n==>>\t.Choice\tPress 1 to search record again : Press 0 to exit: ");
                    // Checking if the user wants to search another record
                    string again = Console.ReadLine();
                    do
                    {
                        if (inputHandler.StatusValidation(again))//Checking if the user's choice input matches the Regular Expression
                        {
                            InnerLoopflag = true;
                            if (again == "1")
                            {
                                MainLoopflag = true;
                            }
                            else
                            {
                                MainLoopflag = false;
                            }
                        }
                    } while (InnerLoopflag == false);//Keep the user in loop unless he chooses the right option
                } while (MainLoopflag);
            }

            void modifyProduct()
            {
                string updateChoice;
                string value;
                bool MainLoopflag = false;//To keep the add menu in loop
                bool InnerLoopflag = false;//To make sure user enters the correct choice
                do
                {
                    Console.Clear();
                    Console.WriteLine("\t\tUpdate Menu\n");
                    Console.WriteLine("==>>\t.1\tProduct ID\n");
                    Console.WriteLine("==>>\t.2\tProduct Name\n");
                    Console.WriteLine("==>>\t.3\tEntry Date\n");
                    Console.WriteLine("==>>\t.4\tProduct Sold\n");
                    Console.WriteLine("==>>\t.5\tProduct Available\n");
                    Console.WriteLine("==>>\t.6\tEXIT\n");
                    Console.Write("Your choice: ");
                    updateChoice = (Console.ReadLine());
                    Console.Write("\n");
                    if (inputHandler.ChoiceValidation(updateChoice))//Checking if choice matches Regular Expression
                    {
                        if (updateChoice != "6")//If the user didn't choose exit option
                        {
                        loop:
                            Console.Write("Enter Product ID: ");
                            value = (Console.ReadLine());
                            if(inputHandler.IDValidation(value))//Checking if entered ID matched Regular Expression
                            {
                                if (updateChoice == "1")//If user wants to update an ID
                                {
                                    newIDloop://Goto loop to keep asking user for the correct ID in case of wrong input
                                    Console.Write("\nEnter New ID: ");
                                    string newID = (Console.ReadLine());
                                    if (inputHandler.IDValidation(newID))//Checking if entered ID matched Regular Expression
                                    {
                                        // Executing update query
                                        string result = inputHandler.Update(newID, int.Parse(updateChoice), value);
                                        if(result!="")
                                        {
                                            Console.Write("\n==>>\t.Error\t"+result+"\n");
                                            goto newIDloop;
                                        }

                                    }
                                    else
                                    {
                                        Console.Write("\n==>>\t.Error\tID can only have numeric characters \n");
                                        goto newIDloop;
                                    }
                                }
                                if (updateChoice == "2")//If user wants to update Name
                                {
                                    newNameloop://Goto loop to keep asking user for the correct name in case of wrong input                           
                                    Console.Write("\nEnter New Name: ");
                                    string newName = (Console.ReadLine());
                                    if(inputHandler.NameValidation(newName))//Checking if entered name matched Regular Expression
                                    {
                                        // Executing update query
                                        string result = inputHandler.Update(newName, int.Parse(updateChoice), value);

                                    }
                                    else
                                    {
                                        Console.Write("\n==>>\t.Error\tName can only have alphanumeric characters \n");
                                        goto newNameloop;
                                    }
                                }
                                if (updateChoice == "3")
                                {
                                    newdateloop://Goto loop to keep asking user for the correct date in case of wrong input                           
                                    Console.Write("\nEnter New Entry Date: ");
                                    string newdate = (Console.ReadLine());
                                    if (inputHandler.DateValidation(newdate))//Checking if entered date matched Regular Expression
                                    {
                                        // Executing update query
                                        string result = inputHandler.Update(newdate, int.Parse(updateChoice), value);

                                    }
                                    else
                                    {
                                        Console.Write("\n==>>\t.Error\t Date can be entered in this format 01 May 2020 \n");
                                        goto newdateloop;
                                    }
                                }
                                if (updateChoice == "4" || updateChoice == "5")
                                {
                                    newStatusloop://Goto loop to keep asking user for the correct status in case of wrong input                         
                                    Console.Write("\nEnter New Status: ");
                                    string newStatus = (Console.ReadLine());
                                    if (inputHandler.StatusValidation(newStatus))//Checking if the entered status matches Regular Expression
                                    {
                                        string result = inputHandler.Update(newStatus, int.Parse(updateChoice), value);

                                    }
                                    else
                                    {
                                        Console.Write("\n==>>\t.Error\t Product Status can be either 1 or 0 \n");
                                        goto newStatusloop;
                                    }
                                }
                            }
                            else
                            {
                                Console.Write("\n==>>\t.Error\tID can only have numeric characters \n");
                                goto loop;
                            }                           
                        }
                    }
                    Console.Write("\n\n==>>\t.Choice\tPress 1 to update record again : Press 0 to exit: ");
                    // Checking if the user wants to update another record
                    string again = Console.ReadLine();
                    do
                    {
                        if (inputHandler.StatusValidation(again))////Checking if the user's choice input matches the Regular Expression
                        {
                            InnerLoopflag = true;
                            if (again == "1")
                            {
                                MainLoopflag = true;
                            }
                            else
                            {
                                MainLoopflag = false;
                            }
                        }
                    } while (InnerLoopflag == false);//To keep the user in loop incase of wrong input
                } while (MainLoopflag);
            }

            void deleteProduct()
            {
                string deleteChoice;
                string value;
                bool MainLoopflag = false;//To keep the delete menu in loop
                bool InnerLoopflag = false;//To make sure user enters the correct choice
                do
                {
                    Console.Clear();
                    Console.WriteLine("\t\tDelete Menu\n");
                    Console.WriteLine("==>>\t.1\tProduct ID\n");
                    Console.WriteLine("==>>\t.2\tEXIT\n");
                    Console.Write("Your choice: ");
                    deleteChoice = (Console.ReadLine());
                    Console.Write("\n");
                    if (inputHandler.ChoiceValidation(deleteChoice))//Checking if the entered choice mathces regular expression
                    {
                        if (deleteChoice != "2")
                        {
                        loop://Goto loop to keep asking user for the correct ID
                            Console.Write("Enter Product ID: ");
                            value = (Console.ReadLine());
                            if (inputHandler.IDValidation(value))//Checking if the entered choice matches regular expression
                            {
                                if (deleteChoice == "1")
                                {
                                    //Executing the delete query
                                    string result = inputHandler.Delete(value);
                                    if (result != "")
                                    {
                                        Console.Write("\n==>>\t.Error\t" + result + "\n");
                                        goto loop;
                                    }        

                                }
                            }
                            else
                            {
                                Console.Write("\n==>>\t.Error\tID can only have numeric characters \n");
                                goto loop;
                            }
                        }
                    }
                    Console.Write("\n\n==>>\t.Choice\tPress 1 to Delete record again : Press 0 to exit: ");
                    // Checking if the user wants to delete another record
                    string again = Console.ReadLine();
                    do
                    {
                        if (inputHandler.StatusValidation(again))//Checking if the user's choice input matches the Regular Expression
                        {
                            InnerLoopflag = true;
                            if (again == "1")
                            {
                                MainLoopflag = true;
                            }
                            else
                            {
                                MainLoopflag = false;
                            }
                        }
                    } while (InnerLoopflag == false);//To keep the user in loop incase of wrong input
                } while (MainLoopflag);
            }
        }

    }
}
