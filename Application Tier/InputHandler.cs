using System;
using System.Data;
using DataTier;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace VPMidTask2
{
    class InputHandler
    {
        DatabaseHandler databaseHandler = new DatabaseHandler();
        string id_pattern;
        string name_pattern;
        string date_pattern;
        string status_pattern;
        string searchChoice_pattern;
        public InputHandler()
        {
            id_pattern = @"(^[0-9]+$)";//Regular Expression ID can be of digits between 0 & 9
            name_pattern = @"(^[0-9A-Za-z ]+$)";//Regular Expression Name can be of alphanumeric characters
            //Regular Expression date can be of the following format 01 May 2020
            date_pattern = @"^((31(?!\ (Feb(ruary)?|Apr(il)?|June?|(Sep(?=\b|t)t?|Nov)(ember)?)))|((30|29)(?!\ Feb(ruary)?))|(29(?=\ Feb(ruary)?\ (((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))|(0?[1-9])|1\d|2[0-8])\ (Jan(uary)?|Feb(ruary)?|Ma(r(ch)?|y)|Apr(il)?|Ju((ly?)|(ne?))|Aug(ust)?|Oct(ober)?|(Sep(?=\b|t)t?|Nov|Dec)(ember)?)\ ((1[6-9]|[2-9]\d)\d{2})$";
            status_pattern = @"(^[0-1]+$)";//Product Status can be either 1 or 0
            searchChoice_pattern = @"(^[1-6]+$)";//Menu choice can be from 1 to 6

        }
        public bool IDValidation(string input)//Checking Product ID
        {
            Match checkID = Regex.Match(input, id_pattern);
            if (checkID.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool NameValidation(string input)// Check Product Name
        {
            Match checkName = Regex.Match(input, name_pattern);
            if (checkName.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DateValidation(string input)// Check Entry Date
        {
            Match checkDate = Regex.Match(input, date_pattern);
            if (checkDate.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool StatusValidation(string input)// Check product status
        {
            Match checkStatus = Regex.Match(input, status_pattern);
            if (checkStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ChoiceValidation(string input)//Check choice
        {
            Match checkserach = Regex.Match(input, searchChoice_pattern);
            if (checkserach.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void insertRecord(string id, string name, string date, bool sold, bool available)
        {
            // Entering data in the product object
            databaseHandler.ProductId = int.Parse(id);
            databaseHandler.Productame = name;
            databaseHandler.EntryDate = date;
            databaseHandler.ProductSold = sold.ToString();
            databaseHandler.ProductAvailable = available.ToString();
            // Inserting data in database
            databaseHandler.StoreData(databaseHandler);
        }
        public DataTable showRecords()
        {
            //Displaying all rows
            return databaseHandler.LoadData();
        }
        public SqlDataReader Search(string value, int choice)
        {
            //Searching for a specific value
            return databaseHandler.SearchData(value, choice);
        }
        public string Update(string value, int choice, string identifier)
        {
            //Updating database
            return databaseHandler.UpdateData(value, choice, identifier);
        }
        public string Delete(string identifier)
        {
            //Deleting a record
            return databaseHandler.DeleteData(identifier);
        }
    }
}
