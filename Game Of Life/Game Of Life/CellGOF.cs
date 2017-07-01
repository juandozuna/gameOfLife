using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
/*
 * Conway's Game of Life
 * Juego de la vida de Conway
 * 
 * Proyecto de Laboratorio de Programacion
 * ID:1069835   JUAN DANIEL OZUNA ESPINAL
 */
namespace JuegoDeLaVidaINTENTO
{
    public class CellGOF
    {
        private bool state;
        private int intState;
        private string StrState = " ";
        private int liveNeighbors = 0;
        private Label cellLabel;
        private List<bool> oldState;
        public static Color vivaColor = Color.Yellow;
        public static Color muertaColor = Color.Black;
        private int size;
        private int x, y;
        public int indexX, indexY;
        Size tamano;
        Point punto;

        public CellGOF(int cellSize)
        {
            oldState = new List<bool>();
            state = false;
            StrState = " ";
            cellLabel = new Label();
            size = cellSize;
            cellLabel.AutoSize = false;
            cellLabel.Size = new Size(size, size);
            cellLabel.BackColor = muertaColor;
            cellLabel.Font = new Font("Microsoft Sans Serif", 6.75F);
            cellLabel.TextAlign = ContentAlignment.MiddleCenter;
            cellLabel.ForeColor = vivaColor;
            cellLabel.Click += new EventHandler(clickOnLabel);
            tamano = new Size(cellSize, cellSize);
        }
        public Label GetCellLabel()
        {
            cellLabel.Size = tamano;
            cellLabel.BackColor = muertaColor;
            cellLabel.Cursor = Cursors.Hand;
            cellLabel.Location = punto;
            return cellLabel;
        }    
        private void clickOnLabel(Object sender, EventArgs e)
        {
            if (state) setState(0);
            else setState(1);
            Program.gof.updateAmountOfLivingCells();
            
        }   
        public void setState(int x)
        {
            if (x == 1) { state = true; cellLabel.BackColor = vivaColor; cellLabel.ForeColor = muertaColor; }
            else { state = false; cellLabel.BackColor = muertaColor; cellLabel.ForeColor = vivaColor; }
            
        }
        public void setWithIntState()
        {
            setState(intState);
        }
        public bool getState() { return state; }
        public void setLiveNeighbors(int x)
        {
            liveNeighbors = x;
            cellLabel.Text = liveNeighbors.ToString();
            x = updateState(liveNeighbors);
        }
        public int updateState(int l)
        {
            oldState.Add(state);
            int i = 0;
            if ((l == 1 || l == 0) && state == true) i = 0;
            else if ((l >= 4) && state == true) i = 0;
            else if ((l == 2 || l == 3) && state == true) i = 1;
            else if ((l == 3) && state == false) i = 1;
            intState = i;
            return i;
            
          
        }       
        public void setCoordinates(int x, int y, int i, int j)
        {
            cellLabel.Name = i + "," + j;
            //cellLabel.Text = cellLabel.Name;
            indexX = i; indexY = j;
            x = x + 3;
            y = y + 3;
            this.x = x; this.y = y;
            punto = new Point(x, y);
            
            cellLabel.Location = new Point(x, y);
        }
        public void previousStep()
        {
            try
            {
                if (oldState.Last()) setState(1);
                else setState(0);
                oldState.RemoveAt(oldState.Count - 1);
            }
            catch (InvalidOperationException) { }
        }
        public void updateColor()
        {
            if (state) { cellLabel.BackColor = vivaColor; cellLabel.ForeColor = muertaColor; }
            else { cellLabel.BackColor = muertaColor; cellLabel.ForeColor = vivaColor; }
        }
    }
}
