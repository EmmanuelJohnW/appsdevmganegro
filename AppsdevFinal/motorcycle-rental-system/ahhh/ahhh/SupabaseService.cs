using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace ahhh
{
    internal static class SupabaseService
    {
        private static readonly string DbPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rental.db");

        private static string ConnStr => $"Data Source={DbPath}";

        static SupabaseService()
        {
            EnsureDbCreated();
        }

        private static void EnsureDbCreated()
        {
            using (var conn = new SqliteConnection(ConnStr))
            {
                conn.Open();
                var statements = new[]
                {
                    @"CREATE TABLE IF NOT EXISTS users (
                        id         INTEGER PRIMARY KEY AUTOINCREMENT,
                        username   TEXT UNIQUE NOT NULL,
                        password   TEXT NOT NULL,
                        created_at TEXT DEFAULT (datetime('now'))
                    )",
                    @"CREATE TABLE IF NOT EXISTS motorcycles (
                        id           INTEGER PRIMARY KEY AUTOINCREMENT,
                        name         TEXT NOT NULL,
                        model        TEXT,
                        daily_rate   REAL DEFAULT 0,
                        is_available INTEGER DEFAULT 1,
                        created_at   TEXT DEFAULT (datetime('now'))
                    )",
                    @"CREATE TABLE IF NOT EXISTS rentals (
                        id            INTEGER PRIMARY KEY AUTOINCREMENT,
                        motorcycle_id INTEGER REFERENCES motorcycles(id),
                        username      TEXT NOT NULL,
                        rented_at     TEXT DEFAULT (datetime('now')),
                        returned_at   TEXT,
                        status        TEXT DEFAULT 'active'
                    )"
                };

                foreach (var sql in statements)
                {
                    using (var cmd = new SqliteCommand(sql, conn))
                        cmd.ExecuteNonQuery();
                }
            }
        }

        // ── Users ──────────────────────────────────────────────────────────────

        public static async Task<bool> UsernameExistsAsync(string username)
        {
            using (var conn = new SqliteConnection(ConnStr))
            {
                await conn.OpenAsync();
                using (var cmd = new SqliteCommand(
                    "SELECT COUNT(*) FROM users WHERE username = @u", conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    var count = (long)await cmd.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }

        public static async Task<bool> RegisterUserAsync(string username, string password)
        {
            using (var conn = new SqliteConnection(ConnStr))
            {
                await conn.OpenAsync();
                using (var cmd = new SqliteCommand(
                    "INSERT INTO users (username, password) VALUES (@u, @p)", conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public static async Task<bool> LoginAsync(string username, string password)
        {
            using (var conn = new SqliteConnection(ConnStr))
            {
                await conn.OpenAsync();
                using (var cmd = new SqliteCommand(
                    "SELECT COUNT(*) FROM users WHERE username = @u AND password = @p", conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);
                    var count = (long)await cmd.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }

        // ── Motorcycles ────────────────────────────────────────────────────────

        public static async Task<List<Dictionary<string, object>>> GetMotorcyclesAsync()
        {
            var list = new List<Dictionary<string, object>>();
            using (var conn = new SqliteConnection(ConnStr))
            {
                await conn.OpenAsync();
                using (var cmd = new SqliteCommand("SELECT * FROM motorcycles ORDER BY id", conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                        list.Add(ReaderToDict(reader));
                }
            }
            return list;
        }

        public static async Task<bool> AddMotorcycleAsync(string name, string model, decimal dailyRate)
        {
            using (var conn = new SqliteConnection(ConnStr))
            {
                await conn.OpenAsync();
                using (var cmd = new SqliteCommand(
                    "INSERT INTO motorcycles (name, model, daily_rate, is_available) VALUES (@n, @m, @r, 1)", conn))
                {
                    cmd.Parameters.AddWithValue("@n", name);
                    cmd.Parameters.AddWithValue("@m", (object)model ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@r", (double)dailyRate);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public static async Task<bool> UpdateMotorcycleAsync(int id, string name, string model, decimal dailyRate)
        {
            using (var conn = new SqliteConnection(ConnStr))
            {
                await conn.OpenAsync();
                using (var cmd = new SqliteCommand(
                    "UPDATE motorcycles SET name=@n, model=@m, daily_rate=@r WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@n", name);
                    cmd.Parameters.AddWithValue("@m", (object)model ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@r", (double)dailyRate);
                    cmd.Parameters.AddWithValue("@id", id);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public static async Task<bool> DeleteMotorcycleAsync(int id)
        {
            using (var conn = new SqliteConnection(ConnStr))
            {
                await conn.OpenAsync();
                using (var cmd = new SqliteCommand("DELETE FROM motorcycles WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        // ── Rentals ────────────────────────────────────────────────────────────

        public static async Task<List<Dictionary<string, object>>> GetRentalsAsync()
        {
            var list = new List<Dictionary<string, object>>();
            using (var conn = new SqliteConnection(ConnStr))
            {
                await conn.OpenAsync();
                using (var cmd = new SqliteCommand("SELECT * FROM rentals ORDER BY rented_at DESC", conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                        list.Add(ReaderToDict(reader));
                }
            }
            return list;
        }

        public static async Task<bool> CreateRentalAsync(int motorcycleId, string username)
        {
            using (var conn = new SqliteConnection(ConnStr))
            {
                await conn.OpenAsync();
                using (var cmd = new SqliteCommand(
                    "INSERT INTO rentals (motorcycle_id, username, status) VALUES (@mid, @u, 'active')", conn))
                {
                    cmd.Parameters.AddWithValue("@mid", motorcycleId);
                    cmd.Parameters.AddWithValue("@u", username);
                    if (await cmd.ExecuteNonQueryAsync() <= 0) return false;
                }
                return await SetAvailabilityAsync(conn, motorcycleId, false);
            }
        }

        public static async Task<bool> ReturnRentalAsync(int rentalId, int motorcycleId)
        {
            using (var conn = new SqliteConnection(ConnStr))
            {
                await conn.OpenAsync();
                using (var cmd = new SqliteCommand(
                    "UPDATE rentals SET status='returned', returned_at=datetime('now') WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", rentalId);
                    if (await cmd.ExecuteNonQueryAsync() <= 0) return false;
                }
                return await SetAvailabilityAsync(conn, motorcycleId, true);
            }
        }

        public static async Task<bool> DeleteRentalAsync(int rentalId, int motorcycleId)
        {
            using (var conn = new SqliteConnection(ConnStr))
            {
                await conn.OpenAsync();
                using (var cmd = new SqliteCommand("DELETE FROM rentals WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", rentalId);
                    if (await cmd.ExecuteNonQueryAsync() <= 0) return false;
                }
                return await SetAvailabilityAsync(conn, motorcycleId, true);
            }
        }

        // ── Private helpers ────────────────────────────────────────────────────

        private static async Task<bool> SetAvailabilityAsync(SqliteConnection conn, int motorcycleId, bool available)
        {
            using (var cmd = new SqliteCommand(
                "UPDATE motorcycles SET is_available=@a WHERE id=@id", conn))
            {
                cmd.Parameters.AddWithValue("@a", available ? 1 : 0);
                cmd.Parameters.AddWithValue("@id", motorcycleId);
                return await cmd.ExecuteNonQueryAsync() > 0;
            }
        }

        private static Dictionary<string, object> ReaderToDict(DbDataReader reader)
        {
            var dict = new Dictionary<string, object>();
            for (int i = 0; i < reader.FieldCount; i++)
                dict[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
            return dict;
        }
    }
}
