using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
/*
 * Conway's Game of Life
 * Juego de la vida de Conway
 * 
 * Proyecto de Laboratorio de Programacion
 * ID:1069835   JUAN DANIEL OZUNA ESPINAL
 */ 
namespace JuegoDeLaVidaINTENTO
{
    public partial class GOFMain : Form
    {       
        public CellGOF[,] cells;
        int cellSize = 15;
        int steps = 0;
        int total = 0;
        int timeInterval = 1000;

        public GOFMain()
        {
            InitializeComponent();
            sumaWH();
            timer1 = new System.Windows.Forms.Timer();
            button8.Enabled = false;


        }
        public void labelNeighbors(string x)
        {
            label7.Text = x;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            sumaWH();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            sumaWH();
        }
        private void sumaWH()
        {
            int width, height;
            string w = textBox1.Text;
            if (!(w != "")) w = "0";
            string h = textBox2.Text;
            if (!(h != "")) h = "0";
            Int32.TryParse(w, out width); Int32.TryParse(h, out height);
            int total = width * height;
            label5LIVING.Text = Convert.ToString(total);
        }
        
        //Crea una matriz de celulas nuevas
        private void button1_Click(object sender, EventArgs e)
        {
            steps = 0;
            label9.Text = steps.ToString();
            CellGOF.muertaColor = button5.BackColor;
            CellGOF.vivaColor = button6.BackColor;
            Cursor = Cursors.WaitCursor;
            int width, height;
            string w = textBox1.Text;
            if (!(w != "")) w = "0";
            string h = textBox2.Text;
            if (!(h != "")) h = "0";
            Int32.TryParse(w, out width); Int32.TryParse(h, out height);
            cells = new CellGOF[width,height]; //Crea el arreglo de 2 dimensiones de las celulas;
            PanelJuego.Controls.Clear();
            //Proceso de generacion de celulas;
            //Esto equivale a la primera generacion;
            int x, y;
            int i = 0; 
            
            for (x = 0; x < width*cellSize; x+=cellSize)
            {

                int j = 0;
                for(y =0; y < height*cellSize; y+=cellSize)
                {
                    
                    cells[i, j] = new CellGOF(cellSize-1);            
                    cells[i,j].setCoordinates(x,y,i,j);
                    //cells[i, j].typeInLabel();
                    PanelJuego.Controls.Add(cells[i, j].GetCellLabel());
                    j++;

                }
                i++;
            }
           updateAmountOfLivingCells();
           CalculateLivingNeighbors();
            
            Cursor = Cursors.Default;
            button8.Enabled = true;

        }

        //Actualiza el numero de celulas vivas que se despliega en la pantalla;
        public void updateAmountOfLivingCells()
        {
            try
            {
                int total = 0;
                this.total = total;
                foreach (CellGOF a in cells)
                {
                    
                    if (a.getState()) total++;
                    
                }
                label7LC.Text = Convert.ToString(total);
            }
            catch (NullReferenceException) { }
        }
        /*
          * <CELL TYPES>
             TopLeft = 1,
             TopRight = 2,
             Top = 3,
             Left = 4,
             Center = 5,
             Right = 6,
             Bottom = 7;
             BottomLeft = 8,
             BottomRight = 9,

         */
         
        //Calcula los vecinos vivos que tiene cada celula.
       public void CalculateLivingNeighbors()
        {

            for(int i = 0; i < cells.GetLength(0); i++)
            {
                for(int j = 0; j < cells.GetLength(1); j++)
                {
                    int h = -1, k = -1;
                    int boundH = 1, boundK = 1;

                    if (i == 0 && j == 0) { h = 0; boundH = 1; k = 0; boundK = 1; }//TOP LEFT   //x = h, y=k
                    else if (i == 0 && j == cells.GetLength(1) - 1) { h = 0; k = -1; boundH = 1; boundK = 0; }//BOTTOM LEFT
                    else if (i == cells.GetLength(0) - 1 && j == 0) { h = -1; boundH = 0; k = 0; boundK = 1; }//TOP RIGHT
                    else if (i == cells.GetLength(0) - 1 && j == cells.GetLength(1) - 1) { h = -1; k = -1; boundH = 0; boundK = 0; } //BOTTOM RIGHT
                    else if (i == 0 && (j > 0 && j < cells.GetLength(1) - 1)) { h = 0; boundH = 1; k = -1; boundK = 1; }//LEFT
                    else if (i == cells.GetLength(0) - 1 && (j > 0 && j < cells.GetLength(1) - 1)) { h = -1; boundH = 0; k = -1; boundK = 1; }//RIGHT
                    else if ((i > 0 && i < cells.GetLength(0) - 1) && j == 0) { h = -1; boundH = 1; k = 0; boundK = 1; } //TOP
                    else if ((i > 0 && i < cells.GetLength(0) - 1) && j == cells.GetLength(1) - 1) { h = -1; boundH = 1; k = -1; boundK = 0; }//BOTTOM
                    else { h = -1; boundH = 1; k = -1; boundK = 1; }//CENTER


                    int live = 0;
                    for(int x = h; x <= boundH; x++)
                    {
                        for(int y = k; y <= boundK; y++)
                        {
                            try
                            {                              
                              if (cells[i + x, j + y].getState() == true)
                               live++;
                            }
                            catch (IndexOutOfRangeException)
                            {                           
                                //MessageBox.Show("La cell " + cells[i, j].GetCellLabel().Name + " sale del arreglo", "", MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                                //continue;
                            }
                        }
                    }
                   if (cells[i, j].getState() == true)
                        live--;
                    cells[i, j].setLiveNeighbors(live);
                }
            }
            foreach(CellGOF c in cells)
            {
                c.setWithIntState();
            }
           
        }
        
