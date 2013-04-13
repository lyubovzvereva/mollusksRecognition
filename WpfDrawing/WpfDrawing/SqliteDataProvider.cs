using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;

namespace WpfDrawing
{
    public class SqliteDataProvider
    {
        #region SQLITE constants
        const int SQLITE_INTEGER = 1;
        const int SQLITE_FLOAT = 2;
        const int SQLITE_TEXT = 3;
        const int SQLITE_BLOB = 4;
        const int SQLITE_NULL = 5;

        public const int SQLITE_OK = 0;/* Successful result */
        public const int SQLITE_ERROR = 1;/* SQL error or missing database */
        public const int SQLITE_INTERNAL = 2;/* Internal logic error in SQLite */
        public const int SQLITE_PERM = 3;/* Access permission denied */
        public const int SQLITE_ABORT = 4;/* Callback routine requested an abort */
        public const int SQLITE_BUSY = 5;/* The database file is locked */
        public const int SQLITE_LOCKED = 6;/* A table in the database is locked */
        public const int SQLITE_NOMEM = 7;/* A malloc() failed */
        public const int SQLITE_READONLY = 8;/* Attempt to write a readonly database */
        public const int SQLITE_INTERRUPT = 9;/* Operation terminated by sqlite3_interrupt()*/
        public const int SQLITE_IOERR = 10;   /* Some kind of disk I/O error occurred */
        public const int SQLITE_CORRUPT = 11;  /* The database disk image is malformed */
        public const int SQLITE_NOTFOUND = 12;   /* Unknown opcode in sqlite3_file_control() */
        public const int SQLITE_FULL = 13;   /* Insertion failed because database is full */
        public const int SQLITE_CANTOPEN = 14;   /* Unable to open the database file */
        public const int SQLITE_PROTOCOL = 15;   /* Database lock protocol error */
        public const int SQLITE_EMPTY = 16;  /* Database is empty */
        public const int SQLITE_SCHEMA = 17;   /* The database schema changed */
        public const int SQLITE_TOOBIG = 18;   /* String or BLOB exceeds size limit */
        public const int SQLITE_CONSTRAINT = 19;   /* Abort due to constraint violation */
        public const int SQLITE_MISMATCH = 20;   /* Data type mismatch */
        public const int SQLITE_MISUSE = 21;   /* Library used incorrectly */
        public const int SQLITE_NOLFS = 22;   /* Uses OS features not supported on host */
        public const int SQLITE_AUTH = 23;   /* Authorization denied */
        public const int SQLITE_FORMAT = 24;   /* Auxiliary database format error */
        public const int SQLITE_RANGE = 25;   /* 2nd parameter to sqlite3_bind out of range */
        public const int SQLITE_NOTADB = 26;   /* File opened that is not a database file */
        public const int SQLITE_ROW = 100;  /* sqlite3_step() has another row ready */
        public const int SQLITE_DONE = 101;  /* sqlite3_step() has finished executing */
        #endregion

        private string m_filename;
        private IntPtr m_pdb;
        private bool m_open;
        /// <summary>
        /// Открытие файла базы данных.
        /// </summary>
        /// <param name="filename"></param>
        public void Open(string filename)
        {
            m_filename = filename;
            int hr;
            IntPtr pfile = Marshal.StringToCoTaskMemUni(filename);
            hr = sqlite3_open16(pfile, ref m_pdb);
            if (hr != SQLITE_OK)
            {
                throw new IOException("Cannot open file");
            }
            m_open = true;
            //настройка параметров, оптимизирующих
            ExecuteNonQuery("PRAGMA page_size=65536");
            ExecuteNonQuery("PRAGMA synchronous=OFF");
            ExecuteNonQuery("PRAGMA main.journal_mode=OFF");
            ExecuteNonQuery("PRAGMA cache_size=1000");
        }
        /// <summary>
        /// Закрытие БД.
        /// </summary>
        public void Close()
        {
            if (m_pdb != IntPtr.Zero)
            {
                sqlite3_close(m_pdb);
                m_pdb = IntPtr.Zero;
                m_open = false;
            }
        }


