using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStoreExercise
{
    struct Garment
    {
        public int GarmentType { get; set; }
        public int Size { get; set; }
        public int Color { get; set; }
        public int Price { get; set; }

        public Garment(int garmentType, int size, int color, int price)
        {
            GarmentType = garmentType;
            Size = size;
            Color = color;
            Price = price;
        }
    }
}
