using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WpfDrawing
{
    class UIManager
    {
        public static UIManager man;
        DataGrid m_grid;
        public UIManager(DataGrid grid)
        {
            m_grid = grid;
        }
        SqliteDataProvider provider;

        public DataGrid GetDataGrid()
        {
            return m_grid;
        }
        public SqliteDataProvider Provider
        {
            get { return provider; }
            set { provider = value; }
        }
        public void Close()
        {
            if (provider != null)
                provider.Close();
        }
    }
}
