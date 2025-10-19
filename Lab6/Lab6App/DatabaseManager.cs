using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Lab6App;

/// <summary>
/// Manages database operations for manufacturers and watches.
/// </summary>
public class DatabaseManager
{
    private readonly string _connectionString = "Data Source=watches.db";

    /// <summary>
    /// Creates the necessary tables in the database if they do not exist.
    /// </summary>
    public void CreateTables()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var createManufacturerTable = @"
            CREATE TABLE IF NOT EXISTS Manufacturer (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Address TEXT NOT NULL,
                IsAChildCompany INTEGER NOT NULL
            )";

        var createWatchesTable = @"
            CREATE TABLE IF NOT EXISTS Watches (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Model TEXT NOT NULL,
                SerialNumber TEXT NOT NULL,
                Type INTEGER NOT NULL,
                ManufacturerId INTEGER NOT NULL,
                FOREIGN KEY (ManufacturerId) REFERENCES Manufacturer (Id)
            )";

        using var command1 = new SqliteCommand(createManufacturerTable, connection);
        command1.ExecuteNonQuery();

        using var command2 = new SqliteCommand(createWatchesTable, connection);
        command2.ExecuteNonQuery();
    }

    /// <summary>
    /// Asynchronously fills the database with sample data for manufacturers and watches.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task FillDataAsync()
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        for (int i = 1; i <= 30; i++)
        {
            var manufacturer = Manufacturer.Create($"Manufacturer {i}", $"Address {i}", i % 2 == 0);
            var manufacturerId = await InsertManufacturerAsync(connection, manufacturer);

             var watches = Watches.Create($"Model {i}", $"SN{i}", (WatchesType)(i % 3), manufacturerId);
            await InsertWatchesAsync(connection, watches);
        }
    }

    private async Task<int> InsertManufacturerAsync(SqliteConnection connection, Manufacturer manufacturer)
    {
        var command = new SqliteCommand("INSERT INTO Manufacturer (Name, Address, IsAChildCompany) VALUES (@name, @address, @isChild); SELECT last_insert_rowid();", connection);
        command.Parameters.AddWithValue("@name", manufacturer.Name);
        command.Parameters.AddWithValue("@address", manufacturer.Address);
        command.Parameters.AddWithValue("@isChild", manufacturer.IsAChildCompany ? 1 : 0);
        var result = await command.ExecuteScalarAsync();
        return Convert.ToInt32(result);
    }

    private async Task InsertWatchesAsync(SqliteConnection connection, Watches watches)
    {
        var command = new SqliteCommand("INSERT INTO Watches (Model, SerialNumber, Type, ManufacturerId) VALUES (@model, @serial, @type, @manId)", connection);
        command.Parameters.AddWithValue("@model", watches.Model);
        command.Parameters.AddWithValue("@serial", watches.SerialNumber);
        command.Parameters.AddWithValue("@type", (int)watches.Type);
        command.Parameters.AddWithValue("@manId", watches.ManufacturerId);
        await command.ExecuteNonQueryAsync();
    }

    /// <summary>
    /// Asynchronously adds a new manufacturer to the database.
    /// </summary>
    /// <param name="manufacturer">The manufacturer to add.</param>
    /// <returns>The ID of the newly added manufacturer.</returns>
    public async Task<int> AddManufacturerAsync(Manufacturer manufacturer)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        return await InsertManufacturerAsync(connection, manufacturer);
    }

    /// <summary>
    /// Asynchronously adds a new watches to the database.
    /// </summary>
    /// <param name="watches">The watches to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddWatchesAsync(Watches watches)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        await InsertWatchesAsync(connection, watches);
    }

    /// <summary>
    /// Asynchronously retrieves a list of watches for a specific manufacturer.
    /// </summary>
    /// <param name="manufacturerId">The ID of the manufacturer.</param>
    /// <returns>A list of watches associated with the manufacturer.</returns>
    public async Task<List<Watches>> GetWatchesByManufacturerAsync(int manufacturerId)
    {
        var watches = new List<Watches>();
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        var command = new SqliteCommand("SELECT Id, Model, SerialNumber, Type, ManufacturerId FROM Watches WHERE ManufacturerId = @manId", connection);
        command.Parameters.AddWithValue("@manId", manufacturerId);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            watches.Add(new Watches
            {
                Id = reader.GetInt32(0),
                Model = reader.GetString(1),
                SerialNumber = reader.GetString(2),
                Type = (WatchesType)reader.GetInt32(3),
                ManufacturerId = reader.GetInt32(4)
            });
        }
        return watches;
    }
}