using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MovieManage
{
    class DataManager
    {
        public static List<Movie> Movies = new List<Movie>();
        public static List<User> Users = new List<User>();

        static DataManager()
        {
            Load();
        }
        public static void Load()
        {
            try
            {
                string moviesOutput = File.ReadAllText(@"./Movies.xml");
                XElement moviesXElement = XElement.Parse(moviesOutput);
                Movies = (from item in moviesXElement.Descendants("movie")
                          select new Movie()
                          {
                              MovNum = item.Element("movnum").Value,
                              Name = item.Element("name").Value,
                              Publisher = item.Element("publisher").Value,
                              RunTime = item.Element("runtime").Value,
                              IsBooked = item.Element("isBooked").Value != "0"? true : false,
                              UserId = int.Parse(item.Element("userId").Value),
                              UserName = item.Element("userName").Value
                          }).ToList<Movie>();

                string usersOutput = File.ReadAllText(@"./Users.xml");
                XElement usersXelement = XElement.Parse(usersOutput);

                Users = (from item in usersXelement.Descendants("user")
                         select new User()
                         {
                             Id = int.Parse(item.Element("id").Value),
                             Name = item.Element("name").Value
                         }).ToList<User>();
            }
            catch(FileNotFoundException exception)
            {
                Save();
            }
        }
        public static void Save()
        {
            string moviesOutput = "";
            moviesOutput += "<movies>\n";
            foreach(var item in Movies)
            {
                moviesOutput += "<movie>\n";
                moviesOutput += "<movnum>" + item.MovNum + "</movnum>\n";
                moviesOutput += "<name>" + item.Name + "</name>\n";
                moviesOutput += "<publisher>" + item.Publisher + "</publisher>\n";
                moviesOutput += "<runtime>" + item.RunTime + "</runtime>\n";
                moviesOutput += "<isBooked>" + (item.IsBooked ? 1:0) + "</isBooked>\n";
                moviesOutput += "<userId>" + item.UserId + "</userId>\n";
                moviesOutput += "<userName>" + item.UserName + "</userName>\n";
                moviesOutput += "</movie>\n";
            }
            moviesOutput += "</movies>";

            string usersOutput = "";
            usersOutput += "<users>\n";

            foreach (var item in Users)
            {
                usersOutput += "<user>\n";
                usersOutput += "<id>\n" + item.Id + "</id>\n";
                usersOutput += "<name>\n" + item.Name + "</name>\n";
                usersOutput += "</user>";
            }
            usersOutput += "</users>";

            File.WriteAllText(@"./Movies.xml", moviesOutput);
            File.WriteAllText(@"./Users.xml", usersOutput);
        }
    }
}