        /// <summary>
        /// Создание потока для записи данных типа картинки и т.п. в таблицу БД.
        /// </summary>
        /// <param name="id">id записи</param>
        /// <param name="size">Размер blob'а в байтах</param>
        /// <returns></returns>
        public Stream WriteData(long id, string tablename, string fieldName, int size)
        {
            ExecuteNonQuery("BEGIN");
            IntPtr stmHandle;
            IntPtr pzTail;
            int hr = sqlite3_prepare_v2(m_pdb, string.Format("UPDATE {1} SET {0}=@blob WHERE ID = @rowid", fieldName, tablename), -1, out stmHandle, out pzTail);

            hr = sqlite3_bind_zeroblob(stmHandle, 1, size);
            if (hr > 0)
                throw new Exception("failed to bind zero-blob: " + hr);
            hr = sqlite3_bind_int64(stmHandle, 2, id);
            if (hr > 0)
                throw new Exception("failed to bind rowid: " + hr);

            hr = sqlite3_step(stmHandle);
            if (hr > 0 && hr < 100)
                throw new Exception("failed to execute zeroblob command: " + hr);

            hr = sqlite3_reset(stmHandle);

            hr = sqlite3_finalize(stmHandle);
            if (hr > 0)
                throw new Exception("failed to finalize zeroblob command: " + hr);

            ExecuteNonQuery("COMMIT");
            return new StorageStream(m_pdb, id, tablename, fieldName, true);
        }

        /// <summary>
        /// Чтение данных из строки с заданным ID.
        /// </summary>
        /// <param name="id">Идентификатор записи.</param>
        /// <returns>Поток данных BLOB'a.</returns>
        public Stream ReadData(long id, string tablename, string fieldName)
        {
            return new StorageStream(m_pdb, id, tablename, fieldName);
        }

        /// <summary>
        /// Путь к файлу.
        /// </summary>
        public string FileName
        {
            get { return m_filename; }
        }

        public void ExecuteNonQuery(string query)
        {
            if (!m_open)
                throw new Exception("SQLite database is not open.");
            IntPtr err_msg = IntPtr.Zero;
            int hr;
            hr = sqlite3_exec(m_pdb, query, IntPtr.Zero, IntPtr.Zero, ref err_msg);
            if (err_msg != IntPtr.Zero)
                sqlite3_free(err_msg);
        }

        public DataTable GetTables()
        {
            string sql = "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name";
            DataTable table = ExecuteQuery(sql);
            return table;
        }

        public DataTable ExecuteQuery(string query)
        {
            if (!m_open)
                throw new Exception("SQLite database is not open.");

            //prepare the statement
            IntPtr stmHandle;
            IntPtr pzTail;
            int bytesLength = Encoding.UTF8.GetByteCount(query);

            if (sqlite3_prepare_v2(m_pdb, query, bytesLength,
                  out stmHandle, out pzTail) != SQLITE_OK)
            {
                string mess = Marshal.PtrToStringAnsi(sqlite3_errmsg(m_pdb));
                throw new Exception(mess);
            }
            //IntPtr stmHandle = Prepare(query);

            //get the number of returned columns
            int columnCount = sqlite3_column_count(stmHandle);

            //create datatable and columns
            DataTable dTable = new DataTable();
            for (int i = 0; i < columnCount; i++)
            {
                IntPtr strPtr = sqlite3_column_origin_name(stmHandle, i);
                string columnName = Marshal.PtrToStringAnsi(strPtr);
                dTable.Columns.Add(columnName, GetColumnType(i, stmHandle));
            }

            //populate datatable
            while (sqlite3_step(stmHandle) == SQLITE_ROW)
            {
                object[] row = new object[columnCount];
                for (int i = 0; i < columnCount; i++)
                {
                    switch (sqlite3_column_type(stmHandle, i))
                    {
                        case SQLITE_INTEGER:
                            row[i] = sqlite3_column_int(stmHandle, i);
                            break;
                        case SQLITE_TEXT:
                            IntPtr ptr = sqlite3_column_text(stmHandle, i);
                            string text = Marshal.PtrToStringAnsi(ptr);
                            row[i] = text;
                            break;
                        case SQLITE_FLOAT:
                            row[i] = sqlite3_column_double(stmHandle, i);
                            break;
                    }
                }

                dTable.Rows.Add(row);
            }
            int hr = sqlite3_reset(stmHandle);
            if (sqlite3_finalize(stmHandle) != SQLITE_OK)
                throw new Exception("Could not finalize SQL statement."); ;

            return dTable;
        }
        public long GetLastRowID()
        {
            long id = sqlite3_last_insert_rowid(m_pdb);
            return id;
        }
        #region private functions


