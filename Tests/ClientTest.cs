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

    [Fact]
    public void Test_Update_UpdatesClientInDatabase()
    {
      string name = "Allison Vu";
      Client testClient = new Client(name, 1);
      testClient.Save();
      string newName = "Alison Vu";

      testClient.Update(newName);

      string result = testClient.GetName();

      Assert.Equal(newName, result);
    }

    [Fact]
    public void Test_Find_FindsClientInDatabase()
    {
      Client testClient = new Client("Alyssa Cortella", 2);
      testClient.Save();

      Client foundClient = Client.Find(testClient.GetId());

      Assert.Equal(testClient, foundClient);
    }

    [Fact]
    public void Test_Delete_DeletesClientFromDatabase()
    {
      Client testClient1 = new Client("Alison Vu", 1);
      testClient1.Save();
      Client testClient2 = new Client("Alyssa Cortella", 2);
      testClient2.Save();

      testClient1.Delete();
      List<Client> resultClients = Client.GetAll();
      List<Client> testClientList = new List<Client> {testClient2};

      Assert.Equal(testClientList, resultClients);
    }

    public void Dispose()
    {
      Client.DeleteAll();
    }

  }
}
