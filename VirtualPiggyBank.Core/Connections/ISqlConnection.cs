using System;
using SQLite;

namespace VirtualPiggyBank.Core.Connections
{
    public interface ISqlConnection
    {
        SQLiteConnection Connection();

    }
}