        Type GetColumnType(int ordinal, IntPtr stmHandle)
        {
            switch (sqlite3_column_type(stmHandle, ordinal))
            {
                case SQLITE_INTEGER:
                    return typeof(int);
                case SQLITE_TEXT:
                    return typeof(string);
                case SQLITE_FLOAT:
                    return typeof(double);
                default:
                    return typeof(object);
            }
        }
        #endregion


        #region external functions
        private const string SQLITE_DLL = "sqlite3.dll";
        /* int sqlite3_open(const char *filename, sqlite3 **ppDb); */
        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_open(string filename, ref IntPtr ppDb);

        /*int sqlite3_open16(const void *filename, sqlite3 **ppDb); */
        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_open16(IntPtr pFileName, ref IntPtr ppDb);

        /* int sqlite3_close(sqlite3 *); */
        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_close(IntPtr ppDb);

        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_blob_open(IntPtr stmt, string zDb, string zTable, string zColumn, long iRow, int flags, ref IntPtr blobHandle);

        /* int sqlite3_blob_bytes(sqlite3_blob *); */
        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_blob_bytes(IntPtr blobHandle);

        /* int sqlite3_blob_close(sqlite3_blob *); */
        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_blob_close(IntPtr blobHandle);

        /* int sqlite3_blob_read(sqlite3_blob *, void *Z, int N, int iOffset); */
        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_blob_read(IntPtr blobHandle, byte[] data, int len, int offset);

        /* int sqlite3_blob_write(sqlite3_blob *, const void *z, int n, int iOffset); */
        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_blob_write(IntPtr blobHandle, byte[] data, int len, int offset);

        /* int sqlite3_prepare_v2(sqlite3 *db, const char *zSql, int nByte, sqlite3_stmt **ppStmt, const char **pzTail); */
        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_prepare_v2(IntPtr ppDb, string sql, int maxLen, out IntPtr stmt, out IntPtr pzTail);

        /* int sqlite3_bind_zeroblob(sqlite3_stmt*, int, int n); */
        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_bind_zeroblob(IntPtr stmt, int prmIndex, int size);

        /* int sqlite3_bind_int64(sqlite3_stmt*, int, sqlite3_int64); */
        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_bind_int64(IntPtr stmt, int prmIndex, long value);

        /* int sqlite3_step(sqlite3_stmt*); */
        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_step(IntPtr stmt);

        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_reset(IntPtr stmt);

        /* int sqlite3_finalize(sqlite3_stmt *pStmt); */
        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_finalize(IntPtr stmt);

        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void sqlite3_free(IntPtr ptr);

        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr sqlite3_errmsg(IntPtr db);

        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_column_count(IntPtr stmHandle);

        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr sqlite3_column_origin_name(
         IntPtr stmHandle, int iCol);



        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_column_type(IntPtr stmHandle, int iCol);

        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_column_int(IntPtr stmHandle, int iCol);

        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr sqlite3_column_text(IntPtr stmHandle, int iCol);

        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern double sqlite3_column_double(IntPtr stmHandle, int iCol);
        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern long sqlite3_last_insert_rowid(IntPtr sqlite3);


