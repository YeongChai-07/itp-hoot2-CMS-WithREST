using System;
using System.Collections.Generic;
using System.Linq;
/*using System.Text;
using System.Threading.Tasks;*/

namespace HootHoot_CMS.DAL
{
    public class RandomItem_Generator<T> where T : class
    {
        private Random randNum_Generator = null;
        private IList<T> inputList = null;

        public RandomItem_Generator(IList<T> input)
        {
            randNum_Generator = new Random();
            inputList = input;
        }

        public IList<T> getRandomItem(int resultSize)
        {
            int randNum = -1;
            int[] usedNum = new int[resultSize];

            IList<T> ahYeah = new List<T>();

            for(byte initCount=0;initCount < resultSize;initCount++)
            {
                usedNum[initCount] = -1;
            }


            byte i = 0;

            while(i < resultSize)
            {
                randNum = randNum_Generator.Next(inputList.Count);
                if (!(usedNum.Contains(randNum)))
                {
                    usedNum[i] = randNum;
                    ahYeah.Add(inputList[randNum]);
                    i++;
                }

            }

            return ahYeah;
            

        }


    }
}
