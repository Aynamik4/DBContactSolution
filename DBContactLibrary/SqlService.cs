using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DBContactLibrary.Models;

namespace DBContactLibrary
{
    public static class SqlService
    {
        static string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBContact;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        static SqlConnection connection = new SqlConnection(connectionString);
        public static void CreateContact(Contact contact)
        {
            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("CreateContact", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddVarCharIn("@SSN", 13, contact.SSN);
                    command.Parameters.AddVarCharIn("@FirstName", 32, contact.LastName);
                    command.Parameters.AddVarCharIn("@LastName", 32, contact.FirstName);
                    command.Parameters.AddIntOut("@ID");

                    if (command.ExecuteNonQuery() == 1)
                        contact.ID = (int)command.Parameters["@ID"].Value;
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public static Contact ReadContact(int ID)
        {
            Contact contact = null;

            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("ReadContact", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddNullableIntIn("@ID", ID);

                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        if(sqlDataReader.Read())
                        {
                            contact = new Contact();
                            contact.ID = (int)sqlDataReader["ID"];
                            contact.SSN = sqlDataReader["SSN"].ToString();
                            contact.FirstName = sqlDataReader["FirstName"].ToString();
                            contact.LastName = sqlDataReader["LastName"].ToString();
                        }
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return contact;
        }
        public static List<Contact> ReadAllContacts()
        {
            List<Contact> contacts = null;
            Contact contact = null;

            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("ReadAllContacts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            contacts = new List<Contact>();

                            while (sqlDataReader.Read())
                            {
                                contact = new Contact();
                                contact.ID = (int)sqlDataReader["ID"];
                                contact.SSN = sqlDataReader["SSN"].ToString();
                                contact.FirstName = sqlDataReader["FirstName"].ToString();
                                contact.LastName = sqlDataReader["LastName"].ToString();
                                contacts.Add(contact);
                            }
                        }
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return contacts;
        }

        public static int UpdateContact(Contact contact) // Ant. poster uppd.
        {
            int rowsAffected = -1;

            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UpdateContact", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddNullableIntIn("@ID", contact.ID);
                    command.Parameters.AddVarCharIn("@SSN", 13, contact.SSN);
                    command.Parameters.AddVarCharIn("@FirstName", 32, contact.LastName);
                    command.Parameters.AddVarCharIn("@LastName", 32, contact.FirstName);
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected;
        }

        public static int DeleteContact(int id) // Ant. poster uppd.
        {
            int rowsAffected = -1;

            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DeleteContact", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddNullableIntIn("@ID", id);
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected;
        }

        public static void CreateContactInfo(ContactInfo info)
        {
            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("CreateContactInfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddVarCharIn("@Info", 64, info.Info);
                    command.Parameters.AddNullableIntIn("@ContactID", info.ContactID);
                    command.Parameters.AddIntOut("@ID");

                    if (command.ExecuteNonQuery() == 1)
                        info.ID = (int)command.Parameters["@ID"].Value;
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public static ContactInfo ReadContactInfo(int ID)
        {
            ContactInfo contactInfo = null;

            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("ReadContactInfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddNullableIntIn("@ID", ID);

                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        if (sqlDataReader.Read())
                        {
                            contactInfo = new ContactInfo();
                            contactInfo.ID = (int)sqlDataReader["ID"];
                            contactInfo.Info = sqlDataReader["Info"].ToString();

                            // The next row replaces the following commented rows.
                            contactInfo.ContactID = sqlDataReader.GetNullOrValue<int?>("ContactID");

                            //int tableColumnIndex = sqlDataReader.GetOrdinal("ContactID");
                            //bool isNull = sqlDataReader.IsDBNull(tableColumnIndex);

                            //if (isNull)
                            //    contactInfo.ContactID = null;
                            //else
                            //    contactInfo.ContactID = (int?)sqlDataReader["ContactID"];
                        }
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return contactInfo;
        }

        public static List<ContactInfo> ReadAllContactInfo()
        {
            List<ContactInfo> allContactInfo = null;
            ContactInfo contactInfo = null;

            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("ReadAllContactInfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            allContactInfo = new List<ContactInfo>();

                            while (sqlDataReader.Read())
                            {
                                contactInfo = new ContactInfo();

                                contactInfo.ID = (int)sqlDataReader["ID"];
                                contactInfo.Info = sqlDataReader["Info"].ToString();

                                // The next row replaces the following commented row.
                                contactInfo.ContactID = sqlDataReader.GetNullOrValue<int?>("ContactID");
                                //contactInfo.ContactID = sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("ContactID")) ? null : (int?)sqlDataReader["ContactID"];

                                allContactInfo.Add(contactInfo);
                            }
                        }
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return allContactInfo;
        }

        public static int UpdateContactInfo(ContactInfo contactInfo) // Ant. poster uppd.
        {
            int rowsAffected = -1;

            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UpdateContactInfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddIntIn("@ID", contactInfo.ID);
                    command.Parameters.AddVarCharIn("@Info", 64, contactInfo.Info);
                    command.Parameters.AddNullableIntIn("@ContactID", contactInfo.ContactID);

                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected;
        }

        public static int DeleteContactInfo(int id) // Ant. poster uppd.
        {
            int rowsAffected = -1;

            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DeleteContactInfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddIntIn("@ID", id);
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected;
        }
    }
}