        [DllImport(SQLITE_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_exec(IntPtr sqlite3, string sql, IntPtr callback, IntPtr p, ref IntPtr errmsg);
        #endregion

        #region internal class
        class StorageStream : Stream
        {
            IntPtr m_blob;
            IntPtr m_pdb;
            long m_position;
            long m_length;
            bool m_canwrite;
            public StorageStream(IntPtr pdb, long id, string table, string field, bool canwrite = false)
            {
                m_pdb = pdb;
                int par = canwrite ? 1 : 0;
                m_canwrite = canwrite;
                int hr = sqlite3_blob_open(m_pdb, "main", table, field, id, par, ref m_blob);
                m_length = sqlite3_blob_bytes(m_blob);
                m_position = 0;
            }

            public override bool CanRead
            {
                get { return true; }
            }

            public override bool CanSeek
            {
                get { return true; }
            }

            public override bool CanWrite
            {
                get { return m_canwrite; }
            }

            public override void Flush()
            {

            }

            public override long Length
            {
                get
                {
                    return m_length;
                }
            }

            public override long Position
            {
                get
                {
                    return m_position;
                }
                set
                {
                    m_position = value;
                }
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                if (buffer == null)
                {
                    throw new ArgumentNullException("buffer is null");
                }
                if (offset < 0)
                {
                    throw new ArgumentOutOfRangeException("offset is negative");
                }
                if (count < 0)
                {
                    throw new ArgumentOutOfRangeException("count is negative");
                }
                if ((buffer.Length - offset) < count)
                {
                    throw new ArgumentException("invalid length");
                }

                int diff = (int)(m_length - m_position);

                int read_count = Math.Min(diff, count);

                byte[] buff;
                if (offset != 0)
                    buff = new byte[read_count];
                else
                    buff = buffer;
                int hr = sqlite3_blob_read(m_blob, buff, read_count, (int)m_position);
                if (hr != 0)
                    throw new Exception("Read exception");
                if (offset != 0)
                    Buffer.BlockCopy(buff, 0, buffer, offset, read_count);

                m_position += read_count;

                //Debug.Print("Stream read {0} bytes, current position = {1}, length = {2}", read_count, m_position, m_length);
                return read_count;

            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        Position = offset;
                        break;
                    case SeekOrigin.Current:
                        Position = Position + offset;
                        break;
                    case SeekOrigin.End:
                        Position = Length - offset;
                        break;
                    default:
                        break;
                }
                return Position;
            }

            public override void SetLength(long value)
            {
                throw new NotImplementedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                if (buffer == null)
                {
                    throw new ArgumentNullException("buffer is null");
                }
                if (offset < 0)
                {
                    throw new ArgumentOutOfRangeException("offset is negative");
                }
                if (count < 0)
                {
                    throw new ArgumentOutOfRangeException("count is negative");
                }
                if ((buffer.Length - offset) < count)
                {
                    throw new ArgumentException("invalid length");
                }
                if (m_position + count > m_length)
                    throw new OutOfMemoryException();
                int write_count = Math.Min(count, (int)(m_length - m_position));

                byte[] buff;
                if (offset != 0)
                {
                    buff = new byte[count];
                    Buffer.BlockCopy(buffer, offset, buff, 0, count);
                }
                else
                    buff = buffer;

                int hr = sqlite3_blob_write(m_blob, buff, write_count, (int)m_position);

                m_position += write_count;
            }
            public override void Close()
            {
                base.Close();
                sqlite3_blob_close(m_blob);
            }
            ~StorageStream()
            {
                this.Close();
            }
        }
        #endregion
    }
}
