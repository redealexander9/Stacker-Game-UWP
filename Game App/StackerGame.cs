using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Game_App
{
    public class StackerGame
    {
        private readonly bool[,] Grid;
        public int YPosition { get; set; }
        public int XPosition { get; set; }
        public int Direction { get; set; }
        private int BlocksNum;
        public int NumBlocks 
        { 
            
            get
            {
                return BlocksNum;
            } 
            set
            {
                if(value == 0)
                {
                    IsGameOver = true;
                    BlocksNum = value;
                }
                else
                {
                    BlocksNum = value;
                }
            }
        
        }
        public int GridSize { get; set; }
        public bool IsGameOver { get; set; } = false;

        public bool IsLayerChanging { get; set; } = false;

        public int CurrentPosition = 0;
        public StackerGame() {
            GridSize = 10;
            NumBlocks = 3;
            YPosition = 0;
            XPosition = 9;
            Direction = 1;
            Grid = new bool[GridSize, GridSize];
            NewGame();

        }

        public bool IsOn(int r, int c)
        {
            return Grid[r, c];
        }

        public void NewGame()
        {
            for(int r = 0; r <= GridSize - 1; r++)
            {
                for(int c = 0; c <= GridSize - 1; c++)
                {
                    if(r == 9 && (c == 0 || c == 1 || c == 2))
                    {
                        Grid[r, c] = true;
                    }
                    else
                    {
                        Grid[r, c] = false;
                    }
                }
            }
            
           
        }

        public void SubtractBlocks(int ClickPosition) // Function takes position user clicked and subtracts correct number of blocks
        {
           
            Point Pos1;
            Point Pos2;
            Point Pos3;     

            switch (NumBlocks)
            {
                case 1:
                    Pos1 = IsBlockBelow(ClickPosition);
                    Pos2.X = -5;
                    Pos3.X = -5;
                    break;
                case 2:
                    Pos1 = IsBlockBelow(ClickPosition);
                    Pos2 = IsBlockBelow(ClickPosition + 1);
                    Pos3.X = -5;
                    break;
                case 3:
                    Pos1 = IsBlockBelow(ClickPosition);
                    Pos2 = IsBlockBelow(ClickPosition + 1);
                    Pos3 = IsBlockBelow(ClickPosition + 2);
                    break;
            }

           

            if (Grid[Pos1.X, Pos1.Y] == false)
            {

                Grid[Pos1.X - 1, Pos1.Y] = false;
                NumBlocks--;
            }

            if (Pos2.X != -5 && Grid[Pos2.X, Pos2.Y] == false)
            {
                Grid[Pos2.X - 1, Pos2.Y] = false;
                NumBlocks--;
            }

            if (Pos3.X != -5 && Grid[Pos3.X, Pos3.Y] == false)
            {
                Grid[Pos3.X - 1, Pos3.Y] = false;
                NumBlocks--;
            }




           





        }


        public Point IsBlockBelow(int C)
        {
            Point Position = new Point();

            for (int i = 0; i < Grid.GetLength(0); i++)
            {

                if (Grid[i, C])
                {
                    Position.X = i + 1;
                    Position.Y = C;
                    return Position;
                    
                }

            }
            Position.X = 3;
            Position.Y = 3;
            return Position;
        }









        

        public void StartAnimation(object sender, object e)
        {
            if (IsLayerChanging)
            {
               
                NextRowAnimation();
                IsLayerChanging = false;
                CurrentPosition = 0;

            }
            else
            {

                switch (BlocksNum)
                {
                    case 3:
                        ThreeBlockAnimation(); break;
                    case 2: 
                        TwoBlockAnimation(); break;
                    case 1:
                        OneBlockAnimation(); break;
                    default:
                        IsGameOver = true;
                        break;



                }
            }

           
        }
        private void ThreeBlockAnimation()
        {
            if (Direction == 15) // Grid ends when three blocks reach 15
            {
                Direction = 14; // Sets Direction to 14 so program knows to move rectangle from right to left
                YPosition += 2; // Add two to Y position to indicate that the rectangle moved
            }
            else if (Direction == 28)
            {
                Direction = 1;
                YPosition -= 2;
            }
            if (Direction % 2 == 1) //Blocks are moving from left to right
            {
                Direction += 2;
                Grid[XPosition, YPosition] = false;
                Grid[XPosition, YPosition + NumBlocks] = true;
                CurrentPosition++;
                YPosition++;

            }
            else if (Direction % 2 == 0) // Blocks are moving from right to left
            {
                Grid[XPosition, YPosition] = false;
                Grid[XPosition, YPosition - NumBlocks] = true;
                YPosition--;
                CurrentPosition--;
                Direction += 2;
            }
        }

        private void TwoBlockAnimation()
        {
            if (Direction == 17) // 2 Blocks reach end of grid when Direction = 17
            {
                Direction = 16;
                YPosition += 1;
            }
            else if (Direction == 32)
            {
                Direction = 1;
                YPosition -= 1;
            }
            if (Direction % 2 == 1) //Blocks are moving from left to right
            {
                Direction += 2;
                Grid[XPosition, YPosition] = false;
                Grid[XPosition, YPosition + NumBlocks] = true;
                CurrentPosition++;
                YPosition++;

            }
            else if (Direction % 2 == 0) // Blocks are moving from right to left
            {
                Grid[XPosition, YPosition] = false;
                Grid[XPosition, YPosition - NumBlocks] = true;
                CurrentPosition--;
                YPosition--;
                Direction += 2;
            }
        }

        private void OneBlockAnimation()
        {
            if (Direction == 19)
            {
                Direction = 18;
                //YPosition += 1;
            }
            else if (Direction == 36)
            {
                Direction = 1;
               // YPosition -= 1;
            }
            if (Direction % 2 == 1) //Blocks are moving from left to right
            {
                Direction += 2;
                Grid[XPosition, YPosition] = false;
                Grid[XPosition, YPosition + NumBlocks] = true;
                CurrentPosition++;
                YPosition++;

            }
            else if (Direction % 2 == 0) // Blocks are moving from right to left
            {
                Grid[XPosition, YPosition] = false;
                Grid[XPosition, YPosition - NumBlocks] = true;
                CurrentPosition--;
                YPosition--;
                Direction += 2;
            }
        }

        public void NextRowAnimation()
        {
            Grid[XPosition, YPosition] = true;

            switch (NumBlocks)
            {
                case 2:
                    Grid[XPosition, YPosition + 1] = true;
                    break;
                case 3:
                    Grid[XPosition, YPosition + 1] = true;
                    Grid[XPosition, YPosition + 2] = true;
                    break;
            }
            

        }


        






        public void PrintGrid()
        {
            int r = 0;
            int c = 0;
            for (r = 0; r < 10; r++)
            {
                for(c = 0; c < 10; c++)
                {

                    //Debug.Write(r + ", " + c + ": ");
                    Debug.Write(c + ": ");

                    Debug.Write(Grid[r, c] + " , ");
                   
                }
                Debug.WriteLine("");
            }
        }

        
    }
}
