using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows;


namespace WpfDrawing
{
    class node//описание вершины
    {
        public bool e;//индикатор захваченности
        public double g;//общая стоимость функции от начальной точки до данной
        public List<Point> p;//пусть сюда
        public bool is_active;
        public int x;
        public int y;
        public int i;
        public int j;
        public node(int x, int y, int i, int j)
        {
            this.x = x;
            this.y = y;
            e = false;
            g = Double.PositiveInfinity;
            p = new List<Point>();
            is_active = false;
            this.i = i;
            this.j = j;
        }
        

    }
    class Graph
    {
        byte[,] data;//цвета изображения
        int wid;
        int heid;
       
        int[,] Gx;//составляющие градиента
        int[,] Gy;
        byte[,] Lapl;
        double max_G;//максимальный градиент на изображении
        node s;//initial/seed node
        List<node> Active_List;//список активных вершин
  
        node[,] nodes;
       
        public Graph(byte[,] bites)//конструктор
        {
            
            Active_List = new List<node>();
            data = bites;
            wid = bites.GetLength(0);
            heid = bites.GetLength(1);
           
            Gx = new int[wid, heid];
            Gy = new int[wid, heid];
            Lapl = new byte[wid, heid];
         
            G_set();//считаем градиент изображения
           
            
            
        }
        public List<Point> return_path(int i, int j)
        {
          
            int prom = i - s.x + 50;
            if (s.x - 50 < 0)
                prom = i;
            int prom2 = j - s.y + 50;
            if (s.y - 50 < 0)
                prom2 = j;
            if (prom >= nodes.GetLength(0))
                prom = nodes.GetLength(0) - 1;
            if (prom2 >= nodes.GetLength(1))
                prom2 = nodes.GetLength(1) - 1;
            return nodes[prom,prom2].p;//WTF???
        }
        
        bool e(node q)//является ли q расширяющейся
        {
            return q.e;
        }
        /// <summary>
        /// общая стоимость перехода из начальной точки в q.
        /// можно только получить, изменить не получится
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        double g(node q)//общая стоимость перехода из начальной точки в q ---можно только получить, изменить не получится
        {
            return q.g;
        }
        private void add_nodes(int x, int y)//добавляем все вершины
        {
            int wid1=50;
            int wid2=50;
            if (x - 50 < 0)
                wid1 = x;
            if (x + 50 > wid)
                wid2 = Math.Max(wid - x,0);

            int heid1 = 50;
            int heid2 = 50;
            if (y - 50 < 0)
                heid1 = y;
            if (y + 50 > heid)
                heid2 = Math.Max(heid - y,0);

            nodes = new node[wid1 + wid2, heid1 + heid2];
            int indexx = x - 50;
            if (x - 50 <= 0)
                indexx = 0;
            int indexy = y - 50;
            if (y - 50 <= 0)
                indexy = 0;
            for (int i = 0; i < nodes.GetLength(0); ++i)
                for (int j = 0; j < nodes.GetLength(1); ++j)
                {
                    nodes[i, j] = new node(indexx + i, indexy + j, i, j);//i-столбец(в изображении), j-Строка
                }
            s = nodes[Math.Min(Math.Max(wid1,0),nodes.GetLength(0)-1),Math.Min( Math.Max(heid1,0),nodes.GetLength(1)-1)];//seed node
            
        }
        
