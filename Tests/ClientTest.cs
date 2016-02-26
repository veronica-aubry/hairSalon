using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }


    [Fact]
    public void Test_ClientsEmptyAtFirst()
    {
      int result = Client.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {

      Client firstClient = new Client("Alison Vu", 1);
      Client secondClient = new Client("Alison Vu", 1);


      Assert.Equal(firstClient, secondClient);
    }

    [Fact]
    public void Test_GetClients_RetrievesAllClients()
    {
      Client firstClient = new Client("Alison Vu", 1);
      firstClient.Save();
      Client secondClient = new Client("Alyssa Cortella", 2);
      secondClient.Save();

      List<Client> testClientList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientList = Client.GetAll();

      Assert.Equal(testClientList, resultClientList);
    }

    [Fact]
    public void Test_Save_SavesClientToDatabase()
    {
      Client testClient = new Client("Alison Vu", 1);
      testClient.Save();


      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};


      Assert.Equal(testList, result);
    }

    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
