using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;

// Add references of MongDB namespaces from MongoDB.Driver (mongoose) & MongoDB.Bson dlls
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PayrollMgmtProj
{
    /// <summary>
    /// Employee class which we would be using in our application to store/retrive employee information
    /// </summary>
    [BsonIgnoreExtraElements] // This attribute is required to avoid the serialization error
    public class Employee
    {
        public ObjectId _id { get; set; } // MongoDB by default create this column (pk), if not present. Hence this is required
        public string Name { get; set; }
        public string Department { get; set; }
        public int Salary { get; set; }
    }

    class Program
    {
        // Need to download the mongocshardriver from nuget
        static void Main(string[] args)
        {
            // TODO : Need to check how the user specific authentication will work
            const string ConnectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(ConnectionString);
            var server = client.GetServer();

            // Open the MongoDB shell and create Test2DB database (if not present already) by using below command
            // use Test2DB
            var db = server.GetDatabase("Test2DB");

            var employees = db.GetCollection<Employee>("employeeList"); //employeeList collection will automatically gets created

            Employee newEmp = new Employee() { 
                Name="Kapil", Department="SQA", Salary=5822
            }; 

            // Save 
            employees.Save<Employee>(newEmp);

            // Retrive a record from employeeList collection
            var emp = Query<Employee>.EQ(e => e.Name,"Mahesh");

            var result = employees.FindOne(emp);

        }
    }
}