           //Va al siguiente paso;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                CalculateLivingNeighbors(); //Primero calcula cuantos vecinos vivos tiene cada celula
                updateAmountOfLivingCells(); //Cada celula determina si vive o muere a partir de ahi
                steps++; //Se la anade un 1 la generacion
                label9.Text = steps.ToString(); //Despliega el numero de la generacion en la pantalla
                
            }
            catch (NullReferenceException) {MessageBox.Show("You must first create the cell grid", "Game of Life", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

       //Va a la generacion anterior
        private void button3_Click(object sender, EventArgs e)
        {
            if (steps > 0)
            {
                for (int i = 0; i < cells.GetLength(0); i++)
                {
                    for (int j = 0; j < cells.GetLength(1); j++)
                    {
                        //Este metodo llama el estado anterior de la celula y se lo aplica nuevamente.
                        cells[i, j].previousStep();
                    }
                }
                steps--; //Le resta un numero a la cantidad de generaciones
                label9.Text = steps.ToString();
            }
        }

        //Este el metodo que permite asignar celulas aleatoriamente.
        private void button4_Click(object sender, EventArgs e)
        {
            steps = 0;
            Random random = new Random();
            int num = Convert.ToInt32(numericUpDown1.Value);
            try
            {
                if (num == 0) num = random.Next(0,cells.Length);
                if (num > cells.Length) MessageBox.Show("The amount of cells doesn't exist in the MATRIX", "Game of Life", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    foreach (CellGOF cell in cells)
                    {
                        cell.setState(0);
                    }
                    int amount = 0;
                    int x, y;
                    while (amount < num)
                    {
                        x = random.Next(cells.GetLength(0)); y = random.Next(cells.GetLength(1));
                        if (cells[x, y].getState()) continue;
                        cells[x, y].setState(1);
                        amount++;


                    }
                }
                
            }
            catch (NullReferenceException) { MessageBox.Show("First you must create the cell grid", "Game of Life", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            updateAmountOfLivingCells();
        }


        private void button5_Click(object sender, EventArgs e)//muerta
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button5.BackColor = colorDialog1.Color;
            }
            colorOfCells();
        }

        private void button6_Click(object sender, EventArgs e)//viva
        {
            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                button6.BackColor = colorDialog2.Color;
            }
            colorOfCells();
        }

        private void colorOfCells()
        {
            CellGOF.vivaColor = button6.BackColor;
            CellGOF.muertaColor = button5.BackColor;
            try
            {
                foreach (CellGOF cell in cells)
                {
                    cell.updateColor();
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("The cells have not been created, you can't change their color", "Game of Life", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(colorDialog3.ShowDialog() == DialogResult.OK)
            {
                PanelJuego.BackColor = colorDialog3.Color;
                button7.BackColor = colorDialog3.Color;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
           
            timer1.Tick += new EventHandler(this.timer1_Tick);
            timer1.Interval = timeInterval;
            
            if (button8.BackColor == Color.DeepSkyBlue)
            {
                button8.BackColor = Color.DarkBlue;
                timer1.Start();
            }
            else
            {
                button8.BackColor = Color.DeepSkyBlue;
                timer1.Stop();
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CalculateLivingNeighbors(); //Primero calcula cuantos vecinos vivos tiene cada celula
            updateAmountOfLivingCells(); //Cada celula determina si vive o muere a partir de ahi
            steps++; //Se la anade un 1 la generacion
            label9.Text = steps.ToString(); //Despliega el numero de la generacion en la pantalla 
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
           timeInterval = hScrollBar1.Value;
           timer1.Interval = timeInterval;
        }

        
        
    }
    
}


