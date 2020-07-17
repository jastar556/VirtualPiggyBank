using System;
using System.Collections.Generic;
using System.IO;
using SQLite;
using VirtualPiggyBank.Core.Connections;

namespace VirtualPiggyBank.Core.Repo
{
    public static class BankRepository
    {
        
        public static SQLiteConnection Connection()
        {
            string RepoPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Library", "BankDB.db3");

            return new SQLiteConnection(RepoPath);
        }
    }
}