        public void set_seed(int x, int y)//установили начальную точку. далее нужно рассчитать основную часть!
        {
            if (x >= wid || y >= heid)
            {
                int f = 1;
            }
            add_nodes(x, y);
            set_weight();
            
        }
        /// <summary>
        /// Просчет путей от seed point до каждой точки в графе
        /// </summary>
        void set_weight()
        {
            s.g = 0;
            Active_List.Add(s);
            s.is_active = true;
            while (Active_List.Count != 0)
            {
                node q = Active_List[find_min()];
                Active_List.Remove(q);
                if (Active_List.Count < 20)
                {
                    q.is_active = false;
                }
                q.is_active = false;
                q.e = true;
                
                for (int i = Math.Max(0, q.i-1); i < Math.Min(q.i + 2, nodes.GetLength(0)); ++i)//+2 потому что не включая это число
                    for (int j = Math.Max(0, q.j - 1); j < Math.Min(q.j+ 2, nodes.GetLength(1)); ++j)
                    {
                        node r = nodes[i, j];
                        if (!e(r))
                        {

                            double g_tmp = g(q) + l(q, r);
                            if (r.is_active && g_tmp < g(r))
                            {
                                r.g = g_tmp;
                                r.p = new List<Point>(q.p);
                                r.p.Add(new Point(q.x, q.y));
                            }
                            if (!r.is_active)
                            {
                                r.g = g_tmp;

                                r.p = new List<Point>(q.p);//скопировали путь до точки q
                                r.p.Add(new Point(q.x, q.y));// добавили саму точку q
                                Active_List.Add(r);
                                r.is_active = true;
                            }
                        }

                    }
            }
            
        }
        /// <summary>
        /// Ищем минимальную по стоимости перехода сюда вершину
        /// </summary>
        /// <returns></returns>
        private int find_min()
        {
            double min = Active_List[0].g;
            int index = 0;
            for (int i = 1; i < Active_List.Count;i++ )
            {
                if (Active_List[i].g < min)
                {
                    min = Active_List[i].g;
                    index = i;
                }
            }
            return index;
        }
        /// <summary>
        /// стоимость ребра от q до r
        /// </summary>
        /// <param name="p">из вершины p</param>
        /// <param name="q">в соседнюю вершину q</param>
        /// <returns></returns>
        double l(node p, node q)
        {
            
            int x = p.x;
            int y = p.y;

            int x1 = q.x;
            int y1 = q.y;
           
            byte f_z = Lapl[x,y];//Лапласиан
            
            double f_g = 1 - Math.Sqrt(Math.Pow(Gx[x,y],2)+Math.Pow(Gy[x,y],2)) / max_G;//величина градиента
            if (Math.Abs(x - q.x) + Math.Abs(y - q.y) == 1)//не диагональный элемент
                f_g *= 1.0 / Math.Sqrt(2);

            vector D_p = new vector(Gy[x, y], -Gx[x, y]);
            vector D_q = new vector(Gy[x1,y1], -Gx[x1,y1]);

            vector L_p_q = L(p, q, D_p);

            double d_p = D_p * L_p_q;
            double d_q = L_p_q * D_q;
            if (d_p != 0)
                d_p = d_p/(D_p.lenght()*L_p_q.lenght());
            if (d_q != 0)
                d_q = d_q / (D_q.lenght() * L_p_q.lenght());
            if (d_q > 1 || d_p > 1)
                MessageBox.Show("1");//мало ли

            double f_d = (2 / (3 * Math.PI)) * (Math.Acos(d_p) + Math.Acos(d_q));
           

            /////////////////////////////
            const double w_z=0.43;
            const double w_g = 0.43;
            const double w_d = 0.14;
            ////////////////////////////

            return w_z*f_z+w_g*f_g+w_d*f_d;

        }
        private vector L(node p, node q, vector D_p)
        {
            vector q_p = new vector(q.x - p.x, q.y - p.y);
            if (D_p * q_p >= 0)
                return q_p;
            return new vector(-q.x + p.x, -q.y + p.y);
        }
        /// <summary>
        /// Просчет Лапласиана, Гаусиана
        /// </summary>
        private void G_set()
        {
            
            int data1 = 0;
            int data2 = 0;
            int data3 = 0;
            int data4 = 0;
            int data6 = 0;
            int data7 = 0;
            int data8 = 0;
            int data9 = 0;
            int I_x = 0;
            int I_y = 0;

            int width=data.GetLength(0);
            int height= data.GetLength(1);
            double sum = 0;
            max_G = 0;//заодно ищем максимальное значение градиента
            for (int j = 0; j < height; ++j)//поменяно для прохождения по строкам сначала. вроде как необязательно
            {
                for (int i = 0; i < width; ++i)
                {
                    //проверка на выход за границы массива


                    //используем градиентный оператор собеля
                    //Gx=(z7+2z8+z9)-(z1+2z2+z3)
                    //Gy=(z3+2z6+z9)-(z1+2z4+z7)

                    //установка значений
                    if (j + 1 < height && i - 1 >= 0)
                        data7 = data[i - 1, j + 1];
                    else
                        data7 = 0;
                    if (j + 1 < height)
                        data8 = data[i, j + 1];
                    else
                        data8 = 0;
                    if (i + 1 < width && j + 1 < height)
                        data9 = data[i + 1, j + 1];
                    else
                        data9 = 0;
                    if (i - 1 >= 0 && j - 1 >= 0)
                        data1 = data[i - 1, j - 1];
                    else
                        data1 = 0;
                    if (j - 1 >= 0)
                        data2 = data[i, j - 1];
                    else
                        data2 = 0;
                    if (j - 1 >= 0 && i + 1 < width)
                        data3 = data[i + 1, j - 1];
                    else
                        data3 = 0;
                    if (i - 1 >= 0)
                        data4 = data[i - 1, j];
                    else
                        data4 = 0;
                    if (i + 1 < width)
                        data6 = data[i + 1, j];
                    else
                        data6 = 0;

                    I_x = (data7 + 2 * data8 + data9) - (data1 + 2 * data2 + data3);
                    I_y = (data3 + 2 * data6 + data9) - (data1 + 2 * data4 + data7);
                    Gx[i, j] = I_x;
                    Gy[i, j] = I_y;

                    int prom = data1 + data2 + data3 + data4 - 8 * data[i, j] + data6 + data7 + data8 + data9;
                    if (prom == 0)

                        Lapl[i, j] = 0;
                    else

                        Lapl[i, j] = 1;
                    sum = Math.Sqrt(Math.Pow(I_x, 2) + Math.Pow(I_y, 2));
                    //G[i, j] = sum;//в принципе можно не хранить этот массив. он получается из двух Gx Gy.
                    if (sum > max_G)
                        max_G = sum;
                }
            }
        }
    }
    class vector
    {
        double x;
        double y;
        public vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public static double operator *(vector a, vector b)
        {
            return a.x * b.x +  a.y * b.y;
        }
        public double lenght()
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
    }


    
}