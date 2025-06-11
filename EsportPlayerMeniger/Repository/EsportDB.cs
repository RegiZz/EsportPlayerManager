using System;
using Dapper;
using Npgsql;

namespace EsportPlayerMeniger.Repository;

public class DatabaseRepository
{
    private readonly string _connectionString;

    public DatabaseRepository(string adminConnectionString, string appConnectionString)
    {
        _connectionString = appConnectionString; // postgres://postgres@localhost:5432/esportdb
    }

    public void CreateDatabaseIfNotExists()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        
        var exists = connection.ExecuteScalar<bool>(
            "SELECT EXISTS(SELECT 1 FROM pg_database WHERE datname = 'esportdb')");

        if (!exists)
        {
            connection.Execute("CREATE DATABASE esportdb");
            Console.WriteLine("Baza danych 'esportdb' została utworzona.");
        }
        else
        {
            Console.WriteLine("Baza danych 'esportdb' już istnieje.");
        }
    }

    public void InitializeDatabase()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        // Gracze
        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Players (
                Id SERIAL PRIMARY KEY,
                Name TEXT NOT NULL,
                Skill INTEGER NOT NULL DEFAULT 0,
                Stress INTEGER NOT NULL DEFAULT 0,
                Fatigue INTEGER NOT NULL DEFAULT 0,
                Points INTEGER NOT NULL DEFAULT 0,
                Game TEXT,
                Money DECIMAL(18,2) NOT NULL DEFAULT 0.00
            );");

        // Turnieje
        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Tournaments (
                Id SERIAL PRIMARY KEY,
                Name TEXT NOT NULL,
                Entry INTEGER NOT NULL, -- koszt wejścia
                Prize DECIMAL(18,2) NOT NULL,
                MinSkillRequired INTEGER NOT NULL
            );");

        // Treningi
        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Trainings (
                Id SERIAL PRIMARY KEY,
                Type TEXT NOT NULL,
                SkillIncrease INTEGER NOT NULL,
                FatigueIncrease INTEGER NOT NULL,
                StressIncrease INTEGER NOT NULL
            );");

        //No way stworzyło się coś co próbujemy stworzyć ale trzeba dać bo tak
        Console.WriteLine("Tabele zostały zainicjalizowane.");
    }
}