using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionGenerator;

namespace RandomSampler {
   public class RandomSampler {
        static Random random = new Random();
        #region Test

        static void Log<T>(T[] Ts) {
            for (int i = 0; i < Ts.Length; i++) {
                Console.WriteLine(Ts[i]);
            }
        }

        static double TestSampleBetween() {
            string[] args = new string[] { "1.0", "3.0" };
            double min = double.Parse(args[0]);
            double max = double.Parse(args[1]);
            return SampleBetween(min, max);
        }

        static double TestRoulette() {
            //サイコロテスト
            double[] Labels = new double[] { 1d, 2d, 3d, 4d, 5d, 6d };
            double[] Probabilities = new double[] { 1d / 6d, 1d / 6d, 1d / 6d, 1d / 6d, 1d / 6d, 1d / 6d };
            return Roulette(Labels, Probabilities);
        }

        static void Test_FreqTable() {
            double[] Labels = new double[] { 1d, 2d, 3d, 4d, 5d, 6d };
            double[] Probabilities = new double[] { 1d / 6d, 1d / 6d, 1d / 6d, 1d / 6d, 1d / 6d, 1d / 6d };
            double[] Val = new double[1000];
            for (int i = 0; i < 1000; i++) {
                Val[i] = Roulette(Labels, Probabilities);
            }
            var Dic = GetFrequencyDictionary(Val);
            foreach (var item in Dic.Values) {
                Console.WriteLine(item);
            }
        }

        #endregion


        //値の配列と、それが現れる確率の配列を受け取って、ルーレットを行う。
        public static double Roulette(double[] Labels, double[] Probabilities) {
            double Value = SampleBetween(0d, Probabilities.Sum());
            double sum = 0d;
            for (int i = 0; i < Labels.Length; i++) {
                sum += Probabilities[i];
                if (Value <= sum) return Labels[i];
            }
            return Labels[Labels.Length];
        }

        //ある範囲からランダムに値を取り出す。
        public static double SampleBetween(double min, double max) {
            return min + random.NextDouble() * (max - min);
        }

        //度数分布を得る。
        public static Dictionary<double, int> GetFrequencyDictionary(double[] Values) {
            //値と度数の対応表を作る。ハッシュテーブルでの実装。
            Dictionary<double, int> KeyCount = new Dictionary<double, int>();
            for (int i = 0; i < Values.Length; i++) {
                if (KeyCount.ContainsKey(Values[i])) {
                    KeyCount[Values[i]]++;
                } else {
                    KeyCount.Add(Values[i], 1);
                }
            }
            return KeyCount;
        }
        //再現性を持たせたいときに、乱数のシードを手動でセットする。
        public static void setRandomSeed(int seed) {
            random = new Random(seed);
        }

    }
}
